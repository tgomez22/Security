## Services
* Create and manage critical functions(network connectivity, storage, memory, sound, creds, etc.)
* Open the `run` program and type in `services.msc`.

## Windows Registry
* container database that stores configuration settings, essential keys and shared preferences for Windows. 
* Use the `run` program and use `regedit` to open Registry editor.

## Event Viewer
* Shows log details about all events: driver updates, hardware failures, changes in OS, invalid Auth. attempts.

Event categories:
* Application: Records events of system components.
* System: Records events of already installed programs.
* Security: Logs events related to security and authentication etc.

Access event viewer by typing `eventvwr` in `run` program.
* events stored in `C:\WINDOWS\systems32\config\folder`

## IAM (Identity & Access Management)
* To create an account `Control Panel > User Accounts`.
* Two types of accounts in Windows: Admin & Standard Account.
* Admin account SHOULD only be used to carry out tasks like software installation, accessing the registry editory, service panel, etc.
* Routine functions like access to regular applications, like Microsoft Office, browser, etc are run by standard accounts.
* enforce passwords, no guest accounts

### User Account Control (UAC)
* Enforces enhanced access control and ensures that all services and applications execute in non-administrator accounts.
* To access UAC, go to `Control Panel -> User Accounts` and click on `Change User Account Control Setting`.

### Local Policy and Group Policies Editor
* used to implement local and group policies. 
* Can be used to limit the execution of vulnerable extensions, set password policies, and other administrative settings.

### Password Policies
* `Security settings > Account Policies > Password policy`
* To set a lockout policy: `Local Security Policy > Windows Setting > Account Policies > Account Lockout Policy` and
configure values to lock out after X attempts.

## Network Mgmt.
* Use the `run` program -> `WF.msc`
* 3 main profiles: Domain, Public and Private.
* Private must be activated with "Blocked Incoming Connections" while using computer at home.
* Configure it with a `default deny` rule before making an exception rule.

### Disable unused Networking Devices
* Go to `Control panel > System and Security Setting > System > Device Manager` -> then disable unused Networking devices.
* Open powershell `Disable-WindowsOptionalFeature -Online -FeatureName SMB1Protocol` to disable SMB.

* Put very restrictive permissions `C:\Windows\System32\Drivers\etc\hosts`.

check ARP entries `arp -a` in cmd prompt. If two MAC addresses map to two IPs then run `arp -d` to clear the table.

### Preventing remote access to machine
* Disable this by using `settings > Remote Desktop`

## Application Management
* Only run apps from the Microsoft Store. 
* You can allow instalattion of apps only from the Microsoft Store by using `Setting > Select Apps and Features` and
then select `The Microsoft Store Only`.

### Windows Defender
* Real-time protection - enables periodic scanning of the Computer.
* Browser integration - enables safe browsing by scanning all downloaded files, etc. 
* Application Guard - Allows complete web session sandboxing to block malicious websites or sessions to make changes in the computer.
* Controlled FOlder Access - Protext memory areas and folders from unwanted applications.

### Microsoft Office Hardening
https://learn.microsoft.com/en-us/microsoft-365/security/defender-endpoint/attack-surface-reduction-rules-reference?view=o365-worldwide
* delete macros

### AppLocker
* AppLocker allows users to block specific executables, scripts, and installers from execution through a set of rules.

* turn on smart screen by going to `Settings > Windows Security > App and Browser Control > Reputation-based Protection` 
-> then scroll down and turn on `SmartScreen option`.

## Storage Management

### Data Encryption Through BitLocker
* `Start > Control Panel > System and Security > BitLocker Drive Encryption` -> Turn it on.
* `Settings > Update and Security > Backup` -> Then `Back up using File History`.
* Finally Click `Start > Settings > Update & Security > Windows Updates.`
