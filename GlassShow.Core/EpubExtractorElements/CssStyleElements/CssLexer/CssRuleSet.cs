using System.Text;

namespace GlassShow.Core.EpubExtractorElements.CssStyleElements.CssLexer;

public class CssRuleSet
{
    public required string Selector { get; init; }

    /// <summary>
    /// contains all the key-value properties of the rule
    /// </summary>
    public Dictionary<string, string> DeclarationBlock { get; init; } = new Dictionary<string, string>();


    public override bool Equals(object? obj)
    {
        if (obj is CssRuleSet other)
        {
            // Compare the basic properties
            bool selectorEquals = string.Equals(Selector, other.Selector, StringComparison.OrdinalIgnoreCase);

            // Compare properties
            bool declarationBlockEquals = DeclarationBlock.Count == other.DeclarationBlock.Count &&
                                          !DeclarationBlock.Except(other.DeclarationBlock).Any();

            return selectorEquals && declarationBlockEquals;
        }
        
        return false;
    }

    // Override GetHashCode method
    public override int GetHashCode()
    {
        // Combine hash codes of all properties
        int hashCode = 17; // Arbitrary prime number to start with

        hashCode = hashCode * 31 + Selector.GetHashCode();
        hashCode = hashCode * 31 + DeclarationBlock.Aggregate(0, (acc, kvp) => acc ^ kvp.Key.GetHashCode() ^ kvp.Value.GetHashCode());

        return hashCode;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append($"{Selector} {{\n");
        foreach (string key in DeclarationBlock.Keys)
        {
            stringBuilder.Append($"    {key}: {DeclarationBlock[key]};\n");
        }
        stringBuilder.Append('}');

        return stringBuilder.ToString();
    }
}