using System.Text;

namespace GlassShow.Core.Epub2FVML;

public class TextWord : ITextUnit
{
    private static readonly List<char> _emotionalPunctuation = ['?', '!']; // removed '…'
    private static readonly List<char> _headComponents = ['\'', '"', '‘', '“', '('];
    private static readonly List<char> _tailComponents = ['\'', '"', '’', '”', '…', ')', '?', '!', '.', ',', ':', ';'];
    private static readonly List<char> _speechComponents = ['“', '”', '"', '\''];
    private static readonly List<char> _hiddenSentanceEnd = [':', '?', '…', '!'];

    private ReadonlyStringWithTags _word;


    public string UnwrappedWord => _word.Text;
    public string WrappedWord => _word.ToString();
    public string OriginalWord { get; private set; } = string.Empty;

    /// <summary>
    /// a list in order of the operations done on the word
    /// </summary>
    private List<TextWordOperations> _operations = new List<TextWordOperations>();

    /// <summary>
    /// a list in order of all the markers added to the word
    /// </summary>
    private List<TextWordMarkers> _markers = new List<TextWordMarkers>();


    //
    // because this bit of processing happens without us knowing the type of word this
    // is (name on not), we need to account for different possibilities.
    // 
    // for example, if the word is a name ending with "s" and is used as a possessive,
    // the "’" at the end should not be converted.
    //
    // there can be possessives that are not names, so you need to check for signs of sentence end,
    // meaning that the tail has to contain EP or comma / period to be speech. if none are found,
    // it is assumed to be possessive and ignore it.
    //
    private string ConvertUkSpeech2International(string text)
    {
        string head = ExtractHead(text);
        string body = ExtractBody(text);
        string tail = ExtractTail(text);
        
        bool isNameCase = char.IsUpper(body.FirstOrDefault());
        bool hasSscq = head.Contains('‘');

        // check if the char before the ESCQ is a trailing "s" 
        int quoteIndex = tail.IndexOf('’');
        int charBeforeEsqcIndex = (head + body).Length - 1 + quoteIndex;
        char charBeforeEsqc = '\0';
        if (quoteIndex > -1 && charBeforeEsqcIndex > -1) // make sure the index calculations are valid
        {
            charBeforeEsqc = text[charBeforeEsqcIndex];    
        }
        bool isPosesive = charBeforeEsqc == 's';

        // there is a period / comma in the tail, check the position relative to the end quote
        int sentanceEndIndex = tail.LastIndexOfAny(['…', '.', ',']);
        bool isSentanceEnd = sentanceEndIndex < quoteIndex;

        if (isPosesive)
        {
            // the word has a SSCQ and a ESCQ but the char before the ESCQ is a "s"
            // convert the SSCQ to SDCQ but ignore the ESCQ 
            if (hasSscq)
            {
                head = head.Replace('‘', '“');
            }
            
            // else leave the ESCQ as is
            // there cannot be both possessive and sentence end
        }
        else
        {
            if (isSentanceEnd) // has a period before the "’"
            {
                // if the period if before the ESCQ and it also has SSCQ, then is probably speech 
                // so replace both SSCQ and ESQC with SDCQ and EDQC. 
                if (hasSscq)
                {
                    tail = tail.ReplaceAt(quoteIndex, '”');
                    head = head.Replace('‘', '“');
                }
                // if there is no SSCQ then the word is end of speech, so replace the ESCQ with EDCQ. 
                else
                {
                    tail = tail.ReplaceAt(quoteIndex, '”');
                }
            }
            else if (hasSscq)
            {
                // if the word is name-case and has a SSCQ, but it is not the end of a sentence or 
                // the possessive form of a noun, then it is the start of a sentence
                if (isNameCase)
                {
                    head = head.Replace('‘', '“');
                }
                // if not name-case and not sentence end, then is the start of a quotation
                else
                {
                    head = head.Replace('‘', '"');
                }
            }
            else
            {
                // there is only the ESCQ, so assume is the end of a quotation
                tail = tail.ReplaceAt(quoteIndex, '"');    
            }
        }
        
        return head + body + tail;
    }


    public TextWord(string word, List<TextWordOptions>? options = null)
    {
        string unwrapped = new ReadonlyStringWithTags(word).Text;
        
        if (options != null)
        {
            foreach (TextWordOptions option in options)
            {
                switch (option)
                {
                    // convert the single-angled-quote british speech tags to double-angled-quotes
                    case TextWordOptions.ConvertUkSpeech2International:
                        unwrapped = ConvertUkSpeech2International(unwrapped);
                        break;
                }
            }
        }

        string rewrapped = new ReadonlyStringWithTags(word).ImpressTagsOnto(unwrapped);

        _word = new ReadonlyStringWithTags(rewrapped);
        OriginalWord = word;
    }

    /// <summary>
    /// a list of all the operations made, in order, as a read-only enumerable
    /// </summary>
    public IEnumerable<TextWordOperations> Operations => _operations.AsReadOnly();


    /// <summary>
    /// a list of all markers added, in order, as a read-only enumerable
    /// </summary>
    public IEnumerable<TextWordMarkers> Markers => _markers.AsReadOnly();


    /// <summary>
    /// add a `TextWordOperations` to the trace if not already there.
    /// </summary>
    /// <param name="operation">the operation to be added to the trace</param>
    public bool AddOperation(TextWordOperations operation)
    {
        if (_operations.Contains(operation)) return false;

        _operations.Add(operation);

        return true;
    }


    /// <summary>
    /// add a `TextWordMarkers` if not already added
    /// </summary>
    /// <param name="marker">the marker to be added</param>
    /// <returns>`False` if the marker already exists, else `True`</returns>
    public bool AddMarker(TextWordMarkers marker)
    {
        if (_markers.Contains(marker)) return false;

        _markers.Add(marker);

        return true;
    }


    /// <summary>
    /// check if the word's tail (without the formatting) contains any EP markers
    /// </summary>
    /// <returns>a boolean result indicating if the word ends in emotional punctuation</returns>
    public bool TailHasEPMarkers() => _emotionalPunctuation.Any(x => GetTail().Contains(x));


    /// <summary>
    /// checks if the word is the end of a sentence (tail has a period)
    /// </summary>
    /// <returns>`True` if there is a period end in the tail, else `False`</returns>
    public bool IsSentenceEnd() => GetTail().Contains('.');


    /// <summary>
    /// checks if the word might be the end of sentance, if it contains any of [ : ? ! … ]
    /// </summary>
    /// <returns>`True` if it might be, else `False`</returns>
    public bool MightBeSentenceEnd() => _hiddenSentanceEnd.Any(x => GetTail().Contains(x));


    /// <summary>
    /// checks if the word is the start of a sentance, if it contains any of [ “, ”, ", ' ]
    /// </summary>
    /// <returns>`True` if it is start of a sentance, else `False`</returns>
    public bool MightBeSentenceStart() => _speechComponents.Any(x => GetHead().Contains(x));


    /// <summary>
    /// checks if all alphabetical characters are lower
    /// </summary>
    /// <returns>`True` if all alpha chars are lower, else `False`</returns>
    public bool HasAllAlphaLower() => GetBody().Where(char.IsLetter).All(char.IsLower);


    /// <summary>
    /// convert the word to lowercase if necessarily
    /// </summary>
    public void Normalize(HashSet<string> names) => _word.ToLower();




    /////// tested

    /// <summary>
    /// the head of the word at the current processing stage
    /// </summary>
    public string GetHead() => ExtractHead(UnwrappedWord);

    /// <summary>
    /// the body of the word at the current processing stage
    /// </summary>
    public string GetBody() => ExtractBody(UnwrappedWord);

    /// <summary>
    /// the tail of the word at the current processing stage
    /// </summary>
    public string GetTail() => ExtractTail(UnwrappedWord);


    /// <summary>
    /// extracts the head from the given word
    /// </summary>
    /// <param name="word">the word to be processed</param>
    /// <returns>a string representing the head of the word</returns>
    private string ExtractHead(string word)
    {
        StringBuilder stringBuilder = new();

        foreach (char elem in word)
        {
            // stop at the first char that is not part of the head
            if (!_headComponents.Contains(elem)) break;

            stringBuilder.Append(elem);
        }

        return stringBuilder.ToString();
    }


    /// <summary>
    /// returns the body of the given word
    /// </summary>
    /// <param name="word">the word to be processed</param>
    /// <returns>a string representing the body of the word</returns>
    private string ExtractBody(string word)
    {
        string head = ExtractHead(word);
        string tail = ExtractTail(word);
        string body = string.Empty;
        
        int bodyLength = word.Length - head.Length - tail.Length;

        if (bodyLength > -1)
        {
            body = word.Substring(head.Length, bodyLength);            
        }

        return body;
    }


    /// <summary>
    /// returns the tail of the given word
    /// </summary>
    /// <param name="word">the word to be processed</param>
    /// <returns>a string representing the tail of the word</returns>
    private string ExtractTail(string word)
    {
        StringBuilder stringBuilder = new();

        foreach (char elem in word.Reverse())
        {
            // stop at the first char that is not part of the tail
            if (!_tailComponents.Contains(elem)) break;

            stringBuilder.Insert(0, elem);
        }

        return stringBuilder.ToString();
    }

    public override string ToString() => WrappedWord;

    /// <summary>
    /// inserts a period (sentence end) if the word does not contain one
    /// </summary>
    public void InsertPeriod()
    {
        InsertSentenceStabilizer('.');
    }

    /// <summary>
    /// insert a comma if the word does not contain one
    /// </summary>
    public void InsertComma()
    {
        InsertSentenceStabilizer(',');
    }


    /// <summary>
    /// inserts a sentence stabilizer in the most likely spot, based
    /// on the context of the word.
    /// 
    /// sentence stabilizers are `[ . , ]` and mark the end of a sentence (`.`)
    /// or separator for clauses, lists, ideas.
    /// </summary>
    /// <param name="character">character to be inserted</param>
    private void InsertSentenceStabilizer(char character)
    {
        string word = UnwrappedWord;

        // ignore if the char already exist
        if (word.Contains(character)) return;

        for (int index = word.Length - 1; index >= 0; index--)
        {
            char currentCharacter = word[index];

            // check up to the first letter
            if (char.IsAsciiLetter(currentCharacter)) { break; }
            
            // insert after the last  EP character
            if (_emotionalPunctuation.Contains(currentCharacter)) 
            {
                _word.InsertIntoString(character, index + 1);
                return;
            }

            // insert before the last speech component
            if (_speechComponents.Contains(currentCharacter))
            {
                _word.InsertIntoString(character, index);
                return;
            }
        }

        //// prioritize insertion after the last EP
        //foreach (char item in _emotionalPunctuation)
        //{
        //    int index = word.LastIndexOf(item);

        //    if (index == -1) continue;

        //    _word.InsertIntoString(character, index + 1);

        //    return;
        //}

        //// check for sentence end, and insert before it
        //if (_speechComponents.Contains(word.Last()))
        //{
        //    int index = word.Length - 1;
        //    _word.InsertIntoString(character, index);

        //    return;
        //}

        // insert at the end of the word
        _word.InsertIntoString(character, word.Length);

        return;
    }
    
    public HashSet<string> GetNames() => new HashSet<string>();
    
}



/// <summary>
/// options that can be supplied to the TextWord for specific processing to occur
/// </summary>
public enum TextWordOptions
{
    // convert the single-angled-quote british speech tags to double-angled-quotes
    ConvertUkSpeech2International,
    // add debug tags when using the `ToString` method
    EnableDebug
}



/// <summary>
/// operations that can be applied to the word
/// </summary>
public enum TextWordOperations
{
    // a period has been inserted in the word
    InsertedSentenceEnd,
    // a comma has been inserted in the word
    InsertedComma,
    // the word has been converted to lowercase
    Normalized
}



/// <summary>
/// markers that can be applied to the word
/// </summary>
public enum TextWordMarkers
{
    // the word in the first time this name has been encountered
    FirstName
}