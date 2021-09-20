**Tristan Gomez**

```
Can you gain access to this gaming server built by amateurs with no experience of web development and take advantage of the deployment system.
```


nmap 
```
└─$ sudo nmap -sV 10.10.110.240
Starting Nmap 7.91 ( https://nmap.org ) at 2021-09-20 11:04 PDT
Nmap scan report for 10.10.110.240
Host is up (0.18s latency).
Not shown: 998 closed ports
PORT   STATE SERVICE VERSION
22/tcp open  ssh     OpenSSH 7.6p1 Ubuntu 4ubuntu0.3 (Ubuntu Linux; protocol 2.0)
80/tcp open  http    Apache httpd 2.4.29 ((Ubuntu))
Service Info: OS: Linux; CPE: cpe:/o:linux:linux_kernel

Service detection performed. Please report any incorrect results at https://nmap.org/submit/ .
Nmap done: 1 IP address (1 host up) scanned in 18.21 seconds

```

10.10.110.240
```
<!-- john, please add some actual content to the site! lorem ipsum is horrible to look at. -->
```


/robots.txt
```
user-agent: *
Allow: /
/uploads/
```

/uploads
```
dict.lst
manifesto.txt
meme.jpg
```


/about.php
```
<?php
	if(isset($_FILES['image'])){
		$errors = array();
		$file_name = $_FILES['image']['name'];
		$file_size = $_FILES['image']['size'];
		$file_tmp = $_FILES['image']['tmp_name'];
		$file_type = $_FILES['image']['type'];
		$file_ext = strtolower(end(explode('.',$FILES['image']['name'])));

		$expensions = array('jpeg', 'jpg', 'png', 'php');

		if(in_array($file_ext,$expensions)=== false){
			$errors[] = "extension not allowed, please choose a different file type.";
		}

		if(empty($errors) == true){
			move_uploaded_file($file_tmp,"uploads/".$filename);
			echo "Success";
		}else{
			print_r($errors);
		}
	}
?>

```

/secret 
->gives ssh private key
 user johntheripper to get passphrase for ssh key
 `letmein`
 
 

What is the user flag?
`a5c2ff8b9c2e3d4fe9d4ff2f1a5a6e7e`

What is the root flag?
