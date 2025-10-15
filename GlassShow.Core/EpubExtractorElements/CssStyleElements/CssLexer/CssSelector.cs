using System.Text;

namespace GlassShow.Core.EpubExtractorElements.CssStyleElements.CssLexer;

public class CssSelector
{
    /// <summary>
    /// represents the name of the HTML element the selector targets. if the selector does not target an HTML element
    /// by name, it will be an empty string.
    /// </summary>
    public string SelectorName { get; set; } = string.Empty;

    /// <summary>
    /// represents the id of the HTML element the selector targets. if the selector does not target an HTML element
    /// by id, it will be an empty string.
    /// </summary>
    public string SelectorId { get; set; } = string.Empty;

    /// <summary>
    /// contains all the classes the selector targets. if the selector does not target a class, the list will be empty.
    /// </summary>
    public List<string> SelectorClasses { get; set; } = new List<string>();

    /// <summary>
    /// contains all the key-value properties of the selector
    /// </summary>
    public Dictionary<string, string> SelectorProperties = new Dictionary<string, string>();
    
    
    /// <summary>
    /// check if the `HtmlElement` will be influenced by the current `CssStyle` element.
    ///
    /// TODO: think about treating the Attribute Selectors ([type="text"]), Pseudo-Classes (:hover)
    ///       and Pseudo-Elements (::before).
    /// </summary>
    /// <param name="htmlElement">the `HtmlElement` that is checked.</param>
    /// <returns>`true` if the `HtmlElement` is being influenced, `false` otherwise.</returns>
    public bool IsApplicableToHtmlElement(HtmlElement htmlElement)
    {
        // check if the `HtmlElement` has a different id
        if (htmlElement.ElementId != SelectorId)
        {
            return false;
        }
        
        // check if the `HtmlElement` does not have all the classes
        foreach (string className in SelectorClasses)
        {
            if (!htmlElement.ElementClasses.Contains(className))
            {
                return false;
            }
        }
        
        // check if the `HtmlElement` does not have the same name
        if (htmlElement.ElementName != SelectorName)
        {
            return false;
        }

        return true;
    }
    
    /// <summary>
    /// returns the equivalent of the selector in pseudo-markdown:
    /// - italic: i;
    /// - bold: b;
    /// - bold italic: b i
    /// </summary>
    /// <returns>the string with the opening tags</returns>
    public string EquivalentInPseudoMarkdown()
    {
        bool isItalic = false;
        bool isBold = false;
        
        switch (SelectorName)
        {
            case "i": isItalic = true; break;
            case "b": isBold = true; break;
        }

        if (SelectorProperties.TryGetValue("font-style", out var fontStyle) && fontStyle == "italic")
        {
            isItalic = true;
        }

        if (SelectorProperties.TryGetValue("font-weight", out string? fontWeight) && fontWeight == "bold")
        {
            isBold = true;
        }

        
        if (SelectorProperties.TryGetValue("font-family", out string? fontFamily))
        {
            if (fontFamily.ToLower().Contains("italic")) isItalic = true;
            if (fontFamily.ToLower().Contains("bold")) isBold = true;
        }

        if (isItalic && isBold) return "<b><i>";
        if (isItalic) return "<i>";
        if (isBold) return "<b>";

        return string.Empty;
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is CssSelector other)
        {
            // Compare the basic properties
            bool namesEqual = string.Equals(SelectorName, other.SelectorName, StringComparison.OrdinalIgnoreCase);
            bool idsEqual = string.Equals(SelectorId, other.SelectorId, StringComparison.OrdinalIgnoreCase);

            // Compare classes
            bool classesEqual = SelectorClasses.Count == other.SelectorClasses.Count &&
                                !SelectorClasses.Except(other.SelectorClasses).Any();

            // Compare properties
            bool propertiesEqual = SelectorProperties.Count == other.SelectorProperties.Count &&
                                   !SelectorProperties.Except(other.SelectorProperties).Any();

            return namesEqual && idsEqual && classesEqual && propertiesEqual;
        }
        
        return false;
    }

    // Override GetHashCode method
    public override int GetHashCode()
    {
        // Combine hash codes of all properties
        int hashCode = 17; // Arbitrary prime number to start with

        hashCode = hashCode * 31 + (SelectorName?.GetHashCode() ?? 0);
        hashCode = hashCode * 31 + (SelectorId?.GetHashCode() ?? 0);
        hashCode = hashCode * 31 + SelectorClasses.Aggregate(0, (acc, s) => acc ^ s.GetHashCode());
        hashCode = hashCode * 31 + SelectorProperties.Aggregate(0, (acc, kvp) => acc ^ kvp.Key.GetHashCode() ^ kvp.Value.GetHashCode());

        return hashCode;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append("{\n");
        stringBuilder.Append($"  Name: {SelectorName}\n");
        stringBuilder.Append($"  Id: {SelectorId}\n");
        stringBuilder.Append($"  Classes: {string.Join(", ", SelectorClasses)}\n");
        stringBuilder.Append("  Properties: {\n");
        foreach (string key in SelectorProperties.Keys)
        {
            stringBuilder.Append($"    {key}: {SelectorProperties[key]}\n");
        }
        stringBuilder.Append("  }\n");
        stringBuilder.Append("}");

        return stringBuilder.ToString();
    }
}