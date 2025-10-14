namespace GlassShow.ConsoleApp.CliTools;

public enum CliArgumentType
{
    OperatingMode, // the operating mode of the process: single/batch
    DestinationDir, // the destination directory of the process (optional)
    OutputRename, // the name of the output directory (optional, single-only)
}   