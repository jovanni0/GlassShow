namespace GlassShow.ConsoleApp.CliTools;

/// <summary>
/// automates CLI-related tasks
/// </summary>
public static class CliManager
{
    /// <summary>
    /// generates a list of input-output paths, making sure for each epub file there is a clear destination directory.
    /// </summary>
    /// <param name="args">argument list from the command line.</param>
    /// <returns>a `List<Tuple<string, string>>` representing input-output path list, or `null` if an error is encountered.</returns>
    public static List<Tuple<string, string>>? GenerateInputOutputTupleList(string[] args)
    {
        List<CliToken> tokens = CliTokenizer.TokenizeArguments(args);
        List<CliArgument>? arguments = CliLexer.LexTokens(tokens);

        if (arguments == null)
        {
            return null;
        }

        List<Tuple<string, string>>? inputOutputTupleList = CliValidator.ValidateArguments(arguments);

        return inputOutputTupleList;
    }
}