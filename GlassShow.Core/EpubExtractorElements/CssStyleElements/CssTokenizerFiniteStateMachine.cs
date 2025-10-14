using System.Text;

namespace GlassShow.Core.EpubExtractorElements.CssStyleElements;

public class CssTokenizerFiniteStateMachine
{
    private string _stylesheet;
    private int _stylesheetLength;
    
    private int _currentIndex = 0;
    private CssFsmState _currentState = CssFsmState.Idle;
    private char[] _whitespace = [' ', '\n', '\r', '\t']; 
        
    public List<CssToken> Tokens { get; } = new List<CssToken>();
    
    public CssTokenizerFiniteStateMachine(string stylesheet)
    {
        _stylesheet = stylesheet;
        _stylesheetLength = stylesheet.Length;
        
        Tokenize();
    }

    /// <summary>
    /// tokenize the given stylesheet
    /// </summary>
    private void Tokenize()
    {
        StringBuilder buffer = new StringBuilder();
            
        while (!IsEnd())
        {
            char currentChar = Consume();
            
            // ignore whitespace while the FSM is idle
            if (_currentState == CssFsmState.Idle && _whitespace.Contains(currentChar))
            {
                continue;
            }

            // the start of a comment
            if (_currentState != CssFsmState.Comment && currentChar == '/' && Peek() == '*')
            {
                _currentState = CssFsmState.Comment;
                Consume(); // consume the `*` character of the comment start identifier
                continue;
            }

            // the end of a comment
            if (_currentState == CssFsmState.Comment && currentChar == '*' && Peek() == '/')
            {
                AddToken(buffer, CssTokenType.Comment);
                Consume(); // consume the '/' character of the comment end identifier
                _currentState = CssFsmState.Idle; // set the FSM to idle
                continue;
            }
            
            // the start of the selector of a rule
            if (_currentState == CssFsmState.Idle)
            {
                _currentState = CssFsmState.RuleSelector;
                buffer.Append(currentChar);
                continue;
            }
            
            // the end of the selector of a rule
            if (_currentState == CssFsmState.RuleSelector && currentChar == '{')
            {
                AddToken(buffer, CssTokenType.Selector, true); // create the token for the selector
                
                buffer.Append(currentChar);
                _currentState = CssFsmState.RuleDescription;
                
                AddToken(buffer, CssTokenType.OpenBrace, true); // create the token for the open brace
                continue;
            }
            
            // the colon of the property
            if (_currentState == CssFsmState.RuleDescription && currentChar == ':')
            {
                AddToken(buffer, CssTokenType.PropertyName, true); // create the token for the property name

                buffer.Append(currentChar);
                _currentState = CssFsmState.DescriptionColon;
                
                AddToken(buffer, CssTokenType.Colon, true); // create the token for the colon
                continue;
            }
            
            // the semicolon of the property
            if (_currentState == CssFsmState.DescriptionColon && currentChar == ';')
            {
                AddToken(buffer, CssTokenType.PropertyValue, true); // create the token for the property value

                buffer.Append(currentChar);
                _currentState = CssFsmState.RuleDescription;
                
                AddToken(buffer, CssTokenType.Semicolon, true); // create the token for the semicolon
                continue;
            }
            
            // then end of a rule description
            if (_currentState == CssFsmState.RuleDescription && currentChar == '}')
            {
                buffer.Append(currentChar);
                _currentState = CssFsmState.Idle;
                
                AddToken(buffer, CssTokenType.CloseBrace, true); // create the token for the closing brace
                continue;
            }

            buffer.Append(currentChar);
        }
        
        // handle unexpected EOF
        if (_currentState != CssFsmState.Idle)
        {
            FinalizeCurrentTokenAtEof(buffer);
        }
    }

    /// <summary>
    /// add the token stored in the buffer to the token list, and clear the buffer.
    /// if the buffer is empty, do nothing. 
    /// </summary>
    /// <param name="buffer">the buffer storing the token value.</param>
    /// <param name="tokenType">the type of the token.</param>
    /// <param name="trim">indicates if the content of the buffer should be trimmed of leading/trailing whitespaces</param>
    private void AddToken(StringBuilder buffer, CssTokenType tokenType, bool trim = false)
    {
        if (buffer.Length == 0)
        {
            return;
        }
        
        CssToken currentToken = new CssToken()
        {
            Type = tokenType,
            Value = trim ? buffer.ToString().Trim() : buffer.ToString() 
        };
        Tokens.Add(currentToken);
        buffer.Clear();
    }
    
    
    /// <summary>
    /// generates the correct token for the in-progress state and cleans everything up.
    /// </summary>
    /// <param name="buffer">the `StringBuilder` instance used for buffering the value of a token.</param>
    private void FinalizeCurrentTokenAtEof(StringBuilder buffer)
    {
        switch (_currentState)
        {
            case CssFsmState.Comment: AddToken(buffer, CssTokenType.Comment); break;
        }
        
        _currentState = CssFsmState.Idle;
    }


    /// <summary>
    /// peeks the stylesheet at the specified offset from the current position.
    /// </summary>
    /// <param name="offset">the offset from the current position.</param>
    /// <returns>the `char` at the specified offset, or `\0` if the offset is outside the string.</returns>
    private char Peek(int offset = 0)
    {
        if (_currentIndex + offset < 0 || _currentIndex + offset >= _stylesheetLength)
        {
            return '\0';
        }
        
        return _stylesheet.ElementAt(_currentIndex + offset);
    }

    /// <summary>
    /// indicates if the end of the stylesheet has been reached.
    /// </summary>
    /// <returns>`true` if the end of the stylesheet has been reached, else `false`.</returns>
    private bool IsEnd()
    {
        return _currentIndex >= _stylesheetLength;
    }

    /// <summary>
    /// returns the next element in the stylesheet and increments the counter.
    /// </summary>
    /// <returns>a `char` representing the next character in the stylesheet.</returns>
    private char Consume()
    {
        return _stylesheet.ElementAt(_currentIndex++);
    }
}