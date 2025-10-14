namespace GlassShow.ConsoleApp.CliTools;

/// <summary>
/// the available types of tokens.
/// </summary>
public enum CliTokenType
{
    OperatingMode, // the operating mode: single/batch
    Option, // optional tags
    Value, // value of tags
}