using Diagnosticism;

using Xunit;

namespace Diagnosticism.Tests;

public sealed class VersionTests
{
    [Fact]
    public void Version_components_match_prefix()
    {
        Assert.Equal(0, LibraryVersion.Major);
        Assert.Equal(0, LibraryVersion.Minor);
        Assert.Equal(1, LibraryVersion.Patch);
    }

    [Fact]
    public void VersionString_is_dotted_triple()
    {
        Assert.Equal("0.0.1", LibraryVersion.VersionString);
    }
}
