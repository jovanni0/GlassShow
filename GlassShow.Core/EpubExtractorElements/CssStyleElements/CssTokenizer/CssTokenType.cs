namespace GlassShow.Core.EpubExtractorElements.CssStyleElements.CssTokenizer;

/// <summary>
/// defines the type of token
/// </summary>
public enum CssTokenType
{
    Comment,        // comments: `/* comment */`
    Selector,       // element selectors: `body`, `p`, `.class`, `#id`
    OpenBrace,      // `{`
    CloseBrace,     // `}`
    PropertyName,   // selector properties: `color`
    Colon,          // `:`
    PropertyValue,  // selector values: `red`, `12px`, `#fff`
    Semicolon,      // `;`
    LieralString,   // a value that is delimited by single/double quotes 
}