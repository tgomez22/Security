// Decompiled with JetBrains decompiler
// Type: SolarWinds.Orion.Core.BusinessLayer.OrionCoreNotificationSubscriber
// Assembly: SolarWinds.Orion.Core.BusinessLayer, Version=2019.4.5200.9083, Culture=neutral, PublicKeyToken=null
// MVID: E12E8C85-5CD9-4E06-8801-182E5104FADE
// Assembly location: C:\Users\user\Desktop\SolarWinds Analysis\32519b85c0b422e4656de6e6c41878e95fd95026267daab4215ee59c107d6c77.dll

using SolarWinds.Common.Utility;
using SolarWinds.InformationService.Contract2;
using SolarWinds.InformationService.Contract2.PubSub;
using SolarWinds.Logging;
using SolarWinds.Orion.Core.Common;
using SolarWinds.Orion.Core.Common.Indications;
using SolarWinds.Orion.Core.Common.InformationService;
using SolarWinds.Orion.Core.Common.Swis;
using SolarWinds.Orion.Swis.Contract.InformationService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;

namespace SolarWinds.Orion.Core.BusinessLayer
{
  public class OrionCoreNotificationSubscriber : INotificationSubscriber
  {
    public const string OrionCoreIndications = "OrionCoreIndications";
    public const string NodeIndications = "NodeIndications";
    private static Log log = new Log(typeof (OrionCoreNotificationSubscriber));
    private ISqlHelper _sqlHelper;

    public OrionCoreNotificationSubscriber(ISqlHelper sqlHelper) => this._sqlHelper = sqlHelper != null ? sqlHelper : throw new ArgumentNullException(nameof (sqlHelper));

    public void OnIndication(
      string subscriptionId,
      string indicationType,
      PropertyBag indicationProperties,
      PropertyBag sourceInstanceProperties)
    {
      if (OrionCoreNotificationSubscriber.log.IsDebugEnabled)
        OrionCoreNotificationSubscriber.log.DebugFormat("Indication of type \"{0}\" arrived.", (object) indicationType);
      try
      {
        object obj;
        if (!(indicationType == IndicationHelper.GetIndicationType((IndicationType) 1)) || !((Dictionary<string, object>) sourceInstanceProperties).TryGetValue("InstanceType", out obj) || !string.Equals(obj as string, "Orion.Nodes", StringComparison.OrdinalIgnoreCase))
          return;
        if (((Dictionary<string, object>) sourceInstanceProperties).ContainsKey("NodeID"))
          this.InsertIntoDeletedTable(Convert.ToInt32(((Dictionary<string, object>) sourceInstanceProperties)["NodeID"]));
        else
          OrionCoreNotificationSubscriber.log.WarnFormat("Indication is type of {0} but does not contain NodeID", (object) indicationType);
      }
      catch (Exception ex)
      {
        OrionCoreNotificationSubscriber.log.Error((object) string.Format("Exception occured when processing incomming indication of type \"{0}\"", (object) indicationType), ex);
      }
    }

    public void Start() => Scheduler.Instance.Add(new ScheduledTask("OrionCoreIndications", new TimerCallback(this.Subscribe), (object) null, TimeSpan.FromSeconds(1.0), TimeSpan.FromMinutes(1.0)));

    public void Stop() => Scheduler.Instance.Remove("OrionCoreIndications");

    private void Subscribe(object state)
    {
      OrionCoreNotificationSubscriber.log.Debug((object) "Subscribing indications..");
      try
      {
        OrionCoreNotificationSubscriber.DeleteOldSubscriptions();
      }
      catch (Exception ex)
      {
        OrionCoreNotificationSubscriber.log.Warn((object) "Exception deleting old subscriptions:", ex);
      }
      try
      {
        string str = ((InformationServiceSubscriptionProviderBase) InformationServiceSubscriptionProviderShared.Instance()).Subscribe("SUBSCRIBE System.InstanceDeleted", (INotificationSubscriber) this, new SubscriptionOptions()
        {
          Description = "OrionCoreIndications"
        });
        if (OrionCoreNotificationSubscriber.log.IsDebugEnabled)
          OrionCoreNotificationSubscriber.log.DebugFormat("PubSub Subscription succeeded. uri:'{0}'", (object) str);
        Scheduler.Instance.Remove("OrionCoreIndications");
      }
      catch (Exception ex)
      {
        OrionCoreNotificationSubscriber.log.Error((object) "Subscription did not succeed, retrying .. (Is SWIS v3 running ?)", ex);
      }
    }

    private void InsertIntoDeletedTable(int nodeId)
    {
      using (SqlCommand textCommand = this._sqlHelper.GetTextCommand("IF NOT EXISTS (SELECT NodeId FROM [dbo].[DeletedNodes] WHERE NodeId=@NodeId)  BEGIN   INSERT INTO [dbo].[DeletedNodes](NodeId)    VALUES(@NodeId)  END "))
      {
        textCommand.Parameters.AddWithValue("@NodeId", (object) nodeId);
        this._sqlHelper.ExecuteNonQuery(textCommand);
      }
    }

    private static void DeleteOldSubscriptions()
    {
      using (IInformationServiceProxy2 iinformationServiceProxy2_1 = ((IInformationServiceProxyCreator) SwisConnectionProxyPool.GetSystemCreator()).Create())
      {
        string str1 = "SELECT Uri FROM System.Subscription WHERE description = @description";
        IInformationServiceProxy2 iinformationServiceProxy2_2 = iinformationServiceProxy2_1;
        string str2 = str1;
        foreach (DataRow dataRow in ((IInformationServiceProxy) iinformationServiceProxy2_2).Query(str2, (IDictionary<string, object>) new Dictionary<string, object>()
        {
          {
            "description",
            (object) "OrionCoreIndications"
          }
        }).Rows.Cast<DataRow>())
          ((IInformationServiceProxy) iinformationServiceProxy2_1).Delete(dataRow[0].ToString());
      }
    }
  }
}