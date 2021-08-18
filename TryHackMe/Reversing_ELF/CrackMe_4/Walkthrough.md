**Tristan Gomez**

# CrackMe 4

### Description: 

```
Analyze and find the password for the binary?
```

### Walkthrough
### Method 1: ltrace

There are two ways that I solved this challenge, so I will go through both methods. The first method is using `ltrace`.
If we run `./crackme4` we get 
```
Usage : ./crackme4 password
This time the string is hidden and we used strcmp
```

Again, we need to pass in some sort of password to get the flag, but what might this password be? Let's use `ltrace` to try to find it. 

`ltrace ./crackme4 pass`

We get the following output from our `ltrace`

```
__libc_start_main(0x400716, 2, 0x7ffff22f4bb8, 0x400760 <unfinished ...>
strcmp("my_m0r3_secur3_pwd", "pass")                                  = -3
printf("password "%s" not OK\n", "pass"password "pass" not OK
)                              = 23
+++ exited (status 0) +++
my_m0r3_secur3_pwd"

```

NOTE: `ltrace` won't work on every problem we encounter, but its good to atleast check. According to the man pages for `ltrace`, it  will "`intercept and record
dynamic library calls ... [and] shows parameters of invoked functions`". This functionality is what allows us to 'catch' the password out in the open. <br />

It looks like `my_m0r3_secur3_pwd` is the password we need. Input it back into the program and ge the flag! Let's take a detour
to the second method that can be used to solve the challenge. 

### Method 2: Ghidra & GDB

The entire level can be solved using GDB, but to make it easier to search through the program I am using `Ghidra` to disassemble the program. `objdump -d` will 
disassemble the program and give you the needed info to solve the level. <br />


Using the default tool in `Ghidra`, I found the `main` function in the program and expanded the `decompiler` pane.
```
undefined8 main(int param_1,undefined8 *param_2)

{
  if (param_1 == 2) {
    compare_pwd(param_2[1]);
  }
  else {
    printf("Usage : %s password\nThis time the string is hidden and we used strcmp\n",*param_2);
  }
  return 0;
}
```

This method is taking an integer as its first parameter, and if the parameter has a value of `2` then `compare_pwd` is called. Let's see what `compare_pwd` is doing.

```

void compare_pwd(char *param_1)

{
  int iVar1;
  long in_FS_OFFSET;
  undefined8 local_28;
  undefined8 local_20;
  undefined2 local_18;
  undefined local_16;
  long local_10;
  
  local_10 = *(long *)(in_FS_OFFSET + 0x28);
  local_28 = 0x7b175614497b5d49;
  local_20 = 0x547b175651474157;
  local_18 = 0x4053;
  local_16 = 0;
  get_pwd(&local_28);
  iVar1 = strcmp((char *)&local_28,param_1);
  if (iVar1 == 0) {
    puts("password OK");
  }
  else {
    printf("password \"%s\" not OK\n",param_1);
  }
  if (local_10 != *(long *)(in_FS_OFFSET + 0x28)) {
                    /* WARNING: Subroutine does not return */
    __stack_chk_fail();
  }
  return;
}
```
We found a `strcmp`! This will be my target. There is a call to `get_pwd` and the address of `local_28` is being passed in, so `local_28` is a pointer getting the password
assigned to it. Then the next line is the `strcmp` where the newly assigned `local_28` variable is being compared against the string passed into `compare_pwd`. 
Looking at the assebly code for the same method, we can see that the arguments to `strcmp` are being loaded into `RDX` and `RAX`.


```
        004006c2 e8 66 ff        CALL       get_pwd                                          undefined get_pwd()
                 ff ff
        004006c7 48 8b 55 d8     MOV        RDX,qword ptr [RBP + local_30]
        004006cb 48 8d 45 e0     LEA        RAX=>local_28,[RBP + -0x20]
        004006cf 48 89 d6        MOV        RSI,RDX
        004006d2 48 89 c7        MOV        RDI,RAX
        004006d5 e8 46 fe        CALL       strcmp                                           int strcmp(char * __s1, char * _
                 ff ff
        004006da 85 c0           TEST       EAX,EAX

```
We have enough information to go off of at this point to solve the level. My plan is to run `GDB` passing in `2` as my 
command-line argument. This will cause the `if` statement in `main` to evaluate to `true` which will cause `compare_pwd` 
to be called. We can set a breakpoint at `0x004006d2`which is the last instruction before the call to `strcmp`. Our argument 
and the password *should* be in `RDX` and `RAX`. Let's try it out!

```
gdb --args ./crackme4 2
`br *0x004006d2`
run
x/s $rdx -> '2'
x/s $rax -> 'my_m0r3_secur3_pwd'
```
Just as I thought, the real password is exposed. Let's pass it in as our argument.

```
./crackme4 my_m0r3_secur3_pwd
password OK
```

Well that was anti-climactic, but we solved it. Onto the next level!
