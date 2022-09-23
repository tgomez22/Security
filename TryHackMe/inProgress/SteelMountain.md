10.10.25.135

# Enumeration 

## Q1) Who is the employee of the month?
* on the main web page at 10.10.25.135, in the source code is...
```
<img src="/img/BillHarper.png" style="width:200px;height:200px;"/>
```
* Answer: Bill Harper


nmap scan
```
sudo nmap -sV -Pn -p- -T 4 -sS '10.10.25.135'
[sudo] password for gomez22: 
Starting Nmap 7.92 ( https://nmap.org ) at 2022-09-23 12:24 PDT
Nmap scan report for 10.10.25.135
Host is up (0.17s latency).
Not shown: 65520 closed tcp ports (reset)
PORT      STATE SERVICE            VERSION
80/tcp    open  http               Microsoft IIS httpd 8.5
135/tcp   open  msrpc              Microsoft Windows RPC
139/tcp   open  netbios-ssn        Microsoft Windows netbios-ssn
445/tcp   open  microsoft-ds       Microsoft Windows Server 2008 R2 - 2012 microsoft-ds
3389/tcp  open  ssl/ms-wbt-server?
5985/tcp  open  http               Microsoft HTTPAPI httpd 2.0 (SSDP/UPnP)
8080/tcp  open  http               HttpFileServer httpd 2.3
47001/tcp open  http               Microsoft HTTPAPI httpd 2.0 (SSDP/UPnP)
49152/tcp open  msrpc              Microsoft Windows RPC
49153/tcp open  msrpc              Microsoft Windows RPC
49154/tcp open  msrpc              Microsoft Windows RPC
49155/tcp open  msrpc              Microsoft Windows RPC
49163/tcp open  msrpc              Microsoft Windows RPC
49170/tcp open  msrpc              Microsoft Windows RPC
49171/tcp open  msrpc              Microsoft Windows RPC
Service Info: OSs: Windows, Windows Server 2008 R2 - 2012; CPE: cpe:/o:microsoft:windows

```

# Initial Access

## Q2) Scan the machine with nmap. What is the other port running a web server on?
* Answer: 8080

## Q3) Take a look at the other web server. What file server is running?
* Answer: rejetto http file server

## Q4) What is the CVE number to exploit this file server?
* 2014-6287

Using the CVE number, I searched for an exploit in metasploit, which I quickly found. I set up the exploit and ran it, 
getting a shell for user `bill`. On Bill's desktop was the first flag.

C:\Windows\system32>whoami
whoami
nt authority\system


## Q5) Use Metasploit to get an initial shell. What is the user flag?

finished using walkthrough
