using GlassShow.Core.EpubExtractorElements.CssStyleElements.CssTokenizer;

namespace GlassShow.Core.EpubExtractorElements.CssStyleElements.CssLexer;

public class CssLinearLexer
{
    private List<CssToken> _tokens;
    private int _tokensLength;
    private int _currentIndex = 0;

    public List<CssRuleSet> Selectors = new List<CssRuleSet>();
    
    public CssLinearLexer(List<CssToken> tokens)
    {
        _tokens = tokens;
        _tokensLength = tokens.Count;
        
        Lex();
    }

    private void Lex()
    {
        CssRuleSet? currentRuleSet = null;
        
        // ignores the following tokes:
        // - Comment
        while (!TokensIsEnd())
        {
            CssToken currentToken = ConsumeNextToken();

            // the token is a selector
            if (currentToken.Type == CssTokenType.Selector)
            {
                // make sure there is no active rule set
                if (currentRuleSet != null)
                {
                    throw new Exception("Selector token while the previous rule set is still active.");
                }

                if (PeekTokens()?.Type != CssTokenType.OpenBrace)
                {
                    throw new Exception("expected OpenBrace token after Selector token.");
                }
                
                // create a new active rule set
                currentRuleSet = new CssRuleSet()
                {
                    Selector = currentToken.Value
                };
                // consume the open brace
                ConsumeNextToken();

                continue;
            }

            // the token is a property name
            if (currentToken.Type == CssTokenType.PropertyName)
            {
                if (currentRuleSet == null)
                {
                    throw new Exception("PropertyName token while there is no current rule set.");
                }

                // check for a colon to exist
                if (PeekTokens()?.Type != CssTokenType.Colon)
                {
                    throw new Exception("expected Colon token after PropertyName token.");
                }
                // consume the colon
                ConsumeNextToken();
                
                // check for a value to exist
                if (PeekTokens()?.Type != CssTokenType.PropertyValue)
                {
                    throw new Exception("expected PropertyValue token after Colon token.");
                }
                
                CssToken nextToken = PeekTokens()!;
                currentRuleSet.DeclarationBlock[currentToken.Value] = nextToken.Value;

                // consume the semicolon if it exists
                if (PeekTokens()?.Type == CssTokenType.Semicolon)
                {
                    ConsumeNextToken();
                }

                continue;
            }

            // the token is a closing brace. make sure there is an active rule set, add it to the list, and
            // set it to null (there is no active rule set anymore).
            if (currentToken.Type == CssTokenType.CloseBrace)
            {
                if (currentRuleSet == null)
                {
                    throw new Exception("CloseBrace token while there is no active rule set.");
                }
                
                Selectors.Add(currentRuleSet);
                currentRuleSet = null;
            }
        }
    }
    
    /// <summary>
    /// peeks the stylesheet at the specified offset from the current position.
    /// </summary>
    /// <param name="offset">the offset from the current position.</param>
    /// <returns>the `char` at the specified offset, or `\0` if the offset is outside the string.</returns>
    private CssToken? PeekTokens(int offset = 0)
    {
        if (_currentIndex + offset < 0 || _currentIndex + offset >= _tokensLength)
        {
            return null;
        }
        
        return _tokens.ElementAt(_currentIndex + offset);
    }

    /// <summary>
    /// indicates if the end of the stylesheet has been reached.
    /// </summary>
    /// <returns>`true` if the end of the stylesheet has been reached, else `false`.</returns>
    private bool TokensIsEnd()
    {
        return _currentIndex >= _tokensLength;
    }

    /// <summary>
    /// returns the next element in the stylesheet and increments the counter.
    /// </summary>
    /// <returns>a `char` representing the next character in the stylesheet.</returns>
    private CssToken ConsumeNextToken()
    {
        return _tokens.ElementAt(_currentIndex++);
    }
}