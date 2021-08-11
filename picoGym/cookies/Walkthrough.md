**Tristan Gomez**

# Cookies

### Author: MADSTACKS

### Description:

`
Who doesn't love cookies? Try to figure out the best one. http://mercury.picoctf.net:21485/
`

### Hints:
  * None

### Step 1

When we first visit the side, we can see a simple search field with the text `Welcome to my cookie search page. See how much I like different kinds of cookies!` above the search bar. The search bar has a pre-filled value of `snickerdoodle`. <br />

Looking at the source of the page in the browser tools, I can see that the form performs a `POST` to `/search`, but there are not other fields. <br />

Since the challenge is called `cookies`, I'm going to click the `Storage` tab on the browser tools. I can see a cookie called `name` with a value of `-1`. <br />

To test the behavior of the site, I entered `oatmeal` into the search bar and clicked `Search`. A red text box appeared above the search area, displaying the text `That doesn't appear to be a valid cookie.` <br />

### Step 2

Since the search functionality wasn't working, I am going to assume that the `name` cookie has something to do with this. I'm going to use the Python requests library to craft payloads for this challenge. To test my hunch I am going to create two payloads.

```
exploit_data = {
  'name' : 'snickerdoodle'
}
```

```
exploit_cookie = {
  'name' : '1'
}
```

In the response payload, the page updates and says `That is a cookie! Not very special though ...`. It also says `I love chocolate chip cookies!`. From this response, I can gather that each integer in the `name` cookie corresponds to some cookie type. That being said I will have to iterate through different integers until I find the flag. See `cookies.py` for my solution. <br />

TLDR: The code iterates through integers checking if the flag is in the response payload. A `name` value of `18` gives us the flag.

```
<b>Flag</b>: <code>picoCTF{3v3ry1_l0v3s_c00k135_94190c8a}</code>
```

**FLAG: picoCTF{3v3ry1_l0v3s_c00k135_94190c8a}**
