namespace GlassShow.Core.Epub2FVML;

public class TextDocument : ANestedTextUnit<TextFragment>
{
    protected override string ChildrenSeparator => "\n\n";

    /// <summary>
    /// split the document into paragraphs elements.
    /// </summary>
    /// <param name="doc">the document to be split.</param>
    private void Split2Paragraphs(string doc)
    {
        string[] paras = doc.Split("\n\n");

        foreach (string para in paras)
        {
            if (string.IsNullOrEmpty(para)) continue;

            if (string.IsNullOrWhiteSpace(para)) continue;

            TextFragment textFragment = new(para);
            Children.Add(textFragment);
        }
    }


    /// <summary>
    /// extend the provided set of names by adding the possesive form for each of them.
    /// </summary>
    /// <param name="names">`HashSet` representing the set of names.</param>
    /// <returns>a new `HashSet` with the extended names.</returns>
    private HashSet<string> ExtendNames(HashSet<string> names)
    {
        HashSet<string> extendedNames = new();

        foreach (string name in names)
        {
            string namePossesive;

            if (name.EndsWith('s')) namePossesive = name + "’";
            else namePossesive = name + "’s";

            extendedNames.Add(name);
            extendedNames.Add(namePossesive);
        }

        return extendedNames;
    }
    
    
    //###########################################
    //                  PRIVATE
    //###########################################
    
    
    public TextDocument(string document)
    {
        Original = document;
        Split2Paragraphs(document);
    }

    
    //###########################################
    //                  PUBLIC
    //###########################################


    public void Parse2Fvml()
    {
        HashSet<string> names = GetNames();
        HashSet<string> extendedNames = ExtendNames(names);

        Normalize(extendedNames);
    }

    public override string ToString()
    {
        string content = base.ToString();
        
        content = content.Replace("<i>", "*").Replace("</i>", "*");
        content = content.Replace("<b>", "**").Replace("</b>", "**");
        
        return content;
    }
}
