# CoreBusinessLayerPlugin.cs

51 included libraries for legitimate code. <br />
24 libraries for malicious code. <br />
There are no "red flags" in my opinion when it comes to the libraries that are included in this file. <br />

This [line](../-/blob/393a969416a5847329b535f2fdd2c1196d82b985/LegitimateCodeSamples/SolarWinds.Orion.Core.BusinessLayer/CoreBusinessLayerPlugin.cs#L79) and this [line](../-/blob/393a969416a5847329b535f2fdd2c1196d82b985/LegitimateCodeSamples/SolarWinds.Orion.Core.BusinessLayer/CoreBusinessLayerPlugin.cs#L99) are examples of code going off the page, which has been common among all files. <br />

[Temporary variables](../-/blob/393a969416a5847329b535f2fdd2c1196d82b985/LegitimateCodeSamples/SolarWinds.Orion.Core.BusinessLayer/CoreBusinessLayerPlugin.cs#L140-142) are named for their utility which is common among all files; however, the names are not as descriptive as they can/should be. In the linked example, the flag variables could be named to reflect what they are controlling. <br />

[Functional notation](../-/blob/393a969416a5847329b535f2fdd2c1196d82b985/LegitimateCodeSamples/SolarWinds.Orion.Core.BusinessLayer/CoreBusinessLayerPlugin.cs#L447-459) examples. These are very common among all files.  <br />

[Chaining together](../-/blob/393a969416a5847329b535f2fdd2c1196d82b985/LegitimateCodeSamples/SolarWinds.Orion.Core.BusinessLayer/CoreBusinessLayerPlugin.cs#L463) of methods is very common in all files. There is heavy use of "filler" words in all files. <br />

Are these ['?'](../-/blob/393a969416a5847329b535f2fdd2c1196d82b985/LegitimateCodeSamples/SolarWinds.Orion.Core.BusinessLayer/CoreBusinessLayerPlugin.cs#L657-658) operators, or are they part of the variable name? Uncommon syntax appears several times in code.<br />

Casting types appears in both the [legitimate code](../-/blob/393a969416a5847329b535f2fdd2c1196d82b985/LegitimateCodeSamples/SolarWinds.Orion.Core.BusinessLayer/CoreBusinessLayerPlugin.cs#L693-696) and the [malicious code](../-/blob/3d3497e62428f819c398afb2bd3f119253fd09d2/MaliciousCode/OrionImprovementBusinessLayer.cs#L2793-2794); however, the legitimate code tends to cast objects to interfaces/complex objects. The malicious code tends to cast to primitive types or generics of primitive types, strings, or Arrays. <br />

# OrionCoreNotificationSubscriber.cs

[14](../-/blob/87aa76a409d94d56a07f93a6d6afbca32c6564ba/LegitimateCodeSamples/SolarWinds.Orion.Core.BusinessLayer/OrionCoreNotificationSubscriber.cs#L7-21) libraries included. None raise any "red flags" in my opinion. <br />

Again, [lines going off the screen](../-/blob/87aa76a409d94d56a07f93a6d6afbca32c6564ba/LegitimateCodeSamples/SolarWinds.Orion.Core.BusinessLayer/OrionCoreNotificationSubscriber.cs#L32) are common in all files. <br />

[Non-descriptive](../-/blob/87aa76a409d94d56a07f93a6d6afbca32c6564ba/LegitimateCodeSamples/SolarWinds.Orion.Core.BusinessLayer/OrionCoreNotificationSubscriber.cs#L100) naming convention. Just using the type as the variable name with some small addition appended to it.
