**Tristan Gomez**

# CrackMe 3

### Description:

```
Use basic reverse engineering skills to obtain the flag
```

### Walkthrough

I started this challenge off by running a quick `chmod +x crackme3` so I can run the binary. <br />

I ran the binary and got the following output.
```
./crackme3
Usage: ./crackme3 PASSWORD
```
Looks like it's the sasme format as the last challenge. We need to pass in some password as a command line arguemnt. Let's
run `strings` on the binary and see what we find. 

```
/lib/ld-linux.so.2
__gmon_start__
libc.so.6
_IO_stdin_used
puts
strlen
malloc
stderr
fwrite
fprintf
strcmp
__libc_start_main
GLIBC_2.0
PTRh
iD$$
D$,;D$ 
UWVS
[^_]
Usage: %s PASSWORD
malloc failed
ZjByX3kwdXJfNWVjMG5kX2xlNTVvbl91bmJhc2U2NF80bGxfN2gzXzdoMW5nNQ==
Correct password!
Come on, even my aunt Mildred got this one!
ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/
;*2$"8
GCC: (Ubuntu/Linaro 4.6.3-1ubuntu5) 4.6.3
.shstrtab
.interp
.note.ABI-tag
.note.gnu.build-id
.gnu.hash
.dynsym
.dynstr
.gnu.version
.gnu.version_r
.rel.dyn
.rel.plt
.init
.text
.fini
.rodata
.eh_frame_hdr
.eh_frame
.ctors
.dtors
.jcr
.dynamic
.got
.got.plt
.data
.bss
.comment

```

Two strings jump out atme from the output. 

* `ZjByX3kwdXJfNWVjMG5kX2xlNTVvbl91bmJhc2U2NF80bGxfN2gzXzdoMW5nNQ==`

* `ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/`

The second string is just A-Z, a-z, 0-9, +, /. This is a telltale sign of base64 encoding. This string is the
generic alphabet used in base64. The first string has two `=` signs on the end of the string. This is also a telltale sign
of base64 encoding. I will put the first string into an online base64 decoding tool, which should work fine for this case.
If the alphabet was any different,w e would have to manually attempt to decode the first string. <br />

When we decode the string, we get the flag!

`f0r_y0ur_5ec0nd_le55on_unbase64_4ll_7h3_7h1ng5`
