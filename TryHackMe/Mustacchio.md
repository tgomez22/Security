
nmap
```
└─$ sudo nmap -sV -Pn 10.10.63.45
Host discovery disabled (-Pn). All addresses will be marked 'up' and scan times will be slower.
Starting Nmap 7.91 ( https://nmap.org ) at 2021-09-20 13:30 PDT
Nmap scan report for 10.10.63.45
Host is up (0.18s latency).
Not shown: 998 filtered ports
PORT   STATE SERVICE VERSION
22/tcp open  ssh     OpenSSH 7.2p2 Ubuntu 4ubuntu2.10 (Ubuntu Linux; protocol 2.0)
80/tcp open  http    Apache httpd 2.4.18 ((Ubuntu))
8765/tcp open  ultraseek-http

Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 22.98 seconds

```

/robots.txt
```
User-agent: *
Disallow: /
```

users.bak
`admin1868e36a6d2b17d4c2745f1659433a54d4bc5f4b
`

`admin:bulldog19`

10.10.63.45:8765

http://10.10.63.45:8765/home.php

 `<!-- Barry, you can now SSH in using your key!-->`
