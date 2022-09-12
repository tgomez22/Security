Techniques for getting the first set of AD creds
1) OSINT
* Find users on public forums (Stack Overflow) who discolse sensitive info in their questions.
* Find Devs/dev scripts with hardcoded creds.
* Find creds disclosed in past breeches. (check `DeHashed` and `HaveIBeenPwned`)

2) Phishing


## NTLM and NetNTLM
```
New Technology LAN Manager (NTLM) is the suite of security protocols used to authenticate users' identities in AD.
NTLM can be used for authentication by using a challenge-response-based scheme called NetNTLM.
This authentication mechanism is heavily used by the services on a network.
However, services that use NetNTLM can also be exposed to the internet.
The following are some of the popular examples:

Internally-hosted Exchange (Mail) servers that expose an Outlook Web App (OWA) login portal.
Remote Desktop Protocol (RDP) service of a server being exposed to the internet.
Exposed VPN endpoints that were integrated with AD.
Web applications that are internet-facing and make use of NetNTLM.
NetNTLM, also often referred to as Windows Authentication or just NTLM Authentication,
allows the application to play the role of a middle man between the client and AD.
All authentication material is forwarded to a Domain Controller in the form of a challenge,
and if completed successfully, the application will authenticate the user.
```

We can attempt to brute-force login, but account lockout is usually enabeled. This means we need to attempt password spraying.
We will try `one` password and attempt to authenticate it with all the different usernames we have found. 
`Hydra` can be used here but I will use the provided Python script instead.

Using the script, I found 4 valid logins
```
hollie.powell
louise.talbot
gordon.stevens
georgina.edwards
```

## LDAP Bind Credentials
(Lightweight Directory Accedd Protocol)
* directly verifies the user's credentials
* If you can gain a foothold on the correct host (Gitlab, Jenkins, Custom-developed web app, printers, vpns, etc.)
then you can just read the configuration files to recover AD creds. They are often stored in plain text config files since
the security model relies on keeping the location and storage configuration file secure rather than its contents.

### LDAP Pass-back Attacks
* common attack against network devices (like printers) when you have gained initial access to an internal network.
* usually default creds still on these devices.
* We can't directly extract the LDAP creds since the password is usually hidden, but we can alter the LDAP config.

Change the hostname or IP of the LDAP sercer to our IP then test the config. This will force the attempt LDAP auth to the rogue device. 
We can then intercept this auth attempt to recover the LDAP creds.

`Default LDAP` port is `389`

on `http://printer.za.tryhackme.com/settings.aspx` we find the username: `svcLDAP`

Can't use nc to grab password because it is too secure. We have to host a LDAP server that is 
insecurely configured to grab creds.

