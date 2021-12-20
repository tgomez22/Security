**Tristan Gomez**

# Who Are You?

### Setup

### Author: MADSTACKS 


### Description:
` Let me in. Let me iiiiiiinnnnnnnnnnnnnnnnnnnn http://mercury.picoctf.net:52362/`


### Hints:
    * Hint: It ain't much, but it's an RFC https://tools.ietf.org/html/rfc2616


### Step 1

When we navigate to the challenge page, We see a classic meme, and some helpful text `Only people who use the official PicoBrowser are allowed on this site!`

I am using Python to solve this challenge with its "requests" library. See "WhoAreYou.py" for the full challenge solution. <br />

Since we need to use the "picobrowser" and I'm using Firefox, we need to make some changes. Setting the `User-Agent` header to `picobrowser` will let me move onto the next step of this challenge. 

```
exploit = {
    'User-Agent' : 'picobrowser'
}
```


We will set the headers parameter in our GET request to use the "exploit" payload. Onto part 2!

### Step 2
When viewing the response payload from our GET request in step 1, we get the text `I don't trust users visiting from another site.` <br />

Cool, how do we make the site think we are visiting from the same page? Exactly, we will use the `Referer` header. Onto step 3!

```
exploit = {
    'User-Agent': 'picobrowser', 
    'Referer' : 'http://mercury.picoctf.net:52362/'
}
```

### Step 3
Viewing our response payload, we get the message `Sorry, this site only worked in 2018.`. Easy, we set the `Date` header to some day in 2018. I used an old calendar to find a valid date in 2018. Onto step 4!

```
exploit = {
    'User-Agent': 'picobrowser', 
    'Referer' : 'http://mercury.picoctf.net:52362/',
    'Date' : 'Wed, 6 June 2018'
}
```

### Step 4

Our response payload gives us `I don't trust users who can be tracked.`. This is where things start getting a bit more difficult. The `DNT` header is depreciated. Setting its value to `1` indicates that `The user prefers not to be tracked on the target site.`. Onto step 5!

```
exploit = {
    'User-Agent': 'picobrowser', 
    'Referer' : 'http://mercury.picoctf.net:52362/',
    'Date' : 'Wed, 6 June 2018',
    'DNT' : '1'
}
```

### Step 5

Looking at our response from the previous payload, we see `This website is only for people from Sweden.`. I struggled with this part of the challenge. I originally thought the `Location` header was the header to use, but this is not the case. Here we want to use the `X-Forwarded-For` header which is used for `identifying the originating IP address of a client connecting to a web server through an HTTP proxy or a load balancer.`. I performed a quick Google search to get a Swedish IP address to set for the value of the `X-Forwarded-For` header. Onto the final step!

```
exploit = {
    'User-Agent': 'picobrowser', 
    'Referer' : 'http://mercury.picoctf.net:52362/',
    'Date' : 'Wed, 6 June 2018',
    'DNT' : '1',
    'X-Forwarded-For' : '103.4.97.18'
}
```
### Step 6

Our response from the previous payload gives us the message `You're in Sweden but you don't speak Swedish?`. This is clearly a hint for use to change the `Accept-Language` header. I had to perform a Google search to see what the string for the Swedish language is and its `sv-SE`. Plugging in that value and sending the payload will solve the level!

```
exploit = {
    'User-Agent': 'picobrowser', 
    'Referer' : 'http://mercury.picoctf.net:52362/',
    'Date' : 'Wed, 6 June 2018',
    'DNT' : '1',
    'X-Forwarded-For' : '103.4.97.18',
    'Accept-Language' : 'sv-SE'
}
```

### Finale
The final response payload yields the flag.

```
What can I say except, you are welcome

picoCTF{http_h34d3rs_v3ry_c0Ol_much_w0w_0c0db339}
```
