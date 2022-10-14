## GNU Privacy Guard (GPG)
* Easy encryption for email and files

* For first use you need to create your own keys. Use `gpg --gen-key`. 
* Ensure new files and directories that are made are secured with the correct permissions. `.gnugpg`
* Verify keys were created with `gpg --list-keys`

### encrypting files with GPG (Symmetric)
* encrypt files with `gpg -c <our_file>`
* Must use a passphrase to lock the file. it is NOT the passphrase used to create your keys.
* IMPORTANT: using the above encryption command on a file leaves behind an `unencrypted backup`.

### decrypting files with GPG (Symmetric)
* `gpg -d <our_file>` 
* Enter secret passphrase when prompted.

### Securing SSH
* open `/etc/ssh/sshd_config` file
* If you see `Protocol 1` or `Protocol 1,2` then disable `protocol 1`. It is compromised and no longer secure.

Considerations about SSH keys:
* Is any user's keys compromised? How do we tell?
* Check perms for users' `.ssh` directory and sub-directories/files.

### Disable Username & Password SSH Login
* Only do this when you are sure that key exchange login works
* Go `/etc/ssh/sshd_config` and change this line: `PasswordAuthentication yes` into `PasswordAuthentication no`.

## X11 Forwarding and SSH Tunneling
* We will want to turn off X11. 
* Go to `sshd_config`.
* Change `X11 Forwarding yes` -> `X11 Forwarding no`.

* We will want SSH tunneling to be disabled. 
* Go to `sshd_config`
* Change `AllowTcpForwarding`,`GatewayPorts`, and `PermitTunnel` to `no`.


## SSH Logging
* log file is created anytimne someone logs in with a protocol that used SSH (SSH, SCP, SFTP,etc.)
* By default this log is stored in `/var/log/auth.log`.
* To increase log output go to `/etc/ssh/sshd_config` -> look for `#LogLevel INFO` -> change to `#LogLevel VERBOSE`.

### Checking open/used ports from inside a machine.
`sudo lsof -i -P -n | grep LISTEN`

## Mandatory Access Control (MAC)
* AppArmor comes preinstalled with Ubuntu.
* SELinux does the same thing.

Benefits of AppArmor:
* It can prevent malicious actors from accessing the data on your systems. As a system administrator, this is extremely important; protecting the confidentiality of your data
* Applications have their own profiles thus making it a little easier
* SELinux and AppArmor have the capability to create your own custom profiles but the scripting in AppArmor is a little easier to understand and reduces the learning curve.

### AppArmor Configuration 
* `/etc/apparmor.d` -> contains all apparmor profiles. `sbin.dhclient` and `usr.*` are the profiles.
* additional profiles can be installed with `sudo apt install apparmor-profiles apparmor-profiles-extra`

### AppArmor Command-Line Utilities
* To get the AppArmor status enter `aa-status`.

AppArmor Modes:
* Enforce - enforces active profiles
* Complain - allows processes to perform disallowed actions by the profile and are logged.
* Audit - The same as Enforce mode but allowed and disallowed actions get logged to `/var/log/audit/audit.log` or system log. 

TO install AppArmor utilities for the command line run: `sudo apt install apparmor-utils`
