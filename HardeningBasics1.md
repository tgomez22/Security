# User Accounts
* The principle of least privilege states that each user should only have enough access to perform their daily tasks.
* Check that root login is disabled
* Allow non-privileged users to perform privileged tasks by entering their own passwords
* Check passwords/password hashes of existing users to see if they are secure enough (try to crack them with rockyou.txt)
* Check all user accounts to see if legitimate
* Check if any syscall hooking via ldd, etc.
* Check all SUID and GUID perms of all binaries.
* Check if Splunk free version can be used on our VM infrastructure. 
* make sure /etc/passwd and /etc/shadow are protected with the correct perms.
* update all software (OS, binaries, website CMS, website plugins)
* Check searchsploit for vulnerable service versions.


### Check GTFObins to see if any vulnerable binaries are running
* https://gtfobins.github.io/

## Adding Users to a Predefined Admin Group
* possibly remove users from the default sudo group.

Be highly suspicious of any user who has this output when running `sudo -l`
```
User XXXX may run the following commands on harden:
     (ALL : ALL) ALL
```
or if you see... (really any variation of allowing sudo with NO password is bad)
```
%sudo ALL=(ALL:ALL) ALL NOPASSWD: ALL
```
* setup command aliases and add users to them to adhere to principle of least privilege

## Disabling Root Access
* Disable root login shell
* Disable root SSH login
* Disabliung room using PAM (Password Authentication Module)

To `disable the root login shell` we will need to edit the `/etc/passwd` file to the following:
`root:x:0:0:root:/root:/usr/sbin/nologin` -> setting it to this will prevent users from doing `sudo -s`

To `disable the Root SSH Login` we need to edit `/etc/ssh/sshd+config.conf` to `#PermitRootLogin no`.

To `disable Root Using PAM` -> WARNING: editing /etc/pam.d/* or /etc/pam.conf can lock you out of your system. 
can configure `/etc/pam.d/sshd` to show...
```
# Custom PAM Configurations
auth    required         pam_listfile.so \
        onerr=succeed    item=user        sense=deny       file=/etc/ssh/deniedusers
```
Then in `/etc/ssh`, we can make a file called `deniedusers` and use `vim` to add root to the top and then save and close.

## Disabling Shell Escapes
* Use `sudoedit` in the sudo policy file. (/etc/sudoers?)
```
XXXX     ALL=(ALL) sudoedit /etc/my_important_conf.conf
```

## Locking Home Directories
* By default user home directories will most likely be `755` or `-rwxr-xr-x`
* Need to change all user home directory perms to `700` or `-rwx------`
* We want only the owner of a directory to see into it.

## Configuring Password Complexity
* Run `sudo apt-get install libpam-pwquality`
* New file in /etc/security/pwquality.conf
* Can set rules for password creation in this file
```
Must not begin or end with a space
atleast 1 number
atleast 1 letter
Prevent weird unicode characters and emojis
Minimum of 15 characters. 
```
* Force all users to reset their passwords to more secure ones?

## Setting password Expiration
* located in `/etc/login.defs` -> scroll down to "Password aging controls"
* PASS_MAX_DAYS: Default 99999; Sets the maximum number of days a password may be used 
* PASS_MIN_DAYS: Default 0; Sets the minimum number of days a user must keep their password before changing it
* PASS_WARN_AGE: Default 7; Sets the number of days out from expiration that the system will warn the user

## Monitoring Password History
* in `/etc/pam.d/common-password`
* Remember the last 10 passwords. set a minimum age of 1 and a password history of 10
```
# here are the per-package modules (the "Primary" block)
password       required                       pam_pwhistory.so remember=2 retry=3
password       [success=1 default=ignore]     pam_unix.so use_authtok obscure sha512 shadow
```
The pwquality line from before has been removed for simplicity. Let's focus on the top line. Again, I'll go through the PAM settings.  

Disclaimer: The PAM is not easy to understand. I did a lot of research and reading for any of these tasks where PAM was used.  Some of these explanations are from the documentation of pam.d found here.

    password: module type we are referencing
    required: module where failure returns a failure to the PAM-API
    pam_pwhistory.so: module that configures the password history requirements
    remember=2: option for the pam_pwhistory.so module to remember the last n passwords (n = 2). These passwords are saved in /etc/security/opasswd
    retry=3: option for the pam_pwhistory.so module to prompt the user 3 times before returning a failure

You may notice a change to the pam_unix.so line below the top one. We make use of use_authtok here and we tell the module to use shadow which will create shadow passwords when updating a user's password.

## lxd group
* Remove any and all users from this group if assigned to it. 

# Firewall Security

## Host-Based firewalls (iptables)
* installed on host machines and monitor traffic from that host.

4 components of iptables:
* Filter table - offers the basic protection that you'd expect a firewall to provide
* Network Address Translation table - connects the public interwebs to the private networks
* Mangle table - for mangling them packets as they go through the firewall
* Security table - only used by SELinux

* check `sudo iptables -L`
* INPUT - packets coming into the firewall
* FORWARD - packets routed to another NIC on the network; for packets on the local network that are being forwarded on.
* OUTPUT - packets going out of the firewall
```
Chain INPUT (policy ACCEPT)
target     prot opt source         destination
```

### iptables configuration
* `Ansible` is a utility that can distribute host-based firewall rules to other hosts quickly and easily.
* We will be adding rules to the Access Control List (ACL). The rules here will only apply to 1 host.
* 

An example of a basic rule:
`sudo iptables -A INPUT -m conntrack --ctstate ESTABLISHED,RELATED -j ACCEPT`

 * -A INPUT: Append to the INPUT Chain
 * -m conntrack: Call an iptable module. In this case we're calling conntrack which keeps track of connections. This option permits the use of the following option
 * --ctstate ESTABLISHED,RELATED: Available due to the previous flag/option set. Will keep track of connections which are already ESTABLISHED and RELATED. RELATED just means that it's new but part of another already established connection
 * -j ACCEPT: The j stands for jump (I don't know why). This option will just ACCEPT the packet and stop processing other rules

### Allowing Traffic Through Specific Ports
* sudo iptables -A INPUT -p tcp --dport ssh -j ACCEPT
* sudo iptables -A INPUT -p tcp --dport 21 -j ACCEPT
* sudo iptables -A INPUT -p udp --dport 4380 -j ACCEPT    

They all have the same options used but the difference is in what they are allowing through and how they are written to allow those things through. We've already seen the -A INPUT option, so let's go over the -p and --dport options.

    -p {option}: Which connection protocol to use. Only "tcp" or "udp" can be used here
    --dport: Controls the destination port that we want the rule to operate on.

Notice in the first example that we set our --dport to "ssh". This is valid syntax. However, we could just as well have entered 22 here since 22 is the port that corresponds to SSH. And likewise, in #2, we use port 21 but could have put ftp here since that is the protocol that operates on port 21.

The last example is just to show that we can use something other than "tcp" with the -p option.
Of course there are other rules that a system admin would want to add such as allowing all incoming web traffic. This is important for employees so that they are able to surf the internet and get out to research things if needed.

### Blocking Incoming Traffic
Continuing with our admins of dark, ashu and skidy from the first tasks, let's say they have an SMB Server that they use internally to share files on the THM network. But, they don't want people from the outside to be able to access it or even try to access it. They could add a rule such as the following:

sudo iptables -A INPUT -p tcp --dport smb -j DROP

This line will drop all incoming packets using the TCP connection protocol and bound for the port that SMB is configured on.

### Implicit Deny Rule
"if I have not explicitly allowed something through the Firewall, then DENY it implicitly, without hesitation".
`sudo iptables -A INPUT -j DROP`
 
 
 ### misc
 Unfortunately, iptables is not saved in memory and needs to be configured each time you reboot your machine. This can be troublesome and annoying for any system admin. Restarting a server is probably an uncommon event but nonetheless, can happen. In order to save iptables configuration, you can enter sudo iptables-save
Uncomplicated Firewall

The Uncomplicated Firewall (UFW) is meant to make creating Firewall rules less complicated. It provides a friendly way to create an IPv4 (or v6) based Firewall. By default, UFW is disabled. You can check the status of UFW with sudo ufw status (UFW must be run as root).  To enable UFW, you simply do sudo ufw enable. And to disable you do sudo ufw disable.  Ez-pz.

Allowing and Denying Ports

It's actually really easy to allow and deny things with UFW. The basic format is as follows

sudo ufw <allow/deny> <port>/<optional: protocol>    

So to allow TCP connections on port 9000 we do sudo ufw allow 9000/tcp. Denying something would be just as easy. Let's say we want to deny telnet traffic on port 23.  We'd do sudo ufw deny 23.

Allowing and Denying Services

UFW also allows for entering of services instead of ports with sudo ufw <allow/deny> <service name>. For example, allowing SSH would be done with sudo ufw allow ssh. It's really that easy. You can do the same with deny in order to deny a service that you don't to pass through the Firewall.

Advanced Syntax

There's more advanced syntax to allow or deny specific IP addresses, ranges or subnets.  We won't cover this here, but if you want to learn more about how to configure UFW, check out https://help.ubuntu.com/community/UFW. 

# SSH and Encryption

# Mandatory Access Control


* Check all cron jobs permissions and if they are editable. Check what files are being referenced by cron jobs.
* Check the secure PATH env variable and make sure it IS NOT writable.
