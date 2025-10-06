namespace GlassShow.Core.Epub2FVML;

public class TextFragment : ANestedTextUnit<TextLine>
{
    protected override string ChildrenSeparator => "\n";
    
    
    /// <summary>
    /// split the para to lines
    /// </summary>
    /// <param name="text">the para to be split</param>
    private void Split2Lines(string text)
    {
        string[] lines = text.Split('\n');

        foreach (string line in lines)
        {
            if (string.IsNullOrEmpty(line)) continue;

            if (string.IsNullOrWhiteSpace(line)) continue;

            TextLine textLine = new(line);
            Children.Add(textLine);
        }
    }
    
    
    //###########################################
    //                  PRIVATE
    //###########################################
    
    
    public TextFragment(string text)
    {
        Split2Lines(text);
    }

    
    //###########################################
    //                  PUBLIC
    //###########################################
}