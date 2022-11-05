
```
└─$ sudo nmap -sV -Pn -p- 10.1.13.1
Starting Nmap 7.92 ( https://nmap.org ) at 2022-11-05 09:51 PDT
Nmap scan report for 10.1.13.1
Host is up (0.17s latency).
Not shown: 65530 filtered tcp ports (no-response)
PORT     STATE SERVICE        VERSION
22/tcp   open  ssh            OpenSSH 8.2p1 Ubuntu 4ubuntu0.5 (Ubuntu Linux; protocol 2.0)
1812/tcp open  http           aiohttp 3.8.3 (Python 3.10)
5005/tcp open  avt-profile-2?
8204/tcp open  lm-perfworks?
9090/tcp open  http           nginx 1.18.0 (Ubuntu)
2 services unrecognized despite returning data. If you know the service/version, please submit the following fingerprints at https://nmap.org/cgi-bin/submit.cgi?new-service :
==============NEXT SERVICE FINGERPRINT (SUBMIT INDIVIDUALLY)==============
SF-Port5005-TCP:V=7.92%I=7%D=11/5%Time=63669598%P=x86_64-pc-linux-gnu%r(Ge
SF:tRequest,1E5,"HTTP/1\.1\x20200\x20OK\r\nContent-Length:\x20254\r\nConne
SF:ction:\x20close\r\nContent-Type:\x20text/html\r\nDate:\x20Sat,\x2005\x2
SF:0Nov\x202022\x2016:55:52\x20GMT\r\nServer:\x20Kestrel\r\nAccept-Ranges:
SF:\x20bytes\r\nETag:\x20\"1d8f0500cc9527e\"\r\nLast-Modified:\x20Fri,\x20
SF:04\x20Nov\x202022\x2013:19:21\x20GMT\r\n\r\n<!doctype\x20html><html\x20
SF:lang=\"en\"><head><meta\x20charset=\"UTF-8\"><meta\x20name=\"viewport\"
SF:\x20content=\"width=device-width,initial-scale=1\"><title>MouseAndScree
SF:n</title><script\x20defer=\"defer\"\x20src=\"\./bundle\.js\"></script><
SF:/head><body><div\x20id=\"phaser\"></div></body></html>")%r(HTTPOptions,
SF:76,"HTTP/1\.1\x20404\x20Not\x20Found\r\nContent-Length:\x200\r\nConnect
SF:ion:\x20close\r\nDate:\x20Sat,\x2005\x20Nov\x202022\x2016:55:53\x20GMT\
SF:r\nServer:\x20Kestrel\r\n\r\n")%r(RTSPRequest,87,"HTTP/1\.1\x20505\x20H
SF:TTP\x20Version\x20Not\x20Supported\r\nContent-Length:\x200\r\nConnectio
SF:n:\x20close\r\nDate:\x20Sat,\x2005\x20Nov\x202022\x2016:55:53\x20GMT\r\
SF:nServer:\x20Kestrel\r\n\r\n")%r(Help,78,"HTTP/1\.1\x20400\x20Bad\x20Req
SF:uest\r\nContent-Length:\x200\r\nConnection:\x20close\r\nDate:\x20Sat,\x
SF:2005\x20Nov\x202022\x2016:56:09\x20GMT\r\nServer:\x20Kestrel\r\n\r\n")%
SF:r(SSLSessionReq,78,"HTTP/1\.1\x20400\x20Bad\x20Request\r\nContent-Lengt
SF:h:\x200\r\nConnection:\x20close\r\nDate:\x20Sat,\x2005\x20Nov\x202022\x
SF:2016:56:09\x20GMT\r\nServer:\x20Kestrel\r\n\r\n")%r(TerminalServerCooki
SF:e,78,"HTTP/1\.1\x20400\x20Bad\x20Request\r\nContent-Length:\x200\r\nCon
SF:nection:\x20close\r\nDate:\x20Sat,\x2005\x20Nov\x202022\x2016:56:10\x20
SF:GMT\r\nServer:\x20Kestrel\r\n\r\n")%r(TLSSessionReq,78,"HTTP/1\.1\x2040
SF:0\x20Bad\x20Request\r\nContent-Length:\x200\r\nConnection:\x20close\r\n
SF:Date:\x20Sat,\x2005\x20Nov\x202022\x2016:56:10\x20GMT\r\nServer:\x20Kes
SF:trel\r\n\r\n")%r(Kerberos,78,"HTTP/1\.1\x20400\x20Bad\x20Request\r\nCon
SF:tent-Length:\x200\r\nConnection:\x20close\r\nDate:\x20Sat,\x2005\x20Nov
SF:\x202022\x2016:56:10\x20GMT\r\nServer:\x20Kestrel\r\n\r\n")%r(FourOhFou
SF:rRequest,76,"HTTP/1\.1\x20404\x20Not\x20Found\r\nContent-Length:\x200\r
SF:\nConnection:\x20close\r\nDate:\x20Sat,\x2005\x20Nov\x202022\x2016:56:2
SF:1\x20GMT\r\nServer:\x20Kestrel\r\n\r\n");
==============NEXT SERVICE FINGERPRINT (SUBMIT INDIVIDUALLY)==============
SF-Port8204-TCP:V=7.92%I=7%D=11/5%Time=63669593%P=x86_64-pc-linux-gnu%r(NU
SF:LL,53,"Welcome\x20to\x20Bambi-Notes!\n=====\x20\[Unauthenticated\]\x20=
SF:====\n\x20\x20\x201\.\x20Register\n\x20\x20\x202\.\x20Login\n>\x20")%r(
SF:DNSVersionBindReqTCP,53,"Welcome\x20to\x20Bambi-Notes!\n=====\x20\[Unau
SF:thenticated\]\x20=====\n\x20\x20\x201\.\x20Register\n\x20\x20\x202\.\x2
SF:0Login\n>\x20")%r(DNSStatusRequestTCP,53,"Welcome\x20to\x20Bambi-Notes!
SF:\n=====\x20\[Unauthenticated\]\x20=====\n\x20\x20\x201\.\x20Register\n\
SF:x20\x20\x202\.\x20Login\n>\x20")%r(Help,53,"Welcome\x20to\x20Bambi-Note
SF:s!\n=====\x20\[Unauthenticated\]\x20=====\n\x20\x20\x201\.\x20Register\
SF:n\x20\x20\x202\.\x20Login\n>\x20")%r(X11Probe,53,"Welcome\x20to\x20Bamb
SF:i-Notes!\n=====\x20\[Unauthenticated\]\x20=====\n\x20\x20\x201\.\x20Reg
SF:ister\n\x20\x20\x202\.\x20Login\n>\x20")%r(LPDString,53,"Welcome\x20to\
SF:x20Bambi-Notes!\n=====\x20\[Unauthenticated\]\x20=====\n\x20\x20\x201\.
SF:\x20Register\n\x20\x20\x202\.\x20Login\n>\x20")%r(LDAPBindReq,53,"Welco
SF:me\x20to\x20Bambi-Notes!\n=====\x20\[Unauthenticated\]\x20=====\n\x20\x
SF:20\x201\.\x20Register\n\x20\x20\x202\.\x20Login\n>\x20")%r(LANDesk-RC,5
SF:3,"Welcome\x20to\x20Bambi-Notes!\n=====\x20\[Unauthenticated\]\x20=====
SF:\n\x20\x20\x201\.\x20Register\n\x20\x20\x202\.\x20Login\n>\x20")%r(Term
SF:inalServer,53,"Welcome\x20to\x20Bambi-Notes!\n=====\x20\[Unauthenticate
SF:d\]\x20=====\n\x20\x20\x201\.\x20Register\n\x20\x20\x202\.\x20Login\n>\
SF:x20")%r(NCP,53,"Welcome\x20to\x20Bambi-Notes!\n=====\x20\[Unauthenticat
SF:ed\]\x20=====\n\x20\x20\x201\.\x20Register\n\x20\x20\x202\.\x20Login\n>
SF:\x20")%r(JavaRMI,53,"Welcome\x20to\x20Bambi-Notes!\n=====\x20\[Unauthen
SF:ticated\]\x20=====\n\x20\x20\x201\.\x20Register\n\x20\x20\x202\.\x20Log
SF:in\n>\x20")%r(afp,53,"Welcome\x20to\x20Bambi-Notes!\n=====\x20\[Unauthe
SF:nticated\]\x20=====\n\x20\x20\x201\.\x20Register\n\x20\x20\x202\.\x20Lo
SF:gin\n>\x20");
Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 375.86 seconds
Segmentation fault
```
P: 116706585909882855949064031805287109785342362019593444350806374691250539363420518352727963235278919560904547014653163999934127413680402874233624349720341428278721405999171625494074973231920112919279849806720193015139277616214247922305758152898782460139892457580307990795559014568114297549630989320291467801149
Q: 976287758474242093559213335343554573828578822023
G: 68120267496017067987470038433060147231455745102412025611062035352172211746894428621291737090884555471760322624663064516944830983491558902801002374078039523400461416594699448684908525186909099657843109172189333805039972689245153683069537218261488250591518822500548188125840759659094561983821038532940188943901
X: 326439758004866533252954819365708265806546777841
Y: 52664308264385407389946307142188036967085584236150028228898148494481031766806589376095372337354147939745953902766157781735792168089245290481716795487616135394119625573296859684165512472029078557943283625228639092200027526423957292867164052293061063724863346074194666284919821486831273068275727816846650635014
pow prefix = 0123456789abcdef (5?)

Ha = 220935506670538986994665387808417170512
Sa = 717758972656832348086368301986017338896758802599,709657925964199947802206026781454090318474318545

Hb = 140468779859769633351864343808974098195
Sb = 717758972656832348086368301986017338896758802599,81827290230629607534803878444351436445975383471
