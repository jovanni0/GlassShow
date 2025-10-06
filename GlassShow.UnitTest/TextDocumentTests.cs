using GlassShow.Core.Epub2FVML;

namespace GlassShow.UnitTest;

public class TextDocumentTests
{
    [Fact]
    public void Split2Paragraph_CleanDocument()
    {
        TextDocument textTextDocument = new("this is para 1.\n\nthis is para 2.");

        List<string> paras = textTextDocument.Children.Select(x => x.ToString()).ToList();
        List<string> expected = new() { "this is para 1.", "this is para 2." };

        Assert.Equal(expected, paras);
    }

    [Fact]
    public void Split2Paragraph_DirtyDocument()
    {
        TextDocument textTextDocument = new("\n\nthis is para 1.\n\n\n\nthis is para 2.\n\n");

        List<string> paras = textTextDocument.Children.Select(x => x.ToString()).ToList();
        List<string> expected = new() { "this is para 1.", "this is para 2." };

        Assert.Equal(expected, paras);
    }
}
