```
WARNING | 2021-08-16 14:35:23,785 | angr.storage.memory_mixins.default_filler_mixin | The program is accessing memory or registers with an unspecified value. This could indicate unwanted behavior.                                  
WARNING | 2021-08-16 14:35:23,786 | angr.storage.memory_mixins.default_filler_mixin | angr will cope with this by generating an unconstrained symbolic variable and continuing. You can resolve this by:                              
WARNING | 2021-08-16 14:35:23,786 | angr.storage.memory_mixins.default_filler_mixin | 1) setting a value to the initial state                                                                                                         
WARNING | 2021-08-16 14:35:23,786 | angr.storage.memory_mixins.default_filler_mixin | 2) adding the state option ZERO_FILL_UNCONSTRAINED_{MEMORY,REGISTERS}, to make unknown regions hold null                                        
WARNING | 2021-08-16 14:35:23,786 | angr.storage.memory_mixins.default_filler_mixin | 3) adding the state option SYMBOL_FILL_UNCONSTRAINED_{MEMORY,REGISTERS}, to suppress these messages.                                            
WARNING | 2021-08-16 14:35:23,786 | angr.storage.memory_mixins.default_filler_mixin | Filling register edi with 4 unconstrained bytes referenced from 0x804d291 (__libc_csu_init+0x1 in 02_angr_find_condition (0x804d291))           
WARNING | 2021-08-16 14:35:23,787 | angr.storage.memory_mixins.default_filler_mixin | Filling register ebx with 4 unconstrained bytes referenced from 0x804d293 (__libc_csu_init+0x3 in 02_angr_find_condition (0x804d293))           
WARNING | 2021-08-16 14:35:24,699 | angr.storage.memory_mixins.default_filler_mixin | Filling memory at 0x7ffeff2d with 11 unconstrained bytes referenced from 0x8188d10 (strcmp+0x0 in libc.so.6 (0x88d10))                          
WARNING | 2021-08-16 14:35:24,699 | angr.storage.memory_mixins.default_filler_mixin | Filling memory at 0x7ffeff50 with 4 unconstrained bytes referenced from 0x8188d10 (strcmp+0x0 in libc.so.6 (0x88d10))                           
b'OYLNXKYO'
```
