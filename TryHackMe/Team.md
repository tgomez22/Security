

nmap 
```
└─$ sudo nmap -p 1-10000 -Pn -sV 10.10.190.176
Host discovery disabled (-Pn). All addresses will be marked 'up' and scan times will be slower.
Starting Nmap 7.91 ( https://nmap.org ) at 2021-09-20 14:17 PDT
Nmap scan report for 10.10.190.176
Host is up (0.16s latency).
Not shown: 9997 filtered ports
PORT   STATE SERVICE VERSION
21/tcp open  ftp     vsftpd 3.0.3
22/tcp open  ssh     OpenSSH 7.6p1 Ubuntu 4ubuntu0.3 (Ubuntu Linux; protocol 2.0)
80/tcp open  http    Apache httpd 2.4.29 ((Ubuntu))
Service Info: OSs: Unix, Linux; CPE: cpe:/o:linux:linux_kernel

```

```
gobuster dir -w /usr/share/wordlists/dirb/common.txt -x .php -u http://10.10.190.176   
/.hta                 (Status: 403) [Size: 278]
/.hta.php             (Status: 403) [Size: 278]
/.htaccess            (Status: 403) [Size: 278]
/.htpasswd.php        (Status: 403) [Size: 278]
/.htaccess.php        (Status: 403) [Size: 278]
/.htpasswd            (Status: 403) [Size: 278]
/index.html           (Status: 200) [Size: 11366]
/server-status        (Status: 403) [Size: 278]  
                                                
```

```
                                                                                                                                                       
└─$ gobuster dir -w /usr/share/wordlists/dirb/common.txt -x .php -u http://team.thm    
/.hta                 (Status: 403) [Size: 273]                                                                    
/.hta.php             (Status: 403) [Size: 273]                                                                    
/.htaccess            (Status: 403) [Size: 273]                                                                    
/.htpasswd            (Status: 403) [Size: 273]                                                                    
/.htaccess.php        (Status: 403) [Size: 273]                                                                    
/.htpasswd.php        (Status: 403) [Size: 273]                                                                    
/assets               (Status: 301) [Size: 305] [--> http://team.thm/assets/]                                      
/images               (Status: 301) [Size: 305] [--> http://team.thm/images/]                                      
/index.html           (Status: 200) [Size: 2966]                                                                   
/robots.txt           (Status: 200) [Size: 5]                                                                      
/scripts              (Status: 301) [Size: 306] [--> http://team.thm/scripts/]
/server-status        (Status: 403) [Size: 273]  
```

http://team.thm/robots.txt
`dale`


ftp
```
Dale
        I have started coding a new website in PHP for the team to use, this is currently under development. It can be
found at ".dev" within our domain.

Also as per the team policy please make a copy of your "id_rsa" and place this in the relevent config file.

Gyles 

```


```
dale@TEAM:~$ cat user.txt
THM{6Y0TXHz7c2d}
```

```
dale@TEAM:~$ sudo -l
Matching Defaults entries for dale on TEAM:
    env_reset, mail_badpass,
    secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin

User dale may run the following commands on TEAM:
    (gyles) NOPASSWD: /home/gyles/admin_checks

```
