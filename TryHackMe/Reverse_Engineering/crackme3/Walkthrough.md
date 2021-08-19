
```
undefined8 main(void)

{
  long in_FS_OFFSET;
  int local_30;
  undefined2 local_2b;
  undefined local_29;
  char local_28 [24];
  long local_10;
  
  local_10 = *(long *)(in_FS_OFFSET + 0x28);
  local_2b = 0x7a61;
  local_29 = 0x74;
  puts("enter your password");
  __isoc99_scanf(&DAT_00100868,local_28);
  local_30 = 0;
  do {
    if (2 < local_30) {
      puts("password is correct");
LAB_001007ae:
      if (local_10 != *(long *)(in_FS_OFFSET + 0x28)) {
                    /* WARNING: Subroutine does not return */
        __stack_chk_fail();
      }
      return 0;
    }
    if (local_28[local_30] != *(char *)((long)&local_2b + (long)local_30)) {
      puts("password is incorrect");
      goto LAB_001007ae;
    }
    local_30 = local_30 + 1;
  } while( true );
}

```

```
                             DAT_00100868                                    XREF[1]:     main:0010074e(*)  
        00100868 25              ??         25h    %
        00100869 73              ??         73h    s
        0010086a 00              ??         00h

```