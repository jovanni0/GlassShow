namespace GlassShow.Core.Epub2FVML;

public interface ITextUnit
{
    public HashSet<string> GetNames();

    public void Normalize(HashSet<string> names);

    public string ToString();
}