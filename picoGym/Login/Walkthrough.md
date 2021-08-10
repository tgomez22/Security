**Tristan Gomez**

# Login

### AUTHOR: BROWNIEINMOTION

### Description:

My dog-sitter's brother made this website but I can't get in; can you help?

login.mars.picoctf.net

### Hints:
  * None

### Step 1

Navigating to the challenge page, we are greeted by a generic looking login form with username and password fields. Upon inspecting the html source we see that the form performs a `POST` to the page we are on. There are no hidden fields. Clicking on the `Debugger` tab in my browser tools, I can see an `index.js` file. Its contents are 

```
(
  async() => {
    await new Promise((
      e => window.addEventListener("load",e))),
      document.querySelector("form").addEventListener("submit", ( 
        e => { e.preventDefault() ; 
        const r={ u:"input[name=username]", p:"input[name=password]"}, t={};
        for(const e in r)
          t[e]=btoa(document.querySelector(r[e]).value).replace(/=/g,"");
        return "YWRtaW4" !== t.u ? alert("Incorrect Username") : "cGljb0NURns1M3J2M3JfNTNydjNyXzUzcnYzcl81M3J2M3JfNTNydjNyfQ" !== t.p ? alert("Incorrect   Password") : void alert(`Correct Password! Your flag is ${atob(t.p)}.`)}))})(
);
```

We appear to have client-side validation and we can see two encoded strings. Let's attempt to find out how these strings are encoded.

### Step 2

I can see in the correct password alert the `atob` method. Looking at some documentation of the method I have found that this method decodes a base64 encoded string. I opened up a Codepen window on my browser and ran


`console.log(atob('YWRtaW4'));` results in `admin`

`console.log(atob('cGljb0NURns1M3J2M3JfNTNydjNyXzUzcnYzcl81M3J2M3JfNTNydjNyfQ'));` results in `picoCTF{53rv3r_53rv3r_53rv3r_53rv3r_53rv3r}`

We can see that the first string is `admin` and looking back at the .js file we can see that this comparison is being made for the `username` field. The second string is being used to check the password. Once decoded we can see that the password is actually the flag for the challenge. If we enter these credentials into the form and submit it, we get a success message!

```
Correct Password! Your flag is
picoCTF{53rv3r_53rv3r_53rv3r_53rv3r_53rv3r}.
```

**Flag: picoCTF{53rv3r_53rv3r_53rv3r_53rv3r_53rv3r}**
