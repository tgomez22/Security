**Tristan Gomez**

# Angr: 00_angr_avoid

This level bagan with a code scaffold that is identical to solve00.py except for the scaffold was missing `3` inputs. It was missing the values for `path_to_binary, print_good_address, and will_not_succeed_address`.

path_to_binary = '01_angr_avoid' is just a given value since the binary lives in the same directory as solve01.py. <br />

Running `objdump -d 01_angr_avoid` gives us the assembly for the binary. Looking through it we see two very interesting methods.

  ```
  080485a8 <avoid_me>:
 80485a8:       55                      push   %ebp
 80485a9:       89 e5                   mov    %esp,%ebp
 80485ab:       c6 05 3d 60 0d 08 00    movb   $0x0,0x80d603d
 80485b2:       90                      nop
 80485b3:       5d                      pop    %ebp
 80485b4:       c3                      ret    

080485b5 <maybe_good>:
 80485b5:       55                      push   %ebp
 80485b6:       89 e5                   mov    %esp,%ebp
 80485b8:       83 ec 08                sub    $0x8,%esp
 80485bb:       0f b6 05 3d 60 0d 08    movzbl 0x80d603d,%eax
 80485c2:       84 c0                   test   %al,%al
 80485c4:       74 29                   je     80485ef <maybe_good+0x3a>
 80485c6:       83 ec 04                sub    $0x4,%esp
 80485c9:       6a 08                   push   $0x8
 80485cb:       ff 75 0c                pushl  0xc(%ebp)
 80485ce:       ff 75 08                pushl  0x8(%ebp)
 80485d1:       e8 3a fe ff ff          call   8048410 <strncmp@plt>
 80485d6:       83 c4 10                add    $0x10,%esp
 80485d9:       85 c0                   test   %eax,%eax
 80485db:       75 12                   jne    80485ef <maybe_good+0x3a>
 80485dd:       83 ec 0c                sub    $0xc,%esp
 80485e0:       68 1e 46 0d 08          push   $0x80d461e
 80485e5:       e8 e6 fd ff ff          call   80483d0 <puts@plt>
 80485ea:       83 c4 10                add    $0x10,%esp
 80485ed:       eb 10                   jmp    80485ff <maybe_good+0x4a>
 80485ef:       83 ec 0c                sub    $0xc,%esp
 80485f2:       68 13 46 0d 08          push   $0x80d4613
 80485f7:       e8 d4 fd ff ff          call   80483d0 <puts@plt>
 80485fc:       83 c4 10                add    $0x10,%esp
 80485ff:       90                      nop
 8048600:       c9                      leave  
 8048601:       c3                      ret    

  ```

I'm going to set the value of `will_not_succeed_address` to `0x80485a8`, which is the starting address for `avoid_me`. Looking into `maybe_good` is a bit more complicated. I originally set `print_good_address` to the starting address but this resulted in incorrect flags. At `0x80485d1` is a call to `strncmp` so I'm guessing the program's input is passed through this method. Then at `0x80485db` is a `jne` instruction which indicates to me that the jump occurs when the user input doesn't match the correct flag. Following what should be the `success` path brings us to a `puts` method call at `0x80485e5`. I set the value of `print_good_address` to this address and ran the program which output 

```
WARNING | 2021-08-16 14:18:41,704 | angr.storage.memory_mixins.default_filler_mixin | The program is accessing memory or registers with an unspecified value. This could indicate unwanted behavior.                                  
WARNING | 2021-08-16 14:18:41,704 | angr.storage.memory_mixins.default_filler_mixin | angr will cope with this by generating an unconstrained symbolic variable and continuing. You can resolve this by:                              
WARNING | 2021-08-16 14:18:41,704 | angr.storage.memory_mixins.default_filler_mixin | 1) setting a value to the initial state                                                                                                         
WARNING | 2021-08-16 14:18:41,704 | angr.storage.memory_mixins.default_filler_mixin | 2) adding the state option ZERO_FILL_UNCONSTRAINED_{MEMORY,REGISTERS}, to make unknown regions hold null                                        
WARNING | 2021-08-16 14:18:41,704 | angr.storage.memory_mixins.default_filler_mixin | 3) adding the state option SYMBOL_FILL_UNCONSTRAINED_{MEMORY,REGISTERS}, to suppress these messages.                                            
WARNING | 2021-08-16 14:18:41,705 | angr.storage.memory_mixins.default_filler_mixin | Filling register edi with 4 unconstrained bytes referenced from 0x80d4591 (__libc_csu_init+0x1 in 01_angr_avoid (0x80d4591))                    
WARNING | 2021-08-16 14:18:41,706 | angr.storage.memory_mixins.default_filler_mixin | Filling register ebx with 4 unconstrained bytes referenced from 0x80d4593 (__libc_csu_init+0x3 in 01_angr_avoid (0x80d4593))                    
WARNING | 2021-08-16 14:18:43,721 | angr.storage.memory_mixins.default_filler_mixin | Filling memory at 0x7ffeff3d with 11 unconstrained bytes referenced from 0x8189300 (strncmp+0x0 in libc.so.6 (0x89300))                         
WARNING | 2021-08-16 14:18:43,721 | angr.storage.memory_mixins.default_filler_mixin | Filling memory at 0x7ffeff60 with 4 unconstrained bytes referenced from 0x8189300 (strncmp+0x0 in libc.so.6 (0x89300))                          
b'JODIDXWF'
```

In a byte literal is our flag!

**Flag: JODIDXWF**
