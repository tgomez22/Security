```
└─$ gobuster dir -w /usr/share/wordlists/dirb/common.txt -x php,txt,js,html -u http://10.10.130.34
===============================================================
Gobuster v3.2.0-dev
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.130.34
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                /usr/share/wordlists/dirb/common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.2.0-dev
[+] Extensions:              php,txt,js,html
[+] Timeout:                 10s
===============================================================
2022/11/01 10:10:07 Starting gobuster in directory enumeration mode
===============================================================
/.html                (Status: 403) [Size: 277]
/.hta                 (Status: 403) [Size: 277]
/.hta.php             (Status: 403) [Size: 277]
/.hta.txt             (Status: 403) [Size: 277]
/.hta.js              (Status: 403) [Size: 277]
/.hta.html            (Status: 403) [Size: 277]
/.htaccess.php        (Status: 403) [Size: 277]
/.htaccess            (Status: 403) [Size: 277]
/.htaccess.txt        (Status: 403) [Size: 277]
/.htaccess.js         (Status: 403) [Size: 277]
/.htaccess.html       (Status: 403) [Size: 277]
/.htpasswd            (Status: 403) [Size: 277]
/.htpasswd.php        (Status: 403) [Size: 277]
/.htpasswd.txt        (Status: 403) [Size: 277]
/.htpasswd.js         (Status: 403) [Size: 277]
/.htpasswd.html       (Status: 403) [Size: 277]
/assets               (Status: 301) [Size: 313] [--> http://10.10.130.34/assets/]
/backup               (Status: 301) [Size: 313] [--> http://10.10.130.34/backup/]
/changelog.txt        (Status: 200) [Size: 816]
/index.php            (Status: 302) [Size: 0] [--> /login.php]
/index.php            (Status: 302) [Size: 0] [--> /login.php]
/internal             (Status: 301) [Size: 315] [--> http://10.10.130.34/internal/]
/login.php            (Status: 200) [Size: 4774]
/logout.php           (Status: 302) [Size: 0] [--> /login.php]
/Readme.txt           (Status: 200) [Size: 222]
/robots.txt           (Status: 200) [Size: 40]
/robots.txt           (Status: 200) [Size: 40]
/server-status        (Status: 403) [Size: 277]
/server-info.php      (Status: 200) [Size: 1689]
/vendor               (Status: 301) [Size: 313] [--> http://10.10.130.34/vendor/]
```

On /robots.txt I found...
```
User-Agent: *
Disallow: /backup/chat.txt
```

On /backup/chat.txt I found...
```
Admin: I have finished setting up the new export2pdf tool.
Kate: Thanks, we will require daily system reports in pdf format.
Admin: Yes, I am updated about that.
Kate: Have you finished adding the internal server.
Admin: Yes, it should be serving flag from now.
Kate: Also Don't forget to change the creds, plz stop using your username as password.
Kate: Hello.. ?
```


I was then able to login using `admin:admin` on the index.html page.
On the admin panel I saw a message in a feed on the right-hand side of the screen `Internal pages hosted at /internal/admin.php. It contains the system flag.`

I then tried to nagivate to that page but got a message that it could only be accessed locally.


I opened a burp suite proxy browser so I could intercept the POST request in flight and edit
the `url` field from `http://127.0.0.1/server-info.php` to `http://127.0.0.1/internal/admin.php`. Then I forwarded the
request and got the flag!
