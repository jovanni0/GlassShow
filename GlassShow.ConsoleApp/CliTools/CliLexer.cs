namespace GlassShow.ConsoleApp.CliTools;

/// <summary>
/// provides lexing options for `CliToken` elements.
/// </summary>
public static class CliLexer
{
    /// <summary>
    /// lexes a list of `CliToken` into a list of `CliArgument`.
    ///
    /// if an error is encounter, it will return `null`.
    /// </summary>
    /// <param name="tokens">tokens to be processed.</param>
    /// <returns>a `List<CliArgument>` or `null` if there is an error.</returns>
    public static List<CliArgument>? LexTokens(List<CliToken> tokens)
    {
        List<CliArgument> arguments = new List<CliArgument>();
        int index = 0;

        while (index < tokens.Count)
        {
            CliToken currentToken = tokens.ElementAt(index);
            CliToken? nextToken = tokens.ElementAtOrDefault(index + 1);

            if (nextToken == null)
            {
                Console.WriteLine("ERROR: argument does not have pair: " + currentToken.Value + ".\n" +
                                  "make sure all arguments are type-value pairs.");
                return null;
            }

            // the element is the mode indicator
            if (index == 0)
            {
                // check the next token to be a value one
                if (nextToken.Value.StartsWith("--"))
                {
                    Console.WriteLine("ERROR: second argument needs to be a path thet matches the mode's requirement.");
                    return null;
                }
                
                arguments.Add(new CliArgument()
                {
                    Type = CliArgumentType.OperatingMode,
                    Name = currentToken.Value,
                    Value = nextToken.Value
                });

                index += 2; // increment over the path token
            }
            // the argument is an option name (starts with --)
            else if (currentToken.TokenType == CliTokenType.Option)
            {
                if (currentToken.Value == "destination")
                {
                    if (nextToken.Value.StartsWith("--"))
                    {
                        Console.WriteLine("ERROR: destination option needs to be provided with a path.");
                        return null;
                    }

                    arguments.Add(new CliArgument()
                    {
                        Type = CliArgumentType.DestinationDir,
                        Name = currentToken.Value,
                        Value = nextToken.Value
                    });

                    index += 2;
                }
                else if (currentToken.Value == "rename")
                {
                    if (nextToken.Value.StartsWith("--"))
                    {
                        Console.WriteLine("ERROR: rename option needs to be provided with a path.");
                        return null;
                    }
                    
                    arguments.Add(new CliArgument()
                    {
                        Type = CliArgumentType.OutputRename,
                        Name = currentToken.Value,
                        Value = nextToken.Value
                    });
                    index += 2;
                }
                else
                {
                    Console.WriteLine("ERROR: invalid option: " + currentToken.Value);
                    return null;
                }
            }
            // the token is a value one
            else
            {
                Console.WriteLine("ERROR: orphan argument: " + currentToken.Value);
                return null;
            }
        }

        return arguments;
    }
}