namespace GlassShow.Core.Epub2FVML;

public abstract class ANestedTextUnit<TChild> : ITextUnit where TChild : ITextUnit
{
    /// <summary>
    /// a sequence of character that separate 2 child elements. 
    /// </summary>
    protected virtual string ChildrenSeparator => " "; 
    
    /// <summary>
    /// list of child elements.
    /// </summary>
    public List<TChild> Children = new();
    
    /// <summary>
    /// list of strings to be checked ahead.
    /// </summary>
    public List<string> SpecialWords { get; set; } = new();
    
    /// <summary>
    /// the original text.
    /// </summary>  
    public string Original { get; set; } = string.Empty;

    
    /// <summary>
    /// parse and extract the names from all child elements.
    /// </summary>
    /// <returns>a `HashSet` of names.</returns>
    public virtual HashSet<string> GetNames()
    {
        HashSet<string> names = new();
        foreach (TChild element in Children)
        {
            HashSet<string> newNames = element.GetNames();
            names.UnionWith(newNames);
        }
        
        return names;
    }

    
    /// <summary>
    /// convert all non-names words to lowercase.
    /// </summary>
    /// <param name="names">a `HashSet` of names.</param>
    public virtual void Normalize(HashSet<string> names)
    {
        foreach (TChild element in Children)
        {
            element.Normalize(names);
        }
    }

    
    /// <summary>
    /// returns the string representation of the TextUnit object.
    /// </summary>
    /// <returns>a string representing the TextUnit.</returns>
    public new virtual string ToString()
    {
        List<string> childrenAsString = Children.Select<TChild, string>(x => x.ToString()).ToList();

        string parentAsString = string.Join(ChildrenSeparator, childrenAsString);

        return parentAsString;
    }
}