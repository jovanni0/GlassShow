using GlassShow.Core.Epub2FVML;

namespace GlassShow.UnitTest;

public class TextWordTests
{
    #region INSERT_PERIOD

    // -- only EPs -- //
    [Fact]
    public void InsertPeriod_NoEP_NoTag()
    {
        string original = "testing";
        string expected = "testing.";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_QuestionEP_NoTag()
    {
        string original = "testing?";
        string expected = "testing?.";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_ExclamationEP_NoTag()
    {
        string original = "testing!";
        string expected = "testing!.";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_EllipsesEP_NoTag()
    {
        string original = "testing…";
        string expected = "testing….";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_WithPeriod_NoTag()
    {
        string original = "testing.";
        string expected = "testing.";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_InterroBang_NoTag()
    {
        string original = "testing?!";
        string expected = "testing?!.";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_EllipsesQuestion_NoTag()
    {
        string original = "testing…?";
        string expected = "testing…?.";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_EllipsesExclamation_NoTag()
    {
        string original = "testing…!";
        string expected = "testing…!.";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    // -- EPs and Speech -- //
    [Fact]
    public void InsertPeriod_Speech_NoTag()
    {
        string original = "testing”";
        string expected = "testing.”";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_QuestionEPSpeech_NoTag()
    {
        string original = "testing?”";
        string expected = "testing?.”";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_ExclamationEPSpeech_NoTag()
    {
        string original = "testing!”";
        string expected = "testing!.”";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_EllipsesEPSpeech_NoTag()
    {
        string original = "testing…”";
        string expected = "testing….”";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_WithPeriodSpeech_NoTag()
    {
        string original = "testing.”";
        string expected = "testing.”";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_InterroBangSpeech_NoTag()
    {
        string original = "testing?!”";
        string expected = "testing?!.”";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_EllipsesQuestionSpeech_NoTag()
    {
        string original = "testing…?”";
        string expected = "testing…?.”";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_EllipsesExclamationSpeech_NoTag()
    {
        string original = "testing…!”";
        string expected = "testing…!.”";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    // -- EPs and quote -- //
    [Fact]
    public void InsertPeriod_NoEPQuote_NoTag()
    {
        string original = "testing\"";
        string expected = "testing.\"";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_QuestionEPQuote_NoTag()
    {
        string original = "testing?\"";
        string expected = "testing?.\"";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_ExclamationEPQuote_NoTag()
    {
        string original = "testing!\"";
        string expected = "testing!.\"";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_EllipsesEPQuote_NoTag()
    {
        string original = "testing…\"";
        string expected = "testing….\"";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_WithPeriodQuote_NoTag()
    {
        string original = "testing.\"";
        string expected = "testing.\"";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_InterroBangQuote_NoTag()
    {
        string original = "testing?!\"";
        string expected = "testing?!.\"";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_EllipsesQuestionQuote_NoTag()
    {
        string original = "testing…?\"";
        string expected = "testing…?.\"";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_EllipsesExclamationQuote_NoTag()
    {
        string original = "testing…!\"";
        string expected = "testing…!.\"";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    // -- only EPs with Tag-- //
    [Fact]
    public void InsertPeriod_NoEP_WithTag()
    {
        string original = "testing</i>";
        string expected = "testing.</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_QuestionEP_WithTag()
    {
        string original = "testing?</i>";
        string expected = "testing?.</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_ExclamationEP_WithTag()
    {
        string original = "testing!</i>";
        string expected = "testing!.</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_EllipsesEP_WithTag()
    {
        string original = "testing…</i>";
        string expected = "testing….</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_WithPeriod_WithTag()
    {
        string original = "testing.</i>";
        string expected = "testing.</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_InterroBang_WithTag()
    {
        string original = "testing?!</i>";
        string expected = "testing?!.</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_EllipsesQuestion_WithTag()
    {
        string original = "testing…?</i>";
        string expected = "testing…?.</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_EllipsesExclamation_WithTag()
    {
        string original = "testing…!</i>";
        string expected = "testing…!.</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    // -- EPs and Speech with Tag -- //
    [Fact]
    public void InsertPeriod_Speech_WithTag()
    {
        string original = "testing”</i>";
        string expected = "testing.”</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_QuestionEPSpeech_WithTag()
    {
        string original = "testing?”</i>";
        string expected = "testing?.”</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_ExclamationEPSpeech_WithTag()
    {
        string original = "testing!”</i>";
        string expected = "testing!.”</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_EllipsesEPSpeech_WithTag()
    {
        string original = "testing…”</i>";
        string expected = "testing….”</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_WithPeriodSpeech_WithTag()
    {
        string original = "testing.”</i>";
        string expected = "testing.”</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_InterroBangSpeech_WithTag()
    {
        string original = "testing?!”</i>";
        string expected = "testing?!.”</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_EllipsesQuestionSpeech_WithTag()
    {
        string original = "testing…?”</i>";
        string expected = "testing…?.”</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_EllipsesExclamationSpeech_WithTag()
    {
        string original = "testing…!”</i>";
        string expected = "testing…!.”</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    // -- EPs and qoute with Tag -- //
    [Fact]
    public void InsertPeriod_NoEPQoute_WithTag()
    {
        string original = "testing\"</i>";
        string expected = "testing.\"</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_QuestionEPQuote_WithTag()
    {
        string original = "testing?\"</i>";
        string expected = "testing?.\"</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_ExclamationEPQuote_WithTag()
    {
        string original = "testing!\"</i>";
        string expected = "testing!.\"</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_EllipsesEPQuote_WithTag()
    {
        string original = "testing…\"</i>";
        string expected = "testing….\"</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_WithPeriodQuote_WithTag()
    {
        string original = "testing.\"</i>";
        string expected = "testing.\"</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_InterroBangQuote_WithTag()
    {
        string original = "testing?!\"</i>";
        string expected = "testing?!.\"</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_EllipsesQuestionQuote_WithTag()
    {
        string original = "testing…?\"</i>";
        string expected = "testing…?.\"</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertPeriod_EllipsesExclamationQuote_WithTag()
    {
        string original = "testing…!\"</i>";
        string expected = "testing…!.\"</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertPeriod();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }
    #endregion


    #region INSERT_COMMA

    // -- only EPs -- //
    [Fact]
    public void InsertComma_NoEP_NoTag()
    {
        string original = "testing";
        string expected = "testing,";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_QuestionEP_NoTag()
    {
        string original = "testing?";
        string expected = "testing?,";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_ExclamationEP_NoTag()
    {
        string original = "testing!";
        string expected = "testing!,";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_EllipsesEP_NoTag()
    {
        string original = "testing…";
        string expected = "testing…,";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_WithPeriod_NoTag()
    {
        string original = "testing,";
        string expected = "testing,";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_InterroBang_NoTag()
    {
        string original = "testing?!";
        string expected = "testing?!,";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_EllipsesQuestion_NoTag()
    {
        string original = "testing…?";
        string expected = "testing…?,";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_EllipsesExclamation_NoTag()
    {
        string original = "testing…!";
        string expected = "testing…!,";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    // -- EPs and Speech -- //
    [Fact]
    public void InsertComma_Speech_NoTag()
    {
        string original = "testing”";
        string expected = "testing,”";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_QuestionEPSpeech_NoTag()
    {
        string original = "testing?”";
        string expected = "testing?,”";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_ExclamationEPSpeech_NoTag()
    {
        string original = "testing!”";
        string expected = "testing!,”";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_EllipsesEPSpeech_NoTag()
    {
        string original = "testing…”";
        string expected = "testing…,”";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_WithPeriodSpeech_NoTag()
    {
        string original = "testing,”";
        string expected = "testing,”";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_InterroBangSpeech_NoTag()
    {
        string original = "testing?!”";
        string expected = "testing?!,”";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_EllipsesQuestionSpeech_NoTag()
    {
        string original = "testing…?”";
        string expected = "testing…?,”";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_EllipsesExclamationSpeech_NoTag()
    {
        string original = "testing…!”";
        string expected = "testing…!,”";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    // -- EPs and qoute -- //
    [Fact]
    public void InsertComma_NoEPQoute_NoTag()
    {
        string original = "testing\"";
        string expected = "testing,\"";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_QuestionEPQuote_NoTag()
    {
        string original = "testing?\"";
        string expected = "testing?,\"";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_ExclamationEPQuote_NoTag()
    {
        string original = "testing!\"";
        string expected = "testing!,\"";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_EllipsesEPQuote_NoTag()
    {
        string original = "testing…\"";
        string expected = "testing…,\"";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_WithPeriodQuote_NoTag()
    {
        string original = "testing,\"";
        string expected = "testing,\"";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_InterroBangQuote_NoTag()
    {
        string original = "testing?!\"";
        string expected = "testing?!,\"";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_EllipsesQuestionQuote_NoTag()
    {
        string original = "testing…?\"";
        string expected = "testing…?,\"";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_EllipsesExclamationQuote_NoTag()
    {
        string original = "testing…!\"";
        string expected = "testing…!,\"";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    // -- only EPs with Tag-- //
    [Fact]
    public void InsertComma_NoEP_WithTag()
    {
        string original = "testing</i>";
        string expected = "testing,</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_QuestionEP_WithTag()
    {
        string original = "testing?</i>";
        string expected = "testing?,</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_ExclamationEP_WithTag()
    {
        string original = "testing!</i>";
        string expected = "testing!,</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_EllipsesEP_WithTag()
    {
        string original = "testing…</i>";
        string expected = "testing…,</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_WithPeriod_WithTag()
    {
        string original = "testing,</i>";
        string expected = "testing,</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_InterroBang_WithTag()
    {
        string original = "testing?!</i>";
        string expected = "testing?!,</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_EllipsesQuestion_WithTag()
    {
        string original = "testing…?</i>";
        string expected = "testing…?,</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_EllipsesExclamation_WithTag()
    {
        string original = "testing…!</i>";
        string expected = "testing…!,</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    // -- EPs and Speech with Tag -- //
    [Fact]
    public void InsertComma_Speech_WithTag()
    {
        string original = "testing”</i>";
        string expected = "testing,”</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_QuestionEPSpeech_WithTag()
    {
        string original = "testing?”</i>";
        string expected = "testing?,”</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_ExclamationEPSpeech_WithTag()
    {
        string original = "testing!”</i>";
        string expected = "testing!,”</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_EllipsesEPSpeech_WithTag()
    {
        string original = "testing…”</i>";
        string expected = "testing…,”</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_WithPeriodSpeech_WithTag()
    {
        string original = "testing,”</i>";
        string expected = "testing,”</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_InterroBangSpeech_WithTag()
    {
        string original = "testing?!”</i>";
        string expected = "testing?!,”</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_EllipsesQuestionSpeech_WithTag()
    {
        string original = "testing…?”</i>";
        string expected = "testing…?,”</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_EllipsesExclamationSpeech_WithTag()
    {
        string original = "testing…!”</i>";
        string expected = "testing…!,”</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    // -- EPs and qoute with Tag -- //
    [Fact]
    public void InsertComma_NoEPQoute_WithTag()
    {
        string original = "testing\"</i>";
        string expected = "testing,\"</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_QuestionEPQuote_WithTag()
    {
        string original = "testing?\"</i>";
        string expected = "testing?,\"</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_ExclamationEPQuote_WithTag()
    {
        string original = "testing!\"</i>";
        string expected = "testing!,\"</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_EllipsesEPQuote_WithTag()
    {
        string original = "testing…\"</i>";
        string expected = "testing…,\"</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_WithPeriodQuote_WithTag()
    {
        string original = "testing,\"</i>";
        string expected = "testing,\"</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_InterroBangQuote_WithTag()
    {
        string original = "testing?!\"</i>";
        string expected = "testing?!,\"</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_EllipsesQuestionQuote_WithTag()
    {
        string original = "testing…?\"</i>";
        string expected = "testing…?,\"</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void InsertComma_EllipsesExclamationQuote_WithTag()
    {
        string original = "testing…!\"</i>";
        string expected = "testing…!,\"</i>";
        TextWord textWord = new TextWord(original);

        textWord.InsertComma();
        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }
    #endregion


    #region HEAD_DISSECTION
    [Fact]
    public void DisectHead_OnlyBody()
    {
        string original = "hello";
        string expected = "";
        TextWord textWord = new(original);

        string result = textWord.GetHead();

        Assert.Equal(expected, result);
    }

    [Fact]
    public void DisectHead_1()
    {
        TextWord textWord = new("'hello");
        string head = textWord.GetHead();

        Assert.Equal("'", head);
    }

    [Fact]
    public void DisectHead_2()
    {
        TextWord textWord = new("“'really?'");
        string head = textWord.GetHead();

        Assert.Equal("“'", head);
    }

    [Fact]
    public void DisectHead_3()
    {
        TextWord textWord = new("“--");
        string head = textWord.GetHead();

        Assert.Equal("“", head);
    }

    [Fact]
    public void DisectHead_4()
    {
        TextWord textWord = new("“…");
        string head = textWord.GetHead();

        Assert.Equal("“", head);
    }

    [Fact]
    public void DisectHead_NoHead1()
    {
        TextWord textWord = new("h“llo");
        string head = textWord.GetHead();

        Assert.Equal("", head);
    }

    [Fact]
    public void DisectHead_NoHead2()
    {
        TextWord textWord = new("hll");
        string head = textWord.GetHead();

        Assert.Equal("", head);
    }

    #endregion


    #region TAIL_DISSECTION

    [Fact]
    public void DisectTail_2()
    {
        TextWord textWord = new("really?'”");
        string result = textWord.GetTail();

        Assert.Equal("?'”", result);
    }

    [Fact]
    public void DisectTail_3()
    {
        TextWord textWord = new("--”");
        string result = textWord.GetTail();

        Assert.Equal("”", result);
    }

    [Fact]
    public void DisectTail_4()
    {
        TextWord textWord = new("…”");
        string result = textWord.GetTail();

        Assert.Equal("…”", result);
    }

    [Fact]
    public void DisectTail_NoTail1()
    {
        TextWord textWord = new("he!!0");
        string result = textWord.GetTail();

        Assert.Equal("", result);
    }

    [Fact]
    public void DisectTail_NoTail2()
    {
        TextWord textWord = new("hello");
        string result = textWord.GetTail();

        Assert.Equal("", result);
    }

    [Fact]
    public void TailDissection_SingleStraightQuote()
    {
        TextWord textWord = new("hello'");
        string result = textWord.GetTail();

        Assert.Equal("'", result);
    }

    [Fact]
    public void TailDissection_DoubleStraightQuote()
    {
        TextWord textWord = new("hello\"");
        string result = textWord.GetTail();

        Assert.Equal("\"", result);
    }

    [Fact]
    public void TailDissection_ClosedSingleAngledQuote()
    {
        TextWord textWord = new("hello’");
        string result = textWord.GetTail();

        Assert.Equal("’", result);
    }

    [Fact]
    public void TailDissection_ClosedDoubleAngledQuote()
    {
        TextWord textWord = new("hello”");
        string result = textWord.GetTail();

        Assert.Equal("”", result);
    }

    [Fact]
    public void TailDissection_HorrizontalEllipses()
    {
        TextWord textWord = new("hello…");
        string result = textWord.GetTail();

        Assert.Equal("…", result);
    }

    [Fact]
    public void TailDissection_CloseParanthesis()
    {
        TextWord textWord = new("hello)");
        string result = textWord.GetTail();

        Assert.Equal(")", result);
    }

    [Fact]
    public void TailDissection_Questionmark()
    {
        TextWord textWord = new("hello?");
        string result = textWord.GetTail();

        Assert.Equal("?", result);
    }

    [Fact]
    public void TailDissection_ExclamationMark()
    {
        TextWord textWord = new("hello!");
        string result = textWord.GetTail();

        Assert.Equal("!", result);
    }

    [Fact]
    public void DisectTail_Interroband()
    {
        TextWord textWord = new("hello?!");
        string result = textWord.GetTail();

        Assert.Equal("?!", result);
    }

    [Fact]
    public void TailDissection_Comma()
    {
        TextWord textWord = new("hello,");
        string result = textWord.GetTail();

        Assert.Equal(",", result);
    }

    [Fact]
    public void TailDissection_Period()
    {
        TextWord textWord = new("hello.");
        string result = textWord.GetTail();

        Assert.Equal(".", result);
    }

    [Fact]
    public void TailDissection_Colon()
    {
        TextWord textWord = new("hello:");
        string result = textWord.GetTail();

        Assert.Equal(":", result);
    }

    [Fact]
    public void TailDissection_Semicolon()
    {
        TextWord textWord = new("hello;");
        string result = textWord.GetTail();

        Assert.Equal(";", result);
    }

    #endregion


    #region BODY_DISSECTION

    [Fact]
    //
    // the word has only head (") and tail (.).
    //
    // the body is an empty string. the TextWord should not throw and IndexOutOfRangeException.
    public void BodyDissection_NoBodyHeadTail()
    {
        string original = "\".";
        string expected = string.Empty;
        TextWord textWord = new(original);
        
        string result = textWord.GetBody();

        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void BodyDissection_1()
    {
        TextWord textWord = new("…”");
        string body = textWord.GetBody();

        Assert.Equal("", body);
    }

    [Fact]
    public void BodyDissection_2()
    {
        TextWord textWord = new("“really?'”");
        string body = textWord.GetBody();

        Assert.Equal("really", body);
    }

    [Fact]
    public void BodyDissection_3()
    {
        TextWord textWord = new("“…");
        string body = textWord.GetBody();

        Assert.Equal("", body);
    }

    #endregion


    #region HAS_ALL_ALPHA_LOWER

    [Fact]
    public void HasAllAlphaLower_ActualLower()
    {
        TextWord textWord = new("hello!.");

        bool isLower = textWord.HasAllAlphaLower();

        Assert.True(isLower);
    }

    [Fact]
    public void HasAllAlphaLower_LowerWithOthers()
    {
        TextWord textWord = new("hell0!.");

        bool isLower = textWord.HasAllAlphaLower();

        Assert.True(isLower);
    }

    [Fact]
    public void HasAllAlphaLower_WithUpper1()
    {
        TextWord textWord = new("Hello!.");

        bool isLower = textWord.HasAllAlphaLower();

        Assert.False(isLower);
    }


    [Fact]
    public void HasAllAlphaLower_WithUpper()
    {
        TextWord textWord = new("heLLo!.");

        bool isLower = textWord.HasAllAlphaLower();

        Assert.False(isLower);
    }
    #endregion


    #region IS_SENTANCE_END

    [Fact]
    public void IsSentanceEnd_WithoutEnd()
    {
        TextWord textWord = new("hello!");

        bool result = textWord.IsSentenceEnd();

        Assert.False(result);
    }

    [Fact]
    public void IsSentanceEnd_WithEnd()
    {
        TextWord textWord = new("hello!.");

        bool result = textWord.IsSentenceEnd();

        Assert.True(result);
    }

    #endregion


    #region MIGHT_BE_SENTANCE_END

    [Fact]
    public void MightBeSentanceEnd_Colon_True()
    {
        TextWord textWord = new("hello:");

        bool result = textWord.MightBeSentenceEnd();

        Assert.True(result);
    }

    [Fact]
    public void MightBeSentanceEnd_Comma()
    {
        TextWord textWord = new("hello,");

        bool result = textWord.MightBeSentenceEnd();

        Assert.False(result);
    }

    #endregion


    #region IS_SENTANCE_START

    [Fact]
    public void IsSentanceStart_WithoutStart()
    {
        TextWord textWord = new("hello");

        bool result = textWord.MightBeSentenceStart();

        Assert.False(result);
    }

    [Fact]
    public void IsSentanceStart_WithStart1_True()
    {
        TextWord textWord = new("\"hello");

        bool result = textWord.MightBeSentenceStart();

        Assert.True(result);
    }

    [Fact]
    public void IsSentenceStart_WithStart2_True()
    {
        TextWord textWord = new("“hello");

        bool result = textWord.MightBeSentenceStart();

        Assert.True(result);
    }

    [Fact]
    public void IsSentenceStart_WithStart3_True()
    {
        TextWord textWord = new("'hello");

        bool result = textWord.MightBeSentenceStart();

        Assert.True(result);
    }

    #endregion


    #region NORMALIZE_WORD

    [Fact]
    public void NormalizeWord_LowercaseWord_DoNothing()
    {
        TextWord textWord = new("hello!.");

        textWord.Normalize(new HashSet<string>());
        string result = textWord.UnwrappedWord;

        Assert.Equal("hello!.", result);
    }

    [Fact]
    public void NormalizeWord_UppercaseWord_ConvertLowercase()
    {
        TextWord textWord = new("Hello!.");
        textWord.Normalize(new HashSet<string>());

        string result = textWord.UnwrappedWord;

        Assert.Equal("hello!.", result);
    }

    #endregion


    #region CONVERT_UK_SPEECH_TO_INTERNATIONAL
    [Fact]
    //
    // word does not contain any SCQ, so nothing should happen.
    //
    public void ConvertUKSpeech2International_NoSpeech()
    {
        string original = "word?.";
        string expected = original;
        List<TextWordOptions> options = [TextWordOptions.ConvertUkSpeech2International];
        TextWord textWord = new(original, options);

        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    
    [Fact]
    //
    // the word is the start of a quotation, so the ESQC should be converted to DSQ.
    //
    public void ConvertUKSpeech2International_StartQuotation()
    {
        string original = "‘word.";
        string expected = "\"word.";
        List<TextWordOptions> options = [TextWordOptions.ConvertUkSpeech2International];
        TextWord textWord = new(original, options);

        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }
    
    
    [Fact]
    //
    // the word is name-case and the start of a sentance, so the ESQC should be converted.
    //
    public void ConvertUKSpeech2International_NamecaseSpeechStart()
    {
        string original = "‘Word.";
        string expected = "“Word.";
        List<TextWordOptions> options = [TextWordOptions.ConvertUkSpeech2International];
        TextWord textWord = new(original, options);

        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    
    [Fact]
    //
    // the word that is the end of speech.
    //
    // the ESCQ should be converted due to the presence of the period before it.
    //
    public void ConvertUKSpeech2International_EndSpeechPeriod()
    {
        string original = "word.’";
        string expected = "word.”";
        List<TextWordOptions> options = [TextWordOptions.ConvertUkSpeech2International];
        TextWord textWord = new(original, options);

        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }
    
    [Fact]
    //
    // the word that is the end of speech and has a trailing "s".
    //
    // the ESCQ should be converted due to the presence of the period before it.
    //
    public void ConvertUKSpeech2International_EndSpeechSTrailingPeriod()
    {
        string original = "words.’";
        string expected = "words.”";
        List<TextWordOptions> options = [TextWordOptions.ConvertUkSpeech2International];
        TextWord textWord = new(original, options);

        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }
    
    
    [Fact]
    //
    // the word that is the end of the sentance and the end of a quotation.
    //
    // the ESCQ should be converted to EDSQ due to the presence of the period after it.
    //
    public void ConvertUKSpeech2International_EndQuotationPeriod()
    {
        string original = "word’.";
        string expected = "word\".";
        List<TextWordOptions> options = [TextWordOptions.ConvertUkSpeech2International];
        TextWord textWord = new(original, options);

        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }
    
    
    [Fact]
    //
    // a word that is the end of speech.
    //
    // the ESCQ should be converted due to the presence of the comma before it.
    //
    public void ConvertUKSpeech2International_EndSpeechComma()
    {
        string original = "word,’";
        string expected = "word,”";
        List<TextWordOptions> options = [TextWordOptions.ConvertUkSpeech2International];
        TextWord textWord = new(original, options);

        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    
    [Fact]
    //
    // a word that is the end of speech (marked by the comma) and has a trailing "s".
    //
    // the ESCQ should be converted due to the presence of the comma before it.
    //
    public void ConvertUKSpeech2International_EndSpeechSTrailingComma()
    {
        string original = "words,’";
        string expected = "words,”";
        List<TextWordOptions> options = [TextWordOptions.ConvertUkSpeech2International];
        TextWord textWord = new(original, options);

        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }
    
    
    [Fact]
    //
    // a word that is the end of speech.
    //
    // the ESCQ should be converted due to the presence of the comma before it.
    //
    public void ConvertUKSpeech2International_EndQuotationComma()
    {
        string original = "word’,";
        string expected = "word\",";
        List<TextWordOptions> options = [TextWordOptions.ConvertUkSpeech2International];
        TextWord textWord = new(original, options);

        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }
    

    [Fact]
    // 
    // a word that is name-case and both the start and the end of speech.
    //
    // beacause of the period at the end, the ESCQ should be converted to EDCQ,
    // and because of the name-case the SSCQ should be converted to SDCQ.
    //
    public void ConvertUKSpeech2International_NamecaseStartEndSpeechPeriod()
    {
        string original = "‘Word.’";
        string expected = "“Word.”";
        List<TextWordOptions> options = [TextWordOptions.ConvertUkSpeech2International];
        TextWord textWord = new(original, options);

        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }
        

    [Fact]
    // 
    // a word that is name-case and both the start and the end of speech.
    //
    // beacause of the comma at the end, the ESCQ should be converted to EDCQ,
    // and because of the name-case the SSCQ should be converted to SDCQ.
    //
    public void ConvertUKSpeech2International_NamecaseStartEndSpeechComma()
    {
        string original = "‘Word,’";
        string expected = "“Word,”";
        List<TextWordOptions> options = [TextWordOptions.ConvertUkSpeech2International];
        TextWord textWord = new(original, options);

        string result = textWord.ToString();

        Assert.Equal(expected, result);
    }

    
    [Fact]
    //
    // the word is a possessive form.
    //
    // the ESCQ should be ignored due to the lack of SEP.
    //
    public void ConvertUKSpeech2International_Possesive()
    {
        string original = "days’";
        string expected = original;
        List<TextWordOptions> options = new() { TextWordOptions.ConvertUkSpeech2International };
        TextWord textWord = new(original, options);
        
        string result = textWord.ToString();
        
        Assert.Equal(expected, result);
    }
    
    
    [Fact]
    //
    // the word is name-case and has a start tag.
    //
    // the SSCQ should be replaced with SDCQ due to the name-case.
    //
    public void ConvertUKSpeech2International_StartSpeechWithTag()
    {
        string original = "<i>‘Move";
        string expected = "<i>“Move";
        List<TextWordOptions> options = new() { TextWordOptions.ConvertUkSpeech2International };
        TextWord textWord = new(original, options);
        
        string result = textWord.ToString();
        
        Assert.Equal(expected, result);
    }
    
    
    [Fact]
    //
    // the word is sentence end and has an end tag.
    //
    // the ESCQ should be replaced with EDCQ due to the period.
    //
    public void ConvertUKSpeech2International_EndSpeechWithTag()
    {
        string original = "right,’</i>";
        string expected = "right,”</i>";
        List<TextWordOptions> options = new() { TextWordOptions.ConvertUkSpeech2International };
        TextWord textWord = new(original, options);
        
        string result = textWord.ToString();
        
        Assert.Equal(expected, result);
    }
    #endregion


    #region ADD_OPERATION

    [Fact]
    public void AddOperation_AddInsertedSentanceEnd()
    {
        TextWord textWord = new("hello");
        textWord.AddOperation(TextWordOperations.InsertedSentenceEnd);

        List<TextWordOperations> operations = textWord.Operations.ToList();
        List<TextWordOperations> expected = new() { TextWordOperations.InsertedSentenceEnd };

        Assert.Equal(expected, operations);
    }

    [Fact]
    public void AddOperation_AddNormalized()
    {
        TextWord textWord = new("hello");
        textWord.AddOperation(TextWordOperations.Normalized);

        List<TextWordOperations> operations = textWord.Operations.ToList();
        List<TextWordOperations> expected = new() { TextWordOperations.Normalized };

        Assert.Equal(expected, operations);
    }

    [Fact]
    public void AddOperation_AddNormalizedAlreadyThere()
    {
        TextWord textWord = new("Hello");
        textWord.Normalize(new HashSet<string>());
        textWord.AddOperation(TextWordOperations.Normalized);

        List<TextWordOperations> operations = textWord.Operations.ToList();
        List<TextWordOperations> expected = new() { TextWordOperations.Normalized };

        Assert.Equal(expected, operations);
    }

    #endregion


    #region ADD_MARKER

    [Fact]
    public void AddMarker_FirstName()
    {
        TextWord textWord = new("hello");

        textWord.AddMarker(TextWordMarkers.FirstName);
        List<TextWordMarkers> markers = textWord.Markers.ToList();
        List<TextWordMarkers> expected = new() { TextWordMarkers.FirstName };

        Assert.Equal(expected, markers);
    }

    #endregion

}
