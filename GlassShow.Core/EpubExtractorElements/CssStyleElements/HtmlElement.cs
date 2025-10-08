namespace GlassShow.Core.EpubExtractorElements.CssStyleElements;

/// <summary>
/// the class `HtmlElement` is supposed to model the elements found in a Html document.
/// this element follow the general structure:
///      ElementName [AttributeName="AttributeValue" AttributeName="" AttributeName ]
/// </summary>
public class HtmlElement
{
    /// <summary>
    /// holds the element name
    /// </summary>
    public string ElementName { get; set; } = string.Empty;

    /// <summary>
    /// holds the element id. if the element has no id, then is it an empty string.
    ///
    /// > https://www.w3schools.com/htmL/html_id.asp
    /// > the id name must contain at least one character, cannot start with a number,
    /// > and must not contain whitespaces (spaces, tabs, etc.).
    /// </summary>
    public string ElementId { get; set; } = string.Empty;

    /// <summary>
    /// holds the element classes. if the element has no classes, then it will be an empty list.
    /// </summary>
    public List<string> ElementClasses { get; set; } = new();

    /// <summary>
    /// holds all other attributes and their values, or `null` if there is no value
    /// </summary>
    public Dictionary<String, String?> GenericAttributes { get; set; } = new();

    
    /// <summary>
    /// checks if the attribute is present in the `GenericAttributes` dictionary.
    ///
    /// TODO: maybe make it case-insensitive.
    /// </summary>
    /// <param name="attributeName">element to be searched.</param>
    /// <returns>`true` if the element exists, `false` otherwise.</returns>
    public bool HasAttribute(string attributeName)
    {
        return GenericAttributes.ContainsKey(attributeName);
    }

    /// <summary>
    /// gets the value of the specifies attribute, or `null` if it does not exist.
    /// </summary>
    /// <param name="attributeName">the name of the attribute.</param>
    /// <returns>the value of the attribute, or `null` if it is not found.</returns>
    public string? GetAttributeValue(string attributeName)
    {
        return GenericAttributes.GetValueOrDefault(attributeName);
    }
}