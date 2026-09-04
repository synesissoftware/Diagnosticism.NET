using Diagnosticism.Testing;

using Xunit;

namespace Diagnosticism.Tests.Testing;

public sealed class AssistTests
{
    [Fact]
    public void ExecuteAroundWriter_captures_written_text()
    {
        string text = Assist.ExecuteAroundWriter(writer =>
        {
            writer.Write("hello");
            writer.Write(' ');
            writer.Write("world");
        });

        Assert.Equal("hello world", text);
    }

    [Fact]
    public void ExecuteAroundWriter_empty_action_returns_empty_string()
    {
        string text = Assist.ExecuteAroundWriter(_ => { });

        Assert.Equal(string.Empty, text);
    }

    [Fact]
    public void ExecuteAroundWriter_throws_for_null_action()
    {
        Assert.Throws<ArgumentNullException>(
            () => Assist.ExecuteAroundWriter(null!));
    }
}
