**Tristan Gomez**

# Who Are You?
<br />
<br />

## Setup

**Author:** MADSTACKS <br />
<br />
**Point Value:** 100<br />

**Description:** Let me in. Let me iiiiiiinnnnnnnnnnnnnnnnnnnn<br />
http://mercury.picoctf.net:52362/
<br />

**Hint:** It ain't much, but it's an RFC https://tools.ietf.org/html/rfc2616


## Step 1

When we navigate to the challenge page, We see a classic meme, and some helpful text "Only people who use the official PicoBrowser are allowed on this site!" <br />

I am using Python to solve this challenge with its "requests" library. See "WhoAreYou.py" for the full challenge solution. <br />

Since we need to use the "picobrowser" and I'm using Firefox, we need to make some changes. I am going to create a totally harmless payload:

```
exploit = {
    'User-Agent' : 'picobrowser'
}
```

We will set the headers parameter in our GET request to use the "exploit" payload. Onto part 2!

## Step 2
When viewing the response payload from our GET request in step 1, we get the text "I don't trust users visiting from another site." <br />

Cool, how do we make the site think we are visiting from the same page? Exactly, we will use the "Referer" header.

```

```

## Step 3
"Sorry, this site only worked in 2018."

## Step 4

"I don't trust users who can be tracked."

## Step 5

"This website is only for people from Sweden."

## Step 6

"You're in Sweden but you don't speak Swedish?"

## Finale

"What can I say except, you are welcome"

picoCTF{http_h34d3rs_v3ry_c0Ol_much_w0w_0c0db339}