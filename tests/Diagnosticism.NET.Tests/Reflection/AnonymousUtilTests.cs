using Diagnosticism.Reflection;

using Xunit;

namespace Diagnosticism.Tests.Reflection;

public sealed class AnonymousUtilTests
{
    [Fact]
    public void ConvertToDictionary_empty_yields_empty_dictionary()
    {
        var v = new { };

        IDictionary<string, object?> r = AnonymousUtil.ConvertToDictionary(
            v,
            StructureConversionOptions.None);

        Assert.NotNull(r);
        Assert.Empty(r);
    }

    [Fact]
    public void ConvertToDictionary_single_public_string()
    {
        var v = new { Name = "the-name" };

        IDictionary<string, object?> r = AnonymousUtil.ConvertToDictionary(
            v,
            StructureConversionOptions.None);

        Assert.Single(r);
        Assert.True(r.ContainsKey("Name"));
        Assert.Equal("the-name", r["Name"]);
    }

    [Fact]
    public void ConvertToDictionary_single_public_integer()
    {
        var v = new { Count = 3 };

        IDictionary<string, object?> r = AnonymousUtil.ConvertToDictionary(
            v,
            StructureConversionOptions.None);

        Assert.Single(r);
        Assert.True(r.ContainsKey("Count"));
        Assert.Equal(3, r["Count"]);
    }

    [Fact]
    public void ConvertToDictionary_single_public_real()
    {
        var v = new { Fraction = 1.234 };

        IDictionary<string, object?> r = AnonymousUtil.ConvertToDictionary(
            v,
            StructureConversionOptions.None);

        Assert.Single(r);
        Assert.True(r.ContainsKey("Fraction"));
        Assert.Equal(1.234, r["Fraction"]);
    }

    [Fact]
    public void ConvertToDictionary_single_public_array_of_strings()
    {
        var v = new { Items = new string[] { "s1", "s2" } };

        IDictionary<string, object?> r = AnonymousUtil.ConvertToDictionary(
            v,
            StructureConversionOptions.None);

        Assert.Single(r);
        Assert.True(r.ContainsKey("Items"));
        Assert.IsType<string[]>(r["Items"]);

        string[] ar = (string[])r["Items"]!;

        Assert.Equal(2, ar.Length);
        Assert.Equal("s1", ar[0]);
        Assert.Equal("s2", ar[1]);
    }

    [Fact]
    public void ConvertToDictionary_embedded_structure()
    {
        var v = new { Inner = new { Name = "the-name", Count = -1 } };

        IDictionary<string, object?> r = AnonymousUtil.ConvertToDictionary(
            v,
            StructureConversionOptions.None);

        Assert.Single(r);
        Assert.True(r.ContainsKey("Inner"));

        Assert.IsAssignableFrom<IDictionary<string, object?>>(r["Inner"]);
        IDictionary<string, object?> inner =
            (IDictionary<string, object?>)r["Inner"]!;

        Assert.Equal(2, inner.Count);
        Assert.Equal("the-name", inner["Name"]);
        Assert.Equal(-1, inner["Count"]);
    }

    [Fact]
    public void IsAnonymousType_true_for_anonymous_instance()
    {
        var v = new { Name = "x" };

        Assert.True(AnonymousUtil.IsAnonymousType(v.GetType()));
    }

    [Fact]
    public void IsAnonymousType_false_for_string()
    {
        Assert.False(AnonymousUtil.IsAnonymousType(typeof(string)));
    }

    [Fact]
    public void IsAnonymousType_throws_for_null()
    {
        Assert.Throws<ArgumentNullException>(
            () => AnonymousUtil.IsAnonymousType(null!));
    }
}
