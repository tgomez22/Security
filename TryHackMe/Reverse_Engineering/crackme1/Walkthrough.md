This first crackme file will give you an introduction to if statements and basic function calling in assembly.
HINT did you check the strings stored in the executable?
```
/lib64/ld-linux-x86-64.so.2
libc.so.6
__isoc99_scanf
puts
__stack_chk_fail
__cxa_finalize
strcmp
__libc_start_main
GLIBC_2.7
GLIBC_2.4
GLIBC_2.2.5
_ITM_deregisterTMCloneTable
__gmon_start__
_ITM_registerTMCloneTable                                                                                      
=y                                                                                                             
=9                                                                                                             
52                                                                                                             
AWAVI                                                                                                          
AUATL                                                                                                          
[]A\A]A^A_                                                                                                     
enter password                                                                                                 
password is correct                                                                                            
password is incorrect                                                                                          
hax0r                                                                                                          
;*3$"                                                                                                          
GCC: (Ubuntu 7.3.0-27ubuntu1~18.04) 7.3.0                                                                      
crtstuff.c                                                                                                     
deregister_tm_clones                                                                                           
__do_global_dtors_aux                                                                                          
completed.7696                                                                                                 
__do_global_dtors_aux_fini_array_entry                                                                         
frame_dummy                                                                                                    
__frame_dummy_init_array_entry                                                                                 
crackme1.c                                                                                                     
__FRAME_END__
__init_array_end
_DYNAMIC
__init_array_start
__GNU_EH_FRAME_HDR
_GLOBAL_OFFSET_TABLE_
__libc_csu_fini
_ITM_deregisterTMCloneTable
puts@@GLIBC_2.2.5
_edata
__stack_chk_fail@@GLIBC_2.4
__libc_start_main@@GLIBC_2.2.5
__data_start
strcmp@@GLIBC_2.2.5
__gmon_start__
__dso_handle
_IO_stdin_used
__libc_csu_init
__bss_start
main
__isoc99_scanf@@GLIBC_2.7
__TMC_END__
_ITM_registerTMCloneTable
__cxa_finalize@@GLIBC_2.2.5
.symtab
.strtab
.shstrtab
.interp
.note.ABI-tag
.note.gnu.build-id
.gnu.hash
.dynsym
.dynstr
.gnu.version
.gnu.version_r
.rela.dyn
.rela.plt
.init
.plt.got
.text
.fini
.rodata
.eh_frame_hdr
.eh_frame
.init_array
.fini_array
.dynamic
.data
.bss
.comment
```

```
        001007b4 e8 87 fe        CALL       __isoc99_scanf                                   undefined __isoc99_scanf()
                 ff ff
        001007b9 48 8d 55 ec     LEA        RDX=>local_1c,[RBP + -0x14]
        001007bd 48 8d 45 f2     LEA        RAX=>local_16,[RBP + -0xe]
        001007c1 48 89 d6        MOV        RSI,RDX
        001007c4 48 89 c7        MOV        RDI,RAX
        001007c7 e8 64 fe        CALL       strcmp                                           int strcmp(char * __s1, char * _
                 ff ff
        001007cc 89 45 e8        MOV        dword ptr [RBP + local_20],EAX
        001007cf 83 7d e8 00     CMP        dword ptr [RBP + local_20],0x0
        001007d3 75 13           JNZ        LAB_001007e8
        001007d5 48 8d 3d        LEA        RDI,[s_password_is_correct_001008a6]             = "password is correct"
                 ca 00 00 00
        001007dc e8 2f fe        CALL       puts                                             int puts(char * __s)
                 ff ff


```
