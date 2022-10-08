# The Basics:

* Java framework that provides functionality to assist with web application penetration testing.
* Also useful for assessing mobile applications
* Also useful for testing APIs.

## Core functionality:
* Captures and manipulates all traffic between an attacker and a webserver. 
* After capturing requests, can send them to other parts of the Burp Suite.

### Burp Suite Professional
* Automated vulnerability scanner
* A non-rate limited fuzzer/bruteforcer
* Saving projects for future use; report generation
* Built-in API that allows integration with other tools
* Unrestricted access to add new extensions for greater functionality
* Burp Suite Collaborator (a unique request catcher self-hosted or running on a Portswigger owned server)

### Burp Suite Enterprise
* Used for continuous scanning
* Automated scanner that periodically scans webapps for vulns.
* Sits on a server and constantly scans target web apps.

## Burp Community
* Proxy: intercepts and modifies requests/responses when interacting with webapps.
* Repeater: captures, modifies, then resends the same request numerous times.
Good for crafting a payload through trial and error (i.e. SQLi) or 
when testing the functionality of an endpoint for flaws.
* Intruder: Harshly rate-limited. Sprays an endpoint with requests. Used for bruteforce attacks or to fuzz endpoints.
* Decoder: Transforms data - either in terms of decoding captured information, or encoding a payload
prior to sending it to the target.
* Comparer: compares two pieces of data at wither word or byte level. 
* Sequencer: Assesses the randomness of tokens like session cookie values or othr supposedly random generated data.
If the algorithm is not generating secure random values then this could find it.
* Extender: can add extensions written in Java, Python, or Ruby.

## Options
* Global settings can be found in the *User options* tab along the top menu bar. 
* Project-specific settings can be found in the *project options* tab

### User options tab
* Connections: allows us to control how Burp makes connections to targets. For example, 
we can set a proxy for Burp to connect through. Useful if we want to use Burp through a network pivot.
* TLS: allows us to enable and disable various TLS options, as well as, giving us a place to upload client
certifications should a web app need one to use for connections.
* Display: change how Burp looks( font, scale, dark mode, changing optons to or the rendering engine in *Repeater*.

### Project options tab
