**Tristan Gomez**

# Picobrowser


### Author: ARCHIT 

### Description:
`
This website can be rendered only by picobrowser, go and catch the flag!
https://jupiter.challenges.picoctf.org/problem/50522/ (link) or http://jupiter.challenges.picoctf.org:50522
`

### Hints:
    * You don't need to download a new web browser


### Setup
For this challenge I'm using Python and the Python requests library in VSCode. See "picobrowser.py" for the full source code I used to solve the level.

### Step 1
When I first visit the challenge site, I can see a large green button in the middle of the page that says "Flag". When I click the button a red, warning textbox appears above the button saying 

```
You're not picobrowser! 
Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:90.0) Gecko/20100101 Firefox/90.0" 
```

In my program, I am first going to use BeautifulSoup to extract the link to the flag page.

### Step 2
Now, I will craft my exploit. I have to change the user agent from my Firefox browser to the picobrowser. 

```
exploit = {
    'User-Agent' : 'picobrowser'
}
```

In my GET request to the flag page, I will attach the 'exploit' object to the request's headers. When I send the exploit, I get back the flag. In my code after receiving the response payload from the site, I do some extra filtering to remove some tags so the flag will be displayed cleanly. 

**FLAG: picoCTF{p1c0_s3cr3t_ag3nt_51414fa7}**

