namespace GlassShow.ConsoleApp.CliTools;

public class CliToken
{
    /// <summary>
    /// the type of the token.
    /// </summary>
    public required CliTokenType TokenType { get; init; }
    
    /// <summary>
    /// the actual value of the token.
    /// </summary>
    public required string Value { get; init; }
}