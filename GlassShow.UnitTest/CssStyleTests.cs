using GlassShow.Core.EpubExtractorElements.CssStyleElements;

namespace GlassShow.UnitTest;   

public class CssSelectorTests
{
    /// <summary>
    /// check if the `CssSelector` affects a `HtmlElement` with the same name. 
    /// </summary>
    [Fact]
    public void IsApplicableToHtmlElement_OnlyElementSelectors_Match()
    {
        CssSelector selector = new CssSelector() { SelectorName = "p"};
        HtmlElement htmlElement = new HtmlElement() { ElementName = "p"}; 
        
        Assert.True(selector.IsApplicableToHtmlElement(htmlElement));
    }
    
    /// <summary>
    /// check if the `CssSelector` does not apply to an `HtmlElement` with a different element name.
    /// </summary>
    [Fact]
    public void IsApplicableToHtmlElement_OnlyElementSelectors_MisMatch()
    {
        CssSelector selector = new CssSelector() { SelectorName = "p"};
        HtmlElement htmlElement = new HtmlElement() { ElementName = "h1" }; 

        Assert.False(selector.IsApplicableToHtmlElement(htmlElement));
    }

    /// <summary>
    /// check if the `CssSelector` affects an `HtmlElement` with the same class.
    /// </summary>
    [Fact]
    public void IsApplicableToHtmlElement_SingleClassSelectors_Match()
    {
        CssSelector selector = new CssSelector() { SelectorClasses = ["class1"] };
        HtmlElement htmlElement = new HtmlElement() { ElementClasses = ["class1"] }; 

        Assert.True(selector.IsApplicableToHtmlElement(htmlElement));
    }
    
    /// <summary>
    /// check if the `CssSelector` does not apply when the `HtmlElement` has a different class.
    /// </summary>
    [Fact]
    public void IsApplicableToHtmlElement_SingleClassSelectors_Mismatch()
    {
        CssSelector selector = new CssSelector() { SelectorClasses = ["class1"] };
        HtmlElement htmlElement = new HtmlElement() { ElementClasses = ["class2"] }; 

        Assert.False(selector.IsApplicableToHtmlElement(htmlElement));
    }
    
    /// <summary>
    /// check if the `CssSelector` affects an `HtmlElement` with the same class (multiple classes).
    /// </summary>
    [Fact]
    public void IsApplicableToHtmlElement_MultipleClassSelectors_Match()
    {
        CssSelector selector = new CssSelector() { SelectorClasses = ["class1", "class2"] };
        HtmlElement htmlElement = new HtmlElement() { ElementClasses = ["class1", "class2"] }; 

        Assert.True(selector.IsApplicableToHtmlElement(htmlElement));
    }
    
    /// <summary>
    /// check if the `CssSelector` does not apply when the `HtmlElement` has a different class.
    /// </summary>
    [Fact]
    public void IsApplicableToHtmlElement_MultipleClassSelectors_Mismatch()
    {
        CssSelector selector = new CssSelector() { SelectorClasses = ["class1", "class2"] };
        HtmlElement htmlElement = new HtmlElement() { ElementClasses = ["class2"] }; 

        Assert.False(selector.IsApplicableToHtmlElement(htmlElement));
    }


    /// <summary>
    /// check if the `CssSelector` affects an `HtmlElement` with the same element and class.
    /// </summary>
    [Fact]
    public void IsApplicableToHtmlElement_SameElementAndClassSelectors_Mismatch()
    {
        CssSelector selector = new CssSelector()
        {
            SelectorName = "p", 
            SelectorClasses = ["class1"]
        };
        HtmlElement htmlElement = new HtmlElement()  
        { 
            ElementName = "p", 
            ElementClasses = ["class1"]
        }; 

        Assert.True(selector.IsApplicableToHtmlElement(htmlElement));
    }
    
    /// <summary>
    /// check if the `CssSelector` does not apply to an `HtmlElement` with the same element but different class.
    /// </summary>
    [Fact]
    public void IsApplicableToHtmlElement_SameElementDifferentClassSelectors_Mismatch()
    {
        CssSelector selector = new CssSelector()
        {
            SelectorName = "p", 
            SelectorClasses = ["class1"]
        };
        HtmlElement htmlElement = new HtmlElement() 
        { 
            ElementName = "p", 
            ElementClasses = ["class2"]
        };

        Assert.False(selector.IsApplicableToHtmlElement(htmlElement));
    }
    
    /// <summary>
    /// check if the `CssSelector` does not apply to an `HtmlElement` with a different element but same class.
    /// </summary>
    [Fact]
    public void IsApplicableToHtmlElement_DifferentElementSameClassSelectors_Mismatch()
    {
        CssSelector selector = new CssSelector()
        {
            SelectorName = "p", 
            SelectorClasses = ["class1"]
        };
        HtmlElement htmlElement = new HtmlElement() 
        { 
            ElementName = "a", 
            ElementClasses = ["class1"]
        };

        Assert.False(selector.IsApplicableToHtmlElement(htmlElement));
    }
    
    /// <summary>
    /// check if the `CssSelector` does not apply to an `HtmlElement` with a different Element and different class.
    /// </summary>
    [Fact]
    public void IsApplicableToHtmlElement_DifferentElementDifferentClassSelectors_Mismatch()
    {
        CssSelector selector = new CssSelector()
        {
            SelectorName = "p", 
            SelectorClasses = ["class1"]
        };
        HtmlElement htmlElement = new HtmlElement() 
        { 
            ElementName = "a", 
            ElementClasses = ["class1"]
        };

        Assert.False(selector.IsApplicableToHtmlElement(htmlElement));
    }

    /// <summary>
    /// check if the `CssSelector` affects an `HtmlElement` with the same id.
    /// </summary>
    [Fact]
    public void IsApplicableToHtmlElement_PureIdSelectors_Match()
    {
        CssSelector selector = new CssSelector()
        {
            SelectorId = "id1"
        };
        HtmlElement htmlElement = new HtmlElement() { ElementId = "id1" };

        Assert.True(selector.IsApplicableToHtmlElement(htmlElement));
    }
    
    /// <summary>
    /// check if the `CssSelector` does not apply when the `HtmlElement` has a different id.
    /// </summary>
    [Fact]
    public void IsApplicableToHtmlElement_PureIdSelectors_Mismatch()
    {
        CssSelector selector = new CssSelector()
        {
            SelectorId = "id1"
        };
        HtmlElement htmlElement = new HtmlElement() { ElementId = "id2" };

        Assert.False(selector.IsApplicableToHtmlElement(htmlElement));
    }
    
    /// <summary>
    /// check if the `CssSelector` affects an `HtmlElement` with the same id and class.
    /// </summary>
    [Fact]
    public void IsApplicableToHtmlElement_SameIdSameClassSelectors_Match()
    {
        CssSelector selector = new CssSelector()
        {
            SelectorId = "id1",
            SelectorClasses = ["class1"]
        };
        HtmlElement htmlElement = new HtmlElement() 
        { 
            ElementId = "id1", 
            ElementClasses = ["class1"]
        };

        Assert.True(selector.IsApplicableToHtmlElement(htmlElement));
    }
    
    /// <summary>
    /// check if the `CssSelector` does not apply to an `HtmlElement` with the same id but different class.
    /// </summary>
    [Fact]
    public void IsApplicableToHtmlElement_SameIdDifferentClassSelectors_Mismatch()
    {
        CssSelector selector = new CssSelector()
        {
            SelectorId = "id1",
            SelectorClasses = ["class1"]
        };
        HtmlElement htmlElement = new HtmlElement() 
        { 
            ElementId = "id1", 
            ElementClasses = ["class2"]
        };

        Assert.False(selector.IsApplicableToHtmlElement(htmlElement));
    }

    /// <summary>
    /// check if the `CssSelector` does not apply to an `HtmlElement` with a different id and same class.
    /// </summary>
    [Fact]
    public void IsApplicableToHtmlElement_DifferentIdSameClassSelectors_Mismatch()
    {
        CssSelector selector = new CssSelector()
        {
            SelectorId = "id1",
            SelectorClasses = ["class1"]
        };
        HtmlElement htmlElement = new HtmlElement() 
        { 
            ElementId = "id2", 
            ElementClasses = ["class1"]
        };

        Assert.False(selector.IsApplicableToHtmlElement(htmlElement));
    }

    /// <summary>
    /// check if the `CssSelector` does not apply to an `HtmlElement` with a different id and class.
    /// </summary>
    [Fact]
    public void IsApplicableToHtmlElement_DifferentIdDifferentClassSelectors_Mismatch()
    {
        CssSelector selector = new CssSelector()
        {
            SelectorId = "id1",
            SelectorClasses = ["class1"]
        };
        HtmlElement htmlElement = new HtmlElement() 
        { 
            ElementId = "id2", 
            ElementClasses = ["class2"]
        };

        Assert.False(selector.IsApplicableToHtmlElement(htmlElement));
    }
}