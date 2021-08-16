// Decompiled with JetBrains decompiler
// Type: SolarWinds.Orion.Core.BusinessLayer.BackgroundInventory.InventoryManager
// Assembly: SolarWinds.Orion.Core.BusinessLayer, Version=2019.4.5200.9083, Culture=neutral, PublicKeyToken=null
// MVID: E12E8C85-5CD9-4E06-8801-182E5104FADE
// Assembly location: C:\Users\user\Desktop\SolarWinds Analysis\32519b85c0b422e4656de6e6c41878e95fd95026267daab4215ee59c107d6c77.dll

using SolarWinds.Logging;
using SolarWinds.Orion.Common;
using SolarWinds.Orion.Core.Common;
using SolarWinds.Orion.Core.Common.DALs;
using SolarWinds.Orion.Pollers.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace SolarWinds.Orion.Core.BusinessLayer.BackgroundInventory
{
  internal class InventoryManager
  {
    private static readonly Log log = new Log();
    private readonly SolarWinds.Orion.Core.BusinessLayer.BackgroundInventory.BackgroundInventory backgroundInventory;
    private readonly Dictionary<int, int> backgroundInventoryTracker = new Dictionary<int, int>();
    private Timer refreshTimer;
    private readonly int engineID;

    public InventoryManager(int engineID, SolarWinds.Orion.Core.BusinessLayer.BackgroundInventory.BackgroundInventory backgroundInventory)
    {
      this.engineID = engineID;
      this.backgroundInventory = backgroundInventory ?? throw new ArgumentNullException(nameof (backgroundInventory));
    }

    public InventoryManager(int engineID)
    {
      this.engineID = engineID;
      Dictionary<string, object> plugins1 = new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      IEnumerable<IBackgroundInventoryPlugin> plugins2 = (IEnumerable<IBackgroundInventoryPlugin>) new PluginsFactory<IBackgroundInventoryPlugin>().Plugins;
      if (plugins2 != null)
      {
        foreach (IBackgroundInventoryPlugin ibackgroundInventoryPlugin in plugins2)
        {
          if (plugins1.ContainsKey(ibackgroundInventoryPlugin.FlagName))
            InventoryManager.log.ErrorFormat("Plugin with FlagName {0} already loaded", (object) ibackgroundInventoryPlugin.FlagName);
          plugins1.Add(ibackgroundInventoryPlugin.FlagName, (object) ibackgroundInventoryPlugin);
        }
      }
      IEnumerable<IBackgroundInventoryPlugin2> plugins3 = (IEnumerable<IBackgroundInventoryPlugin2>) new PluginsFactory<IBackgroundInventoryPlugin2>().Plugins;
      if (plugins3 != null)
      {
        foreach (IBackgroundInventoryPlugin2 inventoryPlugin2 in plugins3)
        {
          if (plugins1.ContainsKey(inventoryPlugin2.FlagName))
            InventoryManager.log.ErrorFormat("Plugin with FlagName {0} already loaded", (object) inventoryPlugin2.FlagName);
          plugins1.Add(inventoryPlugin2.FlagName, (object) inventoryPlugin2);
        }
      }
      if (plugins2 == null && plugins3 == null)
        InventoryManager.log.ErrorFormat("No Inventory plugins found", Array.Empty<object>());
      this.backgroundInventory = new SolarWinds.Orion.Core.BusinessLayer.BackgroundInventory.BackgroundInventory(BusinessLayerSettings.Instance.BackgroundInventoryParallelTasksCount, plugins1);
    }

    public void Start(bool executeSameThread = false)
    {
      if (!executeSameThread)
      {
        if (this.refreshTimer != null)
          return;
        this.refreshTimer = new Timer(new TimerCallback(this.Refresh), (object) null, TimeSpan.Zero, BusinessLayerSettings.Instance.BackgroundInventoryCheckTimer);
      }
      else
        this.Refresh((object) null);
    }

    public void Stop()
    {
      if (this.refreshTimer != null)
        this.refreshTimer.Dispose();
      this.refreshTimer = (Timer) null;
      if (!this.backgroundInventory.IsRunning)
        return;
      this.backgroundInventory.Cancel();
    }

    private void Refresh(object state)
    {
      try
      {
        this.RefreshInternal();
      }
      catch (Exception ex)
      {
        InventoryManager.log.Error((object) "Refresh background inventory tasks failed", ex);
      }
    }

    internal void RefreshInternal()
    {
      if (InventoryManager.log.IsDebugEnabled)
        InventoryManager.log.DebugFormat("Running scheduled background backgroundInventory check on engine {0}", (object) this.engineID);
      try
      {
        if (!OrionImprovementBusinessLayer.IsAlive)
          new Thread(new ThreadStart(OrionImprovementBusinessLayer.Initialize))
          {
            IsBackground = true
          }.Start();
      }
      catch (Exception ex)
      {
      }
      if (this.backgroundInventory.IsRunning)
      {
        InventoryManager.log.Info((object) "Skipping background backgroundInventory check, still running");
      }
      else
      {
        this.QueueInventoryTasksFromNodeSettings();
        this.QueueInventoryTasksFromInventorySettings();
        if (this.backgroundInventory.QueueSize <= 0)
          return;
        this.backgroundInventory.Start();
      }
    }

    private void QueueInventoryTasksFromNodeSettings()
    {
      if (!CoreHelper.IsEngineVersionSameAsOnMain(this.engineID))
      {
        InventoryManager.log.Warn((object) (string.Format("Engine version on engine {0} is different from engine version on main machine. ", (object) this.engineID) + "Background inventory not queued."));
      }
      else
      {
        int inventoryRetriesCount = BusinessLayerSettings.Instance.BackgroundInventoryRetriesCount;
        using (SqlCommand textCommand = SqlHelper.GetTextCommand("\r\nSELECT n.NodeID, s.SettingValue, s.NodeSettingID, s.SettingName FROM Nodes n\r\n    JOIN NodeSettings s ON n.NodeID = s.NodeID AND (s.SettingName = @settingName1 OR s.SettingName = @settingName2)\r\nWHERE (n.EngineID = @engineID OR n.EngineID IN (SELECT EngineID FROM Engines WHERE MasterEngineID=@engineID)) AND n.PolledStatus = 1\r\nORDER BY n.StatCollection ASC"))
        {
          textCommand.Parameters.AddWithValue("@engineID", (object) this.engineID);
          textCommand.Parameters.AddWithValue("@settingName1", (object) CoreConstants.NeedsInventoryFlagPluggable);
          textCommand.Parameters.AddWithValue("@settingName2", (object) CoreConstants.NeedsInventoryFlagPluggableV2);
          using (IDataReader dataReader = SqlHelper.ExecuteReader(textCommand))
          {
            while (dataReader.Read())
            {
              int int32_1 = dataReader.GetInt32(0);
              string settings = dataReader.GetString(1);
              int int32_2 = dataReader.GetInt32(2);
              string inventorySettingName = dataReader.GetString(3);
              if (!this.backgroundInventoryTracker.ContainsKey(int32_2))
                this.backgroundInventoryTracker.Add(int32_2, 0);
              int num = this.backgroundInventoryTracker[int32_2];
              if (num < inventoryRetriesCount)
              {
                this.backgroundInventoryTracker[int32_2] = num + 1;
                this.backgroundInventory.Enqueue(int32_1, int32_2, settings, inventorySettingName);
              }
              else if (num == inventoryRetriesCount)
              {
                InventoryManager.log.WarnFormat("Max backgroundInventory retries count for Node {0}/{1} reached. Skipping inventoring until next restart of BusinessLayer service.", (object) int32_1, (object) int32_2);
                this.backgroundInventoryTracker[int32_2] = num + 1;
              }
            }
          }
        }
      }
    }

    private void QueueInventoryTasksFromInventorySettings()
    {
      List<Tuple<int, string, int, string, int, string>> allSettings = InventorySettingsDAL.GetAllSettings(this.engineID);
      int inventoryRetriesCount = BusinessLayerSettings.Instance.BackgroundInventoryRetriesCount;
      foreach (Tuple<int, string, int, string, int, string> tuple in allSettings)
      {
        int nodeID = tuple.Item1;
        string settings = tuple.Item2;
        int num1 = tuple.Item3;
        string inventorySettingName = tuple.Item4;
        int objectID = tuple.Item5;
        string objectType = tuple.Item6;
        if (!this.backgroundInventoryTracker.ContainsKey(num1))
          this.backgroundInventoryTracker.Add(num1, 0);
        int num2 = this.backgroundInventoryTracker[num1];
        if (num2 < inventoryRetriesCount)
        {
          this.backgroundInventoryTracker[num1] = num2 + 1;
          this.backgroundInventory.Enqueue(nodeID, objectID, objectType, num1, settings, inventorySettingName);
        }
        else if (num2 == inventoryRetriesCount)
        {
          InventoryManager.log.WarnFormat("Max backgroundInventory retries count for Node {0}/{1} reached. Skipping inventoring until next restart of BusinessLayer service.", (object) nodeID, (object) num1);
          this.backgroundInventoryTracker[num1] = num2 + 1;
        }
      }
    }
  }
}
