namespace GlassShow.ConsoleApp;

public class CliArgumentUnifier
{
    public Dictionary<string, string?> Unify(Dictionary<string, string?> arguments)
    {
        // autogenerate the output dir path from the input epub if it's not specified
        if (arguments["outputPath"] == null)
        {
            string? epubPath = arguments["epubPath"] ?? null;

            if (epubPath == null)
            {
                Console.WriteLine($"ERROR at CliValidator: no epub path found");
            }
            else
            {
                string outputDirPath = Directory.GetParent(epubPath)?.FullName ?? string.Empty;
                arguments["outputPath"] = outputDirPath;
            }
        }

        // autogenerate the output dir name based on the name of the epub
        if (arguments["outputDirName"] == null)
        {
            string? epubPath = arguments["epubPath"] ?? null; 
            
            if (epubPath == null)
            {
                Console.WriteLine($"ERROR at CliValidator: no epub path found");
            }
            else
            {
                // string outputDirName = Directory.GetParent(epubPath)?.Name ?? string.Empty;
                string outputDirName = Path.GetFileNameWithoutExtension(epubPath);
                arguments["outputDirName"] = outputDirName;
            }
        }

        return arguments;
    }
}