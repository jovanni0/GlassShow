namespace GlassShow.ConsoleApp.CliTools;

public class CliArgument
{
    /// <summary>
    /// the type of the instance.
    /// </summary>
    public required CliArgumentType Type { get; init; }
    
    /// <summary>
    /// the actual option name that was provided via the command line 
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// the value of the option
    /// </summary>
    public required string Value { get; init; }
}