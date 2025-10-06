namespace GlassShow.ConsoleApp;

public class CliArgumentValidator
{
    private Dictionary<string, CliArgumentDataType> _argumentDataTypes = new()
    {
        { "epubPath", CliArgumentDataType.FilePath },
        { "outputPath", CliArgumentDataType.DirPath },
        { "outputDirName", CliArgumentDataType.DirName },
    };
        
    
    public bool ValidateArguments(Dictionary<string, string?> arguments)
    {
        bool hasErrors = false;
        
        foreach (string tag in arguments.Keys)
        {
            string? value = arguments[tag];

            if (value == null)
            {
                continue;
            }

            CliArgumentDataType dataType; 
            bool resault = _argumentDataTypes.TryGetValue(tag, out dataType);

            if (resault == false)
            {
                Console.WriteLine($"ERROR: unknown tag type for: {tag}");
            }

            switch (dataType)
            {
                // check if the file exists
                case CliArgumentDataType.FilePath:
                    if (!File.Exists(value))
                    {
                        Console.WriteLine($"ERROR: the file does not exist: {value}");
                        hasErrors = true;
                    }
                    break;
                
                // check if the output directory exists
                case CliArgumentDataType.DirPath:
                    if (!Directory.Exists(value))
                    {
                        Console.WriteLine($"ERROR: the output directory's parent does not exists: {value}");
                        hasErrors = true;
                    }
                    break;
                
                case CliArgumentDataType.DirName:
                    string dirPath = arguments["outputPath"] ?? string.Empty;
                    string dirCompletePath = Path.Combine(dirPath, value);

                    if (Directory.Exists(dirCompletePath))
                    {
                        Console.WriteLine($"ERROR: the output directory already exists: {dirCompletePath}");
                        hasErrors = true;
                    }
                    break;
            }
        }

        return hasErrors;
    }
}