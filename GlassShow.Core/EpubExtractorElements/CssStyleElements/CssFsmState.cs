namespace GlassShow.Core.EpubExtractorElements.CssStyleElements;

public enum CssFsmState
{
    Idle,                       // whitespace characters
    Comment,                    // comments
    RuleSelector,               // element selectors: `body`, `p`, `.class`, `#id`
    RuleDescription,            // the description of the rule: `{ color: red; }`
    PropertyColon,              // the colon separating the property name from the value
    PropertyValue,              // the value of a property, beginning after a colon inside a RuleDescription
    SingleQuoteLiteralString,   // a value that is treated as a literal string, including whitespaces, enclosed by single quotes (')
    DoubleQuoteLiteralString,   // a value that is treated as a literal string, including whitespaces, enclosed by double quotes (")
}