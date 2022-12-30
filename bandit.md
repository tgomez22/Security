## Bandit0
```
Level Goal

The goal of this level is for you to log into the game using SSH.
The host to which you need to connect is bandit.labs.overthewire.org, on port 2220.
The username is bandit0 and the password is bandit0.
Once logged in, go to the Level 1 page to find out how to beat Level 1.
```

`ssh bandit0@bandit.labs.overthewire.org -p 2220`

```
bandit0@bandit:~$ ls
readme
bandit0@bandit:~$ cat readme
NH2SXQwcBdpmTEzi3bvBHMM9H66vVXjL
```

## Bandit1
```
Level Goal

The password for the next level is stored in a file called - located in the home directory
Commands you may need to solve this level
```

Navigated to /home/bandit1
```
bandit1@bandit:~$ pwd
/home/bandit1
bandit1@bandit:~$ ls -la
total 24
-rw-r-----  1 bandit2 bandit1   33 Dec  3 08:13 -
drwxr-xr-x  2 root    root    4096 Dec  3 08:13 .
drwxr-xr-x 49 root    root    4096 Dec  3 08:14 ..
-rw-r--r--  1 root    root     220 Jan  6  2022 .bash_logout
-rw-r--r--  1 root    root    3771 Jan  6  2022 .bashrc
-rw-r--r--  1 root    root     807 Jan  6  2022 .profile
```

```
bandit1@bandit:~$ cat ./-
rRGizSaX8Mk1RTb1CNQoXTcYZWU6lgzi
```

## Bandit2
```
The password for the next level is stored in a file called spaces in this filename located in the home directory
```

```
bandit2@bandit:~$ ls -la
total 24
drwxr-xr-x  2 root    root    4096 Dec  3 08:14 .
drwxr-xr-x 49 root    root    4096 Dec  3 08:14 ..
-rw-r--r--  1 root    root     220 Jan  6  2022 .bash_logout
-rw-r--r--  1 root    root    3771 Jan  6  2022 .bashrc
-rw-r--r--  1 root    root     807 Jan  6  2022 .profile
-rw-r-----  1 bandit3 bandit2   33 Dec  3 08:14 spaces in this filename
bandit2@bandit:~$ cat "spaces in this filename"
aBZ0W5EmUfAf7kHTQeOwd8bauFJ2lAiG
```

## Bandit3
```
The password for the next level is stored in a hidden file in the inhere directory.
```

```
bandit3@bandit:~$ ls -la
total 24
drwxr-xr-x  3 root root 4096 Dec  3 08:14 .
drwxr-xr-x 49 root root 4096 Dec  3 08:14 ..
-rw-r--r--  1 root root  220 Jan  6  2022 .bash_logout
-rw-r--r--  1 root root 3771 Jan  6  2022 .bashrc
drwxr-xr-x  2 root root 4096 Dec  3 08:14 inhere
-rw-r--r--  1 root root  807 Jan  6  2022 .profile
bandit3@bandit:~$ cd inhere
bandit3@bandit:~/inhere$ ls -la
total 12
drwxr-xr-x 2 root    root    4096 Dec  3 08:14 .
drwxr-xr-x 3 root    root    4096 Dec  3 08:14 ..
-rw-r----- 1 bandit4 bandit3   33 Dec  3 08:14 .hidden
bandit3@bandit:~/inhere$ cat .hidden
2EW7BBsr6aMMoJ2HjW067dm8EgX26xNe
```

## Bandit4
```
The password for the next level is stored in the only human-readable file in the inhere directory.
Tip: if your terminal is messed up, try the “reset” command.
```

```
bandit4@bandit:~$ cd inhere
bandit4@bandit:~/inhere$ ls
-file00  -file02  -file04  -file06  -file08
-file01  -file03  -file05  -file07  -file09
bandit4@bandit:~/inhere$ file -file00
file: Cannot open `ile00' (No such file or directory)
bandit4@bandit:~/inhere$ file ./-file00
./-file00: data
bandit4@bandit:~/inhere$ file ./-file01
./-file01: data
bandit4@bandit:~/inhere$ file ./-file02
./-file02: data
bandit4@bandit:~/inhere$ file ./-file03
./-file03: data
bandit4@bandit:~/inhere$ file ./-file04
./-file04: data
bandit4@bandit:~/inhere$ file ./-file05
./-file05: data
bandit4@bandit:~/inhere$ file ./-file06
./-file06: data
bandit4@bandit:~/inhere$ file ./-file07
./-file07: ASCII text
bandit4@bandit:~/inhere$ cat ./-file07
lrIWWI6bB37kxfiCQZqUdOIYfr6eEeqR
```

```
The password for the next level is stored in a file somewhere under the inhere directory
and has all of the following properties:

    human-readable
    1033 bytes in size
    not executable

```

I had to do a lot of searching around in directories to find a file that meet all the criteria.
`ls -la` to show the file size in bytes. You can't see it here but the green colored file names are executable files 
and the white colored file names are non-executable. So `.file2` met all the criteria.
```
bandit5@bandit:~/inhere/maybehere07$ ls -la
total 56
drwxr-x---  2 root bandit5 4096 Dec  3 08:14 .
drwxr-x--- 22 root bandit5 4096 Dec  3 08:14 ..
-rwxr-x---  1 root bandit5 3663 Dec  3 08:14 -file1
-rwxr-x---  1 root bandit5 3065 Dec  3 08:14 .file1
-rw-r-----  1 root bandit5 2488 Dec  3 08:14 -file2
-rw-r-----  1 root bandit5 1033 Dec  3 08:14 .file2
-rwxr-x---  1 root bandit5 3362 Dec  3 08:14 -file3
-rwxr-x---  1 root bandit5 1997 Dec  3 08:14 .file3
-rwxr-x---  1 root bandit5 4130 Dec  3 08:14 spaces file1
-rw-r-----  1 root bandit5 9064 Dec  3 08:14 spaces file2
-rwxr-x---  1 root bandit5 1022 Dec  3 08:14 spaces file3
bandit5@bandit:~/inhere/maybehere07$ file .file2
.file2: ASCII text, with very long lines (1000)
bandit5@bandit:~/inhere/maybehere07$ cat .file2
P4L4vucdmLnm8I7Vl7jG1ApGSfjYKqJU
```

## Bandit5
```
Level Goal

The password for the next level is stored somewhere on the server and has all of the following properties:

    owned by user bandit7
    owned by group bandit6
    33 bytes in size

```

This command works too and filters out every wrong file
` find / -group bandit6 -user bandit7 2>/dev/null`
```
bandit6@bandit:/$ find / -group bandit6 2>/dev/null
/var/lib/dpkg/info/bandit7.password
z7WtoNQU2XfjmMtWA8u5rN4vzqu4v99S
```

## Bandit6
```
The password for the next level is stored in the file data.txt next to the word millionth.
```

```
cat data.txt | grep millionth
millionth       TESKZC0XvTetK0S9xNwm25STk5iWrBvP
```
`TESKZC0XvTetK0S9xNwm25STk5iWrBvP`

## Bandit7
```
The password for the next level is stored in the file data.txt
and is the only line of text that occurs only once
```
```
bandit8@bandit:~$ cat data.txt | sort | uniq -u
EN632PlfYiZbn3PhVK3XOGSlNInNE00t
```
## Bandit8
```
Level Goal

The password for the next level is stored in the file data.txt in one of the few human-readable strings,
preceded by several ‘=’ characters.
```


```
bandit9@bandit:~$ cat data.txt | grep -a =
...
G7w8LIi6J3kTb8A7j9LgrywtEUlyyp6s
...
```

alternative command by Julay
```
bandit9@bandit:~$ cat data.txt | strings -e s | grep =
TM9=\
========== the
=Dbb
P,f=l
2v&z+=
p.g=
bktk=
========== password
j[=Cq
========== is=
b@!g=J
        =LG
=0 E
=0}I
F========== G7w8LIi6J3kTb8A7j9LgrywtEUlyyp6s
h=57
```

## Bandit9
```
The password for the next level is stored in the file data.txt, which contains base64 encoded data
```

## Bandit10
```
bandit10@bandit:~$ cat data.txt | base64 --decode
The password is 6zPeziLdR2RKNdNYFNb6nVCKzphlXHBM
```
## Bandit11
```
The password for the next level is stored in the file data.txt, 
where all lowercase (a-z) and uppercase (A-Z) letters have been rotated by 13 positions
```

```
bandit11@bandit:~$ cat data.txt
Gur cnffjbeq vf WIAOOSFzMjXXBC0KoSKBbJ8puQm5lIEi
```
off to cyberchef!
`The password is JVNBBFSmZwKKOP0XbFXOoW8chDz5yVRv`

or alternate one liner from Julay
`tr 'A-Za-z' 'N-ZA-Mn-za-m' < data.txt`
## Bandit12
## Bandit13
## Bandit14
