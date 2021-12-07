## Tristan Gomez


# Shark on Wire 2
## Author: Danny
## Description: `We found this packet capture. Recover the flag that was pilfered from the network.`


**NOTE:** I had to use a walkthrough on this problem. Checkout https://zomry1.github.io/shark-on-wire-2/ to see zomry1's walkthrough that I used when I got stuck. 

For this challenge, I received `capture.pcap`. 

My intuition tells me to focus on the TCP and UDP packets in the trace. 

Following the TCP Stream yields nothing(lots of non-printable characters and no 'real' strings.

Looking at the UDP packets, I got really invested in looking at their payloads. This would turnout to be a dead-end. The first 4 packets are malformed and following their udp string givs us `picopicopicopico`. UDP stream is `kfdsalkfsalkico{N0t_a_fLag}`. UDP stream is `BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB`. Investigating the destination ports, I can see that 8990, 8888, 22, 100, 1234. The source port is mostly 5000, with a *few different ones further down the trace*. We'll come back to this in a bit. So trying to think this out, I can see that there appears to be a lot of payloads that are dead-ends, but I can't seem to break away from the idea that the answer is somewhere in a payload. The 

The only suspicious port to me is 22, so I decide to checkout the packets going to it. Each packet has the same payload `aaaaa`. I saw that there were two packets(1104 and 1303) that were from port 5000. Their payloads are `start` and `end` respectively. I couldn't figure out what was going on. The solution to the problem never would've occurred to me. Here is where I hit my dead-end, and enter **zomry1's walkthrough**. Looking at their walkthrough, I learned that the `source ports` for the packets contain the flag. 

```
5000 5112 5105 5099 5111 5067
5084 5070 5123 5112 5049 5076
5076 5102 5051 5114 5051 5100
5095 5100 5097 5116 5097 5095
5118 5049 5097 5095 5115 5116
5051 5103 5048 5125 5000
```
The 3 least significant digits of the source port represent unicode characters that when viewed in order yield the flag. This absolutely blew my mind when I discovered that!

`112 105 99 111 67 84 70 123 112 49 76 76 102 51 114 51 100 95 100 97 116 97 95 118 49 97 95 115 116 51 103 48 125 -> picoCTF{p1LLf3r3d_data_v1a_st3g0}`
