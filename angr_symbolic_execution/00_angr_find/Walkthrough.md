**Tristan Gomez**

# Angr: 00_angr_find

### Walkthrough

The `solve00.py` file intially was missing values for two variables, `path_to_binary` and `print_good_address`. <br />

The `path_to_binary`'s value is just `00_angr_find` because the binary lives in the same directory as solve00.py. To get the value for `print_good_address`, I ran `objdump -d 00_angr_find > 00.txt` so that I could more easily see the assembly code of the binary. 

Scrolling down the `00.txt` file, I am looking for anything that jumps out to me. One of the first things I see is 

```
080485c7 <main>:
```

Scrolling down, I find what I am looking for. 

```
 8048607:	e8 24 fe ff ff       	call   8048430 <__isoc99_scanf@plt>
 ...
 8048657:	e8 74 fd ff ff       	call   80483d0 <strcmp@plt>
 804865c:	83 c4 10             	add    $0x10,%esp
 804865f:	85 c0                	test   %eax,%eax
 8048661:	74 12                	je     8048675 <main+0xae>
 8048663:	83 ec 0c             	sub    $0xc,%esp
 8048666:	68 33 87 04 08       	push   $0x8048733
 804866b:	e8 90 fd ff ff       	call   8048400 <puts@plt>
 8048670:	83 c4 10             	add    $0x10,%esp
 8048673:	eb 10                	jmp    8048685 <main+0xbe>
 8048675:	83 ec 0c             	sub    $0xc,%esp
 8048678:	68 60 87 04 08       	push   $0x8048760
 804867d:	e8 7e fd ff ff       	call   8048400 <puts@plt>
```

At `0x8048607` is a call to scanf, so here is where my flag will be read in by the program. Scrolling down even further, you can see the call to strcmp at `0x8048657`. This is where things get interesting. At `0x804865f` there is a `test` instruction on the return vaue from strcmp. The next instrucion at `0x8048661` is a `je` or 'jump equal' instruction. The `je` instruction, if it evaluates to true, takes us to `0x8048675`. At this point, there is a `sub` instruction followed by a `push` of the contents of `0x0848760` onto the stack immediately followed by a `puts`. This indicates to me that if the correct password/flag is input into `strcmp` then the `je` instruction will evaluate to true, takingme to the `puts` at `0x804867d`. I set `0x80487d` as my value for `print_good_address`. Running `solve00.py` results in

```
WARNING | 2021-08-16 13:51:30,132 | angr.storage.memory_mixins.default_filler_mixin | The program is accessing memory or registers with an unspecified value. This could indicate unwanted behavior.                                  
WARNING | 2021-08-16 13:51:30,132 | angr.storage.memory_mixins.default_filler_mixin | angr will cope with this by generating an unconstrained symbolic variable and continuing. You can resolve this by:                              
WARNING | 2021-08-16 13:51:30,132 | angr.storage.memory_mixins.default_filler_mixin | 1) setting a value to the initial state                                                                                                         
WARNING | 2021-08-16 13:51:30,132 | angr.storage.memory_mixins.default_filler_mixin | 2) adding the state option ZERO_FILL_UNCONSTRAINED_{MEMORY,REGISTERS}, to make unknown regions hold null                                        
WARNING | 2021-08-16 13:51:30,132 | angr.storage.memory_mixins.default_filler_mixin | 3) adding the state option SYMBOL_FILL_UNCONSTRAINED_{MEMORY,REGISTERS}, to suppress these messages.                                            
WARNING | 2021-08-16 13:51:30,132 | angr.storage.memory_mixins.default_filler_mixin | Filling register edi with 4 unconstrained bytes referenced from 0x80486b1 (__libc_csu_init+0x1 in 00_angr_find (0x80486b1))                     
WARNING | 2021-08-16 13:51:30,133 | angr.storage.memory_mixins.default_filler_mixin | Filling register ebx with 4 unconstrained bytes referenced from 0x80486b3 (__libc_csu_init+0x3 in 00_angr_find (0x80486b3))                     
WARNING | 2021-08-16 13:51:31,067 | angr.storage.memory_mixins.default_filler_mixin | Filling memory at 0x7ffeff60 with 4 unconstrained bytes referenced from 0x8188d10 (strcmp+0x0 in libc.so.6 (0x88d10))                           
b'BFDJLHSQ'
```

The last line of the output is `b'BFDJLHSQ` as our final output. the `b` indicates that the string is a bytes literal. Running 00_angr_find and entering `BFDJLHSQ` successfully solves the level.

**FLAG: BFDJLHSQ**
