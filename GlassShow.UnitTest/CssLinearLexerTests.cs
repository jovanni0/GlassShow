using GlassShow.Core.EpubExtractorElements.CssStyleElements.CssLexer;
using GlassShow.Core.EpubExtractorElements.CssStyleElements.CssTokenizer;

namespace GlassShow.UnitTest;

public class CssLinearLexerTests
{
    [Fact]
    public void CssLinearLexer_Comment()
    {
        List<CssToken> tokens = [
            new() { Type = CssTokenType.Comment, Value = " comment " }
        ];
        CssLinearLexer lexer = new CssLinearLexer(tokens);
        List<CssRuleSet> expected = [];
        
        Assert.Equal(expected, lexer.Selectors);
    }
    
    [Fact]
    public void CssLinearLexer_RuleWithSingleProperty()
    {
        List<CssToken> tokens = [
            new() { Type = CssTokenType.Selector, Value = "body" },
            new() { Type = CssTokenType.OpenBrace, Value = "{" },
            new() { Type = CssTokenType.PropertyName, Value = "color" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = "red" },
            new() { Type = CssTokenType.Semicolon, Value = ";" },
            new() { Type = CssTokenType.CloseBrace, Value = "}" },
        ];
        CssLinearLexer lexer = new CssLinearLexer(tokens);
        List<CssRuleSet> expected = [
            new() { Selector = "body", DeclarationBlock = new Dictionary<string, string>{ {"color", "red"} } }
        ];
        
        Assert.Equal(expected, lexer.Selectors);
    }
    
    [Fact]
    public void CssLinearLexer_RuleWithMultipleProperties()
    {
        List<CssToken> tokens = [
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
        CssLinearLexer lexer = new CssLinearLexer(tokens);
        List<CssRuleSet> expected = [
            new() { Selector = "body", DeclarationBlock = new Dictionary<string, string>
            {
                {"color", "red"}, { "background", "blue" }
            } },
        ];
        
        Assert.Equal(expected, lexer.Selectors);
    }
    
    [Fact]
    public void CssLinearLexer_RuleWithSinglePropertyAndCommentBefore()
    {
        List<CssToken> tokens = [
            new() { Type = CssTokenType.Comment, Value = " comment " },
            new() { Type = CssTokenType.Selector, Value = "body" },
            new() { Type = CssTokenType.OpenBrace, Value = "{" },
            new() { Type = CssTokenType.PropertyName, Value = "color" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = "red" },
            new() { Type = CssTokenType.Semicolon, Value = ";" },
            new() { Type = CssTokenType.CloseBrace, Value = "}" },
        ];
        CssLinearLexer lexer = new CssLinearLexer(tokens);
        List<CssRuleSet> expected = [
            new() { Selector = "body", DeclarationBlock = new Dictionary<string, string>{ {"color", "red"} } }
        ];
        
        Assert.Equal(expected, lexer.Selectors);
    }
    
    [Fact]
    public void CssLinearLexer_RuleWithSinglePropertyAndCommentInside()
    {
        List<CssToken> tokens = [
            new() { Type = CssTokenType.Selector, Value = "body" },
            new() { Type = CssTokenType.OpenBrace, Value = "{" },
            new() { Type = CssTokenType.PropertyName, Value = "color" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = "red" },
            new() { Type = CssTokenType.Semicolon, Value = ";" },
            new() { Type = CssTokenType.Comment, Value = " comment " },
            new() { Type = CssTokenType.CloseBrace, Value = "}" },
        ];
        CssLinearLexer lexer = new CssLinearLexer(tokens);
        List<CssRuleSet> expected = [
            new() { Selector = "body", DeclarationBlock = new Dictionary<string, string>{ {"color", "red"} } }
        ];
        
        Assert.Equal(expected, lexer.Selectors);
    }
    
    [Fact]
    public void CssLinearLexer_RuleWithLiteralStringValue()
    {
        List<CssToken> tokens = [
            new() { Type = CssTokenType.Selector, Value = "body" },
            new() { Type = CssTokenType.OpenBrace, Value = "{" },
            new() { Type = CssTokenType.PropertyName, Value = "color" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = " red " },
            new() { Type = CssTokenType.Semicolon, Value = ";" },
            new() { Type = CssTokenType.CloseBrace, Value = "}" },
        ];
        CssLinearLexer lexer = new CssLinearLexer(tokens);
        List<CssRuleSet> expected = [
            new() { Selector = "body", DeclarationBlock = new Dictionary<string, string>{ {"color", " red "} } }
        ];
        
        Assert.Equal(expected, lexer.Selectors);
    }
    
    [Fact]
    public void CssLinearLexer_RuleWithPropertyNotEndedInSemicolon()
    {
        List<CssToken> tokens = [
            new() { Type = CssTokenType.Selector, Value = "body" },
            new() { Type = CssTokenType.OpenBrace, Value = "{" },
            new() { Type = CssTokenType.PropertyName, Value = "color" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = "red" },
            new() { Type = CssTokenType.CloseBrace, Value = "}" },
        ];
        CssLinearLexer lexer = new CssLinearLexer(tokens);
        List<CssRuleSet> expected = [
            new() { Selector = "body", DeclarationBlock = new Dictionary<string, string>{ {"color", "red"} } }
        ];
        
        Assert.Equal(expected, lexer.Selectors);
    }
    
    [Fact]
    public void CssLinearLexer_RuleWithMultiplePropertiesWithSameName()
    {
        List<CssToken> tokens = [
            new() { Type = CssTokenType.Selector, Value = "body" },
            new() { Type = CssTokenType.OpenBrace, Value = "{" },
            new() { Type = CssTokenType.PropertyName, Value = "color" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = " red " },
            new() { Type = CssTokenType.Semicolon, Value = ";" },
            new() { Type = CssTokenType.PropertyName, Value = "color" },
            new() { Type = CssTokenType.Colon, Value = ":" },
            new() { Type = CssTokenType.PropertyValue, Value = "blue" },
            new() { Type = CssTokenType.CloseBrace, Value = "}" },
        ];
        CssLinearLexer lexer = new CssLinearLexer(tokens);
        List<CssRuleSet> expected = [
            new() { Selector = "body", DeclarationBlock = new Dictionary<string, string>{ {"color", "blue"} } }
        ];
        
        Assert.Equal(expected, lexer.Selectors);
    }
}