namespace GlassShow.Core.Epub2FVML;

public static class StringExtensions
{
    public static string ReplaceAt(this string text, int index, char character)
    {
        if (text == null) { return text; }
        if (index < 0 || index >= text.Length) { return text; }

        return text.Substring(0, index) + character + text.Substring(index + 1);
    }
}
