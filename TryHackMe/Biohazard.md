**Tristan Gomez**


nmap
```
└─$ nmap -Pn 10.10.211.21
Host discovery disabled (-Pn). All addresses will be marked 'up' and scan times will be slower.
Starting Nmap 7.91 ( https://nmap.org ) at 2021-09-22 15:08 PDT
Nmap scan report for 10.10.211.21
Host is up (0.16s latency).
Not shown: 997 closed ports
PORT   STATE SERVICE
21/tcp open  ftp
22/tcp open  ssh
80/tcp open  http

Nmap done: 1 IP address (1 host up) scanned in 12.32 seconds

```

10.10.211.21

```
July 1998, Evening

The STARS alpha team, Chris, Jill, Barry, Weasker and Joseph is in the operation on searching the STARS bravo team in the nortwest of Racoon city.

Unfortunately, the team was attacked by a horde of infected zombie dog. Sadly, Joseph was eaten alive.

The team decided to run for the nearby mansion and the nightmare begin..........
```

/mansionmain
`<!-- It is in the /diningRoom/ -->`

gobuster
```
gobuster dir -w common.txt -u 10.10.211.21

/.hta                 (Status: 403) [Size: 277]                                                                    
/.htaccess            (Status: 403) [Size: 277]                                                                    
/.htpasswd            (Status: 403) [Size: 277]                                                                    
/attic                (Status: 301) [Size: 312] [--> http://10.10.211.21/attic/]                                   
/css                  (Status: 301) [Size: 310] [--> http://10.10.211.21/css/]                                     
/images               (Status: 301) [Size: 313] [--> http://10.10.211.21/images/]                                  
/index.html           (Status: 200) [Size: 692]                                                                    
/js                   (Status: 301) [Size: 309] [--> http://10.10.211.21/js/]                                      
/server-status        (Status: 403) [Size: 277]       

```

/diningRoom

`<!-- SG93IGFib3V0IHRoZSAvdGVhUm9vbS8= -->` -> base64 decoded to -> `How about the /teaRoom/`

http://10.10.211.21/diningRoom/emblem.php
```
emblem{fec832623ea498e20bf4fe1821d58727}

Look like you can put something on the emblem slot, refresh /diningRoom/
```

/teaRoom
```
What the freak is this! This doesn't look like a human.

The undead walk toward Jill. Without wasting much time, Jill fire at least 6 shots to kill that thing

In addition, there is a body without a head laying down the floor

After the investigation, the body belong to kenneth from Bravo team. What happened here?

After a jiff, Barry broke into the room and found out the truth. In addition, Barry give Jill a Lockpick.

Barry also suggested that Jill should visit the /artRoom/
```

http://10.10.211.21/teaRoom/master_of_unlock.html
`lock_pick{037b35e2ff90916a9abf99129c8e1837}`

/artRoom
```
A number of painting and a sculpture can be found inside the room

There is a paper stick on the wall, Investigate it? YES
```

http://10.10.211.21/artRoom/MansionMap.html
```
Look like a map

Location:
/diningRoom/
/teaRoom/
/artRoom/
/barRoom/
/diningRoom2F/
/tigerStatusRoom/
/galleryRoom/
/studyRoom/
/armorRoom/
/attic/
```

http://10.10.211.21/barRoom/
```
Look like the door has been locked

It can be open by a lockpick
```

after unlocking
```
what a messy bar room

A piano can be found in the bar room

Play the piano?
```

moonlight sonata note
```
Look like a music note

NV2XG2LDL5ZWQZLFOR5TGNRSMQ3TEZDFMFTDMNLGGVRGIYZWGNSGCZLDMU3GCMLGGY3TMZL5
```

base32 decoded -> music_sheet{362d72deaf65f5bdc63daece6a1f676e}

golden emblem 
```
gold_emblem{58a8c41a9d08b8a4e38d02a4d7ff4843}

Look like you can put something on the emblem slot, refresh the previous page
```

put first emblem flag into golden emblem slot
`rebecca`

now go back to /diningRoom and put the golden emblem into the slot
`klfvg ks r wimgnd biz mpuiui ulg fiemok tqod. Xii jvmc tbkg ks tempgf tyi_hvgct_jljinf_kvc`

Vigenere cypher, key is `rebecca`.
decoded text is `there is a shield key inside the dining room. The html page is called the_great_shield_key`

/shield key
`shield_key{48a7a9227cd7eb89f0a062590798cbac}`

/galleryRoom
```
Upon Jill walk into the room, she saw a bunch of gallery and zombie crow in the room

Nothing is interesting, expect the note on the wall

Examine the note? EXAMINE
```

note.txt
```
crest 2:
GVFWK5KHK5WTGTCILE4DKY3DNN4GQQRTM5AVCTKE
Hint 1: Crest 2 has been encoded twice
Hint 2: Crest 2 contanis 18 letters
Note: You need to collect all 4 crests, combine and decode to reavel another path
The combination should be crest 1 + crest 2 + crest 3 + crest 4. Also, the combination is a type of encoded base and you need to decode it
```

/armorRoom
```
Look like the door has been locked

A shield symbol is embedded on the door
```

enter the shield key flag to enter the room

```
Jill saw a total 8 armor stands on the right and left of the room

Jill examine the armor one by one and found a note hidden inside one of it

Read the note? READ
```

crest 3
```
crest 3:
MDAxMTAxMTAgMDAxMTAwMTEgMDAxMDAwMDAgMDAxMTAwMTEgMDAxMTAwMTEgMDAxMDAwMDAgMDAxMTAxMDAgMDExMDAxMDAgMDAxMDAwMDAgMDAxMTAwMTEgMDAxMTAxMTAgMDAxMDAwMDAgMDAxMTAxMDAgMDAxMTEwMDEgMDAxMDAwMDAgMDAxMTAxMDAgMDAxMTEwMDAgMDAxMDAwMDAgMDAxMTAxMTAgMDExMDAwMTEgMDAxMDAwMDAgMDAxMTAxMTEgMDAxMTAxMTAgMDAxMDAwMDAgMDAxMTAxMTAgMDAxMTAxMDAgMDAxMDAwMDAgMDAxMTAxMDEgMDAxMTAxMTAgMDAxMDAwMDAgMDAxMTAwMTEgMDAxMTEwMDEgMDAxMDAwMDAgMDAxMTAxMTAgMDExMDAwMDEgMDAxMDAwMDAgMDAxMTAxMDEgMDAxMTEwMDEgMDAxMDAwMDAgMDAxMTAxMDEgMDAxMTAxMTEgMDAxMDAwMDAgMDAxMTAwMTEgMDAxMTAxMDEgMDAxMDAwMDAgMDAxMTAwMTEgMDAxMTAwMDAgMDAxMDAwMDAgMDAxMTAxMDEgMDAxMTEwMDAgMDAxMDAwMDAgMDAxMTAwMTEgMDAxMTAwMTAgMDAxMDAwMDAgMDAxMTAxMTAgMDAxMTEwMDA=
Hint 1: Crest 3 has been encoded three times
Hint 2: Crest 3 contanis 19 letters
Note: You need to collect all 4 crests, combine and decode to reavel another path
The combination should be crest 1 + crest 2 + crest 3 + crest 4. Also, the combination is a type of encoded base and you need to decode it
```

/attic
```
Look like the door has been locked

A shield symbol is embedded on the door
```

enter shield key to open attic door
```
After Jill reached the attic, she was instanly attacked by a giant snake

Jill fired at least 10 shotgun shell before the snake retreat

She found another body lying on the ground which belongs to Richard, another STARS bravo member.

In additional, there is a note inside the pocket of the body

Read the note? READ
```

crest 4
```
crest 4:
gSUERauVpvKzRpyPpuYz66JDmRTbJubaoArM6CAQsnVwte6zF9J4GGYyun3k5qM9ma4s
Hint 1: Crest 2 has been encoded twice
Hint 2: Crest 2 contanis 17 characters
Note: You need to collect all 4 crests, combine and decode to reavel another path
The combination should be crest 1 + crest 2 + crest 3 + crest 4. Also, the combination is a type of encoded base and you need to decode it
```

/diningRoom2F
```
<p>Once Jill reach the room, she saw a tall status with a shiining blue gem on top of it. However, she can't reach it</p>
	<!-- Lbh trg gur oyhr trz ol chfuvat gur fgnghf gb gur ybjre sybbe. Gur trz vf ba gur qvavatEbbz svefg sybbe. Ivfvg fnccuver.ugzy --> 
```

rot13 -> `You get the blue gem by pushing the status to the lower floor. The gem is on the diningRoom first floor. Visit sapphire.html`

http://10.10.211.21/diningRoom/sapphire.html
`blue_jewel{e1d457e96cac640f863ec7bc475d48aa}`

/tigerStatusRoom
use the blue jewel flag to get first crest
```
crest 1:
S0pXRkVVS0pKQkxIVVdTWUpFM0VTUlk9
Hint 1: Crest 1 has been encoded twice
Hint 2: Crest 1 contanis 14 letters
Note: You need to collect all 4 crests, combine and decode to reavel another path
The combination should be crest 1 + crest 2 + crest 3 + crest 4. Also, the combination is a type of encoded base and you need to decode it
```

`RlRQIHVzZXI6IG`
`h1bnRlciwgRlRQIHBh`
`c3M6IHlvdV9jYW50X2h`
`pZGVfZm9yZXZlcg==`

base64 decoded
`FTP user: hunter, FTP pass: you_cant_hide_forever`

ftp
```
001-key.jpg
002-key.jpg
003-key.jpg
helmet_key.txt.gpg
important.txt
```

important.txt
```
Jill,

I think the helmet key is inside the text file, but I have no clue on decrypting stuff.
Also, I come across a /hidden_closet/ door but it was locked.

From,
Barry

```

steghide 001-key.jpg
`cGxhbnQ0Ml9jYW`


strings 002-key.jpg
`5fYmVfZGVzdHJveV9`

binwalk 003-key.jpg
`3aXRoX3Zqb2x0`

full key
`cGxhbnQ0Ml9jYW5fYmVfZGVzdHJveV93aXRoX3Zqb2x0`
base64 decoded

`plant42_can_be_destroy_with_vjolt` is password to decrypt helmet_key.txt.gpg

helmet key
`helmet_key{458493193501d2b94bbab2e727f8db4b}`

/hidden_closet open with helmet_key
```
The closet room lead to an underground cave

In the cave, Jill met injured Enrico, the leader of the STARS Bravo team. He mentioned there is a traitor among the STARTS Alpha team.

When he was about to tell the traitor name, suddenly, a gun shot can be heard and Enrico was shot dead.

Jill somehow cannot figure out who did that. Also, Jill found a MO disk 1 and a wolf Medal

Read the MO disk 1? READ

Examine the wolf medal? EXAMINE
```

MO disk 1 -> `wpbwbxr wpkzg pltwnhro, txrks_xfqsxrd_bvv_fy_rvmexa_ajk`
Vigenere cipher, key is `albert` -> `weasker login password, stars_members_are_my_guinea_pig`

wolf_medal -> `SSH password: T_virus_rules`

/study_room open with helmet_key
```
Jill saw a messy table upon enter the room

After a short search, Jill managed to find a sealed book

Examine the book? EXAMINE
```

book is `doom.tar.gz`. extract it to get eagle_metal.txt which contains 
`SSH user: umbrella_guest`

ftp `weasker:stars_members_are_my_guinea_pig`
weasker_note.txt
```
Weaker: Finally, you are here, Jill.
Jill: Weasker! stop it, You are destroying the  mankind.
Weasker: Destroying the mankind? How about creating a 'new' mankind. A world, only the strong can survive.
Jill: This is insane.
Weasker: Let me show you the ultimate lifeform, the Tyrant.

(Tyrant jump out and kill Weasker instantly)
(Jill able to stun the tyrant will a few powerful magnum round)

Alarm: Warning! warning! Self-detruct sequence has been activated. All personal, please evacuate immediately. (Repeat)
Jill: Poor bastard
```

ssh `umbrella_guest:T_virus_rules`

```
umbrella_guest@umbrella_corp:~$ sudo -l
[sudo] password for umbrella_guest: 
Sorry, user umbrella_guest may not run sudo on umbrella_corp.
```

```
umbrella_guest@umbrella_corp:~$ ls -a
.   .bash_logout  .cache   .dmrc   .ICEauthority  .local    .ssh         .xsession-errors
..  .bashrc       .config  .gnupg  .jailcell      .profile  .Xauthority

```

.jailcell/chris.txt
```
Jill: Chris, is that you?
Chris: Jill, you finally come. I was locked in the Jail cell for a while. It seem that weasker is behind all this.
Jil, What? Weasker? He is the traitor?
Chris: Yes, Jill. Unfortunately, he play us like a damn fiddle.
Jill: Let's get out of here first, I have contact brad for helicopter support.
Chris: Thanks Jill, here, take this MO Disk 2 with you. It look like the key to decipher something.
Jill: Alright, I will deal with him later.
Chris: see ya.

MO disk 2: albert 

```

```
umbrella_guest@umbrella_corp:~$ su weasker
Password: stars_members_are_my_guinea_pig
weasker@umbrella_corp:/home/umbrella_guest$ 
```

```
weasker@umbrella_corp:/home/umbrella_guest$ sudo -l
[sudo] password for weasker: 
Matching Defaults entries for weasker on umbrella_corp:
    env_reset, mail_badpass,
    secure_path=/usr/local/sbin\:/usr/local/bin\:/usr/sbin\:/usr/bin\:/sbin\:/bin\:/snap/bin

User weasker may run the following commands on umbrella_corp:
    (ALL : ALL) ALL

```

run sudo su and cat root.txt
```
In the state of emergency, Jill, Barry and Chris are reaching the helipad and awaiting for the helicopter support.

Suddenly, the Tyrant jump out from nowhere. After a tough fight, brad, throw a rocket launcher on the helipad. Without thinking twice, Jill pick up the launcher and fire at the Tyrant.

The Tyrant shredded into pieces and the Mansion was blowed. The survivor able to escape with the helicopter and prepare for their next fight.

The End

flag: 3c5794a00dc56c35f2bf096571edf3bf

```
