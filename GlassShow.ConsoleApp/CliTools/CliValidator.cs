namespace GlassShow.ConsoleApp.CliTools;

/// <summary>
/// provides validation services for lexed arguments.
/// </summary>
public static class CliValidator
{
    /// <summary>
    /// a view of all available options.
    /// </summary>
    private static readonly Dictionary<string, string?> DefaultArgumentMap = new()
    {
        { "operatingMode", null },
        { "input", null },
        { "destination", null },
        { "rename", null }
    };
        
    /// <summary>
    /// make sure all argument values are valid.
    /// </summary>
    /// <param name="arguments">a list of `CliArgument` objects, representing lexed tokens.</param>
    /// <returns>a `List<Tuple<string, string>>` representing input-output paths, or `null` if there was an error.</returns>
    public static List<Tuple<string, string>>? ValidateArguments(List<CliArgument> arguments)
    {
        Dictionary<string, string?> argumentMap = BuildArgumentMap(arguments);
        bool result = CheckForOperatingModeConflicts(argumentMap);

        if (result == false)
        {
            return null;
        }
        
        List<Tuple<string, string>>? inputOutputTuples = CheckForErrorsAndBuildInputOutputTupleList(argumentMap);

        return inputOutputTuples;
    }
    
    /// <summary>
    /// creates a map of options, for easier processing
    /// </summary>
    /// <param name="arguments">arguments to be mapped.</param>
    /// <returns>a `Dictionary<string, string>` mapping options to values.</returns>
    private static Dictionary<string, string?> BuildArgumentMap(List<CliArgument> arguments)
    {
        Dictionary<string, string?> argumentMap = DefaultArgumentMap;

        foreach (CliArgument argument in arguments)
        {
            switch (argument.Type)
            {
                case CliArgumentType.OperatingMode:
                    argumentMap["operatingMode"] = argument.Name;
                    argumentMap["input"] = argument.Value;
                    break;
                case CliArgumentType.DestinationDir:
                    argumentMap["destination"] = argument.Value;
                    break;
                case CliArgumentType.OutputRename:
                    argumentMap["rename"] = argument.Value;
                    break;
            }
        }

        return argumentMap;
    }

    /// <summary>
    /// checks for conflicts between operating modes and options, and validates the input paths.
    /// </summary>
    /// <param name="argumentMap">a map of the arguments.</param>
    /// <returns>`true` if there was no error, else `false`.</returns>
    private static bool CheckForOperatingModeConflicts(Dictionary<string, string?> argumentMap)
    {
        // check the operating mode and the input path
        if (argumentMap["operatingMode"] == "single")
        {
            if (!File.Exists(argumentMap["input"]))
            {
                Console.WriteLine("ERROR: invalid Epub file path:  " + argumentMap["input"]);
                return false;
            }
        }
        else if (argumentMap["operatingMode"] == "batch")
        {
            if (!Directory.Exists(argumentMap["input"]))
            {
                Console.WriteLine("ERROR: invalid directory path:  " + argumentMap["input"]);
                return false;
            }

            if (argumentMap["rename"] != null)
            {
                Console.WriteLine("ERROR: cannot rename export locations in batch mode.");
                return false;
            }
        }
        else
        {
            Console.WriteLine($"ERROR: invalid operating mode:  {argumentMap["operatingMode"]}");
            return false;
        }

        return true;
    }

    
    /// <summary>
    /// checks for errors related to export paths, and builds a list of input-output tuples, each input file having a
    /// valid and existing output path.
    /// </summary>
    /// <param name="argumentMap">a map of the arguments.</param>
    /// <returns>a `List<Tuple<string, string>>` representing the input-output list, or `null` if there was an error.</returns>
    private static List<Tuple<string, string>>? CheckForErrorsAndBuildInputOutputTupleList(Dictionary<string, string?> argumentMap)
    {
        List<Tuple<string, string>> inputOutputTuples = new List<Tuple<string, string>>();
        
        if (argumentMap["destination"] != null)
        {
            // combine the output base directory with the name of the final directory.
            // the fact that the rename option is not null guaranties that the mode is single
            if (argumentMap["rename"] != null)
            {
                string? exportPath = CheckAndCreateExportDirectory(
                    argumentMap["destination"]!, 
                    argumentMap["rename"]!);

                if (exportPath == null) return null;
                
                inputOutputTuples.Add(new(argumentMap["input"]!, exportPath));
            }
            // combine the output base dir with the name of the input file
            else if (argumentMap["operatingMode"] == "single")
            {
                string? exportPath = CheckAndCreateExportDirectory(
                    argumentMap["destination"]!,
                    Path.GetFileNameWithoutExtension(argumentMap["input"]!));
                
                if (exportPath == null) return null;
                
                inputOutputTuples.Add(new(argumentMap["input"]!, exportPath));
            }
            // the mode is batch and there is no renaming; get all the epub files and create directories with each
            // of their name in the destination directory
            else
            {
                List<string> epubFiles = Directory.GetFiles(argumentMap["input"]!, "*.epub").ToList();

                foreach (string epubFile in epubFiles)
                {
                    string filename = Path.GetFileNameWithoutExtension(epubFile);
                    string? exportPath = CheckAndCreateExportDirectory(
                        argumentMap["destination"]!,
                        filename);
                    
                    if (exportPath == null) return null;
                
                    inputOutputTuples.Add(new(epubFile, exportPath));
                }
            }
        }
        else
        {
            // combine the base directory of the input file with the name of the final directory.
            // the fact that the rename option is not null guaranties that the mode is single
            if (argumentMap["rename"] != null)
            {
                string inputBaseDir = Path.GetDirectoryName(argumentMap["input"]!) ?? string.Empty;
                string? exportPath = CheckAndCreateExportDirectory(
                    inputBaseDir, 
                    argumentMap["rename"]!);
                
                if (exportPath == null) return null;
                
                inputOutputTuples.Add(new(argumentMap["input"]!, exportPath));
            }
            // combine the base dir of the input file with the name of the input file
            else if (argumentMap["operatingMode"] == "single")
            {
                string inputBaseDir = Path.GetDirectoryName(argumentMap["input"]!) ?? string.Empty;
                string? exportPath = CheckAndCreateExportDirectory(
                    inputBaseDir,
                    Path.GetFileNameWithoutExtension(argumentMap["input"]!));
                
                if (exportPath == null) return null;
                
                inputOutputTuples.Add(new(argumentMap["input"]!, exportPath));
            }
            // the mode is batch and there is no renaming; get all the epub files and create directories with each
            // of their name in the input directory
            else
            {
                List<string> epubFiles = Directory.GetFiles(argumentMap["input"]!, "*.epub").ToList();

                foreach (string epubFile in epubFiles)
                {
                    string filename = Path.GetFileNameWithoutExtension(epubFile);
                    string? exportPath = CheckAndCreateExportDirectory(
                        argumentMap["input"]!,
                        filename);
                    
                    if (exportPath == null) return null;
                
                    inputOutputTuples.Add(new(epubFile, exportPath));
                }
            }
        }

        return inputOutputTuples;
    }

    /// <summary>
    /// checks that the directory obtained by combining the `destination` and the `rename` parameters
    /// does not exist, and creates it .
    /// </summary>
    /// <param name="destination">the base directory name</param>
    /// <param name="rename">optional new directory</param>
    /// <returns>the name of the export directory, or `null` if it already exists or is not a valid path.</returns>
    private static string? CheckAndCreateExportDirectory(string destination, string rename)
    {
        string completePath = Path.Combine(destination, rename);

        if (Directory.Exists(completePath))
        {
            Console.WriteLine("ERROR: the export directory already exists: " + completePath);
            return null;
        }

        if (Directory.Exists(completePath))
        {
            Console.WriteLine("ERROR: the export directory already exists: " + completePath);
            return null;
        }
        
        try
        {
            Directory.CreateDirectory(completePath);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }

        return completePath;
    }
}