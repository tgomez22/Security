## Bandit0
```
Level Goal

The goal of this level is for you to log into the game using SSH.
The host to which you need to connect is bandit.labs.overthewire.org, on port 2220.
The username is bandit0 and the password is bandit0.
Once logged in, go to the Level 1 page to find out how to beat Level 1.
```

`ssh bandit0@bandit.labs.overthewire.org -p 2220`

```
bandit0@bandit:~$ ls
readme
bandit0@bandit:~$ cat readme
NH2SXQwcBdpmTEzi3bvBHMM9H66vVXjL
```

## Bandit1
```
Level Goal

The password for the next level is stored in a file called - located in the home directory
Commands you may need to solve this level
```

Navigated to /home/bandit1
```
bandit1@bandit:~$ pwd
/home/bandit1
bandit1@bandit:~$ ls -la
total 24
-rw-r-----  1 bandit2 bandit1   33 Dec  3 08:13 -
drwxr-xr-x  2 root    root    4096 Dec  3 08:13 .
drwxr-xr-x 49 root    root    4096 Dec  3 08:14 ..
-rw-r--r--  1 root    root     220 Jan  6  2022 .bash_logout
-rw-r--r--  1 root    root    3771 Jan  6  2022 .bashrc
-rw-r--r--  1 root    root     807 Jan  6  2022 .profile
```

```
bandit1@bandit:~$ cat ./-
rRGizSaX8Mk1RTb1CNQoXTcYZWU6lgzi
```

## Bandit2
```
The password for the next level is stored in a file called spaces in this filename located in the home directory
```

```
bandit2@bandit:~$ ls -la
total 24
drwxr-xr-x  2 root    root    4096 Dec  3 08:14 .
drwxr-xr-x 49 root    root    4096 Dec  3 08:14 ..
-rw-r--r--  1 root    root     220 Jan  6  2022 .bash_logout
-rw-r--r--  1 root    root    3771 Jan  6  2022 .bashrc
-rw-r--r--  1 root    root     807 Jan  6  2022 .profile
-rw-r-----  1 bandit3 bandit2   33 Dec  3 08:14 spaces in this filename
bandit2@bandit:~$ cat "spaces in this filename"
aBZ0W5EmUfAf7kHTQeOwd8bauFJ2lAiG
```

## Bandit3
```
The password for the next level is stored in a hidden file in the inhere directory.
```

```
bandit3@bandit:~$ ls -la
total 24
drwxr-xr-x  3 root root 4096 Dec  3 08:14 .
drwxr-xr-x 49 root root 4096 Dec  3 08:14 ..
-rw-r--r--  1 root root  220 Jan  6  2022 .bash_logout
-rw-r--r--  1 root root 3771 Jan  6  2022 .bashrc
drwxr-xr-x  2 root root 4096 Dec  3 08:14 inhere
-rw-r--r--  1 root root  807 Jan  6  2022 .profile
bandit3@bandit:~$ cd inhere
bandit3@bandit:~/inhere$ ls -la
total 12
drwxr-xr-x 2 root    root    4096 Dec  3 08:14 .
drwxr-xr-x 3 root    root    4096 Dec  3 08:14 ..
-rw-r----- 1 bandit4 bandit3   33 Dec  3 08:14 .hidden
bandit3@bandit:~/inhere$ cat .hidden
2EW7BBsr6aMMoJ2HjW067dm8EgX26xNe
```

## Bandit4
```
The password for the next level is stored in the only human-readable file in the inhere directory.
Tip: if your terminal is messed up, try the “reset” command.
```

```
bandit4@bandit:~$ cd inhere
bandit4@bandit:~/inhere$ ls
-file00  -file02  -file04  -file06  -file08
-file01  -file03  -file05  -file07  -file09
bandit4@bandit:~/inhere$ file -file00
file: Cannot open `ile00' (No such file or directory)
bandit4@bandit:~/inhere$ file ./-file00
./-file00: data
bandit4@bandit:~/inhere$ file ./-file01
./-file01: data
bandit4@bandit:~/inhere$ file ./-file02
./-file02: data
bandit4@bandit:~/inhere$ file ./-file03
./-file03: data
bandit4@bandit:~/inhere$ file ./-file04
./-file04: data
bandit4@bandit:~/inhere$ file ./-file05
./-file05: data
bandit4@bandit:~/inhere$ file ./-file06
./-file06: data
bandit4@bandit:~/inhere$ file ./-file07
./-file07: ASCII text
bandit4@bandit:~/inhere$ cat ./-file07
lrIWWI6bB37kxfiCQZqUdOIYfr6eEeqR
```

```
The password for the next level is stored in a file somewhere under the inhere directory
and has all of the following properties:

    human-readable
    1033 bytes in size
    not executable

```

I had to do a lot of searching around in directories to find a file that meet all the criteria.
`ls -la` to show the file size in bytes. You can't see it here but the green colored file names are executable files 
and the white colored file names are non-executable. So `.file2` met all the criteria.
```
bandit5@bandit:~/inhere/maybehere07$ ls -la
total 56
drwxr-x---  2 root bandit5 4096 Dec  3 08:14 .
drwxr-x--- 22 root bandit5 4096 Dec  3 08:14 ..
-rwxr-x---  1 root bandit5 3663 Dec  3 08:14 -file1
-rwxr-x---  1 root bandit5 3065 Dec  3 08:14 .file1
-rw-r-----  1 root bandit5 2488 Dec  3 08:14 -file2
-rw-r-----  1 root bandit5 1033 Dec  3 08:14 .file2
-rwxr-x---  1 root bandit5 3362 Dec  3 08:14 -file3
-rwxr-x---  1 root bandit5 1997 Dec  3 08:14 .file3
-rwxr-x---  1 root bandit5 4130 Dec  3 08:14 spaces file1
-rw-r-----  1 root bandit5 9064 Dec  3 08:14 spaces file2
-rwxr-x---  1 root bandit5 1022 Dec  3 08:14 spaces file3
bandit5@bandit:~/inhere/maybehere07$ file .file2
.file2: ASCII text, with very long lines (1000)
bandit5@bandit:~/inhere/maybehere07$ cat .file2
P4L4vucdmLnm8I7Vl7jG1ApGSfjYKqJU
```

## Bandit5
```
Level Goal

The password for the next level is stored somewhere on the server and has all of the following properties:

    owned by user bandit7
    owned by group bandit6
    33 bytes in size

```

This command works too and filters out every wrong file
` find / -group bandit6 -user bandit7 2>/dev/null`
```
bandit6@bandit:/$ find / -group bandit6 2>/dev/null
/var/lib/dpkg/info/bandit7.password
z7WtoNQU2XfjmMtWA8u5rN4vzqu4v99S
```

## Bandit6
```
The password for the next level is stored in the file data.txt next to the word millionth.
```

```
cat data.txt | grep millionth
millionth       TESKZC0XvTetK0S9xNwm25STk5iWrBvP
```
`TESKZC0XvTetK0S9xNwm25STk5iWrBvP`

## Bandit7
```
The password for the next level is stored in the file data.txt
and is the only line of text that occurs only once
```
```
bandit8@bandit:~$ cat data.txt | sort | uniq -u
EN632PlfYiZbn3PhVK3XOGSlNInNE00t
```
## Bandit8
```
Level Goal

The password for the next level is stored in the file data.txt in one of the few human-readable strings,
preceded by several ‘=’ characters.
```


```
bandit9@bandit:~$ cat data.txt | grep -a =
...
G7w8LIi6J3kTb8A7j9LgrywtEUlyyp6s
...
```

alternative command by Julay
```
bandit9@bandit:~$ cat data.txt | strings -e s | grep =
TM9=\
========== the
=Dbb
P,f=l
2v&z+=
p.g=
bktk=
========== password
j[=Cq
========== is=
b@!g=J
        =LG
=0 E
=0}I
F========== G7w8LIi6J3kTb8A7j9LgrywtEUlyyp6s
h=57
```

## Bandit9
```
The password for the next level is stored in the file data.txt, which contains base64 encoded data
```

## Bandit10
```
bandit10@bandit:~$ cat data.txt | base64 --decode
The password is 6zPeziLdR2RKNdNYFNb6nVCKzphlXHBM
```
## Bandit11
```
The password for the next level is stored in the file data.txt, 
where all lowercase (a-z) and uppercase (A-Z) letters have been rotated by 13 positions
```

```
bandit11@bandit:~$ cat data.txt
Gur cnffjbeq vf WIAOOSFzMjXXBC0KoSKBbJ8puQm5lIEi
```
off to cyberchef!
`The password is JVNBBFSmZwKKOP0XbFXOoW8chDz5yVRv`

or alternate one liner from Julay
`tr 'A-Za-z' 'N-ZA-Mn-za-m' < data.txt`

## Bandit12
```
The password for the next level is stored in the file data.txt, which is a hexdump of a file that has been repeatedly compressed. For this level it may be useful to create a directory under /tmp in which you can work using mkdir. For example: mkdir /tmp/myname123. Then copy the datafile using cp, and rename it using mv (read the manpages!)
```

This is a hell of uncompressing. Use `xxd -r data.txt > data.bin` to get the first file type. Use `file` to see its type. if it is of type `gzip` or `tar` then copy/rename the file and add the correct extension type suffix to it. Then use the corresponding tool to unpack it. If it is of type `bz2`/`bzip` then you don't need to add the suffix. Just use the tool. There is like 10 layers of compression. it's hell but keep on pushing through.
```
bandit12@bandit:/tmp/tristan123$ cat data8.bin
The password is wbWdlBxEir4CaE8LaPhauuOo6pwRmrDw
```

## Bandit13
```
The password for the next level is stored in /etc/bandit_pass/bandit14 and can only be read by user bandit14. For this level, you don’t get the next password, but you get a private SSH key that can be used to log into the next level. Note: localhost is a hostname that refers to the machine you are working on
```

sshkey.private
```
-----BEGIN RSA PRIVATE KEY-----
MIIEpAIBAAKCAQEAxkkOE83W2cOT7IWhFc9aPaaQmQDdgzuXCv+ppZHa++buSkN+
gg0tcr7Fw8NLGa5+Uzec2rEg0WmeevB13AIoYp0MZyETq46t+jk9puNwZwIt9XgB
ZufGtZEwWbFWw/vVLNwOXBe4UWStGRWzgPpEeSv5Tb1VjLZIBdGphTIK22Amz6Zb
ThMsiMnyJafEwJ/T8PQO3myS91vUHEuoOMAzoUID4kN0MEZ3+XahyK0HJVq68KsV
ObefXG1vvA3GAJ29kxJaqvRfgYnqZryWN7w3CHjNU4c/2Jkp+n8L0SnxaNA+WYA7
jiPyTF0is8uzMlYQ4l1Lzh/8/MpvhCQF8r22dwIDAQABAoIBAQC6dWBjhyEOzjeA
J3j/RWmap9M5zfJ/wb2bfidNpwbB8rsJ4sZIDZQ7XuIh4LfygoAQSS+bBw3RXvzE
pvJt3SmU8hIDuLsCjL1VnBY5pY7Bju8g8aR/3FyjyNAqx/TLfzlLYfOu7i9Jet67
xAh0tONG/u8FB5I3LAI2Vp6OviwvdWeC4nOxCthldpuPKNLA8rmMMVRTKQ+7T2VS
nXmwYckKUcUgzoVSpiNZaS0zUDypdpy2+tRH3MQa5kqN1YKjvF8RC47woOYCktsD
o3FFpGNFec9Taa3Msy+DfQQhHKZFKIL3bJDONtmrVvtYK40/yeU4aZ/HA2DQzwhe
ol1AfiEhAoGBAOnVjosBkm7sblK+n4IEwPxs8sOmhPnTDUy5WGrpSCrXOmsVIBUf
laL3ZGLx3xCIwtCnEucB9DvN2HZkupc/h6hTKUYLqXuyLD8njTrbRhLgbC9QrKrS
M1F2fSTxVqPtZDlDMwjNR04xHA/fKh8bXXyTMqOHNJTHHNhbh3McdURjAoGBANkU
1hqfnw7+aXncJ9bjysr1ZWbqOE5Nd8AFgfwaKuGTTVX2NsUQnCMWdOp+wFak40JH
PKWkJNdBG+ex0H9JNQsTK3X5PBMAS8AfX0GrKeuwKWA6erytVTqjOfLYcdp5+z9s
8DtVCxDuVsM+i4X8UqIGOlvGbtKEVokHPFXP1q/dAoGAcHg5YX7WEehCgCYTzpO+
xysX8ScM2qS6xuZ3MqUWAxUWkh7NGZvhe0sGy9iOdANzwKw7mUUFViaCMR/t54W1
GC83sOs3D7n5Mj8x3NdO8xFit7dT9a245TvaoYQ7KgmqpSg/ScKCw4c3eiLava+J
3btnJeSIU+8ZXq9XjPRpKwUCgYA7z6LiOQKxNeXH3qHXcnHok855maUj5fJNpPbY
iDkyZ8ySF8GlcFsky8Yw6fWCqfG3zDrohJ5l9JmEsBh7SadkwsZhvecQcS9t4vby
9/8X4jS0P8ibfcKS4nBP+dT81kkkg5Z5MohXBORA7VWx+ACohcDEkprsQ+w32xeD
qT1EvQKBgQDKm8ws2ByvSUVs9GjTilCajFqLJ0eVYzRPaY6f++Gv/UVfAPV4c+S0
kAWpXbv5tbkkzbS0eaLPTKgLzavXtQoTtKwrjpolHKIHUz6Wu+n4abfAIRFubOdN
/+aLoRQ0yBDRbdXMsZN/jvY44eM+xRLdRVyMmdPtP8belRi2E2aEzA==
-----END RSA PRIVATE KEY-----
```

I put the private key into a file called `id_rsa`. I then changed its perms `chmod 600 id_rsa`. Then I ssh'd in as bandit14 using the key `ssh -i /home/gomez22/bandit/id_rsa bandit14@bandit.labs.overthewire.org -p 2220 `

## Bandit14
```
The password for the next level can be retrieved by submitting the password of the current level to port 30000 on localhost.
```

```
bandit14@bandit:/etc/bandit_pass$ cat bandit14
fGrHPx402xGC7U7rXKDaxiWFTOiF0ENq
```

```
bandit14@bandit:~/.ssh$ nc localhost 30000
fGrHPx402xGC7U7rXKDaxiWFTOiF0ENq
Correct!
jN2kgmIXJ6fShzhT2avhotn4Zcka6tnt
```

## Bandit 15
```
bandit15@bandit:~$ openssl s_client localhost:30001 
...
read R BLOCK
jN2kgmIXJ6fShzhT2avhotn4Zcka6tnt
Correct!
JQttfApK4SeyHwDlI9SXGR50qclOAil1
closed
```

## Bandit 16
`JQttfApK4SeyHwDlI9SXGR50qclOAil1`

## Bandit 17
```
bandit16@bandit:~$ nmap -Pn -sV -p31000-32000 localhost
PORT      STATE SERVICE     VERSION
31046/tcp open  echo
31518/tcp open  ssl/echo
31691/tcp open  echo
31790/tcp open  ssl/unknown
31960/tcp open  echo
```

```
bandit16@bandit:~$ openssl s_client localhost:31790
CONNECTED(00000003)
...
read R BLOCK
JQttfApK4SeyHwDlI9SXGR50qclOAil1
Correct!
-----BEGIN RSA PRIVATE KEY-----
MIIEogIBAAKCAQEAvmOkuifmMg6HL2YPIOjon6iWfbp7c3jx34YkYWqUH57SUdyJ
imZzeyGC0gtZPGujUSxiJSWI/oTqexh+cAMTSMlOJf7+BrJObArnxd9Y7YT2bRPQ
Ja6Lzb558YW3FZl87ORiO+rW4LCDCNd2lUvLE/GL2GWyuKN0K5iCd5TbtJzEkQTu
DSt2mcNn4rhAL+JFr56o4T6z8WWAW18BR6yGrMq7Q/kALHYW3OekePQAzL0VUYbW
JGTi65CxbCnzc/w4+mqQyvmzpWtMAzJTzAzQxNbkR2MBGySxDLrjg0LWN6sK7wNX
x0YVztz/zbIkPjfkU1jHS+9EbVNj+D1XFOJuaQIDAQABAoIBABagpxpM1aoLWfvD
KHcj10nqcoBc4oE11aFYQwik7xfW+24pRNuDE6SFthOar69jp5RlLwD1NhPx3iBl
J9nOM8OJ0VToum43UOS8YxF8WwhXriYGnc1sskbwpXOUDc9uX4+UESzH22P29ovd
d8WErY0gPxun8pbJLmxkAtWNhpMvfe0050vk9TL5wqbu9AlbssgTcCXkMQnPw9nC
YNN6DDP2lbcBrvgT9YCNL6C+ZKufD52yOQ9qOkwFTEQpjtF4uNtJom+asvlpmS8A
vLY9r60wYSvmZhNqBUrj7lyCtXMIu1kkd4w7F77k+DjHoAXyxcUp1DGL51sOmama
+TOWWgECgYEA8JtPxP0GRJ+IQkX262jM3dEIkza8ky5moIwUqYdsx0NxHgRRhORT
8c8hAuRBb2G82so8vUHk/fur85OEfc9TncnCY2crpoqsghifKLxrLgtT+qDpfZnx
SatLdt8GfQ85yA7hnWWJ2MxF3NaeSDm75Lsm+tBbAiyc9P2jGRNtMSkCgYEAypHd
HCctNi/FwjulhttFx/rHYKhLidZDFYeiE/v45bN4yFm8x7R/b0iE7KaszX+Exdvt
SghaTdcG0Knyw1bpJVyusavPzpaJMjdJ6tcFhVAbAjm7enCIvGCSx+X3l5SiWg0A
R57hJglezIiVjv3aGwHwvlZvtszK6zV6oXFAu0ECgYAbjo46T4hyP5tJi93V5HDi
Ttiek7xRVxUl+iU7rWkGAXFpMLFteQEsRr7PJ/lemmEY5eTDAFMLy9FL2m9oQWCg
R8VdwSk8r9FGLS+9aKcV5PI/WEKlwgXinB3OhYimtiG2Cg5JCqIZFHxD6MjEGOiu
L8ktHMPvodBwNsSBULpG0QKBgBAplTfC1HOnWiMGOU3KPwYWt0O6CdTkmJOmL8Ni
blh9elyZ9FsGxsgtRBXRsqXuz7wtsQAgLHxbdLq/ZJQ7YfzOKU4ZxEnabvXnvWkU
YOdjHdSOoKvDQNWu6ucyLRAWFuISeXw9a/9p7ftpxm0TSgyvmfLF2MIAEwyzRqaM
77pBAoGAMmjmIJdjp+Ez8duyn3ieo36yrttF5NSsJLAbxFpdlc1gvtGCWW+9Cq0b
dxviW8+TFVEBl1O4f7HVm6EpTscdDxU+bCXWkfjuRb7Dy9GOtt9JPsX8MBTakzh3
vBgsyi/sN3RqRBcGU40fOoZyfAMT8s1m/uYv52O6IgeuZ/ujbjY=
-----END RSA PRIVATE KEY-----

closed
```

## Bandit 18 
```
bandit17@bandit:~$ ls
passwords.new  passwords.old
bandit17@bandit:~$ cat * | diff *
42c42
< hga5tuuCLF6fFzUpnagiMN8ssu9LFrdg
---
> U79zsNCl1urwJ5rU6pg7ZSCi7ifWOWpT
```


## Bandit 19

Found these commands on stack overflow @ `https://unix.stackexchange.com/questions/98698/ssh-parameter-to-ignore-bashrc-script`

```
└─$ ssh bandit18@bandit.labs.overthewire.org -p 2220 "bash --norc"
                         _                     _ _ _
                        | |__   __ _ _ __   __| (_) |_
                        | '_ \ / _` | '_ \ / _` | | __|
                        | |_) | (_| | | | | (_| | | |_
                        |_.__/ \__,_|_| |_|\__,_|_|\__|


                      This is an OverTheWire game server.
            More information on http://www.overthewire.org/wargames

Load key "/home/gomez22/.ssh/id_rsa": Is a directory
bandit18@bandit.labs.overthewire.org's password:
ls
readme
cat readme
awhqfNnAbc1naukrpqDYcF95h7HoMTrC
```

`ssh bandit18@bandit.labs.overthewire.org -p 2220 /bin/dash`

## Bandit 20
```
bandit19@bandit:~$ ./*do cat /etc/bandit_pass/bandit20
VxCazJaVykI6W36BkBU0mJTCM8rR95XT
```

## Bandit 21
consulted this walkthrough `https://medium.com/secttp/overthewire-bandit-level-20-a1af9a042c56`

0. Open two terminal windows on this machine.
1. In one terminal, Start the netcat listener first on any port not currently in use.
2. On the other terminal, run `./suconnect <portnumber>`
3. On the netcat listener, paste in and enter the password from the previous level.
4. You should see output(see below) from `suconnect` saying it read the correct password.
5. You should see output(see below) showing a received password from the nc window.
```
bandit20@bandit:~$ nc -nvlp 4444
Listening on 0.0.0.0 4444
Connection received on 127.0.0.1 38080
VxCazJaVykI6W36BkBU0mJTCM8rR95XT
NvEJF7oVjkddltPSrdKEFOllh9V1IBcq

bandit20@bandit:~$ ./suconnect 4444
Read: VxCazJaVykI6W36BkBU0mJTCM8rR95XT
Password matches, sending next password
```

## Bandit 21
```
bandit21@bandit:~$ cd /etc/cron.d
bandit21@bandit:/etc/cron.d$ ls
cronjob_bandit15_root  cronjob_bandit23       e2scrub_all
cronjob_bandit17_root  cronjob_bandit24       otw-tmp-dir
cronjob_bandit22       cronjob_bandit25_root  sysstat
...
bandit21@bandit:/etc/cron.d$ cat cronjob_bandit22
@reboot bandit22 /usr/bin/cronjob_bandit22.sh &> /dev/null
* * * * * bandit22 /usr/bin/cronjob_bandit22.sh &> /dev/null
```

```
bandit21@bandit:/etc/cron.d$ cd /usr/bin
bandit21@bandit:/usr/bin$ ls -la | grep cronjob
-rwx------  1 root     root          142 Dec  3 08:13 cronjob_bandit15_root.sh
-rwx------  1 root     root          443 Dec  3 08:13 cronjob_bandit17_root.sh
-rwxr-x---  1 bandit22 bandit21      130 Dec  3 08:14 cronjob_bandit22.sh
-rwxr-x---  1 bandit23 bandit22      211 Dec  3 08:14 cronjob_bandit23.sh
-rwxr-x---  1 bandit24 bandit23      384 Dec  3 08:14 cronjob_bandit24.sh
-rwx------  1 root     root          497 Dec  3 08:14 cronjob_bandit25_root.sh
...
```

```
bandit21@bandit:/usr/bin$ cat cronjob_bandit22.sh
#!/bin/bash
chmod 644 /tmp/t7O6lds9S0RqQh9aMcz6ShpAoZKF7fgv
cat /etc/bandit_pass/bandit22 > /tmp/t7O6lds9S0RqQh9aMcz6ShpAoZKF7fgv
bandit21@bandit:~$ cat /tmp/t7O6lds9S0RqQh9aMcz6ShpAoZKF7fgv
WdDozAdTM2z9DiFEQ2mGlwngMfj4EZff
```

## Bandit 22
```
bandit22@bandit:~$ cd /etc/cron.d
bandit22@bandit:/etc/cron.d$ ls
cronjob_bandit15_root  cronjob_bandit23       e2scrub_all
cronjob_bandit17_root  cronjob_bandit24       otw-tmp-dir
cronjob_bandit22       cronjob_bandit25_root  sysstat
bandit22@bandit:/etc/cron.d$ cat cronjob_bandit23
@reboot bandit23 /usr/bin/cronjob_bandit23.sh  &> /dev/null
* * * * * bandit23 /usr/bin/cronjob_bandit23.sh  &> /dev/null
bandit22@bandit:/etc/cron.d$  cat /usr/bin/cronjob_bandit23.sh
#!/bin/bash

myname=$(whoami)
mytarget=$(echo I am user $myname | md5sum | cut -d ' ' -f 1)

echo "Copying passwordfile /etc/bandit_pass/$myname to /tmp/$mytarget"

cat /etc/bandit_pass/$myname > /tmp/$mytarget
bandit22@bandit:/etc/cron.d$ echo I am user bandit23 | md5sum |cut -d ' '
 -f1
8ca319486bfbbc3663ea0fbe81326349
bandit22@bandit:/etc/cron.d$ cat /tmp/8ca319486bfbbc3663ea0fbe81326349
QYw0Y2aiA672PsMmh9puTQuhoz8SyR2G
```
