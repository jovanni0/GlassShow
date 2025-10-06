using GlassShow.Core.Epub2FVML;

namespace Crawler.UnitTests;

public class ReadonlyStringWithTagsTests
{
    #region TAG_UNWRAPPING
    [Fact]
    public void TagUnwrapping_NoTag()
    {
        string originalWord = "abracadabra.";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(originalWord);

        string unwrappedWord = textWord.Text;

        Assert.Equal(originalWord, unwrappedWord);
    }

    [Fact]
    public void TagUnwrapping_TagOutside()
    {
        string originalWord = "<i>abracadabra.</i>";
        string expectedWord = "abracadabra.";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(originalWord);

        string unwrappedWord = textWord.Text;

        Assert.Equal(expectedWord, unwrappedWord);
    }

    [Fact]
    public void TagUnwrapping_TagInside()
    {
        string originalWord = "<i>abracadabra</i>.";
        string expectedWord = "abracadabra.";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(originalWord);

        string unwrappedWord = textWord.Text;

        Assert.Equal(expectedWord, unwrappedWord);
    }

    [Fact]
    public void TagUnwrapping_TagMoreInside()
    {
        string originalWord = "<i>abr<i>acad</i>abra</i>.";
        string expectedWord = "abracadabra.";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(originalWord);

        string unwrappedWord = textWord.Text;

        Assert.Equal(expectedWord, unwrappedWord);
    }

    [Fact]
    public void TagUnwrapping_TouchingTgas()
    {
        string originalWord = "<i><i>abr</i></i>.";
        string expectedWord = "abr.";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(originalWord);

        string unwrappedWord = textWord.Text;

        Assert.Equal(expectedWord, unwrappedWord);
    }

    [Fact]
    public void TagUnwrapping_OnlyTags()
    {
        string originalWord = "<i><i></i></i>";
        string expectedWord = "";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(originalWord);

        string unwrappedWord = textWord.Text;

        Assert.Equal(expectedWord, unwrappedWord);
    }
    #endregion


    #region TAG_INSERTING
    [Fact]
    public void TagInserting_NoTag()
    {
        string originalWord = "abracadabra.";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(originalWord);

        string word = textWord.ToString();

        Assert.Equal(originalWord, word);
    }

    [Fact]
    public void TagInserting_TagOutside()
    {
        string originalWord = "<i>abracadabra.</i>";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(originalWord);

        string word = textWord.ToString();

        Assert.Equal(originalWord, word);
    }

    [Fact]
    public void TagInserting_TagInside()
    {
        string originalWord = "<i>abracadabra</i>.";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(originalWord);

        string word = textWord.ToString();

        Assert.Equal(originalWord, word);
    }

    [Fact]
    public void TagInserting_MoreTagInside()
    {
        string originalWord = "<i>abrac</i>ad<i>abra</i>.";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(originalWord);

        string word = textWord.ToString();

        Assert.Equal(originalWord, word);
    }


    [Fact]
    public void TagInserting_TouchingTgas()
    {
        string originalWord = "<i><i>abr</i></i>.";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(originalWord);

        string word = textWord.ToString();

        Assert.Equal(originalWord, word);
    }

    [Fact]
    public void TagInserting_OnlyTags()
    {
        string originalWord = "<i><b></b></i>";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(originalWord);

        string word = textWord.ToString();

        Assert.Equal(originalWord, word);
    }
    #endregion


    #region TAG_SHIFTING
    [Fact]
    public void TagShifting_NoTag()
    {
        string original = "abracadabra";
        string expected = "abracadabra.";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(original);

        textWord.InsertIntoString('.', original.Length);
        string resault = textWord.ToString();

        Assert.Equal(expected, resault);
    }

    [Fact]
    public void TagShifting_NoTagAndIndex2Big()
    {
        string original = "abracadabra";
        string expected = "abracadabra";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(original);

        textWord.InsertIntoString('.', original.Length+1);
        string resault = textWord.ToString();

        Assert.Equal(expected, resault);
    }

    [Fact]
    public void TagShifting_NoTagAndIndex2Small()
    {
        string original = "abracadabra";
        string expected = "abracadabra";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(original);

        textWord.InsertIntoString('.', -2);
        string resault = textWord.ToString();

        Assert.Equal(expected, resault);
    }

    [Fact]
    public void TagShifting_TagsOutside_CharInsertBetween_ForceShift()
    {
        string original = "<i>abracadabra</i>";
        string withoutTags = "abracadabra";
        string expected = "<i>abra0cadabra</i>";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(original);

        textWord.InsertIntoString('0', 4);
        string resault = textWord.ToString();

        Assert.Equal(expected, resault);
    }

    [Fact]
    public void TagShifting_TagsOutside_CharInsertBetween_AvoidShift()
    {
        string original = "<i>abracadabra</i>";
        string withoutTags = "abracadabra";
        string expected = "<i>abra0cadabra</i>";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(original);

        textWord.InsertIntoString('0', 4, InsertType.AvoidTagShift);
        string resault = textWord.ToString();

        Assert.Equal(expected, resault);
    }

    [Fact]
    public void TagShifting_TagsOutside_CharInsertOutside_ForceShift()
    {
        string original = "<i>a</i>";
        string withoutTags = "a";
        string expected = "<i>a?</i>";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(original);

        textWord.InsertIntoString('?', withoutTags.Length);
        string resault = textWord.ToString();

        Assert.Equal(expected, resault);
    }

    [Fact]
    public void TagShifting_TagsOutside_CharInsertOutside_AvoidShift()
    {
        string original = "<i>abr</i>";
        string withoutTags = "abr";
        string expected = "<i>abr</i>?";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(original);

        textWord.InsertIntoString('?', withoutTags.Length, InsertType.AvoidTagShift);
        string resault = textWord.ToString();

        Assert.Equal(expected, resault);
    }

    [Fact]
    public void TagShifting_TagsOutside_CharInsertAtBegining_ForceShift()
    {
        string original = "<i>abracadabra</i>";
        string expected = "-<i>abracadabra</i>";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(original);

        textWord.InsertIntoString('-', 0);
        string resault = textWord.ToString();

        Assert.Equal(expected, resault);
    }

    /// <summary>
    /// E001: appropriate index value check.
    /// </summary>
    [Fact]
    public void TagUnwrapping_IndexError()
    {
        string originalWord = ">.";
        ReadonlyStringWithTags textWord = new ReadonlyStringWithTags(originalWord);

        string unwrappedWord = textWord.Text;

        Assert.Equal(originalWord, unwrappedWord);
    }
    #endregion
}
