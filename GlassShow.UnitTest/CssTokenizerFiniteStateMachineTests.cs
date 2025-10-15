using GlassShow.Core.EpubExtractorElements.CssStyleElements;
using GlassShow.Core.EpubExtractorElements.CssStyleElements.CssTokenizer;

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
    public void CssTokenizerFiniteStateMachine_RuleWithSingleProperty_Correct()
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
    
    [Fact]
    public void CssTokenizerFiniteStateMachine_RuleWithMultipleProperties_Correct()
    {
        CssTokenizerFiniteStateMachine machine = new("body { color: red; background: blue; }");
        List<CssToken> expectedTokens = [
            new() { Type = CssTokenType.Selector, Value = "body" },
            new() { Type = CssTokenType.OpenBrace, Value = "{" },
            new() { Type = CssTokenType.PropertyName, Value = "color" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = "red" },
            new() { Type = CssTokenType.Semicolon, Value = ";" },
            new() { Type = CssTokenType.PropertyName, Value = "background" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = "blue" },
            new() { Type = CssTokenType.Semicolon, Value = ";" },
            new() { Type = CssTokenType.CloseBrace, Value = "}" },
        ];
        
        Assert.Equal(expectedTokens, machine.Tokens);
    }
    
    [Fact]
    public void CssTokenizerFiniteStateMachine_CommentAndRuleWithSingleProperty_Correct()
    {
        CssTokenizerFiniteStateMachine machine = new("/* comment */ \n body { color: red; }");
        List<CssToken> expectedTokens = [
            new() { Type = CssTokenType.Comment, Value = " comment " },
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
    
    [Fact]
    public void CssTokenizerFiniteStateMachine_CommentInlineAndRuleWithSingleProperty_Correct()
    {
        CssTokenizerFiniteStateMachine machine = new("body { color: red; /* comment */ }");
        List<CssToken> expectedTokens = [
            new() { Type = CssTokenType.Selector, Value = "body" },
            new() { Type = CssTokenType.OpenBrace, Value = "{" },
            new() { Type = CssTokenType.PropertyName, Value = "color" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = "red" },
            new() { Type = CssTokenType.Semicolon, Value = ";" },
            new() { Type = CssTokenType.Comment, Value = " comment " },
            new() { Type = CssTokenType.CloseBrace, Value = "}" },
        ];
        
        Assert.Equal(expectedTokens, machine.Tokens);
    }
    
    [Fact]
    public void CssTokenizerFiniteStateMachine_RuleWithSingleLiteralStringProperty_SingleQuotes_Correct()
    {
        CssTokenizerFiniteStateMachine machine = new("body { color: ' red ';}");
        List<CssToken> expectedTokens = [
            new() { Type = CssTokenType.Selector, Value = "body" },
            new() { Type = CssTokenType.OpenBrace, Value = "{" },
            new() { Type = CssTokenType.PropertyName, Value = "color" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = " red " },
            new() { Type = CssTokenType.Semicolon, Value = ";" },
            new() { Type = CssTokenType.CloseBrace, Value = "}" },
        ];
        
        Assert.Equal(expectedTokens, machine.Tokens);
    }
    
    [Fact]
    public void CssTokenizerFiniteStateMachine_RuleWithSingleLiteralStringProperty_DoubleQuotes_Correct()
    {
        CssTokenizerFiniteStateMachine machine = new("body { color: \" red \";}");
        List<CssToken> expectedTokens = [
            new() { Type = CssTokenType.Selector, Value = "body" },
            new() { Type = CssTokenType.OpenBrace, Value = "{" },
            new() { Type = CssTokenType.PropertyName, Value = "color" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = " red " },
            new() { Type = CssTokenType.Semicolon, Value = ";" },
            new() { Type = CssTokenType.CloseBrace, Value = "}" },
        ];
        
        Assert.Equal(expectedTokens, machine.Tokens);
    }
    
    [Fact]
    public void CssTokenizerFiniteStateMachine_RuleWithSingleLiteralStringProperty_SingleQuotesAndEscaped_Correct()
    {
        CssTokenizerFiniteStateMachine machine = new("body { color: ' red\\' ';}");
        List<CssToken> expectedTokens = [
            new() { Type = CssTokenType.Selector, Value = "body" },
            new() { Type = CssTokenType.OpenBrace, Value = "{" },
            new() { Type = CssTokenType.PropertyName, Value = "color" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = " red\\' " },
            new() { Type = CssTokenType.Semicolon, Value = ";" },
            new() { Type = CssTokenType.CloseBrace, Value = "}" },
        ];
        
        Assert.Equal(expectedTokens, machine.Tokens);
    }
    
    [Fact]
    public void CssTokenizerFiniteStateMachine_RuleWithSingleLiteralStringProperty_DoubleQuotesAndEscaped_Correct()
    {
        CssTokenizerFiniteStateMachine machine = new("body { color: \" red\\\" \";}");
        List<CssToken> expectedTokens = [
            new() { Type = CssTokenType.Selector, Value = "body" },
            new() { Type = CssTokenType.OpenBrace, Value = "{" },
            new() { Type = CssTokenType.PropertyName, Value = "color" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = " red\\\" " },
            new() { Type = CssTokenType.Semicolon, Value = ";" },
            new() { Type = CssTokenType.CloseBrace, Value = "}" },
        ];
        
        Assert.Equal(expectedTokens, machine.Tokens);
    }

    [Fact]
    public void CssTokenizerFiniteStateMachine_RuleWithGroupedSelector_Correct()
    {
        CssTokenizerFiniteStateMachine machine = new("H1, H2, H3 { font-family: helvetica; }");
        List<CssToken> expectedTokens = [
            new() { Type = CssTokenType.Selector, Value = "H1, H2, H3" },
            new() { Type = CssTokenType.OpenBrace, Value = "{" },
            new() { Type = CssTokenType.PropertyName, Value = "font-family" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = "helvetica" },
            new() { Type = CssTokenType.Semicolon, Value = ";" },
            new() { Type = CssTokenType.CloseBrace, Value = "}" },
        ];
        
        Assert.Equal(expectedTokens, machine.Tokens);
    }
    
    [Fact]
    public void CssTokenizerFiniteStateMachine_RuleWithPropertyValueNotEndedInSemicolon_Correct()
    {
        CssTokenizerFiniteStateMachine machine = new("body { color: red }");
        List<CssToken> expectedTokens = [
            new() { Type = CssTokenType.Selector, Value = "body" },
            new() { Type = CssTokenType.OpenBrace, Value = "{" },
            new() { Type = CssTokenType.PropertyName, Value = "color" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = "red" },
            new() { Type = CssTokenType.CloseBrace, Value = "}" },
        ];
        
        Assert.Equal(expectedTokens, machine.Tokens);
    }
    
    [Fact]
    public void CssTokenizerFiniteStateMachine_RuleWithContextualSelector_Correct()
    {
        CssTokenizerFiniteStateMachine machine = new("body h1 { color: red }");
        List<CssToken> expectedTokens = [
            new() { Type = CssTokenType.Selector, Value = "body h1" },
            new() { Type = CssTokenType.OpenBrace, Value = "{" },
            new() { Type = CssTokenType.PropertyName, Value = "color" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = "red" },
            new() { Type = CssTokenType.CloseBrace, Value = "}" },
        ];
        
        Assert.Equal(expectedTokens, machine.Tokens);
    }
    
    [Fact]
    public void CssTokenizerFiniteStateMachine_AnchoredPseudoClasses_Correct()
    {
        CssTokenizerFiniteStateMachine machine = new("A:link { color: red }");
        List<CssToken> expectedTokens = [
            new() { Type = CssTokenType.Selector, Value = "A:link" },
            new() { Type = CssTokenType.OpenBrace, Value = "{" },
            new() { Type = CssTokenType.PropertyName, Value = "color" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = "red" },
            new() { Type = CssTokenType.CloseBrace, Value = "}" },
        ];
        
        Assert.Equal(expectedTokens, machine.Tokens);
    }
    
    [Fact]
    public void CssTokenizerFiniteStateMachine_UnAnchoredPseudoClasses_Correct()
    {
        CssTokenizerFiniteStateMachine machine = new(":link { color: red }");
        List<CssToken> expectedTokens = [
            new() { Type = CssTokenType.Selector, Value = ":link" },
            new() { Type = CssTokenType.OpenBrace, Value = "{" },
            new() { Type = CssTokenType.PropertyName, Value = "color" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = "red" },
            new() { Type = CssTokenType.CloseBrace, Value = "}" },
        ];
        
        Assert.Equal(expectedTokens, machine.Tokens);
    }
}