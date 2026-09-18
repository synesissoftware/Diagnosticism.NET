using Diagnosticism.Diagnostics;

using Xunit;

namespace Diagnosticism.Tests.Diagnostics;

public sealed class TimingsMapTests
{
    [Fact]
    public void Add_action_records_timing_under_key()
    {
        TimingsMap<string> map = new();

        map.Add("work", () => { });

        Assert.Equal(1, map.Count);
        Assert.True(map.Frequency > 0);

        KeyValuePair<string, IList<Timing>> pair = Assert.Single(map);
        Assert.Equal("work", pair.Key);
        Assert.Single(pair.Value);
        Assert.True(pair.Value[0].Ticks >= 0);
    }

    [Fact]
    public void Add_function_returns_value_and_records_timing()
    {
        TimingsMap<int> map = new();

        int result = map.Add(42, () => 7);

        Assert.Equal(7, result);
        Assert.Equal(1, map.Count);

        KeyValuePair<int, IList<Timing>> pair = Assert.Single(map);
        Assert.Equal(42, pair.Key);
        Assert.Single(pair.Value);
    }

    [Fact]
    public void Add_appends_multiple_timings_for_same_key()
    {
        TimingsMap<string> map = new();

        map.Add("k", () => { });
        map.Add("k", () => { });

        Assert.Equal(1, map.Count);
        Assert.Equal(2, Assert.Single(map).Value.Count);
    }

    [Fact]
    public void Add_action_throws_for_null_fn()
    {
        TimingsMap<string> map = new();

        Assert.Throws<ArgumentNullException>(() => map.Add("k", (Action)null!));
    }

    [Fact]
    public void Add_function_throws_for_null_fn()
    {
        TimingsMap<string> map = new();

        Assert.Throws<ArgumentNullException>(
            () => map.Add("k", (Func<int>)null!));
    }

    [Fact]
    public void TimingMin_selects_smallest_ticks()
    {
        List<Timing> timings =
        [
            new Timing { Ticks = 30, TimeSpan = TimeSpan.FromTicks(30) },
            new Timing { Ticks = 10, TimeSpan = TimeSpan.FromTicks(10) },
            new Timing { Ticks = 20, TimeSpan = TimeSpan.FromTicks(20) },
        ];

        Assert.Equal(10, TimingsMap<string>.TimingMin(timings).Ticks);
    }

    [Fact]
    public void TimingMax_selects_largest_ticks()
    {
        List<Timing> timings =
        [
            new Timing { Ticks = 30, TimeSpan = TimeSpan.FromTicks(30) },
            new Timing { Ticks = 10, TimeSpan = TimeSpan.FromTicks(10) },
            new Timing { Ticks = 20, TimeSpan = TimeSpan.FromTicks(20) },
        ];

        Assert.Equal(30, TimingsMap<string>.TimingMax(timings).Ticks);
    }

    [Fact]
    public void TimingMean_averages_ticks()
    {
        List<Timing> timings =
        [
            new Timing { Ticks = 10, TimeSpan = TimeSpan.FromTicks(10) },
            new Timing { Ticks = 30, TimeSpan = TimeSpan.FromTicks(30) },
        ];

        Timing mean = TimingsMap<string>.TimingMean(timings);

        Assert.Equal(20, mean.Ticks);
    }

    [Fact]
    public void TimingMean_empty_returns_default()
    {
        Timing mean = TimingsMap<string>.TimingMean([]);

        Assert.Equal(0, mean.Ticks);
        Assert.Equal(TimeSpan.Zero, mean.TimeSpan);
    }

    [Fact]
    public void TimingFrequencies_counts_by_ticks()
    {
        List<Timing> timings =
        [
            new Timing { Ticks = 5, TimeSpan = TimeSpan.FromTicks(5) },
            new Timing { Ticks = 5, TimeSpan = TimeSpan.FromTicks(5) },
            new Timing { Ticks = 9, TimeSpan = TimeSpan.FromTicks(9) },
        ];

        IDictionary<Timing, int> frequencies =
            TimingsMap<string>.TimingFrequencies(timings);

        Assert.Equal(2, frequencies.Count);
        Assert.Equal(2, frequencies[new Timing { Ticks = 5 }]);
        Assert.Equal(1, frequencies[new Timing { Ticks = 9 }]);
    }

    [Fact]
    public void TimingMin_throws_for_null_or_empty()
    {
        Assert.Throws<ArgumentNullException>(
            () => TimingsMap<string>.TimingMin(null!));
        Assert.Throws<ArgumentException>(
            () => TimingsMap<string>.TimingMin([]));
    }

    [Fact]
    public void TimingMax_throws_for_null_or_empty()
    {
        Assert.Throws<ArgumentNullException>(
            () => TimingsMap<string>.TimingMax(null!));
        Assert.Throws<ArgumentException>(
            () => TimingsMap<string>.TimingMax([]));
    }

    [Fact]
    public void TimingMean_and_Frequencies_throw_for_null()
    {
        Assert.Throws<ArgumentNullException>(
            () => TimingsMap<string>.TimingMean(null!));
        Assert.Throws<ArgumentNullException>(
            () => TimingsMap<string>.TimingFrequencies(null!));
    }
}
