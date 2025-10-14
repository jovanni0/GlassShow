namespace GlassShow.Core.EpubExtractorElements.CssStyleElements;

public enum CssFsmState
{
    Idle,               // whitespace characters
    Comment,            // comments
    RuleSelector,       // element selectors: `body`, `p`, `.class`, `#id`
    RuleDescription,    // the description of the rule: `{ color: red; }`
    DescriptionColon ,  // the separation of the property and the value
}