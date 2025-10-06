using GlassShow.Core.Epub2FVML;
using GlassShow.Core.EpubExtractorElements;
using VersOne.Epub;

namespace GlassShow.Core;

public class Epub2FvmlEngine
{
    private List<TextDocument> _children = new();
    private EpubExtractor _epubExtractor;
    private List<List<string>> _replacements = [
        [ " ", " "], // non break space
        [ "—", " -- " ],
        [ "-- ”", "--.”" ],
        [ "–", " -- " ],
        [ "  ", " " ],
        [ " . . . .", "…." ],
        [ " . . .", "…" ],
        [ "....", "…." ],
        [ "...", "…" ],
        [ "… ”", "….”" ],
        [ "…”", "….”" ],
        [ " </i>", "</i> "], // HOT FIX: end tags tend to migrate over the space. should pe a separate pre-processor step
        [ " </b>", "</b> "]
    ];
    

    /// <summary>
    /// make all the replacements in the `_replacements` list.
    /// </summary>
    /// <param name="text">the text to have the replacements done.</param>
    /// <returns>the text with replacements done.</returns>
    private string FvmlPreparations(string text)
    {
        string newText = text;

        foreach (List<string> elem in _replacements)
        {
            newText = newText.Replace(elem[0], elem[1]);
        }

        return newText;
    }

    
    /// <summary>
    /// split the epub into children `TextDocument`.
    /// </summary>
    private void Split2Children()
    {
        List<string> splitsMarkdown = _epubExtractor.GetSplitsAsPseudoMarkdown();

        foreach (string split in splitsMarkdown)
        {
            string processedSplit = FvmlPreparations(split);
            
            TextDocument sdbsTextDocument = new(processedSplit);
            sdbsTextDocument.Parse2Fvml();
            _children.Add(sdbsTextDocument);
        }
    }

    
    //###########################################
    //                  PRIVATE
    //###########################################
    

    public Epub2FvmlEngine(string pathToEpub)
    {
        EpubBook epubBook = null;

        try
        {
            epubBook = EpubReader.ReadBook(pathToEpub);
        }
        catch (Exception e)
        {
            Console.WriteLine($"exception while opening the EPUB:\n{e}");
        }

        if (epubBook == null)
        {
            return;
        }

        _epubExtractor = new(epubBook);
        
        Split2Children();
    }
    
    
    //###########################################
    //                  PUBLIC
    //###########################################
    
    
    /// <summary>
    /// convert the epub at the provided path in the constructor into a list of parsed documents.
    /// </summary>
    /// <returns>a list of parsed documents.</returns>
    public List<string> Epub2Fvml()
    {
        List<string> splitsSdbs = new();
        foreach (TextDocument doc in _children)
        {
            doc.Parse2Fvml();
            splitsSdbs.Add(doc.ToString());
        }
        return splitsSdbs;
    }
    
    
    /// <summary>
    /// wrap the document with debug simbols in a html structure.
    /// </summary>
    /// <returns>a list of strings representing html documents.</returns>
    public List<string> GetDebugTaceAsHtmlDocuments()
    {
        List<string> htmlDocuments = new();
        List<string> documents = Epub2Fvml();

        foreach (string document in documents)
        {
            string[] paras = document.Split("\n\n");
    
            string body = string.Join("\n\n", paras.Select(x => $"<p>{x}</p>").ToList());
            string html = $"<html><head></head><body>{body}</body></html>";
            
            htmlDocuments.Add(html);
        }
        
        return htmlDocuments;
    }


    /// <summary>
    /// generate a list of documents with debug symbols.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public List<string> GetDebugTrace()
    {
        throw new NotImplementedException();
    }
}
