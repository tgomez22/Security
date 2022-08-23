initial nmap scan
```
└─$ sudo nmap -sV 10.10.14.212
Starting Nmap 7.92 ( https://nmap.org ) at 2022-08-23 13:59 PDT
Nmap scan report for 10.10.14.212
Host is up (0.17s latency).
Not shown: 998 closed tcp ports (reset)
PORT   STATE SERVICE VERSION
22/tcp open  ssh     OpenSSH 8.2p1 Ubuntu 4ubuntu0.4 (Ubuntu Linux; protocol 2.0)
80/tcp open  http    Apache httpd 2.4.41 ((Ubuntu))
Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 13.56 seconds
```

Navigated to 10.10.14.212 and found a login page
`phpIPAM IP address management [v1.4.5]` with possible user `0day` (0day@ollieshouse.thm)

gobuster
```
└─$ gobuster dir -w /usr/share/wordlists/dirb/common.txt -x php,html,txt,js -u 10.10.14.212
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.14.212
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                /usr/share/wordlists/dirb/common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              php,html,txt,js
[+] Timeout:                 10s
===============================================================
2022/08/23 14:03:41 Starting gobuster in directory enumeration mode
===============================================================
/.hta                 (Status: 403) [Size: 277]
/.hta.js              (Status: 403) [Size: 277]
/.hta.php             (Status: 403) [Size: 277]
/.hta.html            (Status: 403) [Size: 277]
/.htaccess            (Status: 403) [Size: 277]
/.hta.txt             (Status: 403) [Size: 277]
/.htpasswd            (Status: 403) [Size: 277]
/.htaccess.txt        (Status: 403) [Size: 277]
/.htpasswd.php        (Status: 403) [Size: 277]
/.htaccess.js         (Status: 403) [Size: 277]
/.htpasswd.html       (Status: 403) [Size: 277]
/.htaccess.php        (Status: 403) [Size: 277]
/.htpasswd.txt        (Status: 403) [Size: 277]
/.htaccess.html       (Status: 403) [Size: 277]
/.htpasswd.js         (Status: 403) [Size: 277]
/api                  (Status: 301) [Size: 310] [--> http://10.10.14.212/api/]
/app                  (Status: 301) [Size: 310] [--> http://10.10.14.212/app/]
/config.php           (Status: 200) [Size: 0]                                 
/css                  (Status: 301) [Size: 310] [--> http://10.10.14.212/css/]
/db                   (Status: 301) [Size: 309] [--> http://10.10.14.212/db/] 
/functions            (Status: 301) [Size: 316] [--> http://10.10.14.212/functions/]
/imgs                 (Status: 301) [Size: 311] [--> http://10.10.14.212/imgs/]     
/index.php            (Status: 302) [Size: 0] [--> http://10.10.14.212/index.php?page=login]
/index.php            (Status: 302) [Size: 0] [--> http://10.10.14.212/index.php?page=login]
/install              (Status: 301) [Size: 314] [--> http://10.10.14.212/install/]          
/javascript           (Status: 301) [Size: 317] [--> http://10.10.14.212/javascript/]       
/js                   (Status: 301) [Size: 309] [--> http://10.10.14.212/js/]               
/misc                 (Status: 301) [Size: 311] [--> http://10.10.14.212/misc/]             
/robots.txt           (Status: 200) [Size: 54]                                              
/robots.txt           (Status: 200) [Size: 54]                                              
/server-status        (Status: 403) [Size: 277]                                             
/upgrade              (Status: 301) [Size: 314] [--> http://10.10.14.212/upgrade/] 
```

in /db/schema.sql I found this being inserted in to the `users` table.
```
(1,
'Admin',
X'243624726F756E64733D33303030244A51454536644C394E70766A6546733424524B3558336F6132382E557A742F6835564166647273766C56652E3748675155594B4D58544A5573756438646D5766507A5A51506252626B38784A6E314B797974342E64576D346E4A4959684156326D624F5A33672E',
X'','Administrator','phpIPAM Admin','admin@domain.local',X'30','statistics;favourite_subnets;changelog;access_logs;error_logs;top10_hosts_v4', 'Yes');


```
The password is a hex encoded string which decodes to
```
$6$rounds000$JQEE6dL9NpvjeFs4$RK5X3oa28.Uzt/h5VAfdrsvlVe.7HgQUYKMXTJUsud8dmWfPzZQPbRbk8xJn1Kyyt4.dWm4nJIYhAV2mbOZ3g.
```

I couldn't crack this hash, so I went back and added `ollieshouse.thm` to my /etc/hosts. I tried to NC connect to port 1337 because I kept
getting weird output from it.
```
└─$ nc ollieshouse.thm 1337
Hey stranger, I'm Ollie, protector of panels, lover of deer antlers.

What is your name? ollie
What's up, Ollie! It's been a while. What are you here for? stuff
Ya' know what? Ollie. If you can answer a question about me, I might have something for you.


What breed of dog am I? I'll make it a multiple choice question to keep it easy: Bulldog, Husky, Duck or Wolf? Bulldog
You are correct! Let me confer with my trusted colleagues; Benny, Baxter and Connie...
Please hold on a minute
Ok, I'm back.
After a lengthy discussion, we've come to the conclusion that you are the right person for the job.Here are the credentials for our administration panel.

                    Username: admin

                    Password: OllieUnixMontgomery!

PS: Good luck and next time bring some treats!

```
