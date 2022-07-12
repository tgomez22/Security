nmap
```
└─$ sudo nmap -sX -sV 10.129.192.68
Starting Nmap 7.92 ( https://nmap.org ) at 2022-07-11 16:32 PDT
Nmap scan report for 10.129.192.68
Host is up (0.084s latency).
Not shown: 998 closed tcp ports (reset)
PORT     STATE SERVICE    VERSION
22/tcp   open  ssh        OpenSSH 8.2p1 Ubuntu 4ubuntu0.5 (Ubuntu Linux; protocol 2.0)
8080/tcp open  http-proxy
```

on `http://10.129.192.68:8080/` is a site `Red Panda Search`


gobuster output
```
└─$ gobuster dir -w /usr/share/wordlists/dirb/common.txt -x php,html,txt,js -u http://10.129.192.68:8080/
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.129.192.68:8080/
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                /usr/share/wordlists/dirb/common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              php,html,txt,js
[+] Timeout:                 10s
===============================================================
2022/07/11 16:36:32 Starting gobuster in directory enumeration mode
===============================================================
/error                (Status: 500) [Size: 86]
/search               (Status: 405) [Size: 117]
/stats                (Status: 200) [Size: 987]
```

the /stats page has what looks like usernames: `woodenk` and `damian`.
