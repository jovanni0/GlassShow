namespace GlassShow.Core.EpubExtractorElements.CssStyleElements;

class CssStyle
{
    public string Name { get; set; } = string.Empty;
    public List<string> Classes { get; set; } = new();
    public string Id { get; set; } = string.Empty;
    public CssStyleType ElementType { get; private set; } = CssStyleType.Unknown;
    public Dictionary<string, string> Declaration { get; private set; } = new();

    public CssStyle()
    {
        
    }

    public CssStyle(string head, string body)
    {
        Declaration = new();

        ParseHead(head);
        ParseBody(body);
    }

    public string EquivalentInMarkdown()
    {
        string pseudoMarkdownTag = EquivalentInPseudoMarkdown();
        string markdownTag = pseudoMarkdownTag.Replace("<i>", "*").Replace("<b>", "**");

        return markdownTag;
    }

    public string EquivalentInPseudoMarkdown()
    {
        switch (Name)
        {
            case "i": return "<i>";
            case "b": return "<b>";
            default: break;
        }

        bool isItalic = false;
        bool isBold = false;

        string? fontStyle;
        if (Declaration.TryGetValue("font-style", out fontStyle) && fontStyle == "italic")
        {
            isItalic = true;
        }

        string? fontWeight;
        if (Declaration.TryGetValue("font-weight", out fontWeight) && fontWeight == "bold")
        {
            isBold = true;
        }

        string? fontFamily;
        Declaration.TryGetValue("font-family", out fontFamily);
        if (fontFamily != null)
        {
            if (fontFamily.ToLower().Contains("italic")) isItalic = true;
            if (fontFamily.ToLower().Contains("bold")) isBold = true;
        }

        if (isItalic && isBold) return "<b><i>";
        if (isItalic) return "<i>";
        if (isBold) return "<b>";

        return string.Empty;
    }

    private void ParseHead(string head)
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
            ElementType = CssStyleType.Unknown;
            Name = selector;
            return;
        }

        if (selector.StartsWith('.'))
        {
            ElementType = CssStyleType.Class;
            Classes.Add(selector[1..]);
        }
        else if (selector.StartsWith('#'))
        {
            ElementType = CssStyleType.Id;
            Id = selector.Length > 1 ? selector[1..] : string.Empty;
        }
        else
        {
            if (selector.Contains('.'))
            {
                string[] parts = selector.Split('.');
                
                if (parts.Length < 2)
                {
                    ElementType = CssStyleType.Unknown;
                    Name = selector;
                }

                ElementType = CssStyleType.ElementClass;
                Name = parts[0];
                Classes.AddRange(parts.Skip(1).ToArray());
            }
            else
            {
                ElementType = CssStyleType.Element;
                Name = selector;
            }
                
        }
    }

    private void ParseBody(string body)
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
            Declaration[descriptorKey] = descriptorValue;
        }
    }

    public bool SkeletonEquals(CssStyle otherElement)
    {
        //return false;

        if (Name != string.Empty && Name == otherElement.Name) return true;
        if (Id != string.Empty && Id == otherElement.Id) return true;

        foreach (string item in Classes)
        {
            if (item != string.Empty && otherElement.Classes.Contains(item))
            {
                return true;
            }
        }

        return false;
    }

    public override string ToString()
    {
        string classes = string.Empty;
        Classes.ForEach(x =>  classes += $"{x.ToString()}; ");
        return $"{Name}\t{Id}\t{classes}";
    }
}
