using GlassShow.Core;

namespace GlassShow.ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        // make sure there are enough arguments
        if (args.Length < 2)
        {
            Console.WriteLine($"ERROR: not enough arguments: {args.Length}.");
            return;
        }

        // parse the CLI arguments
        CliArgumentsParser cliParser = new CliArgumentsParser();
        Dictionary<string, string?> argumentDict = cliParser.ParseArguments(args);

        CliArgumentUnifier cliUnifier = new CliArgumentUnifier();
        Dictionary<string, string?> unifiedArgumentDict = cliUnifier.Unify(argumentDict);

        CliArgumentValidator cliValidator = new CliArgumentValidator();
        bool hasErrors = cliValidator.ValidateArguments(unifiedArgumentDict);

        if (hasErrors)
        {
            Console.WriteLine("\nprocess terminated doe to the previous errors.");
            return;
        }
        
        // print the current settings to the console
        Console.WriteLine("GlassShow is being run with the following options:");
        foreach (string option in argumentDict.Keys)
        {
            string? value = argumentDict[option];
            
            if (value != null)
            {
                Console.WriteLine($"{{ {option} : {value} }}");
            }
        }
        Console.WriteLine();

        string epubPath = unifiedArgumentDict["epubPath"]!;
        string outputDir = Path.Combine(unifiedArgumentDict["outputPath"]!, unifiedArgumentDict["outputDirName"]!);
        
        // start the conversion process
        Epub2FvmlEngine conversionEngine = new Epub2FvmlEngine(epubPath);
        var splits = conversionEngine.Epub2Fvml();
        
        // make the output directory
        Directory.CreateDirectory(outputDir);
        
        // write the documents
        foreach (string split in splits)
        {
            string filepath = Path.Combine(outputDir, $"{splits.IndexOf(split)}.md");
            File.WriteAllText(filepath, split);
        }
    }
}