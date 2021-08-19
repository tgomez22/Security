This is the second crackme file - Unlike the first file, this will involve examining registers, how and where values are compared

```

undefined8 main(void)

{
  long in_FS_OFFSET;
  int local_14;
  long local_10;
  
  local_10 = *(long *)(in_FS_OFFSET + 0x28);
  puts("enter your password");
  __isoc99_scanf(&DAT_00100838,&local_14);
  if (local_14 == 0x137c) {
    puts("password is valid");
  }
  else {
    puts("password is incorrect");
  }
  if (local_10 != *(long *)(in_FS_OFFSET + 0x28)) {
                    /* WARNING: Subroutine does not return */
    __stack_chk_fail();
  }
  return 0;
}

```

```
                             DAT_00100838                                    XREF[1]:     main:00100744(*)  
        00100838 25              ??         25h    %
        00100839 64              ??         64h    d
        0010083a 00              ??         00h

```

`0x137c` == `4988`
