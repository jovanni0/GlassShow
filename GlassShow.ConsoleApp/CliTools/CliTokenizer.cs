namespace GlassShow.ConsoleApp.CliTools;

/// <summary>
/// provides tokenization for raw CommandLine arguments list
/// </summary>
public static class CliTokenizer
{
    /// <summary>
    /// go through all the arguments and create `CliToken` instances for each one.
    /// </summary>
    /// <param name="arguments">the list of CLI arguments to be tokenized.</param>
    /// <returns>a list of `CliToken` instances with the appropriate info.</returns>
    public static List<CliToken> TokenizeArguments(string[] arguments)
    {
        List<CliToken> tokens = new List<CliToken>();

        for (int index = 0; index < arguments.Length; index++)
        {
            string element = arguments.ElementAt(index);
            
            // optional arguments
            if (element.StartsWith("--"))
            {
                CliToken newToken = new CliToken()
                {
                    TokenType = CliTokenType.Option,
                    Value = element.Substring(2),
                };
                tokens.Add(newToken);
                continue;
            }

            if (index == 0)
            {
                tokens.Add(new CliToken()
                {
                    TokenType = CliTokenType.OperatingMode,
                    Value = element
                });
            }
            else
            {
                tokens.Add(new CliToken()
                {
                    TokenType = CliTokenType.Value,
                    Value = element
                });
            }
        }
        
        return tokens;
    }
}