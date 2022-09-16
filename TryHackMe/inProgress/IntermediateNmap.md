initial nmap scan
```
└─$ scan 10.10.48.120
Starting Nmap 7.92 ( https://nmap.org ) at 2022-09-16 14:18 PDT
Nmap scan report for 10.10.48.120
Host is up (0.17s latency).
Not shown: 65532 closed tcp ports (conn-refused)
PORT      STATE SERVICE VERSION
22/tcp    open  ssh     OpenSSH 8.2p1 Ubuntu 4ubuntu0.4 (Ubuntu Linux; protocol 2.0)
2222/tcp  open  ssh     OpenSSH 8.2p1 Ubuntu 4ubuntu0.4 (Ubuntu Linux; protocol 2.0)
31337/tcp open  Elite?
1 service unrecognized despite returning data. If you know the service/version, please submit the following fingerprint at https://nmap.org/cgi-bin/submit.cgi?new-service :
SF-Port31337-TCP:V=7.92%I=7%D=9/16%Time=6324EA07%P=x86_64-pc-linux-gnu%r(N
SF:ULL,35,"In\x20case\x20I\x20forget\x20-\x20user:pass\nubuntu:Dafdas!!/st   
SF:r0ng\n\n")%r(GetRequest,35,"In\x20case\x20I\x20forget\x20-\x20user:pass   
SF:\nubuntu:Dafdas!!/str0ng\n\n")%r(SIPOptions,35,"In\x20case\x20I\x20forg   
SF:et\x20-\x20user:pass\nubuntu:Dafdas!!/str0ng\n\n")%r(GenericLines,35,"I   
SF:n\x20case\x20I\x20forget\x20-\x20user:pass\nubuntu:Dafdas!!/str0ng\n\n"   
SF:)%r(HTTPOptions,35,"In\x20case\x20I\x20forget\x20-\x20user:pass\nubuntu   
SF::Dafdas!!/str0ng\n\n")%r(RTSPRequest,35,"In\x20case\x20I\x20forget\x20-   
SF:\x20user:pass\nubuntu:Dafdas!!/str0ng\n\n")%r(RPCCheck,35,"In\x20case\x   
SF:20I\x20forget\x20-\x20user:pass\nubuntu:Dafdas!!/str0ng\n\n")%r(DNSVers   
SF:ionBindReqTCP,35,"In\x20case\x20I\x20forget\x20-\x20user:pass\nubuntu:D   
SF:afdas!!/str0ng\n\n")%r(DNSStatusRequestTCP,35,"In\x20case\x20I\x20forge   
SF:t\x20-\x20user:pass\nubuntu:Dafdas!!/str0ng\n\n")%r(Help,35,"In\x20case   
SF:\x20I\x20forget\x20-\x20user:pass\nubuntu:Dafdas!!/str0ng\n\n")%r(SSLSe   
SF:ssionReq,35,"In\x20case\x20I\x20forget\x20-\x20user:pass\nubuntu:Dafdas   
SF:!!/str0ng\n\n")%r(TerminalServerCookie,35,"In\x20case\x20I\x20forget\x2   
SF:0-\x20user:pass\nubuntu:Dafdas!!/str0ng\n\n")%r(TLSSessionReq,35,"In\x2   
SF:0case\x20I\x20forget\x20-\x20user:pass\nubuntu:Dafdas!!/str0ng\n\n")%r(   
SF:Kerberos,35,"In\x20case\x20I\x20forget\x20-\x20user:pass\nubuntu:Dafdas   
SF:!!/str0ng\n\n")%r(SMBProgNeg,35,"In\x20case\x20I\x20forget\x20-\x20user   
SF::pass\nubuntu:Dafdas!!/str0ng\n\n")%r(X11Probe,35,"In\x20case\x20I\x20f   
SF:orget\x20-\x20user:pass\nubuntu:Dafdas!!/str0ng\n\n")%r(FourOhFourReque   
SF:st,35,"In\x20case\x20I\x20forget\x20-\x20user:pass\nubuntu:Dafdas!!/str   
SF:0ng\n\n")%r(LPDString,35,"In\x20case\x20I\x20forget\x20-\x20user:pass\n
SF:ubuntu:Dafdas!!/str0ng\n\n")%r(LDAPSearchReq,35,"In\x20case\x20I\x20for
SF:get\x20-\x20user:pass\nubuntu:Dafdas!!/str0ng\n\n")%r(LDAPBindReq,35,"I
SF:n\x20case\x20I\x20forget\x20-\x20user:pass\nubuntu:Dafdas!!/str0ng\n\n"
SF:)%r(LANDesk-RC,35,"In\x20case\x20I\x20forget\x20-\x20user:pass\nubuntu:
SF:Dafdas!!/str0ng\n\n")%r(TerminalServer,35,"In\x20case\x20I\x20forget\x2
SF:0-\x20user:pass\nubuntu:Dafdas!!/str0ng\n\n");
Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 490.11 seconds
Segmentation fault

```

```
┌──(gomez22㉿DESKTOP-V2K8SJ4)-[~]
└─$ nc 10.10.48.120 31337                                                    
In case I forget - user:pass
ubuntu:Dafdas!!/str0ng
```

I used the creds to ssh in on port 22
```

$ ls
$ pwd
/home/ubuntu
$ ls -a
.  ..  .bash_logout  .bashrc  .cache  .profile
$ cd ..
$ ls
ubuntu  user
$ cd user
$ ls -a
.  ..  flag.txt
$ cat flag.txt
flag{251f309497a18888dde5222761ea88e4}$
```
