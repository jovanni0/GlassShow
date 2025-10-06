namespace GlassShow.Core.Epub2FVML;

/// <summary>
/// represents a string with XML tags as part of it, not only on the outside.
/// eg: <i>Martin</i>'s
///     <b>dumb</b>.”
///     
/// this object is expected to preserve the position on the XML tags while the text
/// is processed and altered.
/// 
/// allows insertion on characters in the non-XML parts of the string.
/// the behaviour when inserting is dependent on the `InsertType`, which regulates 
/// that happens if the inserted char overlaps with a tag:
/// - `ForceTag2Shift` will shift the tag to the right and insert the char before it;
/// - `AvoidTagShift` will keep the tag in place and insert the char after it;
/// </summary>
public class ReadonlyStringWithTags
{
    private List<Tag> _tags = new List<Tag>();


    /// <summary>
    /// remove the XML-like tags from inside the string and preserve them in the `_tags` list.
    /// </summary>
    /// <param name="word">the string to have the tags removed</param>
    /// <returns>the string with no tags in it</returns>
    private string UnwrapTags(string word)
    {
        int index = 0;
        int startIndex = -1;
        int endIndex = -1;
        while (index < word.Length && word != string.Empty)
        {
            if (word[index] == '<' && startIndex == -1)
            {
                startIndex = index;
                index++;
                continue;
            }
            // E001: make sure that the start index has a valid value.
            // ex: `>.` will generate error because the start index will be `-1`.
            if (word[index] == '>' && startIndex > -1)
            {
                endIndex = index;

                Tag tag = new Tag()
                {
                    TagName = word.Substring(startIndex, endIndex - startIndex + 1),
                    StartIndex = startIndex
                };
                _tags.Add(tag);

                word = word.Substring(0, startIndex) + word.Substring(endIndex + 1, word.Length - endIndex - 1);

                startIndex = -1;
                endIndex = -1;
                index = 0;
                continue;
            }

            index++;
        }

        return word;
    }


    private string WrapTags(string word)
    {
        List<Tag> reversedTags = _tags.AsEnumerable().Reverse().ToList();

        foreach (Tag tag in reversedTags)
        {
            word = word.Insert(tag.StartIndex, tag.TagName);
        }

        return word;
    }
    

    public ReadonlyStringWithTags(string text)
    {
        Text = UnwrapTags(text);
    }

    public string Text { get; private set; } = string.Empty;

    
    public void ToLower()
    {
        Text = Text.ToLower();
    }


    /// <summary>
    /// insert the specified string in the specified position
    /// </summary>
    /// <param name="insertedString">string to be inserted</param>
    /// <param name="insertStartIndex">start position to be inserted in</param>
    public void InsertIntoString(string insertedString, int insertStartIndex, InsertType insertType = InsertType.ForceTag2Shift)
    {
        for (int index = 0; index < insertedString.Length; index++)
        {
            InsertIntoString(insertedString[index], insertStartIndex + index, insertType);
        }
    }


    /// <summary>
    /// insert the specified character in the specified position
    /// </summary>
    /// <param name="insertedChar">character to be inserted</param>
    /// <param name="insertStartIndex">position to be inserted in</param>
    public void InsertIntoString(char insertedChar, int insertStartIndex, InsertType insertType = InsertType.ForceTag2Shift)
    {
        int wordLength = Text.Length;

        if (insertStartIndex < 0 || insertStartIndex > wordLength) { return; }

        Text = Text.Insert(insertStartIndex, insertedChar.ToString());

        foreach (Tag tag in _tags)
        {
            if (tag.StartIndex == insertStartIndex && insertType != InsertType.AvoidTagShift) { tag.StartIndex++; }
            else if (tag.StartIndex > insertStartIndex) { tag.StartIndex++; }
        }
    }


    /// <summary>
    /// insert the tags from the original word onto any other string with the same length.
    /// </summary>
    /// <param name="text">the string to have the tags inserted into.</param>
    /// <returns>the provided string with tags inserted into it.</returns>
    /// <exception cref="Exception">the length of the original string and the given string do not match.</exception>
    public string ImpressTagsOnto(string text)
    {
        if (text.Length != Text.Length)
        {
            throw new Exception("length of strings is not identical.");
        }

        return WrapTags(text);
    }


    /// <summary>
    /// returns the complete string, including the XML-like tags
    /// </summary>
    /// <returns>the string with the tags.</returns>
    public override string ToString() => WrapTags(Text);
}


public enum InsertType
{
    ForceTag2Shift,
    AvoidTagShift
}
