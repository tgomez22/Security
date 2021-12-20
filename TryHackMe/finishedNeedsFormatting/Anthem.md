

```
└─$ sudo nmap -sV -Pn 10.10.174.124
[sudo] password for gomez22: 
Starting Nmap 7.92 ( https://nmap.org ) at 2021-12-07 14:59 PST
Nmap scan report for 10.10.174.124
Host is up (0.16s latency).
Not shown: 998 filtered tcp ports (no-response)
PORT     STATE SERVICE       VERSION
80/tcp   open  http          Microsoft HTTPAPI httpd 2.0 (SSDP/UPnP)
3389/tcp open  ms-wbt-server Microsoft Terminal Services
Service Info: OS: Windows; CPE: cpe:/o:microsoft:windows

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 62.05 seconds

```

http://10.10.174.124/robots.txt
```
UmbracoIsTheBest!

# Use for all search robots
User-agent: *

# Define the directories not to crawl
Disallow: /bin/
Disallow: /config/
Disallow: /umbraco/
Disallow: /umbraco_client/
```

```
─$ gobuster dir -w common.txt -x php,html,txt -u http://10.10.174.124
===============================================================
Gobuster v3.1.0
by OJ Reeves (@TheColonial) & Christian Mehlmauer (@firefart)
===============================================================
[+] Url:                     http://10.10.174.124
[+] Method:                  GET
[+] Threads:                 10
[+] Wordlist:                common.txt
[+] Negative Status codes:   404
[+] User Agent:              gobuster/3.1.0
[+] Extensions:              php,html,txt
[+] Timeout:                 10s
===============================================================
2021/12/07 15:03:08 Starting gobuster in directory enumeration mode
===============================================================
/archive              (Status: 301) [Size: 118] [--> /]
/Archive              (Status: 301) [Size: 118] [--> /]
/authors              (Status: 200) [Size: 4075]       
/Blog                 (Status: 200) [Size: 5399]       
/blog                 (Status: 200) [Size: 5399]       
/categories           (Status: 200) [Size: 3496]       
/install              (Status: 302) [Size: 126] [--> /umbraco/]
/robots.txt           (Status: 200) [Size: 192]                
/robots.txt           (Status: 200) [Size: 192]                
/rss                  (Status: 200) [Size: 1867]               
/RSS                  (Status: 200) [Size: 1867]               
/search               (Status: 200) [Size: 3422]               
/Search               (Status: 200) [Size: 3422]               
/sitemap              (Status: 200) [Size: 1042]               
/SiteMap              (Status: 200) [Size: 1042]               
/tags                 (Status: 200) [Size: 3549]               
/umbraco              (Status: 200) [Size: 4078]  
```

`SG@anthem.com:UmbracoIsTheBest!`

Hidden in C: is backup(dir). In backup is restore.txt that can't be read but we can change permissions. Add your user profile \SG and allow all. Open file to see `ChangeMeBaby1MoreTime`.

Logout and log back in as `Administrator:ChangeMeBaby1MoreTime`

On the Desktop in root.txt is `THM{Y0U_4R3_1337}`
