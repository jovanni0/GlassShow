namespace GlassShow.Core.EpubExtractorElements.CssStyleElements;

public class CssToken
{
    public CssTokenType Type { get; set; }
    public string Value { get; set; }
    
    
    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not CssToken other)
        {
            return false;
        }
        
        return this.Type == other.Type && this.Value == other.Value;
    }

    
    public override int GetHashCode()
    {
        return HashCode.Combine(Type, Value);
    }
}