using System.Text;

namespace GlassShow.Core.EpubExtractorElements.CssStyleElements.CssTokenizer;

public class CssTokenizerFiniteStateMachine
{
    private string _stylesheet;
    private int _stylesheetLength;
    
    private int _currentIndex = 0;
    public Stack<CssFsmState> _stateStack = new Stack<CssFsmState>([CssFsmState.Idle]);
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
            
        while (!StylesheetIsEnd())
        {
            char currentChar = ConsumeNextChar();
            
            // ignore whitespace while the FSM is the following states:
            // - Comment: it is tokenizing a comment, the whitespaces are important
            // - RuleSelector: you can have contextual selectors that contain spaces: H1 EM { color: red }
            // - PropertyValue: it is tokenizing the value of a property (eg: color: blue - blue is the value); 
            //                  the property can contain spaces: font: bold 12pt/14pt helvetica
            // - SingleQuoteLiteralString/DoubleQuoteLiteralString: the spaces are important in literal strings
            if ( _stateStack.Peek() != CssFsmState.Comment && _stateStack.Peek() != CssFsmState.PropertyValue && 
                 _stateStack.Peek() != CssFsmState.RuleSelector && _stateStack.Peek() != CssFsmState.SingleQuoteLiteralString && 
                 _stateStack.Peek() != CssFsmState.DoubleQuoteLiteralString && _whitespace.Contains(currentChar))
            {
                continue;
            }

            // the start of a comment
            if (_stateStack.Peek() != CssFsmState.Comment && currentChar == '/' && PeekStylesheet() == '*')
            {
                _stateStack.Push(CssFsmState.Comment); // push the current state on the stack
                ConsumeNextChar(); // consume the `*` character of the comment start identifier
                continue;
            }

            // the end of a comment
            if (_stateStack.Peek() == CssFsmState.Comment && currentChar == '*' && PeekStylesheet() == '/')
            {
                AddToken(buffer, CssTokenType.Comment);
                ConsumeNextChar(); // consume the '/' character of the comment end identifier
                _stateStack.Pop(); // remove the CssFsmState.Comment state from the stack
                continue;
            }
            
            // the end of a literal string
            // make sure to check that the detected terminator is not escaped
            if ( (_stateStack.Peek() == CssFsmState.SingleQuoteLiteralString && currentChar == '\'' && buffer[^1] != '\\') ||
                 (_stateStack.Peek() == CssFsmState.DoubleQuoteLiteralString && currentChar == '"' && buffer[^1] != '\\') )
            {
                AddToken(buffer, CssTokenType.PropertyValue);
                _stateStack.Pop();
                continue;
            }
            
            // for a "normal" character, if the FSM is idle then it is the start of the selector of a rule
            if (_stateStack.Peek() == CssFsmState.Idle)
            {
                _stateStack.Push(CssFsmState.RuleSelector);
                buffer.Append(currentChar);
                continue;
            }
            
            // the end of the selector of a rule
            if (_stateStack.Peek() == CssFsmState.RuleSelector && currentChar == '{')
            {
                AddToken(buffer, CssTokenType.Selector, true); // create the token for the selector
                
                buffer.Append(currentChar);
                _stateStack.Pop(); // remove the RuleSelector state
                _stateStack.Push(CssFsmState.RuleDescription);
                
                AddToken(buffer, CssTokenType.OpenBrace); // create the token for the open brace
                continue;
            }
            
            // the colon of the property
            if (_stateStack.Peek() == CssFsmState.RuleDescription && currentChar == ':')
            {
                AddToken(buffer, CssTokenType.PropertyName); // create the token for the property name

                buffer.Append(currentChar);
                _stateStack.Push(CssFsmState.PropertyColon);
                
                AddToken(buffer, CssTokenType.Colon); // create the token for the colon
                continue;
            }
            
            // we have passed the colon, and the character is not a whitespace, so assume the value of the property has
            // started; remove the PropertyColon state and push the PropertyValue.
            if (_stateStack.Peek() == CssFsmState.PropertyColon)
            {
                _stateStack.Pop();
                _stateStack.Push(CssFsmState.PropertyValue);
                
                // a literal string enclosed by single quotes
                if (currentChar == '\'')
                {
                    _stateStack.Push(CssFsmState.SingleQuoteLiteralString);
                    // in the case of a literal string, the string boundary (', ") is not preserved in the value, so 
                    // it is not added to the buffer
                }
                else if (currentChar == '"')
                {
                    _stateStack.Push(CssFsmState.DoubleQuoteLiteralString);
                }
                // any value that's not a literal string, so we need to preserve the character
                else
                {
                    buffer.Append(currentChar);   
                }
                continue;
            }
            
            // the semicolon of the property
            if (_stateStack.Peek() == CssFsmState.PropertyValue && currentChar == ';')
            {
                AddToken(buffer, CssTokenType.PropertyValue); // create the token for the property value

                buffer.Append(currentChar);
                _stateStack.Pop(); // remove the DescriptionColon state, and restore the previous state
                
                AddToken(buffer, CssTokenType.Semicolon); // create the token for the semicolon
                continue;
            }
            
            // then end of a rule description.
            // unless we are inside a comment, the } is only used as the end of a rule descriptor.
            if (_stateStack.Peek() != CssFsmState.Comment && currentChar == '}')
            {
                AddToken(buffer, CssTokenType.PropertyValue, true); // add whatever was in the buffer as a property value
                
                // remove all states until we hit the RuleDescription
                while (_stateStack.Peek() != CssFsmState.RuleDescription)
                {
                    _stateStack.Pop();
                }
                
                buffer.Append(currentChar);
                _stateStack.Pop(); // pop the rule descriptor
                
                AddToken(buffer, CssTokenType.CloseBrace); // create the token for the closing brace
                continue;
            }
            

            buffer.Append(currentChar);
        }
        
        // handle unexpected EOF
        if (_stateStack.Peek() != CssFsmState.Idle)
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
    /// <param name="trim">indicates if the content of the buffer should be trimmed of trailing whitespaces</param>
    private void AddToken(StringBuilder buffer, CssTokenType tokenType, bool trim = false)
    {
        if (buffer.Length == 0)
        {
            return;
        }
        
        CssToken currentToken = new CssToken()
        {
            Type = tokenType,
            Value = trim ? buffer.ToString().TrimEnd() : buffer.ToString() 
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
        switch (_stateStack.Peek())
        {
            case CssFsmState.Comment: AddToken(buffer, CssTokenType.Comment); break;
        }
    }


    /// <summary>
    /// peeks the stylesheet at the specified offset from the current position.
    /// </summary>
    /// <param name="offset">the offset from the current position.</param>
    /// <returns>the `char` at the specified offset, or `\0` if the offset is outside the string.</returns>
    private char PeekStylesheet(int offset = 0)
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
    private bool StylesheetIsEnd()
    {
        return _currentIndex >= _stylesheetLength;
    }

    /// <summary>
    /// returns the next element in the stylesheet and increments the counter.
    /// </summary>
    /// <returns>a `char` representing the next character in the stylesheet.</returns>
    private char ConsumeNextChar()
    {
        return _stylesheet.ElementAt(_currentIndex++);
    }
}