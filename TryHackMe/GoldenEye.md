

http://10.10.74.95
```
Naviagate to /sev-home/ to login
```

terminal.js
```
//
//Boris, make sure you update your default password. 
//My sources say MI6 maybe planning to infiltrate. 
//Be on the lookout for any suspicious network traffic....
//
//I encoded you p@ssword below...
//
//&#73;&#110;&#118;&#105;&#110;&#99;&#105;&#98;&#108;&#101;&#72;&#97;&#99;&#107;&#51;&#114;
//
//BTW Natalya says she can break your codes
//

```

CyberChef
`From HTML Entity` -> `InvincibleHack3r`

Login to /sev-home/ using `boris:InvincibleHack3r`

```
GoldenEye
GoldenEye is a Top Secret Soviet oribtal weapons project.
Since you have access you definitely hold a Top Secret clearance and qualify to be a certified GoldenEye Network Operator (GNO) 
Please email a qualified GNO supervisor to receive the online <b>GoldenEye Operators Training</b> to become an Administrator of the GoldenEye system
Remember, since security by obscurity is very effective, we have configured our pop3 service to run on a very high non-default port
```

nmap
```
PORT   STATE SERVICE
25/tcp open  smtp
80/tcp open  http
55006/tcp open  unknown
55007/tcp open  unknown -> POP3exit

Nmap done: 1 IP address (1 host up) scanned in 3.15 seconds
```

`telnet 10.10.74.95 55007` results in...
```
Trying 10.10.74.95...
Connected to 10.10.74.95.
Escape character is '^]'.
+OK GoldenEye POP3 Electronic-Mail System
USER Boris
+OK

```

Hmm, Boris's info doesn't work for `GoldenEye POP3`, but we can see if Boris has a different password using Hydra.

