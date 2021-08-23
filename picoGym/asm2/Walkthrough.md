**Tristan Gomez**


# asm2

### Author: Sanjay C

### Description:

```
What does asm2(0x4,0x2d) return? 
Submit the flag as a hexadecimal value (starting with '0x'). 
NOTE: Your submission for this question will NOT be in the normal flag format.
```

### Walkthrough

* arg1 = `0x4`
* arg2 = `0x2d`


The first three instructions are setting up the stack for this function.
```
asm2:
	<+0>:	push   ebp
	<+1>:	mov    ebp,esp
	<+3>:	sub    esp,0x10
```

Our first real step is at `<asm2+6>`. Here the value for the second argument (0x2d) is moved into `eax`.
Then the value stored in `eax` is moved into a local variable in the function (ebp-0x4). 
```
  <+6>:	mov    eax,DWORD PTR [ebp+0xc]
	<+9>:	mov    DWORD PTR [ebp-0x4],eax
```


The next step is at <asm+12> where the first argument (0x4) is being moved into `eax`. At <asm+15> the value in
`eax` is then moved into a second local variable for the fuction (ebp-0x8). Then we take an unconditional jump to
<asm2+31>.

```
  <+12>:	mov    eax,DWORD PTR [ebp+0x8]
	<+15>:	mov    DWORD PTR [ebp-0x8],eax
	<+18>:	jmp    0x50c <asm2+31>
```

Here is the core of the challenge. At `<asm2+31>` is a compare instruction. 0x5fa1 is being comnpared atainst the contents of
ebp-0x8 which contains the value 0x4. Since 0x4 is indeed less than 0x5fa1, we take the jle instruction to `<asm2+20>`. At `<asm2+20>`
0x1 is being added to the contents of ebp-0x4, which in the first iteration contains 0x2d. The next instruction is also an add instruction.
`<asm2+24>` adds 0xd1 to the contents of ebp-0x8 which currently holds 0x4. <br />

To solve this, I converted the hex values to their base 10 values to make it easier for my simple human mind.

* 0xd1 = 209
* 0x4 = 4
* 0x2d = 45
* 0x5fa1 = 24481

209 goes into 24481 just over 117 times, and 209 * 117 = 24453. 24453 + 4 = 24457 which is still smaller than 24481, 
so we have to go through the loop one more time for a total of 118 iterations.  
```
	
	<+20>:	add    DWORD PTR [ebp-0x4],0x1
	<+24>:	add    DWORD PTR [ebp-0x8],0xd1
	<+31>:	cmp    DWORD PTR [ebp-0x8],0x5fa1
	<+38>:	jle    0x501 <asm2+20>
	<+40>:	mov    eax,DWORD PTR [ebp-0x4]
	<+43>:	leave  
	<+44>:	ret    

```

45 + 118 = 163 = `0xA3` is then moved into `eax` after we fall through the `jle` instruction at `<asm2+38>`. Our answer is stored in`eax` as the method returns.

**Flag: 0xA3**
