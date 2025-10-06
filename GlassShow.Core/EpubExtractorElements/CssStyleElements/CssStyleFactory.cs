using System.Text.RegularExpressions;

namespace GlassShow.Core.EpubExtractorElements.CssStyleElements;

static class CssStyleFactory
{
    static public List<CssStyle> GetCssElements(string cssFileContent)
    {
        string cssContentNoComments = Regex.Replace(cssFileContent, @"/\*[\s\S]*?\*/", string.Empty);
        string cssContentNoNewlines = cssContentNoComments.Replace("\n", "").Replace("\r", "");

        return IdentifyCssElements(cssContentNoNewlines);
    }

    static private List<CssStyle> IdentifyCssElements(string fileContent)
    {
        List<CssStyle> cssElements = new();
        string[] cssElementFrameworks = fileContent.Split('}');

        foreach (string item in cssElementFrameworks)
        {
            var newElements = GetCssElement(item);
            cssElements.AddRange(newElements);
        }

        return cssElements;
    }

    static private List<CssStyle> GetCssElement(string cssElementFramework)
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

        List<CssStyle> cssElements = new();
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

                CssStyle newElement = new CssStyle(head, elementBody);
                cssElements.Add(newElement);
            }
        }

        return cssElements;
    }
}
