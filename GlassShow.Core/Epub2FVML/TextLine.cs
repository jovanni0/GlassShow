using System.Drawing;

namespace GlassShow.Core.Epub2FVML;

public class TextLine : ANestedTextUnit<TextWord>
{
    protected override string ChildrenSeparator => " ";

    public TextLine()
    {
        SpecialWords = new()
        {
            "said",
            "asked",
        };
    }
    
    
    /// <summary>
    /// split the line into word objects, delimitated by a newline character `\n`.
    /// </summary>
    /// <param name="line">the line to be split.</param>
    private void Split2Words(string line)
    {
        string[] words = line.Split(" ");

        foreach (string item in words)
        {
            string word = item.Trim();

            if (string.IsNullOrEmpty(word)) { continue; }

            List<TextWordOptions> options = new()
            {
                TextWordOptions.ConvertUkSpeech2International
            };
            
            TextWord textWord = new(word, options);
            Children.Add(textWord);
        }
    }


    /// <summary>
    /// iterates trough each word and inserts a period where required
    /// </summary>
    private List<TextWord> InsertSentenceEnds(List<TextWord> words, HashSet<string>? names = null)
    {
        List<TextWord> newWords = new();

        for (int index = 0; index < words.Count; index++)
        {
            TextWord originalTextWord = words[index];
            TextWord newTextWord = new(originalTextWord.WrappedWord);
            newWords.Add(newTextWord);

            // ignore words without EP
            if (!newTextWord.TailHasEPMarkers()) { continue; }
            
            // ignore words that are sentance ends
            if (newTextWord.IsSentenceEnd()) { continue; }

            if (index < Children.Count - 1)
            {
                TextWord nextTextWord = words[index + 1];

                // insert comma if next is all lowercase 
                if (nextTextWord.HasAllAlphaLower())
                {
                    newTextWord.InsertComma();
                    continue;
                }

                // this does not work, as most of the time the name does not indicate that the sentence is continued.
                //// insert comma if next is a name
                //if (names != null && names.Contains(nextTextWord.UnwrappedWord))
                //{
                //    newTextWord.InsertComma();
                //    continue;
                //}

                // check if any word ahead is in the `SpecialWords`; assume the current word is speech end, but not 
                // sentence end
                if (LookAhead(index + 1, 3))
                {
                    newTextWord.InsertComma();
                    continue;
                }
            }

            newTextWord.InsertPeriod();
        }

        return newWords;
    }

    
    /// <summary>
    /// check if any of the words inside the `[startIndex; startIndex+visionRange]` is found in the
    /// `SpecialWords` list.
    /// </summary>
    /// <param name="startIndex">the index of the first word to be checked</param>
    /// <param name="visionRange">the length of the interval to be considered</param>
    /// <returns>`True` if any of the words in the range is in the `SpecialWords`, else `False`</returns>
    private bool LookAhead(int startIndex, int visionRange)
    {
        int endIndex = int.Min(Children.Count - 1, startIndex + visionRange);

        for (int index = startIndex; index <= endIndex; index++)
        {
            TextWord textWord = Children[index];
            string textWordBody = textWord.GetBody();

            if (SpecialWords.Contains(textWordBody)) return true;
        }

        return false;
    }

    
    //###########################################
    //                  PRIVATE
    //###########################################
    
    
    /// <summary>
    /// creates a new TextLine object, representing a string of characters
    /// upto the first newline character `\n`.
    /// </summary>
    /// <param name="lineText">the string representing the line.</param>
    public TextLine(string lineText)
    {
        Original = lineText;
        Split2Words(lineText);
    }
    
    
    //###########################################
    //                  PUBLIC
    //###########################################


    public override HashSet<string> GetNames()
    {
        List<TextWord> words = InsertSentenceEnds(Children);
        HashSet<string> names = new();

        for (int index = 1; index < words.Count; index++)
        {
            TextWord currentTextWord = words[index];

            // ignore lowercase words
            if (currentTextWord.HasAllAlphaLower()) { continue; }

            TextWord prevWord = words[index - 1];

            // ignore words at sentance start
            if (prevWord.IsSentenceEnd() || currentTextWord.MightBeSentenceStart()) { continue; }

            // ignore potential sentance start
            if (prevWord.MightBeSentenceEnd()) { continue; }
            
            string name = currentTextWord.GetBody();
            names.Add(name);
        }

        return names;
    }


    public override void Normalize(HashSet<string> names)
    {
        // insert SEP where necessary and override the original
        Children = InsertSentenceEnds(Children, names);

        foreach (TextWord word in Children)
        {
            string body = word.GetBody();

            if (names.Contains(body)) { continue; }

            word.Normalize(names);
        }
    }
}
