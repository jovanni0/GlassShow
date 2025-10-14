using GlassShow.Core.EpubExtractorElements.CssStyleElements;

namespace GlassShow.UnitTest;

public class CssTokenizerFiniteStateMachineTests
{
    /// <summary>
    /// a simple comment without padding
    /// </summary>
    [Fact]
    public void CssTokenizerFiniteStateMachine_Comment_Correct()
    {
        CssTokenizerFiniteStateMachine machine = new("/* comment */");
        List<CssToken> expectedTokens = [
            new() { Type = CssTokenType.Comment, Value = " comment " }
        ];
        
        Assert.Equal(expectedTokens, machine.Tokens);
    }
    
    /// <summary>
    /// a simple comment without padding
    /// </summary>
    [Fact]
    public void CssTokenizerFiniteStateMachine_CommentWithoutEnd_Correct()
    {
        CssTokenizerFiniteStateMachine machine = new("/* comment /*");
        List<CssToken> expectedTokens = [
            new() { Type = CssTokenType.Comment, Value = " comment /*" }
        ];
        
        Assert.Equal(expectedTokens, machine.Tokens);
    }
    
    /// <summary>
    /// a simple comment with padding
    /// </summary>
    [Fact]
    public void CssTokenizerFiniteStateMachine_CommentWithPadding_Correct()
    {
        CssTokenizerFiniteStateMachine machine = new("  /* comment */ ");
        List<CssToken> expectedTokens = [
            new() { Type = CssTokenType.Comment, Value = " comment " }
        ];
        
        Assert.Equal(expectedTokens, machine.Tokens);
    }

    [Fact]
    public void CssTokenizerFiniteStateMachine_Rule_Correct()
    {
        CssTokenizerFiniteStateMachine machine = new("body { color: red; }");
        List<CssToken> expectedTokens = [
            new() { Type = CssTokenType.Selector, Value = "body" },
            new() { Type = CssTokenType.OpenBrace, Value = "{" },
            new() { Type = CssTokenType.PropertyName, Value = "color" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = "red" },
            new() { Type = CssTokenType.Semicolon, Value = ";" },
            new() { Type = CssTokenType.CloseBrace, Value = "}" },
        ];
        
        Assert.Equal(expectedTokens, machine.Tokens);
    }
}