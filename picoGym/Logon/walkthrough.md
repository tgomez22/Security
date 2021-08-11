**Tristan Gomez**

# Logon

### Author: BOBSON 

### Description:

```
The factory is hiding things from all of its users. Can you login as Joe and find what they've been looking at?

https://jupiter.challenges.picoctf.org/problem/15796/ (link) or http://jupiter.challenges.picoctf.org:15796
```

## Hints:
  * Hmm it doesn't seem to check anyone's password, except for Joe's?

## Step 1<br />

When I navigate to the challenge page, I am greeted by a standard login page with `username` and `password` fields. I attempted to type in "Joe" as a username and I didn't enter a password. Upon clicking the `Sign In` button, a warning message appears on the page, `I'm sorry Joe's password is super secure. You're not getting in that way.`<br />
<br />

## Step 2 <br />
Leaving the username and password fields empty I decided to click the `Sign In` button. Surprisingly, I am allowed to login. I am greeted by a success message on the upper middle of the screen, `Success: You logged in! Not sure you'll be able to see the flag though`. In the middle of the screen is big black text stating `No flag for you`. Hmm, this page is definitely where I want to be but I need to investigate it deeper. <br />
<br />

Looking at the cookies stored for the page, I found an interesting cookie named `Admin` and its value is set to `False`. In my browser's developer tools `Storage` tab, I set the value of the `Admin` cookie to `True` then reloaded the page.  <br />

<br />

Success!! The text in the middle of the screen has changed to `picoCTF{th3_c0nsp1r4cy_l1v3s_6edb3f5f}`

**Flag: picoCTF{th3_c0nsp1r4cy_l1v3s_6edb3f5f} **
