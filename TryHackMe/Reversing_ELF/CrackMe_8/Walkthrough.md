**Tristan Gomez**

# CrackMe 8

### Description:
`Analyze the binary and obtain the flag`


### Walkthrough

Let's start by running the program to see if there is anything we can learn.
```
./crackme8
Usage: ./crackme8 password
```

Hmm, nothing theresadly. Let's run `ltrace` to see if we can catch any low-hanging fruit.
```
ltrace ./crackme8 pass
__libc_start_main(0x804849b, 2, 0xff8ad8e4, 0x80485c0 <unfinished ...>
atoi(0xff8af410, 0xff8ad8e4, 0xff8ad8f0, 0x80485e1)                 = 0
puts("Access denied."Access denied.
)                                              = 15
+++ exited (status 1) +++

```

Annnnndddddd, no dice there. Let's open the binary up in `Ghidra` using its default tool. Navigate into the main method
and expand the decompiler pane to see
```

undefined4 main(int param_1,undefined4 *param_2)

{
  undefined4 uVar1;
  int iVar2;
  
  if (param_1 == 2) {
    iVar2 = atoi((char *)param_2[1]);
    if (iVar2 == -0x35010ff3) {
      puts("Access granted.");
      giveFlag();
      uVar1 = 0;
    }
    else {
      puts("Access denied.");
      uVar1 = 1;
    }
  }
  else {
    printf("Usage: %s password\n",*param_2);
    uVar1 = 1;
  }
  return uVar1;
}
```

This code is very similar to the last challenge. Let's look at the disassembly view. 
```
        08048505 68 83 86        PUSH       s_Access_granted._08048683                       = "Access granted."
                 04 08
        0804850a e8 41 fe        CALL       puts                                             int puts(char * __s)
                 ff ff
        0804850f 83 c4 10        ADD        ESP,0x10
        08048512 e8 0d 00        CALL       giveFlag                                         undefined giveFlag()
                 00 00
        08048517 b8 00 00        MOV        EAX,0x0
                 00 00
```

I'll solve this challenge using the same method as the last one. I am going to run the binary in `gdb`.
I will set a breakpoint to `*main` and run the program. When I hit the breakpoint, I will jump to `0x08048505` which is where 'Access granted' is puched onto 
the stack. The program should then display 'Access granted' then call `giveFlag`. See my gdb output below.


```
Breakpoint 1, 0x0804849b in main ()
(gdb) jump *0x08048505
Continuing at 0x8048505.
Access granted.
flag{at_least_this_cafe_wont_leak_your_credit_card_numbers}

Program received signal SIGSEGV, Segmentation fault.
```

Looks like we found our flag!
