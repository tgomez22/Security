**Tristan Gomez**

#  Crack Me 2


### Description
```
Find the super-secret password! and use it to obtain the flag
```

### Walkthrough

Again, `chmod +x crackme2` so the binary can run.

I ran the binary and got 
```
./crackme2
Usage: ./crackme2 password
```

So we need to find the password. Hmm, I'm going to run `strings` to see if there is any low-hanging fruit.
```
/lib/ld-linux.so.2
libc.so.6
_IO_stdin_used
puts
printf
memset
strcmp
__libc_start_main
/usr/local/lib:$ORIGIN
__gmon_start__
GLIBC_2.0                                                                                                          
PTRh                                                                                                               
j3jA                                                                                                               
[^_]                                                                                                               
UWVS                                                                                                               
t$,U                                                                                                               
[^_]                                                                                                               
Usage: %s password                                                                                                 
super_secret_password                                                                                              
Access denied.                                                                                                     
Access granted.                                                                                                    
;*2$"(                                                                                                             
GCC: (Ubuntu 5.4.0-6ubuntu1~16.04.9) 5.4.0 20160609                                                                
crtstuff.c                                                                                                         
__JCR_LIST__                                                                                                       
deregister_tm_clones                                                                                               
__do_global_dtors_aux                                                                                              
completed.7209                                                                                                     
__do_global_dtors_aux_fini_array_entry                                                                             
frame_dummy                                                                                                        
__frame_dummy_init_array_entry
conditional1.c
giveFlag
__FRAME_END__
__JCR_END__
__init_array_end
_DYNAMIC
__init_array_start
__GNU_EH_FRAME_HDR
_GLOBAL_OFFSET_TABLE_
__libc_csu_fini
strcmp@@GLIBC_2.0
_ITM_deregisterTMCloneTable
__x86.get_pc_thunk.bx
printf@@GLIBC_2.0
_edata
__data_start
puts@@GLIBC_2.0
__gmon_start__
__dso_handle
_IO_stdin_used
__libc_start_main@@GLIBC_2.0
__libc_csu_init
memset@@GLIBC_2.0
_fp_hw
__bss_start
main
_Jv_RegisterClasses
__TMC_END__
_ITM_registerTMCloneTable
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
.rel.dyn
.rel.plt
.init
.plt.got
.text
.fini
.rodata
.eh_frame_hdr
.eh_frame
.init_array
.fini_array
.jcr
.dynamic
.got.plt
.data
.bss
.comment
```


Well we found a suspicious string, `super_secret_password`. I'm going to pass this in as the `password` argument to the bninary.

```
./crackme2 super_secret_password
Access granted.
flag{if_i_submit_this_flag_then_i_will_get_points}
```

Bam, flag found. Onto the next one!

### Question 1: What is the super secret password?
* `super_secret_password`

### Question 2: What is the flag?
* `flag{if_i_submit_this_flag_then_i_will_get_points}`

