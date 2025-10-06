using System.Net;
using GlassShow.Core.EpubExtractorElements.CssStyleElements;
using HtmlAgilityPack;
using VersOne.Epub;

namespace GlassShow.Core.EpubExtractorElements;

public class EpubExtractor
{
    #region PRIVATE_MEMBERS

    private EpubBook _epubBook;

    #endregion



    public EpubExtractor(EpubBook epubBook)
    {
        _epubBook = epubBook;
    }



    #region PUBLIC_METHODS

    /// <summary>
    /// get the splits of the EpubBook in Markdown Formatting
    /// </summary>
    /// <returns>a list of strings representing the splits</returns>
    public List<string> GetSplitsAsMarkdown()
    {
        List<string> pseudoMarkdownSplits = GetSplitsAsPseudoMarkdown(false);
        List<string> markdownSplits = pseudoMarkdownSplits.Select(x => Pseudo2Markdown(x)).ToList();
        List<string> cleanedMarkdownSplits = markdownSplits.Select(x => CleanUpMarkdown(x)).ToList();

        return cleanedMarkdownSplits;
    }


    public List<string> GetSplitsAsPseudoMarkdown(bool cleaned = true)
    {
        List<CssStyle> allCssElements = IdentifyCssElements();
        List<CssStyle> cssElements = allCssElements.Where(
            x => (
                (x.Declaration.ContainsKey("font-style") && x.Declaration["font-style"] == "italic") ||
                (x.Declaration.ContainsKey("font-weight") && x.Declaration["font-weight"] == "bold")
            )).ToList();

        Console.WriteLine("\nCSS elements of interest:");
        cssElements.ForEach(x => Console.WriteLine(x.ToString()));

        List<string> pseudoMarkdownSplits = new();

        foreach (EpubLocalTextContentFile item in _epubBook.ReadingOrder.AsEnumerable())
        {
            string splitContent = item.Content;

            HtmlDocument splitHtml = new();
            splitHtml.LoadHtml(splitContent);

            string pseudoMarkdownSplit = HtmlDocBody2PseudoMarkdown(splitHtml, cssElements);

            if (cleaned) pseudoMarkdownSplit = CleanUpMarkdown(pseudoMarkdownSplit);

            pseudoMarkdownSplits.Add(pseudoMarkdownSplit);
        }

        return pseudoMarkdownSplits;
    }

    #endregion



    #region PRIVATE_METHODS

    /// <summary>
    /// parse the css stylesheets
    /// </summary>
    /// <returns>a list of unique css elements</returns>
    private List<CssStyle> IdentifyCssElements()
    {
        List<CssStyle> cssElements = new() // default bold and italic elements
        {
            new CssStyle() { Name = "i", Declaration = { {"font-style", "italic" } } },
            new CssStyle() { Name = "em", Declaration = { {"font-style", "italic" } } },
            new CssStyle() { Name = "b", Declaration = { {"font-weight", "bold" } } }
        };
        EpubLocalTextContentFile[] cssFiles = _epubBook.Content.Css.Local.ToArray();

        foreach (EpubLocalTextContentFile cssFile in cssFiles)
        {
            string fileContent = cssFile.Content;

            List<CssStyle> newElements = CssStyleFactory.GetCssElements(fileContent);
            cssElements.AddRange(newElements);
        }

        List<CssStyle> uniqueElements = cssElements.Distinct().ToList();

        return uniqueElements;
    }


    /// <summary>
    /// process the body of the HTML document to pseudo Markdown
    /// </summary>
    /// <param name="htmlDocument">the HTML document</param>
    /// <param name="cssElements">the CSS style elements to be considered</param>
    /// <returns>the body of the document in pseudo Markdown formatting</returns>
    private string HtmlDocBody2PseudoMarkdown(HtmlDocument htmlDocument, List<CssStyle> cssElements)
    {
        string title = GetNodeContent(htmlDocument, "//head/title");
        string body = GetNodeContent(htmlDocument, "//body");

        HtmlDocument bodyHtml = new();
        bodyHtml.LoadHtml(body);

        string bodyMarkdown = Html2PseudoMarkdown(bodyHtml, cssElements);
        string splitMarkdown = $"#{title}\n\n{bodyMarkdown}";

        return splitMarkdown;
    }


    /// <summary>
    /// get the HTML content of a node
    /// </summary>
    /// <param name="htmlDocument">document containing the node</param>
    /// <param name="nodePath">path to the node</param>
    /// <returns>the inner HTML of the node</returns>
    private string GetNodeContent(HtmlDocument htmlDocument, string nodePath)
    {
        HtmlNode targetNode = htmlDocument.DocumentNode.SelectSingleNode(nodePath);

        if (targetNode == null) return string.Empty;

        return targetNode.InnerHtml;
    }


    /// <summary>
    /// convert a HTML document to Markdown
    /// </summary>
    /// <param name="htmlDocument">the HTML document</param>
    /// <param name="cssElements">the CSS style elements</param>
    /// <returns>the document in Markdown format</returns>
    private string Html2PseudoMarkdown(HtmlDocument htmlDocument, List<CssStyle> cssElements)
    {
        IEnumerable<HtmlNode> innerNodes = htmlDocument.DocumentNode.Descendants();

        foreach (HtmlNode node in innerNodes)
        {
            string nodeElement = node.Name;
            IEnumerable<string> nodeClasses = node.GetClasses();
            string nodeId = node.Id;

            CssStyle skeletonElement = new()
            {
                Name = nodeElement,
                Classes = nodeClasses.ToList(),
                Id = nodeId
            };

            if (skeletonElement.Name == "#text")
            {
                continue;
            }

            CssStyle? completeElement = cssElements.FirstOrDefault(x => x.SkeletonEquals(skeletonElement));

            if (completeElement == null) continue;

            string startPseudoMarkdownFormatTag = completeElement.EquivalentInPseudoMarkdown();
            string endPseudoMarkdownFormatTag = startPseudoMarkdownFormatTag.Replace("<", "</");

            //// add the markdown "stars"
            if (!string.IsNullOrEmpty(startPseudoMarkdownFormatTag))
            {
                node.PrependChild(htmlDocument.CreateTextNode(startPseudoMarkdownFormatTag));
                node.AppendChild(htmlDocument.CreateTextNode(endPseudoMarkdownFormatTag));
            }

            //// add a newline after the `p` element
            if (nodeElement.ToLower() == "p")
            {
                node.AppendChild(htmlDocument.CreateTextNode("\n\n"));
            }
        }

        return htmlDocument.DocumentNode.InnerText;
    }


    private string Pseudo2Markdown(string text)
    {
        string markdown = text.Replace("<i>", "*").Replace("<b>", "**");

        return markdown;
    }


    /// <summary>
    /// remove any extra newlines or multiple spaces and decodes the HTML special chars
    /// </summary>
    /// <param name="content">content to be processed</param>
    /// <returns>the processed content</returns>
    private string CleanUpMarkdown(string content)
    {
        List<string> lines = new();

        foreach (string item in content.Split("\n\n"))
        {
            string line = item.Trim();

            if (string.IsNullOrEmpty(line)) continue;

            line = WebUtility.HtmlDecode(line);
            line = line.Replace("  ", " ");

            lines.Add(line);
        }

        content = string.Join("\n\n", lines);

        return content;
    }

    #endregion
}
