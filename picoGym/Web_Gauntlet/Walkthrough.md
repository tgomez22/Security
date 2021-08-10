**Tristan Gomez**

# Web Gauntlet
<br />
<br />

### Author: MADSTACKS <br />


### Description:
```
Can you beat the filters? Log in as admin.
http://jupiter.challenges.picoctf.org:29164/ http://jupiter.challenges.picoctf.org:29164/filter.php
```

### Hints:
    * You are not allowed to login with valid credentials.
    * Write down the injections you use in case you lose your progress.
    * For some filters it may be hard to see the characters, always (always) look at the raw hex in the response.
    * sqlite
    * If your cookie keeps getting reset, try using a private browser window

### Setup
From the hints we can see that this will be a sql injection challenge. The author specified that we will need to use sqlite syntax, so we will need to be aware of any quirks this version may have(hint: it comes in handy later)

### Step 1
### OR is filtered out.

I need to find out if this sql uses a single quotes or double quotes. Lets try ' and ' for username and password. <br />

Ah, thats super nice of this challenge. At the top left corner of the screen I can see: <br /> 

```
SELECT *
FROM users
WHERE username='''
AND password='''
```

Now we know that this is using the single quote. We can now break this. The challenge wants us to login as 'admin'. Let's try <br /> 

```
admin'--
```

as our injection for the username and "a" for our password(The password really doesn't matter).<br />

*BAM!* Level 2. Our injection set the username to admin, but we closed the username field with the ' after the "n" in admin. Then using the -- we commented out the "AND password=" field, bypassing it completely.

### Step 2
### OR AND LIKE = -- filtered out

We have less injection methods in this part, but this injection will be similar to the first in that we will use sql comments to bypass the password. In the injection we will use a block comment.

```
    admin' /*
```

Again, I will set my password to "a" because it really doesn't matter. Onto level 3!

### Step 3
### OR AND = LIKE > < -- filtered out

This step removes more potential tools, but it doesn't remove anything that we used in the last step. The same injection allows us to proceed to level 4.

### Step 4
### OR AND = LIKE > < -- admin filtered out

This step gets very interesting, we can't use the word 'admin' any more. Here is where knowledge of sqlite comes in. The || operator is the concatenation operator in sqlite, which is super weird to me. Anyways, the | character isn't filtered out, so we can use this injection to win the level.


```
a'||'d'||'m'||'i'||'n'/*
```

We concatenated all of the characters for the word 'admin' which the filter reads individually. Sqlite evaluates the expression to concatenate the characters into 'admin' then evaluates the sql expression. Onto the last level. 

### Step 5
### OR AND = LIKE > < -- UNION ADMIN filtered out
With even less tools at out disposal, notice how no character from our last injection is filtered out. We can reuse it and win the challenge. <br />

When we win, we get to a screen that says "Congrats! You won! Check out filter.php" <br />

When we checkout /filter.php we can see the source code for the game. At the bottom of the page is our flag:

```
picoCTF{y0u_m4d3_1t_a3ed4355668e74af0ecbb7496c8dd7c5}
```
