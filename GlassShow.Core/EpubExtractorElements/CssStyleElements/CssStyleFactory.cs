using System.Text.RegularExpressions;

namespace GlassShow.Core.EpubExtractorElements.CssStyleElements;

public static class CssStyleFactory
{
    static public List<CssSelector> GetCssElements(string cssFileContent)
    {
        string cssContentNoComments = Regex.Replace(cssFileContent, @"/\*[\s\S]*?\*/", string.Empty);
        string cssContentNoNewlines = cssContentNoComments.Replace("\n", "").Replace("\r", "");

        return IdentifyCssElements(cssContentNoNewlines);
    }

    static private List<CssSelector> IdentifyCssElements(string fileContent)
    {
        List<CssSelector> cssElements = new List<CssSelector>();
        string[] cssElementFrameworks = fileContent.Split('}');

        foreach (string item in cssElementFrameworks)
        {
            var newElements = GetCssElement(item);
            cssElements.AddRange(newElements);
        }

        return cssElements;
    }

    static private List<CssSelector> GetCssElement(string cssElementFramework)
    {
        // elements to consider:
        // - .class
        // - #id
        // - type_selector
        // - grouping1, grouping2  -- needs special tratament
        // - .th-5b615e01 em, .th-5b615e01 i = compound selector

        char[] otherTypeIdentifiers = [
            '@',                // rule
            '*',                // universal selector
            '[', ']',           // attribute selector
            ':',                // pseudo-class / -element
            '>', ' ', '+', '~', // combinator
            ','                 // grouping
        ];

        List<CssSelector> cssSelectors = new List<CssSelector>();
        string[] elementParts = cssElementFramework.Split('{');

        if (elementParts.Length < 2) return new();

        string elementHead = elementParts[0];
        string elementBody = elementParts[1];

        string[] grouping = elementHead.Split(',');
        foreach (string item in grouping)
        {
            string[] compountParts = item.Split(' ');

            foreach (string compoundItem in compountParts)
            {
                string head = compoundItem.Trim();

                if (string.IsNullOrEmpty(head)) continue;

                CssSelector newSelector = new CssSelector();
                ParseHead(head, ref newSelector);
                ParseBody(elementBody, ref newSelector);
                
                cssSelectors.Add(newSelector);
            }
        }

        return cssSelectors;
    }
    
    /// <summary>
    /// parse the head of a CssSelector
    /// </summary>
    /// <param name="head">the head to be parsed</param>
    /// <param name="currentStyle">the `CssSelector` the head info will be added to.</param>
    private static void ParseHead(string head, ref CssSelector currentStyle)
    {
        char[] otherTypeIdentifiers = [
            '@',                // rule
            '*',                // universal selector
            '[', ']',           // attribute selector
            ':',                // pseudo-class / -element
            '>', ' ', '+', '~', // combinator
            ','                 // grouping
        ];
        string selector = head.Trim();

        if (otherTypeIdentifiers.Any(selector.Contains))
        {
            currentStyle.SelectorName = selector;
            return;
        }

        if (selector.StartsWith('.'))
        {
            currentStyle.SelectorClasses.Add(selector[1..]);
        }
        else if (selector.StartsWith('#'))
        {
            currentStyle.SelectorId = selector.Length > 1 ? selector[1..] : string.Empty;
        }
        else
        {
            if (selector.Contains('.'))
            {
                string[] parts = selector.Split('.');
                
                if (parts.Length < 2)
                {
                    currentStyle.SelectorName = selector;
                }

                currentStyle.SelectorName = parts[0];
                currentStyle.SelectorClasses.AddRange(parts.Skip(1).ToArray());
            }
            else
            {
                currentStyle.SelectorName = selector;
            }
        }
    }
    
    /// <summary>
    /// parse the body of the selector
    /// </summary>
    /// <param name="body">the body of the selector</param>
    /// <param name="currentSelector">the `CssSelector` that will receive the properties</param>
    private static void ParseBody(string body, ref CssSelector currentSelector)
    {
        string[] parts = body.Split(';');

        foreach (string item in parts)
        {
            if (string.IsNullOrWhiteSpace(item) || string.IsNullOrEmpty(item)) continue;

            string[] itemParts = item.Split(':');

            if (itemParts.Length < 2)
            {
                Console.WriteLine($"malformed descriptor: {item}");
                continue;
            }

            string descriptorKey = itemParts[0].Trim();
            string descriptorValue = itemParts[1].Trim();
            currentSelector.SelectorProperties[descriptorKey] = descriptorValue;
        }
    }
}
