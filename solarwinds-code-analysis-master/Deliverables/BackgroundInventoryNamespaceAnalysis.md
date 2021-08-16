# BackgroundInventory.cs

No "red flags" in either legitimate or malicious file library inclusions. <br />

Lots of [filler words](../-/blob/46c1c1fec7329afe6fdc1663bd491250bee1b7dc/LegitimateCodeSamples/SolarWinds.Orion.BusinessLayer.BackgroundInventory/BackgroundInventory.cs#L28) in variable declarations. <br />

A [rare example](../-/blob/46c1c1fec7329afe6fdc1663bd491250bee1b7dc/LegitimateCodeSamples/SolarWinds.Orion.BusinessLayer.BackgroundInventory/BackgroundInventory.cs#L68-73) of more helpful, descriptive variable names. This is *inconsistent* with most variable naming in all files. <br />

A [textbook](../-/blob/46c1c1fec7329afe6fdc1663bd491250bee1b7dc/LegitimateCodeSamples/SolarWinds.Orion.BusinessLayer.BackgroundInventory/BackgroundInventory.cs#L228-233) example of a function that is difficult to decipher. Note the use of "filler" words and the non-descriptive variable names that are just slight variations on their data type. <br />

This is an example of [confusing variable names](../-/blob/46c1c1fec7329afe6fdc1663bd491250bee1b7dc/LegitimateCodeSamples/SolarWinds.Orion.BusinessLayer.BackgroundInventory/BackgroundInventory.cs#L272-274) where the passed in argument of the method is very similar to a class' method name (this.disposed vs disposing). <br />

#  PluginsFactory_1.cs

No "red flags" in legitimate library inclusions. <br />

I haven't seen [this syntax](../-/blob/819057a6b8d09fc773c9cda2ac1c42f6bc220b2c/LegitimateCodeSamples/SolarWinds.Orion.BusinessLayer.BackgroundInventory/PluginsFactory_1.cs#L49-52) for generics before. I find it confusing, but I need to find out if this is industry standard. <br />

[Non-descriptive variable naming](../-/blob/819057a6b8d09fc773c9cda2ac1c42f6bc220b2c/LegitimateCodeSamples/SolarWinds.Orion.BusinessLayer.BackgroundInventory/PluginsFactory_1.cs#L156). Inconsistent variable naming conventions between files.<br />


# Additional Problems

[Line 214](https://gitlab.cecs.pdx.edu/gomez22/solarwinds-code-analysis/-/blob/819057a6b8d09fc773c9cda2ac1c42f6bc220b2c/LegitimateCodeSamples/SolarWinds.Orion.BusinessLayer.BackgroundInventory/BackgroundInventory.cs#L214): 

`if (task.InventoryInput == SolarWinds.Orion.Core.BusinessLayer.BackgroundInventory.BackgroundInventory.InventoryTask.InventoryInputSource.NodeSettings)`

Here we can see what is basically the text example of how to not name your variables. Even compared to the classic example in the java standard library:

`InternalFrameInternalFrameTitlePaneInternalFrameTitlePaneMaximizeButtonWindowNotFocusedState`

Not to mention it's even longer, with the code in question being 123 characters long, compared to the java example at 92 characters long. To make matters worse, on [line 270](https://gitlab.cecs.pdx.edu/gomez22/solarwinds-code-analysis/-/blob/819057a6b8d09fc773c9cda2ac1c42f6bc220b2c/LegitimateCodeSamples/SolarWinds.Orion.BusinessLayer.BackgroundInventory/BackgroundInventory.cs#L270) its used with the ternary operator to make a seriously obcenely long line of code:

`private string GetSettingsForTask(SolarWinds.Orion.Core.BusinessLayer.BackgroundInventory.BackgroundInventory.InventoryTask task) => task.InventoryInput != SolarWinds.Orion.Core.BusinessLayer.BackgroundInventory.BackgroundInventory.InventoryTask.InventoryInputSource.NodeSettings ? InventorySettingsDAL.GetInventorySettings(task.ObjectSettingID, task.InventorySettingName) : NodeSettingsDAL.GetNodeSettings(task.ObjectSettingID, task.InventorySettingName);`

When something like this is in the code base, it isn't surprising that someone could hide functions and malicious code where people couldn't find or see it. There is almost a level of sillyness to this. One would have to either scroll about 3 page widths to the side to read it or look at the line-broken version, which is so long that few would assume it is all a single statement at a glance.
