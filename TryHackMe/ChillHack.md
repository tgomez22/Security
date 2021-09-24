

nmap
```
└─$ sudo nmap -sS -Pn 10.10.202.225
Host discovery disabled (-Pn). All addresses will be marked 'up' and scan times will be slower.
Starting Nmap 7.91 ( https://nmap.org ) at 2021-09-23 16:42 PDT
Nmap scan report for 10.10.202.225
Host is up (0.16s latency).
Not shown: 997 closed ports
PORT   STATE SERVICE
21/tcp open  ftp
22/tcp open  ssh
80/tcp open  http

Nmap done: 1 IP address (1 host up) scanned in 7.30 seconds

```

anonymous ftp is allowed. got note.txt
```
└─$ cat note.txt
Anurodh told me that there is some filtering on strings being put in the command -- Apaar

```

10.10.202.225
`<!--made by vipul mirajkar thevipulm.appspot.com-->`

gobuster
```
/.hta.php             (Status: 403) [Size: 278]
/.hta.txt             (Status: 403) [Size: 278]
/.hta.html            (Status: 403) [Size: 278]
/.hta                 (Status: 403) [Size: 278]
/.htaccess.php        (Status: 403) [Size: 278]
/.htpasswd.html       (Status: 403) [Size: 278]
/.htaccess            (Status: 403) [Size: 278]
/.htpasswd            (Status: 403) [Size: 278]
/.htaccess.txt        (Status: 403) [Size: 278]
/.htpasswd.php        (Status: 403) [Size: 278]
/.htaccess.html       (Status: 403) [Size: 278]
/.htpasswd.txt        (Status: 403) [Size: 278]
/about.html           (Status: 200) [Size: 21339]
/blog.html            (Status: 200) [Size: 30279]
/contact.php          (Status: 200) [Size: 0]    
/contact.html         (Status: 200) [Size: 18301]
/css                  (Status: 301) [Size: 312] [--> http://10.10.202.225/css/]
/fonts                (Status: 301) [Size: 314] [--> http://10.10.202.225/fonts/]
/images               (Status: 301) [Size: 315] [--> http://10.10.202.225/images/]
/index.html           (Status: 200) [Size: 35184]                                 
/index.html           (Status: 200) [Size: 35184]                                 
/js                   (Status: 301) [Size: 311] [--> http://10.10.202.225/js/]    
/news.html            (Status: 200) [Size: 19718]                                 
/secret               (Status: 301) [Size: 315] [--> http://10.10.202.225/secret/]
/server-status        (Status: 403) [Size: 278]                                   
/team.html            (Status: 200) [Size: 19868]   
```


```
<?php
        if(isset($_POST['command']))
        {
                $cmd = $_POST['command'];
                $store = explode(" ",$cmd);
                $blacklist = array('nc', 'python', 'bash','php','perl','rm','cat','head','tail','python3','more','less','sh','ls');
                for($i=0; $i<count($store); $i++)
                {
                        for($j=0; $j<count($blacklist); $j++)
                        {
                                if($store[$i] == $blacklist[$j])
				{?>
					<h1 style="color:red;">Are you a hacker?</h1>
					<style>
						body
						{
							background-image: url('images/FailingMiserableEwe-size_restricted.gif');
							background-position: center center;
  							background-repeat: no-repeat;
  							background-attachment: fixed;
  							background-size: cover;					
	}	
					</style>
<?php					 return;
				}
                        }
                }
		?><h2 style="color:blue;"><?php echo shell_exec($cmd);?></h2>
			<style>
                             body
                             {
                                   background-image: url('images/blue_boy_typing_nothought.gif');  
				   background-position: center center;
  				   background-repeat: no-repeat;
  				   background-attachment: fixed;
  				   background-size: cover;
}
                          </style>
	<?php }
?>
```
