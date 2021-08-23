**Tristan Gomez**

# asm1

### Author: SANJAY C

### Description:
```
What does asm1(0x2e0) return?
Submit the flag as a hexadecimal value (starting with '0x'). 
NOTE: Your submission for this question will NOT be in the normal flag format. Source
```

### Hints:
* assembly conditions


### Walkthrough

Our passed in parameter to this method is `0x2e0 == 736`.

`<asm1+0>` and `<asm1+1>` are steps we don't need to worry about in this challenmge because they are operations
that setup the stack for the current method. Our first real step is at `<asm1+3>` where we have a comparison operation. 
Our parameter (`0x2e0`) is being tested against `0x37b`. The next instruction is a `jg` or jump greater-than. This jump would be taken if the passed in parameter 
was larger than `0x3fb`, but our passed in argument is smaller in value than `0x3fb`, so we 'fall through' this instruction to `<asm1+12>`. <br />

```
asm1:
	<+0>:	push   ebp
	<+1>:	mov    ebp,esp
	<+3>:	cmp    DWORD PTR [ebp+0x8],0x3fb
	<+10>:	jg     0x512 <asm1+37>
	<+12>:	cmp    DWORD PTR [ebp+0x8],0x280
```

At this step, `0x280` is compared against `0x2e0`. The next instruction is a `jne` or jump not-equal. Since both operands are not equal this jump is taken, so we move to `<asm1+29>`. <br />
```
<+19>:	jne    0x50a <asm1+29>
```

At this step is a `mov` instruction where the passed in parameter is moved into register `eax`. The next instruction is a `sub` or subtraction instruction. Here I will subtract `0xa` from `0x2e0` which results in `0x2d6` stored in `eax`. The next instruction is a non-conditional jump to `<asm+60>`. `ebp` is then popped off the stack and the next instruction is a `ret` or return. The return value of asm1 is stored in `eax` so out flag is...

```
        <+29>:	mov    eax,DWORD PTR [ebp+0x8]
	<+32>:	sub    eax,0xa
	<+35>:	jmp    0x529 <asm1+60>
	...
	<+60>:	pop    ebp
	<+61>:	ret    
```
**FLAG: 0x2d6**
