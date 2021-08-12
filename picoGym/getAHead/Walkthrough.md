**Tristan Gomez**

# GET aHEAD

### Author: MADSTACKS

### Description:
`
Find the flag being held on this server to get ahead of the competition http://mercury.picoctf.net:15931/
`

### Hints:
  * Maybe you have more than 2 choices
  * Check out tools like Burpsuite to modify your requests and look at the responses
  
 ### Walkthrough
 
 Navigating to the challenge page, I am greeted by very bright red background. There are two boxes containing buttons on the page. 
 One button says `Choose Red` and the other says `Choose Blue`. Upon inspecting the page source, I find that clicking the `Choose Red` button sends a
 `GET` request to `/index.php`, while clicking the `Choose Blue` button sends a `POST` request to `/index.php`. <br />
 
 The first hint references `GET` and `POST` requests, but our third option(the one which solves the level) is `HEAD`. According to Mozilla's documentation
 `HEAD` `requests the headers that would be returned if the HEAD request's URL was instead requested with the HTTP GET method`. Using the Python requests 
 library, I will send a `HEAD` request to `/index.php`. I will then print the response headers yielding the flag.
 
 **Flag: picoCTF{r3j3ct_th3_du4l1ty_82880908}**
