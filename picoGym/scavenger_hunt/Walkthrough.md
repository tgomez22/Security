**Tristan Gomez**

# Scavenger Hunt

### Author: MADSTACKS

### Description:
`
There is some interesting information hidden around this site http://mercury.picoctf.net:27393/. Can you find it?
`

### Hints:
 * You should have enough hints to find the files, don't run a brute forcer

### Step 1

This challenge is very similar to the `Insp3ct0r` level. In fact, I would say that this level is an extension of it. Opening up the dev tools in my broswer, I am navigating to the `Inspector` tab. Expanding the HTML reveals the first part of the flag 'hidden' in a comment.

```
<--Here's the first part of the flag: picoCTF{t -->
```

### Step 2

Clicking the `What` page link, I am taken to a new page with the text

```
           What
I used these to make this site:
           HTML
           CSS
      JS(JavaScript)
```

Again, this page gives us enough threads to pull to find the flags. Lets go to the css. I clicked the `Style Editor` tab in my developer tools and opened up `mycss.css`. There is nothing of note in the plain `css` page. In `mycss.css` at the bottom of the file is another part of our flag 'hidden' in a comment.

```
/* CSS makes the page look nice, and yes, it also has part of the flag. Here's part 2: h4ts_4_l0 */
```

### Step 3

Clicking the `Debugger` tab in the browser tools, I can see a directory tree on the left side of the `Debugger` panel. Expand the directories unil `myjs.js` appears. Click into the file and scroll to the bottom to get not a part of the flag but another hint.

`
/*How can I keep Google from indexing my website? */
`

This is a very big hint. Configuring a `/robots.txt` page properly is how to prevent search engines from indexing certain pages on a website. 
<br />

When the `/robots.txt` page loads I am greeted by the following text and part 3 of the flag.
```
User-agent: *
Disallow: /index.html
# Part 3: t_0f_pl4c
# I think this is an apache server... can you Access the next flag?
```

### Step 4

Our next hint is that this is an `apache` server. `/.htaccess` files are used to `provide a way to make configuration changes on a per-directory basis.` If we try to access this file using the browser, we ge the next part of the flag. 

```
# Part 4: 3s_2_lO0k
# I love making websites on my Mac, I can Store a lot of information there.
```

### Step 5

The hint in the previous step specifically mentions `Mac` and the word `Store` is capitalized. I don't think that `Store` is capitalized mistakenly, so lets pull this thread. Doing more research, I found that `MacOS` creates `.DS_Store` files. This file `is an invisible file on the macOS operating system that gets automatically created anytime you look into a folder with ‘Finder.’ This file will then follow the folder everywhere it goes, including when archived, like in ‘ZIP.’`

Huh, that doesn't seem super secure. Lets try to access it in the browser.

### Finale

Navigating to the `.DS_Store` file, we get the last part of the flag.

```
Congrats! You completed the scavenger hunt. Part 5: _d375c750}
```

**Flag: picoCTF{th4ts_4_l0t_0f_pl4c3s_2_lO0k_d375c750}**
