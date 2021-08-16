**Tristan Gomez**

# Angr: 00_angr_avoid

This level bagan with a code scaffold that is identical to solve00.py except for the scaffold was missing `3` inputs. It was missing the values for `path_to_binary, print_good_address, and will_not_succeed_address`.

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
