# Wireshark doo dooo do doo...
## Tristan Gomez

### Category: Forensics
### Author: Dylan
### Description: Can you find the flag?

The challenge gives us a packet capture: `shark1.pcapng` to sift through.

Looking through the packet capture, I see a lot of TCP packets. I decided to click `Analyze->Follow->TCP Stream` and I searched through each TCP stream until I found...
* `Gur synt vf cvpbPGS{c33xno00_1_f33_h_qrnqorrs}` located in tcp.stream eq 5


Using CyberChef, I tried a few different ciphers until I found that ROT13 decoded the message into...

* `The flag is picoCTF{p33kab00_1_s33_u_deadbeef}`
