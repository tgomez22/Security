**Tristan Gomez**

# Insp3ct0r

### Author: Zaratec/Danny

### Description:
`
Kishor Balan tipped us off that the following code may need inspection: https://jupiter.challenges.picoctf.org/problem/44924/ (link) or http://jupiter.challenges.picoctf.org:44924
`

### Hints:
  * How do you inspect web code on a browser?
  * There's 3 parts.
  
### Step 1

When I first navigate to the challenge site, I can see a large `Inspect Me` banner with two page links: `What` and `How`. The `What` page just has some text stating `I made a website`.

Opening up the browser developer tools, I navigate to the `Inspector` tab and start expanding the page's HTML. Near the bottom of the page's source I found a comment.

```
<-- Html is neat. Anyways have 1/3 of the flag: picoCTF{tru3_d3 -->
```
There doesn't appear to be anything more on this page. Clicking the link to the `How` page reveals more hints for the challenge. 

```
            How
I used these to make this site:
            HTML
            CSS
       JS(JavaScript)
```

Since we've found the HTML portion of the flag, let's check out the other resources for the site.

### Step 2

In the browser's developer tools I am going to click the `Style Editor` tab and here I can see two clickable options: `css` and `mycss.css`. The `css` option doesn't have anything of note in it, but `mycss.css` is more interesting. Scrolling to the bottom of the file reveals the second part of the flag.

```
/* You need CSS to make pretty pages. Here's part 2/3 of the flag: t3ct1ve_0r_ju5t */
```

With the second part of the flag in hand, let's find the JS flag.

### Step 3

Still using the browser's developer tools, I am going to click the `Debugger` tab. This shows some nested directories


```
Main Thread -> jupiter.challenges.picoctf.org -> problem/44924 -> myjs.js
```

We want to expand any minimized folders to get to the `myjs.js` file.  After we open the file, scroll to the bottom to get the last part of the flag.

```
/* Javascript sure is neat. Anyways part 3/3 of the flag: _lucky?f10be399} */
```


**Flag: picoCTF{tru3_d3t3ct1ve_0r_ju5t_lucky?f10be399}***
