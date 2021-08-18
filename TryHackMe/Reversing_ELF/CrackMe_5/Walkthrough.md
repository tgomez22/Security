**Tristan Gomez**

# CrackMe 5

### Decription:

What will be the input of the file to get output `Good game` ?

I started by just running the program to see what kind of information that I could gather.

```
./crackme5
Enter your input:
yeet
Always dig deeper
```

Doesn't appear that there is anything to go off of in the output.

### Method 1: ltrace

I ran the program again but with ltrace. Using the same input, we can see `yeet` being compared
against the real password in the call to `strncmp`. Copy the password and use it as the program's input to solve the challenge.

```
ltrace ./crackme5
__libc_start_main(0x400773, 1, 0x7ffcb6e88118, 0x4008d0 <unfinished ...>
puts("Enter your input:"Enter your input:
)                                             = 18
__isoc99_scanf(0x400966, 0x7ffcb6e87fd0, 0, 0x7fcdab356f33yeet
)           = 1
strlen("yeet")                                                        = 4
strlen("yeet")                                                        = 4
strlen("yeet")                                                        = 4
strlen("yeet")                                                        = 4
strlen("yeet")                                                        = 4
strncmp("yeet", "OfdlDSA|3tXb32~X3tX@sX`4tXtz", 28)                   = 42
puts("Always dig deeper"Always dig deeper
)                                             = 18
+++ exited (status 0) +++
```


### Method 2: Ghidra & GDB

I opened the binary in `Ghidra` and looked at the `main` method in the decompiler pane.
```

undefined8 main(void)

{
  int iVar1;
  long in_FS_OFFSET;
  undefined local_58 [32];
  undefined local_38;
  undefined local_37;
  undefined local_36;
  undefined local_35;
  undefined local_34;
  undefined local_33;
  undefined local_32;
  undefined local_31;
  undefined local_30;
  undefined local_2f;
  undefined local_2e;
  undefined local_2d;
  undefined local_2c;
  undefined local_2b;
  undefined local_2a;
  undefined local_29;
  undefined local_28;
  undefined local_27;
  undefined local_26;
  undefined local_25;
  undefined local_24;
  undefined local_23;
  undefined local_22;
  undefined local_21;
  undefined local_20;
  undefined local_1f;
  undefined local_1e;
  undefined local_1d;
  long local_10;
  
  local_10 = *(long *)(in_FS_OFFSET + 0x28);
  local_38 = 0x4f;
  local_37 = 0x66;
  local_36 = 100;
  local_35 = 0x6c;
  local_34 = 0x44;
  local_33 = 0x53;
  local_32 = 0x41;
  local_31 = 0x7c;
  local_30 = 0x33;
  local_2f = 0x74;
  local_2e = 0x58;
  local_2d = 0x62;
  local_2c = 0x33;
  local_2b = 0x32;
  local_2a = 0x7e;
  local_29 = 0x58;
  local_28 = 0x33;
  local_27 = 0x74;
  local_26 = 0x58;
  local_25 = 0x40;
  local_24 = 0x73;
  local_23 = 0x58;
  local_22 = 0x60;
  local_21 = 0x34;
  local_20 = 0x74;
  local_1f = 0x58;
  local_1e = 0x74;
  local_1d = 0x7a;
  puts("Enter your input:");
  __isoc99_scanf(&DAT_00400966,local_58);
  iVar1 = strcmp_(local_58,&local_38,&local_38);
  if (iVar1 == 0) {
    puts("Good game");
  }
  else {
    puts("Always dig deeper");
  }
  if (local_10 != *(long *)(in_FS_OFFSET + 0x28)) {
                    /* WARNING: Subroutine does not return */
    __stack_chk_fail();
  }
  return 0;
}
```

There is a call to `scanf` to get our input and then there is immediately a call to `strcmp`. Let's look at the same code but in the disassembled view.

```
        0040081c e8 9f fd        CALL       __isoc99_scanf                                   undefined __isoc99_scanf()
                 ff ff
        00400821 48 8d 55 d0     LEA        RDX=>local_38,[RBP + -0x30]
        00400825 48 8d 45 b0     LEA        RAX=>local_58,[RBP + -0x50]
        00400829 48 89 d6        MOV        RSI,RDX
        0040082c 48 89 c7        MOV        RDI,RAX
        0040082f e8 a2 fe        CALL       strcmp_                                          undefined strcmp_()
                 ff ff

```

Just before the call to `strcmp` we can see `RDX` and `RAX` having some values loaded into them. These registers will hold the arguments to `strcmp`. If we set a breakpoint in `GDB`
to `0x0040082c` then we can examine the contents of `RDX` and `RAX` which should reveal the password!

```
(gdb) x/s $rax
0x7fffffffdf80: "yeet"
(gdb) x/s $rdx
0x7fffffffdfa0: "OfdlDSA|3tXb32~X3tX@sX`4tXtz"
(gdb) 
``` 

Now plug in the password to solve the challenge!

```
./crackme5                                                                                                     
Enter your input:
OfdlDSA|3tXb32~X3tX@sX`4tXtz
Good game
```
