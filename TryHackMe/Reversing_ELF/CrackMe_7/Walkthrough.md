**Tristan Gomez**

# CrackMe 7

### Description:

`Analyze the binary to get the flag`

### Walkthrough

Let's try running the program to see if there is anything we can gain.

```
./crackme7
Menu:

[1] Say hello
[2] Add numbers
[3] Quit

[>] 1
What is your name? tristan
Hello, tristan!
Menu:

[1] Say hello
[2] Add numbers
[3] Quit

[>] 2 
Enter first number: 1
Enter second number: 1
1 + 1 = 2
Menu:                                                                                                          
                                                                                                               
[1] Say hello                                                                                                  
[2] Add numbers                                                                                                
[3] Quit                                                                                                       
                                                                                                               
[>] 3                                                                                                          
Goodbye!    
```

It appears as though there are a few menu options, but there's nothing that really speaks to me. We can open the binary up in `Ghidra`
using the `deafult tool`. Run the normal suite of analysis tools, then find the `main` function. In the decompiled view of the `main` function is

```
    if (local_14 != 2) {
      if (local_14 == 3) {
        puts("Goodbye!");
      }
      else {
        if (local_14 == 0x7a69) {
          puts("Wow such h4x0r!");
          giveFlag();
        }
        else {
          printf("Unknown choice: %d\n",local_14);
        }
      }
      return 0;
    }
```

We can see that if `local_14` evaluates to `0x7a69` then the `giveFlag` function is called. We*could* put a break point to that if statement and change the value of 
local_14 to the desired value....OR....<br />

We can do this the easy way. I am going to open this program up in `gdb`. Then I will jump the instruction pointer to the address of the call to `puts`. This should
result in `Wow such h4x0r` to be displayed to the screen and have `giveFlag` be called. 

```
        08048674 e8 f7 fc        CALL       puts                                             int puts(char * __s)
                 ff ff
        08048679 83 c4 10        ADD        ESP,0x10
        0804867c e8 25 00        CALL       giveFlag                                         undefined giveFlag()
                 00 00

```

Let's run the program in `gdb`. Set a breakpoint at `*main` and run the program. When you hit the breakpoint, enter `jump *0x08048674` which results in


```
Breakpoint 1, 0x080484bb in main ()
(gdb) jump *0x08048674
Continuing at 0x8048674.
����
    P���
flag{much_reversing_very_ida_wow}
```

And there we go! Level solved!
