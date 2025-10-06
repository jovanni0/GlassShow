using GlassShow.Core.Epub2FVML;

namespace GlassShow.UnitTest;

public class TextParagraphTests
{
    [Fact]
    public void Split2Line_Lines1()
    {
        TextFragment textFragment = new("this is line one.\nthis is line 2.");

        List<string> lines = textFragment.Children.Select(x => x.ToString()).ToList();
        List<string> expected = new() { "this is line one.", "this is line 2." };

        Assert.Equal(expected, lines);
    }
}
