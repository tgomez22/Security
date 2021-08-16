# Goals

1. Make it as *hard* as possible for malicious code to hide in plain sight.
2. Make it easier to ascertain the intention of methods.
3. Create consistency across method implementation, method naming, and variable naming conventions.


# Remediations

1. Remove all functional notation and replace it with standard method notation.
    - This would establish consistency across all method implementations.
    - This would help fix the problem of long lines of code running off the <br />
    edge of the page. The methods using functional notation commonly stretch off the <br />
    the page.

2. Change method names to be self-describing
    - Using action verbs like "get, set, is, etc." in method names would help to convey<br />
    what the method is supposed to be doing without requiring the code base as context. 
    - This would have the effect of raising "red-flags" with analysts if a method is not doing<br />
    what is says it should be doing. 

3. Change variable names to be more descriptive
    - There are many methods whose names describe their utility, like "flag1, flag2, and flag3"<br />
    but I don't know what these are flags for. Using names that would describe what the purpose is of<br />
    the variable would help greatly with understanding program flow.

4. Remove filler words from class names.
`SolarWinds.Orion.Core.BusinessLayer.BackgroundInventory.BackgroundInventory(BusinessLayerSettings.Instance.BackgroundInventory`

    -The code snippet above is a prime example of filler words being overused in the code base. They serve to clutter up the code, making it harder to maintain or analyze. 
    -Performing a major "deep clean"/refactoring of class names, etc. would help to make it easier to under the code, as well as, would cut down on the line length issue described in an above point.

5. Reduce method chaining to a more manageable level
    - I do not have a non-arbitrary suggestion for the number of methods which should be allowed <br />
    to be chained together. 
    - There are many methods which have 4 or 5+ methods chaned together using the "." notation. While <br />
    it does provide powerful functionality in a compressed space, it makes it more difficult to <br /> 
    determine *exactly* what that line is doing.
