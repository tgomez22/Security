# OrionImprovementBusinessLayer.cs

## Attempts at obfuscating code. <br />

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1. [These](../-/blob/3d3497e62428f819c398afb2bd3f119253fd09d2/MaliciousCode/OrionImprovementBusinessLayer.cs#L39-198) claim to be timestamps but they are actually hashes of program names. These hashes are used to identify specific target programs running on the infected machine. They are also used to identify malware reverse engineering tools in order to hamper security analysts. They are highly suspicious because unix time would not have such a wide deviation between them. For example, [this](../-/blob/3d3497e62428f819c398afb2bd3f119253fd09d2/MaliciousCode/OrionImprovementBusinessLayer.cs#L181) according to a unix timestamp converter is "Sun, Oct 18 2511", and [this](../-/blob/3d3497e62428f819c398afb2bd3f119253fd09d2/MaliciousCode/OrionImprovementBusinessLayer.cs#L192) is "Tue, Jun 18 1996". Intuition tells me that both of these "dates" have no valid use if they actually represented dates. <br />

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- If these "timestamps" were legitimate then why would you [compare the hash of a running process' name](../-/blob/3d3497e62428f819c398afb2bd3f119253fd09d2/MaliciousCode/OrionImprovementBusinessLayer.cs#L274) against a timestamp. Under investigation it doesn't make sense, but of course things like this can slip past developers (as evidenced by this malware circulating successfully in the wild). <br /> 

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2. [This](../-/blob/3d3497e62428f819c398afb2bd3f119253fd09d2/MaliciousCode/OrionImprovementBusinessLayer.cs#L423-433) section is *clearly* malicious (Be sure to scroll to the end of the lines). These lines are encoded, specifically using a base64 encoding scheme with a custom alphabet. The strings being unzipped have the tell tale sign of being base64 encoded, which is the "=" at the end of the string. This character is used as padding if the binary representation of the string isn't cleanly divisible by 6 and 8. <br />
    
## This file "does too much"
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Notice all of the [library inclusions](../-/blob/3d3497e62428f819c398afb2bd3f119253fd09d2/MaliciousCode/OrionImprovementBusinessLayer.cs#L7-31). It is suspicious that this file is capable of so much functionality. These libraries on there own, or included in small groups is not suspicious. All of this functionality combined is a strong indication of malicious activity, and without even looking at the code a malware analyst can infer what the malicious code will do upon execution. 

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-The "System.IO.Pipes" and "System.Threading" inclusions are very common in malware samples because these libraries protect the malware from itself by preventing multiple instances of the infected code from executing. Specifically, the "System.IO.Pipes" is commonly used to prevent reverse engineering at an assembly code level. <br />

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-The "System.Net.NetworkInformation", "System.Net.Sockets", "System.Net", and "System.Net.Security" inclusions indicate that the malware is capable of data exfiltration, as well as, is capable of receiving instructions or more code from a command and control server. <br />

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-The "System.Security.AccessControl", "System.Security.Principal", and "System.Security.Cryptography" inclusions let me know that there will be obfuscation/encryption in some form. It could be encrypting data before exfiltrating it or it could be obscuring what the code is doing to prevent reverse engineering of the malware. They also tell me that this malware is "looking" for users and credentials. This could be with the intention of identifying specific targets. As identified by FireEye, this malware targeted specific machines belonging to specific organizations. Also identified by FireEye, this malware looks through user profiles on a machine in order to find accounts with the desired amount of access to privileged information/systems.

## Interesting Malicious activity

[This](../-/blob/3d3497e62428f819c398afb2bd3f119253fd09d2/MaliciousCode/OrionImprovementBusinessLayer.cs#274) line is the first line of the bulk of the malicious code. The linked "if-statement" gets the hash of the currently running process. Referencing FireEye's analysis posted to their blog on December 13th, 2020, "This hash matches a process named 'solarwinds.businesslayerhost'". If this program is *not* running then the method returns. The second half of the "if-statement" gets the last write time of the file. If the last write time was 12-14 days before the current time then the program will execute. The exact time for execution is a randomly chosen interval between 12-14 days. This program runs in the background, checking consistently if the time interval is up. When the chosen interval time has elapsed then the program will execute. This functionaliy allows the malware to hide in plain sight. 


## Obfuscation



**1. [Line 477](https://gitlab.cecs.pdx.edu/gomez22/solarwinds-code-analysis/-/blob/master/MaliciousCode/OrionImprovementBusinessLayer.cs): Obfuscation through function chaining**

`return ((IEnumerable<NetworkInterface>) NetworkInterface.GetAllNetworkInterfaces()).Where<NetworkInterface>((Func<NetworkInterface, bool>) (nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)).Select<NetworkInterface, string>((Func<NetworkInterface, string>) (nic => nic.GetPhysicalAddress().ToString())).FirstOrDefault<string>();`

This is essentially a where/select search that uses a generic interface, with arrow function notation, which we can break up:

`((IEnumerable<NetworkInterface>) NetworkInterface.GetAllNetworkInterfaces())`

- Casting whatever `GetAllNetworkInterfaces()` returns (from NetworkInterface) and cast it as an `IEnumerable`

The resulting list of interfaces is searched with `.Where`:

- `(Func<NetworkInterface, bool>)`
  - Defines a boolean function
-  `(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)`
  - Returns `nic` when nic’s operational status is running or “Up” and is not a loopback type network interface.


From the resulting objects, we `.Select` with another generic arrow function that returns the physical address as a string:

- `.Select<NetworkInterface, string>(`...
- `(Func<NetworkInterface, string>) (nic => nic.GetPhysicalAddress().ToString())`
  - Gets our string representation of the physical address
- …`).FirstOrDefault<string>();`
  - Built in C# method for returning the first element of a sequence or default value if no element is found


All this was a single line in “ReadDeviceInfo”. The line itself seems useful, but does not actually seem to match what the name implies. It returns addresses for running network interfaces rather than returning device information, though this may be me not understanding the full context of this code. 

It is important however to note that this is part of the fully malicious code file, so this makes sense for them to do, though it unfortunately matches patterns found in the original code base.


**2. [Line 339](https://gitlab.cecs.pdx.edu/gomez22/solarwinds-code-analysis/-/blob/master/MaliciousCode/OrionImprovementBusinessLayer.cs): `?` Operator consequences become obscured when lines are too long (both off page and with function chaining.**

`hostName = addressFamilyEx == OrionImprovementBusinessLayer.AddressFamilyEx.Error ? cryptoHelper.GetCurrentString() : cryptoHelper.GetPreviousString(out last);`

**What does it mean?**

Takes the form  `condition ? consequent : alternative`
A ternary operator. If the condition is true, execute the consequent, otherwise execute the alternative. The entire expression is evaluated to be strictly as either the result of the consequent or the alternative.

**Now lets consider the `??` operator.**

The `??` operator makes things worse by further condensing the already ambiguating properties of the single `?`. It is similar to the single `?` operator only now it evaluates its right operand if the left value is null. Below is an example:

`Res = Operand1 ?? Operand2`

Is the same as:  `Res = Operand1 != null ? Operand1 : Operand2`

Now an example of chaining: `return (foo ?? bar) ?? splat;` which returns the first item that is not null between foo, bar, and splat.

**How this can lead to problems?**

- Question marks can obscure the consequences of the line. 
  - The operator typically leads to longer lines, which when leading offscreen, mean that on null, the line can execute a completely different operation
  - Because the expression has 3 operands, this makes nested operations much harder to understand.
  - Even if we can immediately see the consequent and alternative, all 3 operands can potentially have side effects that go unnoticed when the line is executed.

**Base64 Encoded Strings**

Within the OrionImprovementBusinessLayer.cs are numerous strings that are obfuscated using Base64 encoding. 

[These strings turn out to be IPv4 and IPv6 addresses](../-/blob/051aa868a3e61eddce267906a13b1fa8e0741a52/MaliciousCode/DecodedStrings.txt#L21-64). Using IP address look up tools, I have found that most of these belong to cloud providers. There are numerous providers like [Liquid Telecommunications Ltd](./-/blob/051aa868a3e61eddce267906a13b1fa8e0741a52/MaliciousCode/DecodedStrings.txt#L35) in Kenya, [Google Cloud](../-/blob/051aa868a3e61eddce267906a13b1fa8e0741a52/MaliciousCode/DecodedStrings.txt#L37) in Kansas, [Orange Mali](../-/blob/051aa868a3e61eddce267906a13b1fa8e0741a52/MaliciousCode/DecodedStrings.txt#L39) in Mali, and [Amazon Web Services](../-/blob/051aa868a3e61eddce267906a13b1fa8e0741a52/MaliciousCode/DecodedStrings.txt#L59) to name some. <br />
 
[This](../-/blob/051aa868a3e61eddce267906a13b1fa8e0741a52/MaliciousCode/DecodedStrings.txt#L185) is the custom alphabet for the base64 encoded strings. 


