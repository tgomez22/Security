**Tristan Gomez**

# Irish-Name-Repo-3

### Author: Xingyang Pan

### Description:
`
There is a secure website running at https://jupiter.challenges.picoctf.org/problem/40742/ (link) or http://jupiter.challenges.picoctf.org:40742. Try to see if you can login as admin!
`

## Hints:
  * Seems like the password is encrypted

## Step 1

Navigating to the challenge website, we are (yet again) greeted by the "List 'o the Irish!" page full of pictures of Irish celebrities. Click the hamburger menu on the top left of the screen and select the "Admin Login" option. On this page, we can see a different login form that only takes a password. Let's inspect the page's HTML. On closer inspection, we see that the form is performing a `POST` to `/login.php` again. The form's parameters are `password` and `debug`. `debug` being set to `0`.

## Step 2

We will craft a payload to see what debugging information we get back when we set the `debug` parameter to `1`. <br />

```
 setDebugFlagPayload = {
  'password' : 'admin',
  'debug' : '1'
 }
```

I will send this payload using a POST from Python's Requests library. Below is the response payload.

```
<pre>
password: admin
SQL query: SELECT * FROM admin where password = 'nqzva'
</pre>
<h1>Login failed.</h1>
```

We have a very similiar query format to the previous challenges, but this time the password we sent in is encoded and compared against a value in the database. 

## Step 3

We need to identify what type of encryption is being used on the `password` field so that we can come up with ideas of how to craft an exploit payload. The info we have to work with is

```
admin
nqzva
```

We can see that `a` maps to `n` and each `r` maps to `e`. I went to https://www.boxentriq.com/code-breaking/cipher-identifier and entered the encoded text from the response payload. I clicked the `Analyze Text` button and found that the `Vigenere Cipher` is the most likely way the password was encoded. I followed a link to https://www.boxentriq.com/code-breaking/vigenere-cipher and on the page was a full `Vigenere Cipher`. Check it out, it involves identifying a character from your alphabet and then shifting each character in the word you want to encode by the position of a single character in your alphabet.

## Step 4

With this cipher knowledge, we can craft an exploit. I want to send `' or 1=1 --` as my payload, but I need to encode it using my cipher. The resulting string is `' be 1=1 --`.

Our resulting payload is

```
exploit = {
  "password" : """' be 1=1 --"""
}
```

We can either send the payload using Python requests or we can enter it manually on the site. After using the exploit we made it to the end of the challenge. We see 

```
Logged in!
Your flag is: picoCTF{3v3n_m0r3_SQL_4424e7af}
```


**Flag: picoCTF{3v3n_m0r3_SQL_4424e7af}**
