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
## Natas 11
## Natas 12
## Natas 13
## Natas 14 
## Natas 15 
## Natas 16
## Natas 17
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
