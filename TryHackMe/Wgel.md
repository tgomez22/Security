**Tristan Gomez**


nmap
```
─$ sudo nmap -sV 10.10.210.190                                                                                   
Starting Nmap 7.91 ( https://nmap.org ) at 2021-09-16 11:57 PDT
Nmap scan report for 10.10.210.190
Host is up (0.16s latency).
Not shown: 998 closed ports
PORT   STATE SERVICE VERSION
22/tcp open  ssh     OpenSSH 7.2p2 Ubuntu 4ubuntu2.8 (Ubuntu Linux; protocol 2.0)
80/tcp open  http    Apache httpd 2.4.18 ((Ubuntu))
Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 12.37 seconds

```
` <!-- Jessie don't forget to udate the webiste -->`

gobuster
* `/sitemap`
* `/sitemap/images`
* `/sitemap/css`
* `/sitemap/js`
* 
