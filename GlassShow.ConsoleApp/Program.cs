using GlassShow.ConsoleApp.CliTools;
using GlassShow.Core;

namespace GlassShow.ConsoleApp;

static class Program
{
    static void Main(string[] args)
    {
        // make sure there are enough arguments
        if (args.Length < 2)
        {
            Console.WriteLine($"ERROR: not enough arguments: {args.Length}.");
            return;
        }

        // parse the CLI arguments into a list if input-output tuples
        List<Tuple<string, string>>? inputOutputTupleList = CliManager.GenerateInputOutputTupleList(args);

        if (inputOutputTupleList == null)
        {
            Console.WriteLine("\nprocess terminated doe to the previous errors.");
            return;
        }

        foreach (Tuple<string, string> tuple in inputOutputTupleList)
        {
            string inputPath = tuple.Item1;
            string outputPath = tuple.Item2;
            
            // start the conversion process
            Epub2FvmlEngine conversionEngine = new Epub2FvmlEngine(inputPath);
            var splits = conversionEngine.Epub2Fvml();
            
            // write the documents
            foreach (string split in splits)
            {
                string filepath = Path.Combine(outputPath, $"{splits.IndexOf(split)}.md");

                try
                {
                    File.WriteAllText(filepath, split);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return;
                }
            }
            
            Console.WriteLine("exported converted ebook " + Path.GetFileNameWithoutExtension(inputPath) + " to: " + outputPath);
        }
    }
}