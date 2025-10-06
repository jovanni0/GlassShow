namespace GlassShow.ConsoleApp;

public class CliArgumentsParser
{
    /// <summary>
    /// a dictionary containing all possible CLI arguments and their value after parsing.
    /// if the value is `null` then the argument was not used or had no value.
    /// </summary>
    private readonly Dictionary<string, string?> _argumentsTemplate = new()
    {
        { "epubPath", null },
        { "outputPath", null },
        { "outputDirName", null },
    };

    /// <summary>
    /// a dict for translating the short CLI tags to explicit names
    /// </summary>
    private readonly Dictionary<string, string> _tagTranslation = new()
    {
        { "e", "epubPath" },
        { "o", "outputPath" },
        { "od", "outputDirName" },
    };
    
    
    /// <summary>
    /// parse the provided list of raw CLI arguments into a dictionary format.
    /// the list should not contain the name of the executable.
    /// </summary>
    /// <param name="arguments">the row arguments.</param>
    /// <returns>a dictionary with pairs of tags and values.</returns>
    public Dictionary<string, string?> ParseArguments(string[] arguments)
    {
        List<CliToken> tokens = TokenizeArguments(arguments);
        List<CliTagValuePair> parsePairs = ParseTokens(tokens);
        Dictionary<string, string?> argumentsDict = TranslateIntoDict(parsePairs);

        return argumentsDict;
    }

    
    /// <summary>
    /// go through all the arguments and create `CliToken` instances for each one.
    /// </summary>
    /// <param name="arguments">the list of CLI arguments to be tokenized.</param>
    /// <returns>a list of `CliToken` instances with the appropriate info.</returns>
    private List<CliToken> TokenizeArguments(string[] arguments)
    {
        List<CliToken> tokens = new List<CliToken>();
        
        foreach (string element in arguments)
        {
            // if the element starts with a dash "-", it is a tag
            if (element.StartsWith("-"))
            {
                CliToken newToken = new CliToken()
                {
                    TokenType = CliTokenType.Tag,
                    Value = element.Substring(1),
                };
                tokens.Add(newToken);
            }
            // else the element must be argument value
            else
            {
                CliToken newToken = new CliToken()
                {
                    TokenType = CliTokenType.Value,
                    Value = element,
                };
                tokens.Add(newToken);
            }
        }
        
        return tokens;
    }

    
    /// <summary>
    /// go through all the tokens and create tag-value pairs. 
    /// </summary>
    /// <param name="tokens">the list of tokens to be parsed.</param>
    /// <returns>a list of `CliTagValuePair` instances.</returns>
    private List<CliTagValuePair> ParseTokens(List<CliToken> tokens)
    {
        List<CliTagValuePair> parseResults = new List<CliTagValuePair>();

        int index = 0;
        while (index < tokens.Count)
        {
            CliToken currentToken = tokens[index];
            
            if (currentToken.TokenType == CliTokenType.Tag)
            {
                // the tag does not have a value pair (is the last element of the value is missing)
                if (index + 1 > tokens.Count || tokens[index + 1].TokenType == CliTokenType.Tag)
                {
                    Console.WriteLine($"WARNING: CLI tag without value: {currentToken.Value}");
                    index++;
                    continue;
                }

                CliToken nextToken = tokens[index + 1];

                // create a new tag-value pair with the current token as the tag and the next token as value
                CliTagValuePair tagValuePair = new CliTagValuePair()
                {
                    Tag = currentToken.Value,
                    Value = nextToken.Value
                };
                parseResults.Add(tagValuePair);
                
                // increment the counter over the current and next token (the tag and value)
                index += 2;
            }
            // the current tag is a value tag, so either the tag is missing or the logic error along the way
            else
            {
                Console.WriteLine($"WARNING: CLI value without a tag: {currentToken.Value}");
                index++;
            }
        }

        return parseResults;
    }

    /// <summary>
    /// go through all the tags and translate them from short form to explicit.
    /// </summary>
    /// <param name="tagValuePairs">list of `CliTagValuePair`.</param>
    /// <returns>a dictionary containing the explicit keys and the values.</returns>
    private Dictionary<string, string?> TranslateIntoDict(List<CliTagValuePair> tagValuePairs)
    {
        Dictionary<string, string?> arguments = _argumentsTemplate;

        foreach (CliTagValuePair element in tagValuePairs)
        {
            string tagName = element.Tag;

            if (_tagTranslation.TryGetValue(tagName, out var actualName))
            {
                arguments[actualName] = element.Value;
            }
            else
            {
                Console.WriteLine($"ERROR: unknown CLI tag: {tagName}");
            }
        }

        return arguments;
    }
}