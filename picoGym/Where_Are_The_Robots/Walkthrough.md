**Tristan Gomez**

# Where Are The Robots?
<br />
<br />

## Setup

**Author:** ZARATEC/DANNY <br />
<br />
**Point Value:** 100<br />

**Description:** Can you find the robots?<br />
https://jupiter.challenges.picoctf.org/problem/60915/ (link) or http://jupiter.challenges.picoctf.org:60915 <br />

**Hint:** What part of the website could tell you where the creator doesn't want you to look?

## Step 1
When first opening the challenge URL, I come to a black screen with some text. "Welcome" and "Where are the robots?" <br />
<br />
From the title of the challenge and the text on the page, I can assume that this challenge relates to the robots.txt page. Adding this to the url yields

```
User-agent: *
Disallow: /8028f.html
```

## Step 2
Now that we have a lead to a potentially interesting page, I will append the "/8028f.html" to the original challenge url. After navigating to this new page, I am greeted by:

```
Guess you found the robots
picoCTF{ca1cu1at1ng_Mach1n3s_8028f}
```

Flag: picoCTF{ca1cu1at1ng_Mach1n3s_8028f}

