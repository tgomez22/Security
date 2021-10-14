**Tristan Gomez**

nmap
```
└─$ sudo nmap -Pn -sV 10.10.69.122
Host discovery disabled (-Pn). All addresses will be marked 'up' and scan times will be slower.
Starting Nmap 7.91 ( https://nmap.org ) at 2021-09-23 15:53 PDT
Nmap scan report for 10.10.69.122
Host is up (0.19s latency).
Not shown: 997 closed ports
PORT   STATE SERVICE VERSION
21/tcp open  ftp     ProFTPD 1.3.5a
22/tcp open  ssh     OpenSSH 7.2p2 Ubuntu 4ubuntu2.10 (Ubuntu Linux; protocol 2.0)
80/tcp open  http    Apache httpd 2.4.18 ((Ubuntu))
Service Info: OSs: Unix, Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 12.02 seconds
```

10.10.69.122
```
Welcome to Beacon Mental Hospital. Sebastian Castellanos and his partners, Joseph Oda and Juli Kidman received a call from dispatch that there was just an incident at the hospital. So they began their investigation. Unfortunately, the team got separated.


Your job is to stand beside the team and help them to withstand the challenges which are coming ahead ...
```

hidden in source code is `<!-- Sebastian sees a path through the darkness which leads to a room => /sadistRoom -->`

/sadistRoom
hidden in src `<!-- To find more about Sadist visit https://theevilwithin.fandom.com/wiki/Sadist -->`

clicking link results in 
```
Key to locker Room => 532219a04ab7a02b56faafbec1a4c1ea
```

use the password to enter /lockerRoom once in...
```

Sebastian is hiding inside a locker to make it harder for the sadist to find him. While Sebastian was inside the locker he found a note. That looks like a map of some kind.

Decode this piece of text "Tizmg_nv_zxxvhh_gl_gsv_nzk_kovzhv" and get the key to access the map

Click here to veiw the map ...
```

`Tizmg_nv_zxxvhh_gl_gsv_nzk_kovzhv` -> Atbash Cipher -> `Grant_me_access_to_the_map_please`

/SafeHeaven
`<!-- I think I'm having a terrible nightmare. Search through me and find it ... -->`
