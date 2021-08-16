// Decompiled with JetBrains decompiler
// Type: SolarWinds.Orion.Core.BusinessLayer.CoreBusinessLayerPlugin
// Assembly: SolarWinds.Orion.Core.BusinessLayer, Version=2019.4.5200.9083, Culture=neutral, PublicKeyToken=null
// MVID: E12E8C85-5CD9-4E06-8801-182E5104FADE
// Assembly location: C:\Users\user\Desktop\SolarWinds Analysis\32519b85c0b422e4656de6e6c41878e95fd95026267daab4215ee59c107d6c77.dll

using SolarWinds.BusinessLayerHost.Contract;
using SolarWinds.Common.Utility;
using SolarWinds.InformationService.Contract2.PubSub;
using SolarWinds.InformationService.Linq.Plugins.Core.Orion;
using SolarWinds.Logging;
using SolarWinds.Orion.Channels.Security;
using SolarWinds.Orion.Common;
using SolarWinds.Orion.Common.Models;
using SolarWinds.Orion.Core.Auditing;
using SolarWinds.Orion.Core.BusinessLayer.Agent;
using SolarWinds.Orion.Core.BusinessLayer.BackgroundInventory;
using SolarWinds.Orion.Core.BusinessLayer.DAL;
using SolarWinds.Orion.Core.BusinessLayer.DowntimeMonitoring;
using SolarWinds.Orion.Core.BusinessLayer.Engines;
using SolarWinds.Orion.Core.BusinessLayer.MaintenanceMode;
using SolarWinds.Orion.Core.BusinessLayer.NodeStatus;
using SolarWinds.Orion.Core.BusinessLayer.OneTimeJobs;
using SolarWinds.Orion.Core.BusinessLayer.Thresholds;
using SolarWinds.Orion.Core.CertificateUpdate;
using SolarWinds.Orion.Core.Common;
using SolarWinds.Orion.Core.Common.Configuration;
using SolarWinds.Orion.Core.Common.DALs;
using SolarWinds.Orion.Core.Common.EntityMonitor;
using SolarWinds.Orion.Core.Common.i18n;
using SolarWinds.Orion.Core.Common.IndicationMonitor;
using SolarWinds.Orion.Core.Common.InformationService;
using SolarWinds.Orion.Core.Common.JobEngine;
using SolarWinds.Orion.Core.Common.Models;
using SolarWinds.Orion.Core.Common.Settings;
using SolarWinds.Orion.Core.Common.Swis;
using SolarWinds.Orion.Core.Common.Upgrade;
using SolarWinds.Orion.Core.Discovery;
using SolarWinds.Orion.Core.Discovery.DataAccess;
using SolarWinds.Orion.Core.JobEngine.Routing.ServiceDirectory;
using SolarWinds.Orion.Core.Strings;
using SolarWinds.Orion.ServiceDirectory;
using SolarWinds.Orion.Swis.PubSub.InformationService;
using SolarWinds.ServiceDirectory.Client.Contract;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;

namespace SolarWinds.Orion.Core.BusinessLayer
{
  [BusinessLayerPlugin]
  public class CoreBusinessLayerPlugin : 
    BusinessLayerPlugin,
    ISupportFreeEngine,
    IServiceStateProvider
  {
    private static readonly Log log = new Log();
    private const string CertificateMaintenanceTaskName = "CertificateMaintenance";
    private readonly IEngineDAL engineDal = (IEngineDAL) new EngineDAL();
    private BackgroundInventoryManager backgroundInventory;
    private readonly Dictionary<int, int> backgroundInventoryTracker = new Dictionary<int, int>();
    private Exception fatalException;
    private CoreBusinessLayerService businessLayerService;
    private ServiceHost businessLayerServiceHost;
    private IDisposable slaveEnginesMonitor;
    private IDisposable remoteCollectorConnectedMonitor;
    private IRemoteCollectorAgentStatusProvider remoteCollectorAgentStatusProvider;
    private readonly ConcurrentDictionary<int, CoreBusinessLayerServiceInstance> businessLayerServiceInstances = new ConcurrentDictionary<int, CoreBusinessLayerServiceInstance>();
    private readonly object syncRoot = new object();
    private DiscoveryJobSchedulerEventsService discoveryJobSchedulerCallbackService;
    private ServiceHost discoveryJobSchedulerCallbackHost;
    private OrionDiscoveryJobSchedulerEventsService orionDiscoveryJobSchedulerCallbackService;
    private ServiceHost orionDiscoveryJobSchedulerCallbackHost;
    private readonly IServiceDirectoryClient serviceDirectoryClient;
    private IOneTimeJobManager oneTimeJobManager;
    private ServiceHost oneTimeJobManagerCallbackHost;
    private InformationServiceSubscriptionProviderBase subscribtionProvider;
    internal OrionCoreNotificationSubscriber orionCoreNotificationSubscriber;
    internal AuditingNotificationSubscriber auditingNotificationSubscriber;
    internal DowntimeMonitoringNotificationSubscriber downtimeMonitoringNotificationSubscriber;
    internal DowntimeMonitoringEnableSubscriber downtimeMonitoringEnableSubscriber;
    internal MaintenanceIndicationSubscriber maintenanceIndicationSubscriber;
    internal EnhancedNodeStatusCalculationSubscriber enhancedNodeStatusCalculationSubscriber;
    internal RollupModeChangedSubscriber rollupModeChangedSubscriber;
    internal NodeChildStatusParticipationSubscriber nodeChildStatusParticipationSubscriber;
    private OrionFeatureResolver orionFeatureResolver;
    private const string ConfigurationSectionName = "orion.serviceLocator";
    private static readonly SolarWinds.Orion.Common.ServiceContainer serviceContainer = new SolarWinds.Orion.Common.ServiceContainer("orion.serviceLocator");
    private static bool? jobEngineServiceEnabled;
    private bool isDiscoveryJobReschedulingEnabled;
    private InventoryManager backgroundInventoryPluggable;

    public IServiceProvider ServiceContainer => (IServiceProvider) CoreBusinessLayerPlugin.serviceContainer;

    private static bool JobEngineServiceEnabled
    {
      get
      {
        if (!CoreBusinessLayerPlugin.jobEngineServiceEnabled.HasValue)
          CoreBusinessLayerPlugin.jobEngineServiceEnabled = new bool?(CoreBusinessLayerPlugin.GetIsJobEngineServiceEnabled());
        return CoreBusinessLayerPlugin.jobEngineServiceEnabled.Value;
      }
    }

    public virtual string Name => "Core Business Layer";

    public Exception FatalException
    {
      get
      {
        lock (this.syncRoot)
          return this.fatalException;
      }
      set
      {
        lock (this.syncRoot)
          this.fatalException = value;
      }
    }

    public virtual void Start()
    {
      Log.Configure("SolarWinds.Orion.Core.BusinessLayer.dll.config");
      using (CoreBusinessLayerPlugin.log.Block())
      {
        try
        {
          SWEventLogging.WriteEntry("Starting Core Service [" + Environment.MachineName.ToUpperInvariant() + "]", EventLogEntryType.Information);
          bool flag1 = RegistrySettings.IsFullOrion();
          bool flag2 = RegistrySettings.IsAdditionalPoller();
          bool flag3 = RegistrySettings.IsFreePoller();
          int num1 = BusinessLayerSettings.Instance.DBConnectionRetries;
          if (num1 < 1)
            num1 = 10;
          int num2 = BusinessLayerSettings.Instance.DBConnectionRetryInterval;
          if (num2 < 10)
            num2 = 30;
          int millisecondsTimeout = num2 * 1000;
          int num3 = 0;
          while (!DatabaseFunctions.ValidateDatabaseConnection())
          {
            if (num3 >= num1)
            {
              int num4 = BusinessLayerSettings.Instance.DBConnectionRetryIntervalOnFail;
              if (num4 < 59)
                num4 = 300;
              millisecondsTimeout = num4 * 1000;
            }
            SWEventLogging.WriteEntry("Core Service [" + Environment.MachineName.ToUpperInvariant() + "] Database connection verification failed", EventLogEntryType.Warning);
            ++num3;
            Thread.Sleep(millisecondsTimeout);
          }
          MasterEngineInitiator masterEngineInitiator = new MasterEngineInitiator();
          masterEngineInitiator.InitializeEngine();
          this.StartServiceLog();
          this.oneTimeJobManager = (IOneTimeJobManager) new OneTimeJobManager((IServiceStateProvider) this);
          this.isDiscoveryJobReschedulingEnabled = flag1 | flag2 && CoreBusinessLayerPlugin.JobEngineServiceEnabled;
          int engineId = masterEngineInitiator.EngineID;
          this.StartEngineServices(masterEngineInitiator);
          this.ScheduleUpdateEngineTable();
          CoreBusinessLayerPlugin.ScheduleMaintenanceRenewals();
          CoreBusinessLayerPlugin.ScheduleCheckOrionProductTeamBlog();
          if (flag1)
          {
            CoreBusinessLayerPlugin.ScheduleCheckLicenseSaturation();
            CoreBusinessLayerPlugin.ScheduleMaintananceExpiration();
            CoreBusinessLayerPlugin.ScheduleSaveElementsUsageInfo();
            CoreBusinessLayerPlugin.ScheduleSavePollingCapacityInfo();
            CoreBusinessLayerPlugin.ScheduleCheckPollerLimit();
            CoreBusinessLayerPlugin.ScheduleCheckDatabaseLimit();
            this.ScheduleCheckEvaluationExpiration();
            this.ScheduleCertificateMaintenance();
            this.ScheduleOrionFeatureUpdate();
            CoreBusinessLayerPlugin.ScheduleEnhancedNodeStatusIndications();
            DiscoveryNetObjectStatusManager.Instance.RequestUpdateAsync((Action) null, BusinessLayerSettings.Instance.DiscoveryUpdateNetObjectStatusStartupDelay);
            CoreBusinessLayerPlugin.log.Info((object) "Updating BL Scheduler with Report Jobs");
            List<ReportJobConfiguration> allJobs = ReportJobDAL.GetAllJobs();
            for (int index = 0; index < allJobs.Count; ++index)
              ReportJobInitializer.AddActionsToScheduler(allJobs[index], this.businessLayerService);
            CoreBusinessLayerPlugin.log.Info((object) "Preparing partitioned historical tables");
            HistoryTableDdlDAL.EnsureHistoryTables();
            GeolocationJobInitializer.AddActionsToScheduler(this.businessLayerService);
          }
          this.discoveryJobSchedulerCallbackService = new DiscoveryJobSchedulerEventsService(this);
          this.discoveryJobSchedulerCallbackHost = new ServiceHost((object) this.discoveryJobSchedulerCallbackService, Array.Empty<Uri>());
          this.discoveryJobSchedulerCallbackHost.Open();
          this.orionDiscoveryJobSchedulerCallbackService = new OrionDiscoveryJobSchedulerEventsService(this, (IOneTimeAgentDiscoveryJobFactory) this.businessLayerService);
          this.orionDiscoveryJobSchedulerCallbackHost = new ServiceHost((object) this.orionDiscoveryJobSchedulerCallbackService, Array.Empty<Uri>());
          this.orionDiscoveryJobSchedulerCallbackHost.Open();
          this.oneTimeJobManagerCallbackHost = new ServiceHost((object) this.oneTimeJobManager, Array.Empty<Uri>());
          this.oneTimeJobManager.SetListenerUri(this.oneTimeJobManagerCallbackHost.Description.Endpoints.First<ServiceEndpoint>().ListenUri.AbsoluteUri);
          this.oneTimeJobManagerCallbackHost.Open();
          this.UpdateEngineInfoInDB();
          try
          {
            this.businessLayerService.ForceDiscoveryPluginsToLoadTypes();
          }
          catch (Exception ex)
          {
            CoreBusinessLayerPlugin.log.Error((object) "There was problem while forcing loading discovery plugins.", ex);
          }
          try
          {
            DiscoveryProfileEntry.CheckCrashedJobsAfterStartup();
          }
          catch (Exception ex)
          {
            CoreBusinessLayerPlugin.log.Error((object) "Failed to check crashed jobs.", ex);
          }
          if (flag1)
            NodeChildStatusParticipationDAL.ResyncAfterStartup();
          this.ScheduleRemoveOldOneTimeJob();
          this.ScheduleBackgroundInventory(engineId);
          this.ScheduleDeleteOldLogs();
          if (flag1)
          {
            this.ScheduleLazyUpgradeTask();
            this.ScheduleDBMaintanance();
            this.orionCoreNotificationSubscriber = new OrionCoreNotificationSubscriber((ISqlHelper) new SqlHelperAdapter());
            this.orionCoreNotificationSubscriber.Start();
            this.auditingNotificationSubscriber = new AuditingNotificationSubscriber();
            this.auditingNotificationSubscriber.Start();
            this.downtimeMonitoringNotificationSubscriber = new DowntimeMonitoringNotificationSubscriber((INetObjectDowntimeDAL) this.ServiceContainer.GetService<INetObjectDowntimeDAL>());
            this.downtimeMonitoringNotificationSubscriber.Start();
            this.downtimeMonitoringEnableSubscriber = new DowntimeMonitoringEnableSubscriber(this.downtimeMonitoringNotificationSubscriber);
            this.downtimeMonitoringEnableSubscriber.Start();
            this.enhancedNodeStatusCalculationSubscriber = new EnhancedNodeStatusCalculationSubscriber(SubscriptionManager.Instance, (ISqlHelper) new SqlHelperAdapter()).Start();
            this.rollupModeChangedSubscriber = new RollupModeChangedSubscriber(SubscriptionManager.Instance, (ISqlHelper) new SqlHelperAdapter()).Start();
            this.nodeChildStatusParticipationSubscriber = new NodeChildStatusParticipationSubscriber(SubscriptionManager.Instance, (ISqlHelper) new SqlHelperAdapter()).Start();
            if (BusinessLayerSettings.Instance.MaintenanceModeEnabled)
            {
              this.maintenanceIndicationSubscriber = new MaintenanceIndicationSubscriber();
              this.maintenanceIndicationSubscriber.Start();
            }
            OrionReportHelper.InitReportsWatcher();
            try
            {
              OrionReportHelper.SyncLegacyReports();
            }
            catch (Exception ex)
            {
              CoreBusinessLayerPlugin.log.ErrorFormat("Failed to synchronize reports! Error - {0}", (object) ex);
            }
            this.ScheduleThresholdsProcessing();
          }
          if (flag1 | flag2 | flag3)
            Scheduler.Instance.Add(new ScheduledTask("SychronizeSettingsToRegistry", new TimerCallback(this.SynchronizeSettingsToRegistry), (object) null, BusinessLayerSettings.Instance.SettingsToRegistryFrequency));
          Scheduler.Instance.Begin(this.businessLayerService.ShutdownWaitHandle);
          if (SmtpServerDAL.GetDefaultSmtpServer() == null)
            CoreBusinessLayerPlugin.log.ErrorFormat("Default Smtp Server is not defined", Array.Empty<object>());
          CoreBusinessLayerPlugin.log.DebugFormat("{0} started.  Current App Domain: {1}", (object) base.Name, (object) AppDomain.CurrentDomain.FriendlyName);
        }
        catch (Exception ex)
        {
          SWEventLogging.WriteEntry(string.Format("Unhandled Exception caught in Core Service Engine startup. " + ex.Message), EventLogEntryType.Error, 1024);
          CoreBusinessLayerPlugin.log.Fatal((object) "Unhandled Exception caught in plugin startup.", ex);
          this.FatalException = ex;
          throw;
        }
      }
    }

    private void StartEngineServices(MasterEngineInitiator masterEngineInitiator)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CoreBusinessLayerPlugin.\u003C\u003Ec__DisplayClass45_0 cDisplayClass450 = new CoreBusinessLayerPlugin.\u003C\u003Ec__DisplayClass45_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass450.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass450.masterEngineId = masterEngineInitiator.EngineID;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass450.masterEngineName = masterEngineInitiator.ServerName;
      this.StartMasterEngineService(masterEngineInitiator);
      ParameterExpression parameterExpression1;
      ParameterExpression parameterExpression2;
      // ISSUE: method reference
      // ISSUE: type reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: type reference
      // ISSUE: method reference
      // ISSUE: type reference
      // ISSUE: method reference
      // ISSUE: type reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      this.slaveEnginesMonitor = ObservableExtensions.Subscribe<EntityChangeEvent<\u003C\u003Ef__AnonymousType4<int?, string, string>>>(Observable.Do<EntityChangeEvent<\u003C\u003Ef__AnonymousType4<int?, string, string>>>((IObservable<M0>) Observable.SelectMany<IReadOnlyList<EntityChangeEvent<\u003C\u003Ef__AnonymousType4<int?, string, string>>>, EntityChangeEvent<\u003C\u003Ef__AnonymousType4<int?, string, string>>>(Observable.Do<IReadOnlyList<EntityChangeEvent<\u003C\u003Ef__AnonymousType4<int?, string, string>>>>((IObservable<M0>) EntityChangeObservableFactory<SolarWinds.InformationService.Linq.Plugins.Core.Orion.Engines>.CreateDefaultFactory().CreateObservable((Expression<Func<SolarWinds.InformationService.Linq.Plugins.Core.Orion.Engines, M0>>) Expression.Lambda<Func<SolarWinds.InformationService.Linq.Plugins.Core.Orion.Engines, \u003C\u003Ef__AnonymousType4<int?, string, string>>>((Expression) Expression.New((ConstructorInfo) MethodBase.GetMethodFromHandle(__methodref (\u003C\u003Ef__AnonymousType4<int?, string, string>.\u002Ector), __typeref (\u003C\u003Ef__AnonymousType4<int?, string, string>)), (IEnumerable<Expression>) new Expression[3]
      {
        (Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SolarWinds.InformationService.Linq.Plugins.Core.Orion.Engines.get_EngineID))),
        (Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SolarWinds.InformationService.Linq.Plugins.Core.Orion.Engines.get_ServerName))),
        (Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SolarWinds.InformationService.Linq.Plugins.Core.Orion.Engines.get_EngineProperties))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (EngineProperties.get_PropertyValue)))
      }, (MemberInfo) MethodBase.GetMethodFromHandle(__methodref (\u003C\u003Ef__AnonymousType4<int?, string, string>.get_EngineID), __typeref (\u003C\u003Ef__AnonymousType4<int?, string, string>)), (MemberInfo) MethodBase.GetMethodFromHandle(__methodref (\u003C\u003Ef__AnonymousType4<int?, string, string>.get_ServerName), __typeref (\u003C\u003Ef__AnonymousType4<int?, string, string>)), (MemberInfo) MethodBase.GetMethodFromHandle(__methodref (\u003C\u003Ef__AnonymousType4<int?, string, string>.get_RemoteAgentGuid), __typeref (\u003C\u003Ef__AnonymousType4<int?, string, string>))), parameterExpression1), Expression.Lambda<Func<SolarWinds.InformationService.Linq.Plugins.Core.Orion.Engines, bool>>((Expression) Expression.AndAlso((Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SolarWinds.InformationService.Linq.Plugins.Core.Orion.Engines.get_MasterEngineID))), (Expression) Expression.Convert((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass450, typeof (CoreBusinessLayerPlugin.\u003C\u003Ec__DisplayClass45_0)), FieldInfo.GetFieldFromHandle(__fieldref (CoreBusinessLayerPlugin.\u003C\u003Ec__DisplayClass45_0.masterEngineId))), typeof (int?))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SolarWinds.InformationService.Linq.Plugins.Core.Orion.Engines.get_EngineProperties))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (EngineProperties.get_PropertyName))), (Expression) Expression.Constant((object) "AgentGuid", typeof (string)))), parameterExpression2), new TimeSpan?()), (Action<M0>) new Action<IReadOnlyList<EntityChangeEvent<\u003C\u003Ef__AnonymousType4<int?, string, string>>>>(cDisplayClass450.\u003CStartEngineServices\u003Eb__2)), (Func<M0, IEnumerable<M1>>) (e => e)), (Action<M0>) (e => CoreBusinessLayerPlugin.log.Info((object) string.Format("Slave Engine change detected {0}", (object) e)))), (Action<M0>) new Action<EntityChangeEvent<\u003C\u003Ef__AnonymousType4<int?, string, string>>>(cDisplayClass450.\u003CStartEngineServices\u003Eb__5));
      // ISSUE: reference to a compiler-generated field
      this.remoteCollectorConnectedMonitor = (IDisposable) new NotificationMonitor((INotificationSubscriber) new RemoteCollectorConnectedNotificationSubscriber((ISwisContextFactory) new SwisContextFactory(), new Action<IEngineComponent>(this.RemoteCollectorStatusChangedCallback), cDisplayClass450.masterEngineId), (ISubscriptionDAL) new SwisSubscriptionDAL((IInformationServiceProxyCreator) new SwisConnectionProxyFactory()), RemoteCollectorConnectedNotificationSubscriber.RemoteCollectorConnectedSubscriptionQuery);
    }

    private void RegisterSlaveJobEngineRoutes(
      string masterLegacyEngine,
      string slaveLegacyEngine,
      string remoteAgentId)
    {
      new RoutingTableRegistrator(ServiceDirectoryClient.Instance).RegisterAsync(masterLegacyEngine, slaveLegacyEngine, remoteAgentId).Wait();
    }

    private void RemoteCollectorStatusChangedCallback(IEngineComponent engineComponent)
    {
      this.remoteCollectorAgentStatusProvider.InvalidateCache();
      if (engineComponent.GetStatus() != EngineComponentStatus.Up)
        return;
      this.RunRescheduleEngineDiscoveryJobsTask(engineComponent.EngineId);
    }

    private void StartMasterEngineService(MasterEngineInitiator masterEngineInitiator)
    {
      lock (this.businessLayerServiceInstances)
      {
        int engineId = masterEngineInitiator.EngineID;
        this.businessLayerService = new CoreBusinessLayerService(this, this.oneTimeJobManager, engineId);
        this.businessLayerServiceHost = new ServiceHost((object) this.businessLayerService, Array.Empty<Uri>());
        masterEngineInitiator.InterfacesSupported = this.businessLayerService.AreInterfacesSupported;
        double totalSeconds = BusinessLayerSettings.Instance.RemoteCollectorStatusCacheExpiration.TotalSeconds;
        this.remoteCollectorAgentStatusProvider = (IRemoteCollectorAgentStatusProvider) new RemoteCollectorStatusProvider(SwisConnectionProxyPool.GetCreator(), engineId, (int) totalSeconds);
        CoreBusinessLayerServiceInstance layerServiceInstance = new CoreBusinessLayerServiceInstance(engineId, (IEngineInitiator) masterEngineInitiator, this.businessLayerService, (ServiceHostBase) this.businessLayerServiceHost, this.serviceDirectoryClient);
        if (!this.businessLayerServiceInstances.TryAdd(engineId, layerServiceInstance))
        {
          CoreBusinessLayerPlugin.log.Warn((object) string.Format("Unexpected new master Engine detected with id={0}, BusinessLayer service host already exists, new service host creation skipped.", (object) engineId));
        }
        else
        {
          if (!LegacyServicesSettings.Instance.NetPipeWcfEndpointEnabled)
          {
            CoreBusinessLayerPlugin.log.Debug((object) "Net.Pipe endpoint will be removed");
            ServiceEndpoint serviceEndpoint = this.businessLayerServiceHost.Description.Endpoints.FirstOrDefault<ServiceEndpoint>((Func<ServiceEndpoint, bool>) (n => n.Binding is NetNamedPipeBinding));
            if (serviceEndpoint != null)
            {
              this.businessLayerServiceHost.Description.Endpoints.Remove(serviceEndpoint);
              CoreBusinessLayerPlugin.log.Debug((object) "Net.Pipe endpoint removed");
            }
          }
          WcfSecurityExtensions.ConfigureCertificateAuthentication(this.businessLayerServiceHost.Credentials);
          this.businessLayerServiceHost.Open();
          if (!this.isDiscoveryJobReschedulingEnabled)
            return;
          layerServiceInstance.InitRescheduleEngineDiscoveryJobsTask(true);
        }
      }
    }

    private void StartSlaveEngineService(int engineId, string engineName)
    {
      lock (this.businessLayerServiceInstances)
      {
        RemoteCollectorEngineComponent collectorEngineComponent = new RemoteCollectorEngineComponent(engineId, this.remoteCollectorAgentStatusProvider);
        RemoteCollectorEngineInitiator collectorEngineInitiator = new RemoteCollectorEngineInitiator(engineId, engineName, this.businessLayerService.AreInterfacesSupported, this.engineDal, (IThrottlingStatusProvider) new ThrottlingStatusProvider(), (IEngineComponent) collectorEngineComponent);
        CoreBusinessLayerServiceInstance layerServiceInstance = new CoreBusinessLayerServiceInstance(engineId, (IEngineInitiator) collectorEngineInitiator, this.businessLayerService, (ServiceHostBase) this.businessLayerServiceHost, this.serviceDirectoryClient);
        if (!this.businessLayerServiceInstances.TryAdd(engineId, layerServiceInstance))
        {
          CoreBusinessLayerPlugin.log.Warn((object) string.Format("Unexpected new slave Engine detected with id={0}, BusinessLayer service host already exists, new service host creation skipped.", (object) engineId));
        }
        else
        {
          layerServiceInstance.RegisterAsync().Wait();
          layerServiceInstance.InitializeEngine();
          if (!this.isDiscoveryJobReschedulingEnabled)
            return;
          layerServiceInstance.InitRescheduleEngineDiscoveryJobsTask(false);
        }
      }
    }

    private void StopSlaveEngineService(int engineId)
    {
      lock (this.businessLayerServiceInstances)
      {
        CoreBusinessLayerServiceInstance layerServiceInstance;
        if (!this.businessLayerServiceInstances.TryRemove(engineId, out layerServiceInstance))
        {
          CoreBusinessLayerPlugin.log.Warn((object) string.Format("Unexpected disappeared slave Engine detected with id={0}.", (object) engineId));
        }
        else
        {
          layerServiceInstance.StopRescheduleEngineDiscoveryJobsTask();
          layerServiceInstance.UnregisterAsync().Wait();
        }
      }
    }

    internal CoreBusinessLayerServiceInstance GetServiceInstance(
      int engineId)
    {
      CoreBusinessLayerServiceInstance layerServiceInstance;
      if (this.businessLayerServiceInstances.TryGetValue(engineId, out layerServiceInstance))
        return layerServiceInstance;
      throw new InvalidOperationException(string.Format("The requested engine with EngineId={0} was not found.", (object) engineId));
    }

    internal void AddServiceInstance(CoreBusinessLayerServiceInstance serviceInstance)
    {
      if (this.businessLayerServiceInstances.TryAdd(serviceInstance.EngineId, serviceInstance))
        return;
      CoreBusinessLayerPlugin.log.Warn((object) string.Format("Unexpected new slave Engine detected with id={0}, BusinessLayer service host already exists, new service host creation skipped.", (object) serviceInstance.EngineId));
    }

    internal void RunRescheduleEngineDiscoveryJobsTask(int engineId)
    {
      CoreBusinessLayerServiceInstance layerServiceInstance;
      if (!this.businessLayerServiceInstances.TryGetValue(engineId, out layerServiceInstance))
        CoreBusinessLayerPlugin.log.Warn((object) string.Format("Unexpected disappeared slave Engine detected with id={0}.", (object) engineId));
      else
        layerServiceInstance.RunRescheduleEngineDiscoveryJobsTask();
    }

    private static bool GetIsJobEngineServiceEnabled() => ServicesConfigurationExtensions.IsServiceEnabled(new ServicesConfiguration(), "JobEngineV2");

    private void ScheduleUpdateEngineTable() => Scheduler.Instance.Add(new ScheduledTask("UpdateEngineTable", (TimerCallback) (o => this.UpdateEngineInfoTask()), (object) null, BusinessLayerSettings.Instance.UpdateEngineTimer));

    private void UpdateEngineInfoTask()
    {
      if (CoreBusinessLayerPlugin.log.IsTraceEnabled)
        CoreBusinessLayerPlugin.log.Trace((object) "Starting scheduled task UpdateEngineTable.");
      foreach (KeyValuePair<int, CoreBusinessLayerServiceInstance> layerServiceInstance in this.businessLayerServiceInstances)
        layerServiceInstance.Value.UpdateEngine(CoreBusinessLayerPlugin.JobEngineServiceEnabled);
      if (!CoreBusinessLayerPlugin.log.IsTraceEnabled)
        return;
      CoreBusinessLayerPlugin.log.Trace((object) "UpdateEngineTable task has finished.");
    }

    private void ScheduleCheckEvaluationExpiration() => Scheduler.Instance.Add(new ScheduledTask("CheckEvaluationExpiration", new TimerCallback(this.CheckEvaluationExpiration), (object) null, TimeSpan.FromHours((double) BusinessLayerSettings.Instance.EvaluationExpirationCheckIntervalHours)), true);

    private static void ScheduleCheckDatabaseLimit() => Scheduler.Instance.Add(new ScheduledTask("CheckDatabaseLimit", new TimerCallback(CoreBusinessLayerPlugin.CheckDatabaseLimit), (object) "CheckDatabaseLimit", BusinessLayerSettings.Instance.CheckDatabaseLimitTimer));

    private static void ScheduleCheckPollerLimit() => Scheduler.Instance.Add(new ScheduledTask("CheckPollerLimit", new TimerCallback(CoreBusinessLayerPlugin.CheckPollerLimit), (object) null, BusinessLayerSettings.Instance.PollerLimitTimer));

    private static void ScheduleSavePollingCapacityInfo() => Scheduler.Instance.Add((ScheduledTask) new ScheduledTaskInExactTime("SavePollingCapacityInfo", new TimerCallback(CoreBusinessLayerPlugin.SavePollingCapacityInfo), (object) null, DateTime.Today.AddMinutes(2.0), true));

    private static void ScheduleSaveElementsUsageInfo() => Scheduler.Instance.Add((ScheduledTask) new ScheduledTaskInExactTime("SaveElementsUsageInfo", new TimerCallback(CoreBusinessLayerPlugin.SaveElementsUsageInfo), (object) null, DateTime.Today.AddMinutes(1.0), true));

    private static void ScheduleMaintananceExpiration() => Scheduler.Instance.Add((ScheduledTask) new ScheduledTaskInExactTime("CheckMaintenanceExpiration", new TimerCallback(CoreBusinessLayerPlugin.CheckMaintenanceExpiration), (object) null, DateTime.Today.AddSeconds(1.0), true));

    private static void ScheduleCheckLicenseSaturation() => Scheduler.Instance.Add(new ScheduledTask("CheckLicenseSaturation", new TimerCallback(CoreBusinessLayerPlugin.CheckLicenseSaturation), (object) null, TimeSpan.FromMinutes((double) BusinessLayerSettings.Instance.LicenseSaturationCheckInterval)));

    private static void ScheduleCheckOrionProductTeamBlog()
    {
      if (SolarWinds.Orion.Core.BusinessLayer.Settings.IsProductsBlogDisabled)
        return;
      Scheduler.Instance.Add(new ScheduledTask("CheckOrionProductTeamBlog", new TimerCallback(CoreBusinessLayerPlugin.CheckOrionProductTeamBlog), (object) null, SolarWinds.Orion.Core.BusinessLayer.Settings.CheckOrionProductTeamBlogTimer));
    }

    private static void ScheduleMaintenanceRenewals()
    {
      if (SolarWinds.Orion.Core.BusinessLayer.Settings.IsMaintenanceRenewalsDisabled)
        return;
      Scheduler.Instance.Add(new ScheduledTask("CheckMaintenanceRenewals", new TimerCallback(CoreBusinessLayerPlugin.CheckMaintenanceRenewals), (object) null, SolarWinds.Orion.Core.BusinessLayer.Settings.CheckMaintenanceRenewalsTimer));
    }

    private void ScheduleSynchronizeSettingsToRegistry() => Scheduler.Instance.Add(new ScheduledTask("SychronizeSettingsToRegistry", new TimerCallback(this.SynchronizeSettingsToRegistry), (object) null, BusinessLayerSettings.Instance.SettingsToRegistryFrequency));

    private void ScheduleThresholdsProcessing()
    {
      if (BusinessLayerSettings.Instance.ThresholdsProcessingEnabled)
      {
        Scheduler.Instance.Add(new ScheduledTask("ThresholdsProcessing", (TimerCallback) (o => ThresholdProcessingManager.Instance.Engine.UpdateThresholds()), (object) null, BusinessLayerSettings.Instance.ThresholdsProcessingDefaultTimer), true);
        CoreBusinessLayerPlugin.log.Info((object) "Threshold processing is enabled.");
      }
      else
        CoreBusinessLayerPlugin.log.Info((object) "Threshold processing is disabled.");
    }

    private void ScheduleDBMaintanance()
    {
      this.subscribtionProvider = (InformationServiceSubscriptionProviderBase) InformationServiceSubscriptionProviderShared.Instance();
      Scheduler.Instance.Add(ScheduledTaskFactory.CreateDatabaseMaintenanceTask(this.subscribtionProvider));
    }

    private void ScheduleLazyUpgradeTask() => Scheduler.Instance.Add(new ScheduledTask("LazyUpgradeTask", (TimerCallback) (o => LazyUpgradeTask.Instance.TryRunLazyUpgrade()), (object) null, TimeSpan.FromMinutes(5.0)));

    private void ScheduleDeleteOldLogs() => Scheduler.Instance.Add(new ScheduledTask("DeleteOldLogs", new TimerCallback(LogHelper.DeleteOldLogs), (object) ((IEnumerable<ModuleInfo>) ModulesCollector.GetInstalledModules()).Where<ModuleInfo>((Func<ModuleInfo, bool>) (x => x.RemoveOldJobResult != null)).Select<ModuleInfo, RemoveOldOnetimeJobResultsInfo>((Func<ModuleInfo, RemoveOldOnetimeJobResultsInfo>) (x => x.RemoveOldJobResult)).ToArray<RemoveOldOnetimeJobResultsInfo>(), BusinessLayerSettings.Instance.CheckForOldLogsTimer));

    private void ScheduleBackgroundInventory(int engineId)
    {
      Scheduler.Instance.Add(new ScheduledTask("BackgroundInventoryCheck", new TimerCallback(this.RunBackgroundInventoryCheck), (object) engineId, BusinessLayerSettings.Instance.BackgroundInventoryCheckTimer));
      this.backgroundInventoryPluggable = new InventoryManager(engineId);
      this.backgroundInventoryPluggable.Start();
    }

    private void ScheduleRemoveOldOneTimeJob() => Scheduler.Instance.Add(new ScheduledTask("RemoveOldOnetimeJobResults", (TimerCallback) (o =>
    {
      CoreBusinessLayerPlugin.log.DebugFormat("Clearing onetime job resuts older than {0}", (object) (TimeSpan) DiscoverySettings.OneTimeJobResultMaximalAge);
      DiscoveryResultCache.Instance.CrearOldResults((TimeSpan) DiscoverySettings.OneTimeJobResultMaximalAge);
    }), (object) null, (TimeSpan) DiscoverySettings.OneTimeJobResultClearInetrval)
    {
      RethrowExceptions = false
    });

    private void ScheduleCertificateMaintenance()
    {
      this.businessLayerService.StartCertificateMaintenance();
      Scheduler.Instance.Add(new ScheduledTask("CertificateMaintenance", (TimerCallback) (o => this.CheckCertificateMaintenanceStatus()), (object) null, BusinessLayerSettings.Instance.CertificateMaintenanceTaskCheckInterval, BusinessLayerSettings.Instance.CertificateMaintenanceTaskCheckInterval, TimeSpan.Zero));
    }

    private void ScheduleOrionFeatureUpdate()
    {
      if (this.orionFeatureResolver == null)
      {
        this.orionFeatureResolver = new OrionFeatureResolver((IOrionFeaturesDAL) new OrionFeaturesDAL(), (IOrionFearureProviderFactory) OrionFeatureProviderFactory.CreateInstance());
        CoreBusinessLayerPlugin.serviceContainer.AddService<OrionFeatureResolver>((object) this.orionFeatureResolver);
      }
      this.RefreshOrionFeatures();
      Scheduler.Instance.Add(new ScheduledTask("RefreshOrionFeatures", (TimerCallback) (o => this.RefreshOrionFeatures()), (object) null, BusinessLayerSettings.Instance.OrionFeatureRefreshTimer, BusinessLayerSettings.Instance.OrionFeatureRefreshTimer, TimeSpan.Zero), true);
    }

    private static void ScheduleEnhancedNodeStatusIndications() => Scheduler.Instance.Add(new ScheduledTask("EnhancedStatusIndication", (TimerCallback) (o => CoreBusinessLayerPlugin.EnhancedStatusIndication()), (object) null, TimeSpan.FromSeconds(30.0)));

    private static void EnhancedStatusIndication() => new EnhancedNodeStatusIndicator((ISqlHelper) new SqlHelperAdapter(), (IIndicationReporterPublisher) IndicationPublisher.CreateV3()).Execute();

    public void StartServiceLog()
    {
      string str = string.Format(Resources.LIBCODE_VB0_3, (object) Environment.MachineName.ToUpperInvariant());
      SWEventLogging.WriteEntry(str, EventLogEntryType.Information);
      BusinessLayerOrionEvent.WriteEvent(str, CoreEventTypes.ServiceStartedEventType);
    }

    public void StopServiceLog()
    {
      string str = string.Format(Resources.LIBCODE_VB0_4, (object) Environment.MachineName.ToUpperInvariant());
      SWEventLogging.WriteEntry(str, EventLogEntryType.Information);
      BusinessLayerOrionEvent.WriteEvent(str, CoreEventTypes.ServiceStoppedEventType);
    }

    private void UpdateEngineInfoInDB()
    {
      string upperInvariant = Environment.MachineName.ToUpperInvariant();
      foreach (ServiceEndpoint endpoint in (Collection<ServiceEndpoint>) this.businessLayerServiceHost.Description.Endpoints)
      {
        if (endpoint.Binding.Name.Equals("netTcpBinding", StringComparison.InvariantCultureIgnoreCase))
        {
          CoreBusinessLayerPlugin.UpdateEnginePortInDB(upperInvariant, endpoint.Address.Uri.Port);
          break;
        }
      }
    }

    private static void UpdateEnginePortInDB(string serverName, int port)
    {
      using (SqlCommand textCommand = SqlHelper.GetTextCommand("Update Engines set BusinessLayerPort = @BusinessLayerPort where ServerName = @ServerName"))
      {
        textCommand.Parameters.Add("@ServerName", SqlDbType.NVarChar).Value = (object) serverName;
        textCommand.Parameters.Add("@BusinessLayerPort", SqlDbType.Int).Value = (object) port;
        SqlHelper.ExecuteNonQuery(textCommand);
      }
    }

    public bool IsServiceDown
    {
      get
      {
        lock (this.syncRoot)
          return this.fatalException != null;
      }
    }

    private static void CheckMaintenanceRenewals(object state)
    {
      if (!SettingsDAL.Get("MaintenanceRenewals-Check").Equals("1"))
        return;
      CoreHelper.CheckMaintenanceRenewals(true);
    }

    private static void CheckOrionProductTeamBlog(object state)
    {
      if (!SettingsDAL.Get("ProductsBlog-EnableContent").Equals("1"))
        return;
      CoreHelper.CheckOrionProductTeamBlog();
    }

    private static void CheckLicenseSaturation(object state)
    {
      if (SolarWinds.Orion.Core.BusinessLayer.Settings.IsLicenseSaturationDisabled)
        return;
      using (LocaleThreadState.EnsurePrimaryLocale())
        LicenseSaturationHelper.CheckLicenseSaturation();
    }

    private static void SaveElementsUsageInfo(object state)
    {
      using (LocaleThreadState.EnsurePrimaryLocale())
        LicenseSaturationHelper.SaveElementsUsageInfo();
    }

    private static void CheckMaintenanceExpiration(object state)
    {
      if (!SettingsDAL.Get("MaintenanceExpiration-Check").Equals("1"))
        return;
      using (LocaleThreadState.EnsurePrimaryLocale())
        MaintenanceExpirationHelper.CheckMaintenanceExpiration();
    }

    private static void CheckPollerLimit(object state)
    {
      if (!SettingsDAL.Get("PollerLimit-Check").Equals("1"))
        return;
      using (LocaleThreadState.EnsurePrimaryLocale())
        PollerLimitHelper.CheckPollerLimit();
    }

    private static void SavePollingCapacityInfo(object state)
    {
      using (LocaleThreadState.EnsurePrimaryLocale())
        PollerLimitHelper.SavePollingCapacityInfo();
    }

    private static void CheckDatabaseLimit(object state)
    {
      using (LocaleThreadState.EnsurePrimaryLocale())
      {
        if (new DatabaseLimitNotificationItemDAL().CheckNotificationState())
          return;
        CoreBusinessLayerPlugin.log.Debug((object) "Removing database limit check");
        Scheduler.Instance.Remove((string) state);
      }
    }

    private void CheckEvaluationExpiration(object state)
    {
      if (!SettingsDAL.Get("EvaluationExpiration-Check").Equals("1"))
        return;
      using (LocaleThreadState.EnsurePrimaryLocale())
        new EvaluationExpirationNotificationItemDAL().CheckEvaluationExpiration();
    }

    public virtual void Stop()
    {
      using (CoreBusinessLayerPlugin.log.Block())
      {
        this.StopServiceLog();
        CoreBusinessLayerPlugin.log.Debug((object) "Stoping Core Business Layer Plugin");
        Scheduler.Instance.End();
        this.slaveEnginesMonitor?.Dispose();
        this.remoteCollectorConnectedMonitor?.Dispose();
        if (this.backgroundInventory != null && this.backgroundInventory.IsRunning)
          this.backgroundInventory.Cancel();
        if (this.subscribtionProvider != null)
          this.subscribtionProvider.UnsubscribeAll();
        if (this.orionCoreNotificationSubscriber != null)
          this.orionCoreNotificationSubscriber.Stop();
        if (this.auditingNotificationSubscriber != null)
          this.auditingNotificationSubscriber.Stop();
        if (this.downtimeMonitoringNotificationSubscriber != null)
          this.downtimeMonitoringNotificationSubscriber.Stop();
        if (this.downtimeMonitoringEnableSubscriber != null)
          this.downtimeMonitoringEnableSubscriber.Stop();
        if (this.enhancedNodeStatusCalculationSubscriber != null)
        {
          this.enhancedNodeStatusCalculationSubscriber.Dispose();
          this.enhancedNodeStatusCalculationSubscriber = (EnhancedNodeStatusCalculationSubscriber) null;
        }
        if (this.rollupModeChangedSubscriber != null)
        {
          this.rollupModeChangedSubscriber.Dispose();
          this.rollupModeChangedSubscriber = (RollupModeChangedSubscriber) null;
        }
        if (this.nodeChildStatusParticipationSubscriber != null)
        {
          this.nodeChildStatusParticipationSubscriber.Dispose();
          this.nodeChildStatusParticipationSubscriber = (NodeChildStatusParticipationSubscriber) null;
        }
        if (this.maintenanceIndicationSubscriber != null)
          this.maintenanceIndicationSubscriber.Dispose();
        if (this.backgroundInventoryPluggable != null)
          this.backgroundInventoryPluggable.Stop();
        foreach (KeyValuePair<int, CoreBusinessLayerServiceInstance> layerServiceInstance in this.businessLayerServiceInstances)
          layerServiceInstance.Value.StopRescheduleEngineDiscoveryJobsTask();
        this.businessLayerService.Shutdown();
        MessageUtilities.ShutdownCommunicationObject((ICommunicationObject) this.businessLayerServiceHost);
        MessageUtilities.ShutdownCommunicationObject((ICommunicationObject) this.discoveryJobSchedulerCallbackHost);
        MessageUtilities.ShutdownCommunicationObject((ICommunicationObject) this.orionDiscoveryJobSchedulerCallbackHost);
        MessageUtilities.ShutdownCommunicationObject((ICommunicationObject) this.oneTimeJobManagerCallbackHost);
        CoreBusinessLayerPlugin.log.DebugFormat("{0} stopped.  Current App Domain: {1}", (object) base.Name, (object) AppDomain.CurrentDomain.FriendlyName);
      }
    }

    public CoreBusinessLayerPlugin()
      : this(ServiceDirectoryClient.Instance)
    {
    }

    internal CoreBusinessLayerPlugin(IServiceDirectoryClient serviceDirectoryClient)
    {
      AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CoreBusinessLayerPlugin.AppDomain_UnhandledException);
      this.serviceDirectoryClient = serviceDirectoryClient ?? throw new ArgumentNullException(nameof (serviceDirectoryClient));
      try
      {
        SWEventLogging.EventSource = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileDescription;
      }
      catch
      {
      }
    }

    private static void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      if (e.ExceptionObject is Exception exceptionObject)
      {
        CoreBusinessLayerPlugin.log.Error((object) "Unhandled exception.", exceptionObject);
        SWEventLogging.WriteEntry(string.Format("Unhandled exception: {0}", (object) exceptionObject.Message), EventLogEntryType.Error);
      }
      else
      {
        string str = string.Format("Non-exception object of type {0} was thrown and not caught: {1}", (object) e.ExceptionObject.GetType(), e.ExceptionObject);
        CoreBusinessLayerPlugin.log.Error((object) str);
        SWEventLogging.WriteEntry(str, EventLogEntryType.Error);
      }
    }

    private void RunBackgroundInventoryCheck(object state)
    {
      int engineId = (int) state;
      if (!CoreHelper.IsEngineVersionSameAsOnMain(engineId))
      {
        CoreBusinessLayerPlugin.log.Warn((object) (string.Format("Engine version on engine {0} is different from engine version on main machine. ", (object) engineId) + "Background inventory check not run."));
      }
      else
      {
        int inventoryRetriesCount = BusinessLayerSettings.Instance.BackgroundInventoryRetriesCount;
        if (CoreBusinessLayerPlugin.log.IsDebugEnabled)
          CoreBusinessLayerPlugin.log.DebugFormat("Running scheduled background inventory check on engine {0}", (object) engineId);
        if (this.backgroundInventory == null)
          this.backgroundInventory = new BackgroundInventoryManager(BusinessLayerSettings.Instance.BackgroundInventoryParallelTasksCount);
        if (this.backgroundInventory.IsRunning)
        {
          CoreBusinessLayerPlugin.log.Info((object) "Skipping background inventory check, still running");
        }
        else
        {
          using (SqlCommand textCommand = SqlHelper.GetTextCommand("\nSELECT n.NodeID, s.SettingValue FROM Nodes n \n    JOIN NodeSettings s ON n.NodeID = s.NodeID AND s.SettingName = 'Core.NeedsInventory'\nWHERE (n.EngineID = @engineID OR n.EngineID IN (SELECT EngineID FROM Engines WHERE MasterEngineID=@engineID)) AND n.PolledStatus = 1\nORDER BY n.StatCollection ASC"))
          {
            textCommand.Parameters.Add("@engineID", SqlDbType.Int).Value = (object) engineId;
            using (IDataReader dataReader = SqlHelper.ExecuteReader(textCommand))
            {
              while (dataReader.Read())
              {
                int int32 = dataReader.GetInt32(0);
                string settings = dataReader.GetString(1);
                if (!this.backgroundInventoryTracker.ContainsKey(int32))
                  this.backgroundInventoryTracker.Add(int32, 0);
                int num = this.backgroundInventoryTracker[int32];
                if (num < inventoryRetriesCount)
                {
                  this.backgroundInventoryTracker[int32] = num + 1;
                  this.backgroundInventory.Enqueue(int32, settings);
                }
                else if (num == inventoryRetriesCount)
                {
                  CoreBusinessLayerPlugin.log.WarnFormat("Max inventory retries count for Node {0} reached. Skipping inventoring until next restart of BusinessLayer service.", (object) int32);
                  this.backgroundInventoryTracker[int32] = num + 1;
                }
              }
            }
          }
          if (this.backgroundInventory.QueueSize <= 0)
            return;
          this.backgroundInventory.Start();
        }
      }
    }

    private void SynchronizeSettingsToRegistry(object state) => new SettingsToRegistry().Synchronize();

    private void CheckCertificateMaintenanceStatus()
    {
      CertificateMaintenanceResult maintenanceStatus = this.businessLayerService.GetCertificateMaintenanceStatus();
      CoreBusinessLayerPlugin.log.InfoFormat("CheckCertificateMaintenanceStatus reports maintenance status: {0}", (object) maintenanceStatus);
      switch ((int) maintenanceStatus)
      {
        case 0:
        case 1:
          CoreBusinessLayerPlugin.log.Info((object) "Removing certificate maintenance task.");
          Scheduler.Instance.Remove("CertificateMaintenance");
          break;
        case 2:
          CoreBusinessLayerPlugin.log.Info((object) "Certificate maintenance task needs user confirmation because it can't be finished without breaking changes.");
          this.businessLayerService.RequestUserApprovalForCertificateMaintenance();
          break;
        case 4:
        case 5:
          CoreBusinessLayerPlugin.log.Info((object) "Certificate maintenance needs retry. Starting new attempt.");
          this.businessLayerService.RetryCertificateMaintenance();
          break;
      }
    }

    private void RefreshOrionFeatures()
    {
      if (this.orionFeatureResolver == null)
        throw new InvalidOperationException("orionFearureResolves was not initialized");
      try
      {
        this.orionFeatureResolver.Resolve();
      }
      catch (Exception ex)
      {
        CoreBusinessLayerPlugin.log.Error((object) ex);
      }
    }
  }
}