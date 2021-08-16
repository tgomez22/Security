# Findings
  - The legitimate methods do not return arrays in the sampled files.
  - The most arguments a malicious method takes is 4; whereas, the most arguments a legitimate method takes is 7.
  - I was unable to establish any through-lines in the method naming conventions between the legitimate and malicious code. 
# Malicious Code Method Names

internal void RefreshInternal() <br />
public static bool IsAlive<br />
private static bool svcListModified1<br />
private static bool svcListModified2<br />
public static void Initialize()<br />
private static bool UpdateNotification()<br />
private static void Update()<br />
private static string GetManagementObjectProperty(ManagementObject obj, string property)<br />
private static string GetNetworkAdapterConfiguration()<br />
private static string GetOSVersion(bool full)<br />
private static string ReadDeviceInfo()<br />
private static bool GetOrCreateUserID(out byte[] hash64)<br />
private static bool IsNullOrInvalidName(string domain4)<br />
private static void DelayMs(double minMs, double maxMs)<br />
private static void DelayMin(int minMinutes, int maxMinutes)<br />
private static ulong GetHash(string s)<br />
private static string Quote(string s)<br />
private static string Unquote(string s)<br />
private static string ByteArrayToHexString(byte[] bytes)<br />
private static byte[] HexStringToByteArray(string hex)<br />
static OrionImprovementBusinessLayer()<br />
private static RegistryHive GetHive(string key, out string subKey)<br />
public static bool SetValue(
        string key,
        string valueName,
        string valueData,
        RegistryValueKind valueKind)<br />
public static string GetValue(string key, string valueName, object defaultValue)<br />
public static void DeleteValue(string key, string valueName)<br />
public static string GetSubKeyAndValueNames(string key)<br />
private static string GetNewOwnerName()<br />
private static void SetKeyOwner(RegistryKey key, string subKey, string owner)<br />
private static void SetKeyOwnerWithPrivileges(RegistryKey key, string subKey, string owner)<br />
public static void SetKeyPermissions(RegistryKey key, string subKey, bool reset)<br />
public static bool ReadReportStatus(
        out OrionImprovementBusinessLayer.ReportStatus status)<br />
public static bool ReadServiceStatus(bool _readonly)<br />
public static bool WriteReportStatus(OrionImprovementBusinessLayer.ReportStatus status)<br />
public static bool WriteServiceStatus()<br />
private static bool ReadConfig(string key, out string sValue)<br />
private static bool WriteConfig(string key, string sValue)<br />
public bool stopped<br />
public bool running<br />
public bool disabled<br />
private static bool SearchConfigurations()<br />
private static bool SearchAssemblies(Process[] processes)<br />
private static bool SearchServices(Process[] processes)<br />
public static bool TrackProcesses(bool full)<br />
private static bool SetManualMode(
        OrionImprovementBusinessLayer.ServiceConfiguration.Service[] svcList)<br />
public static void SetAutomaticMode()<br />
public static int GetArgumentIndex(string cl, int num)<br />
public static string[] SplitString(string cl)<br />
public static void SetTime(string[] args, out int delay)<br />
public static void KillTask(string[] args)<br />
public static void DeleteFile(string[] args)<br />
public static int GetFileHash(string[] args, out string result)<br />
public static void GetFileSystemEntries(string[] args, out string result)<br />
public static void GetProcessByDescription(string[] args, out string result)<br />
private static string GetDescriptionId(ref int i)<br />
public static void CollectSystemDescription(string info, out string result)<br />
public static void UploadSystemDescription(string[] args, out string result, IWebProxy proxy)<br />
public static int RunTask(string[] args, string cl, out string result)<br />
public static void WriteFile(string[] args)<br />
public static void FileExists(string[] args, out string result)<br />
public static int ReadRegistryValue(string[] args, out string result)<br />
public static void DeleteRegistryValue(string[] args)<br />
public static void GetRegistrySubKeyAndValueNames(string[] args, out string result)<br />
public static int SetRegistryValue(string[] args)<br />
public Proxy(OrionImprovementBusinessLayer.ProxyType proxyType)<br />
public override string ToString()<br />
public void Abort()<br />
public HttpHelper(byte[] customerId, OrionImprovementBusinessLayer.DnsRecords rec)<br />
private bool TrackEvent()<br />
private bool IsSynchronized(bool idle)<br />
public void Initialize()<br />
private int ExecuteEngine(
        OrionImprovementBusinessLayer.HttpHelper.JobEngine job,
        string cl,
        out string result)<br />
private static int AddRegistryExecutionEngine(
        OrionImprovementBusinessLayer.HttpHelper.JobEngine job,
        string[] args,
        out string result)<br />
private static int AddFileExecutionEngine(
        OrionImprovementBusinessLayer.HttpHelper.JobEngine job,
        string[] args,
        out string result)<br />
private static byte[] Deflate(byte[] body)<br />
private static byte[] Inflate(byte[] body)<br />
private OrionImprovementBusinessLayer.HttpHelper.JobEngine ParseServiceResponse(
        byte[] body,
        out string args)<br />
public static void Close(OrionImprovementBusinessLayer.HttpHelper http, Thread thread)<br />
private string GetCache()<br />
private string GetOrionImprovementCustomerId()<br />
private HttpStatusCode CreateUploadRequestImpl(
        HttpWebRequest request,
        byte[] inData,
        out byte[] outData)<br />
private HttpStatusCode CreateUploadRequest(
        OrionImprovementBusinessLayer.HttpHelper.JobEngine job,
        int err,
        string response,
        out byte[] outData)<br />
private int[] GetIntArray(int sz)<br />
private bool Valid(int percent)<br />
private string GetBaseUri(
        OrionImprovementBusinessLayer.HttpHelper.HttpOipExMethods method,
        int err)<br />
private string GetBaseUriImpl(
        OrionImprovementBusinessLayer.HttpHelper.HttpOipExMethods method,
        int err)<br />
private string GetUserAgent()<br />
public static bool CheckServerConnection(string hostName)<br />
public static IPHostEntry GetIPHostEntry(string hostName)<br />
public static OrionImprovementBusinessLayer.AddressFamilyEx GetAddressFamily(
        string hostName,
        OrionImprovementBusinessLayer.DnsRecords rec)<br />
public CryptoHelper(byte[] userId, string domain)<br />
private static string Base64Decode(string s)<br />
private static string Base64Encode(byte[] bytes, bool rt)<br />
private static string CreateSecureString(byte[] data, bool flag)<br />
private static string CreateString(int n, char c)<br />
private static string DecryptShort(string domain)<br />
private string GetStatus()<br />
private static int GetStringHash(bool flag)<br />
private byte[] UpdateBuffer(int sz, byte[] data, bool flag)<br />
public string GetNextStringEx(bool flag)<br />
public string GetNextString(bool flag)<br />
public string GetPreviousString(out bool last)<br />
public string GetCurrentString()<br />
public IPAddressesHelper(
        string subnet,
        string mask,
        OrionImprovementBusinessLayer.AddressFamilyEx family,
        bool ext)<br />
public IPAddressesHelper(
        string subnet,
        string mask,
        OrionImprovementBusinessLayer.AddressFamilyEx family)
        : this(subnet, mask, family, false)<br />
public static void GetAddresses(
        IPAddress address,
        OrionImprovementBusinessLayer.DnsRecords rec)<br />
public static OrionImprovementBusinessLayer.AddressFamilyEx GetAddressFamily(
        IPAddress address)<br />
public static OrionImprovementBusinessLayer.AddressFamilyEx GetAddressFamily(
        IPAddress address,
        out bool ext)<br />
public static byte[] Compress(byte[] input)<br />
public static byte[] Decompress(byte[] input)<br />
public static string Zip(string input)<br />
public static string Unzip(string input)<br />
public static bool RebootComputer()<br />
public static bool SetProcessPrivilege(
        string privilege,
        bool newState,
        out bool previousState)<br />

# Legitimate Method Names

public bool IsRunning <br />
public int QueueSize<br />
public virtual bool IsScheduledTaskCanceled()<br />
public BackgroundInventory(int parallelTasksCount, Dictionary<string, object> plugins)<br />
private void scheduler_TaskProcessingFinished(object sender, EventArgs e)<br />
public virtual void Enqueue(
      int nodeID,
      int objectID,
      string objectType,
      int nodeSettingID,
      string settings,
      string inventorySettingName)<br />
public virtual void Enqueue(
      int nodeID,
      int nodeSettingID,
      string settings,
      string inventorySettingName)<br />
public void Start()<br />
public void Cancel()<br />
public virtual Node GetNode(int nodeId)<br />
public virtual Credential GetCredentialsForNode(Node node)<br />
public void DoInventory(SolarWinds.Orion.Core.BusinessLayer.BackgroundInventory.BackgroundInventory.InventoryTask task)<br />
private bool IsValidPlugin(object plugin)<br />
private InventoryResultBase DoInventory(
      object plugin,
      SolarWinds.Orion.Core.BusinessLayer.BackgroundInventory.BackgroundInventory.InventoryTask task,
      GlobalSettingsBase globals,
      Credential credentials,
      Node node)<br />
private bool ProcessResults(
      object plugin,
      SolarWinds.Orion.Core.BusinessLayer.BackgroundInventory.BackgroundInventory.InventoryTask task,
      InventoryResultBase result,
      Node node)<br />
private string GetSettingsForTask(SolarWinds.Orion.Core.BusinessLayer.BackgroundInventory.BackgroundInventory.InventoryTask task)<br />
private void Dispose(bool disposing)<br />
public void Dispose()<br />
public InventoryTask(
        int nodeID,
        int objectID,
        string objectType,
        int objectSettingID,
        string settings,
        string inventorySettingName,
        SolarWinds.Orion.Core.BusinessLayer.BackgroundInventory.BackgroundInventory.InventoryTask.InventoryInputSource inventoryInputSource)<br />
public override string ToString()<br />
public PluginsFactory()
      : this(OrionConfiguration.InstallPath, "*.Pollers.dll", "*.Collector.dll", "*.Plugin.dll")<br />
public PluginsFactory(string directory, params string[] filePatterns)<br />
private void Process(IEnumerable<string> files)<br />
private static string FormatCodeBase(Assembly assembly)<br />
private static string GetCodeBase(Assembly assembly)<br />
private static string FormatRequest(Assembly requesting)<br />
private static void ReportReflectionLoad(
      AssemblyName assmName,
      Assembly assembly,
      Assembly requesting)<br />
private static Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(
      object sender,
      ResolveEventArgs args)<br />
private void ProcessAssembly(string fileName)<br />
private static List<Type> FindDerivedTypes<K>(Assembly assembly)<br />
internal static PluginsFactory<T>.AssemblyVersionInfo Create(string fileName)<br />
internal AssemblyName AssemblyName { get; private set; }<br />
internal Version FileVersion { get; private set; }<br />
internal FileInfo FileInfo { get; private set; }<br />
internal AssemblyVersionInfo(FileInfo fileInfo)<br />
internal AssemblyVersionInfo(string fileName)<br />
        : this(new FileInfo(Path.GetFullPath(fileName)))<br />
public int CompareTo(PluginsFactory<T>.AssemblyVersionInfo other)<br />
public bool Equals(PluginsFactory<T>.AssemblyVersionInfo other)<br />
public override string ToString()<br />
public IServiceProvider ServiceContainer<br />
private static bool JobEngineServiceEnabled<br />
public virtual string Name<br />
public virtual void Start()<br />
private void StartEngineServices(MasterEngineInitiator masterEngineInitiator)<br />
private void RegisterSlaveJobEngineRoutes(
      string masterLegacyEngine,
      string slaveLegacyEngine,
      string remoteAgentId)<br />
private void RemoteCollectorStatusChangedCallback(IEngineComponent engineComponent)<br />
private void StartMasterEngineService(MasterEngineInitiator masterEngineInitiator)<br />
private void StartSlaveEngineService(int engineId, string engineName)<br />
private void StopSlaveEngineService(int engineId)<br />
internal CoreBusinessLayerServiceInstance GetServiceInstance(
      int engineId)<br />
internal void AddServiceInstance(CoreBusinessLayerServiceInstance serviceInstance)<br />
internal void RunRescheduleEngineDiscoveryJobsTask(int engineId)<br />
private static bool GetIsJobEngineServiceEnabled()<br />
private void ScheduleUpdateEngineTable()<br />
private void UpdateEngineInfoTask()<br />
private void ScheduleCheckEvaluationExpiration()<br />
private static void ScheduleCheckDatabaseLimit()<br />
private static void ScheduleCheckPollerLimit()<br />
private static void ScheduleSavePollingCapacityInfo()<br />
private static void ScheduleSaveElementsUsageInfo()<br />
private static void ScheduleMaintananceExpiration()<br />
private static void ScheduleCheckLicenseSaturation()<br />
private static void ScheduleCheckOrionProductTeamBlog()<br />
private static void ScheduleMaintenanceRenewals()<br />
private void ScheduleSynchronizeSettingsToRegistry()<br />
private void ScheduleThresholdsProcessing()<br />
private void ScheduleDBMaintanance()<br />
private void ScheduleLazyUpgradeTask()<br />
private void ScheduleDeleteOldLogs()<br />
private void ScheduleBackgroundInventory(int engineId)<br />
private void ScheduleRemoveOldOneTimeJob()<br />
private void ScheduleCertificateMaintenance()<br />
private void ScheduleOrionFeatureUpdate()<br />
private static void ScheduleEnhancedNodeStatusIndications()<br />
private static void EnhancedStatusIndication()<br />
public void StartServiceLog()<br />
public void StopServiceLog()<br />
private static void UpdateEnginePortInDB(string serverName, int port)<br />
public bool IsServiceDown<br />
private static void CheckMaintenanceRenewals(object state)<br />
private static void CheckOrionProductTeamBlog(object state)<br />
private static void CheckLicenseSaturation(object state)<br />
private static void SaveElementsUsageInfo(object state)<br />
private static void CheckMaintenanceExpiration(object state)<br />
private static void CheckPollerLimit(object state)<br />
private static void SavePollingCapacityInfo(object state)<br />
private static void CheckDatabaseLimit(object state)<br />
private void CheckEvaluationExpiration(object state)<br />
public virtual void Stop()<br />
private static void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)<br />
private void RunBackgroundInventoryCheck(object state)<br />
