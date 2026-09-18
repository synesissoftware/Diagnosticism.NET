// Created: 7th May 2019
// Updated: 4th September 2026

namespace Diagnosticism.Diagnostics;

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
///  Maps keys to lists of measured timings, using execute-around
///  timing of actions and functions.
/// </summary>
/// <typeparam name="TKey">
///  The key type used to group timings.
/// </typeparam>
public class TimingsMap<TKey>
    : IEnumerable<KeyValuePair<TKey, IList<Timing>>>
    where TKey : notnull
{
    private readonly Stopwatch m_stopwatch;
    private readonly Dictionary<TKey, IList<Timing>> m_dict;
    private readonly long m_frequency;

    /// <summary>
    ///  Constructs an empty timings map.
    /// </summary>
    public TimingsMap()
    {
        m_stopwatch = new Stopwatch();
        m_dict = new Dictionary<TKey, IList<Timing>>();
        m_frequency = Stopwatch.Frequency;
    }

    /// <summary>
    ///  Obtains a typed enumerator over key / timings pairs.
    /// </summary>
    public IEnumerator<KeyValuePair<TKey, IList<Timing>>> GetEnumerator()
    {
        return m_dict.GetEnumerator();
    }

    /// <summary>
    ///  Obtains an untyped enumerator over key / timings pairs.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return m_dict.GetEnumerator();
    }

    /// <summary>
    ///  The stopwatch frequency used when converting ticks to
    ///  <see cref="TimeSpan"/>.
    /// </summary>
    public long Frequency => m_frequency;

    /// <summary>
    ///  The number of distinct keys for which timings have been
    ///  recorded.
    /// </summary>
    public int Count => m_dict.Count;

    /// <summary>
    ///  Applies execute-around timing to a function and appends the
    ///  elapsed time under <paramref name="key"/>.
    /// </summary>
    /// <typeparam name="TReturn">
    ///  The return type of <paramref name="fn"/>.
    /// </typeparam>
    /// <param name="key">
    ///  The key under which to record the timing.
    /// </param>
    /// <param name="fn">
    ///  The function whose execution time is measured. May not be
    ///  <c>null</c>.
    /// </param>
    /// <returns>
    ///  The return value of <paramref name="fn"/>.
    /// </returns>
    public TReturn Add<TReturn>(TKey key, Func<TReturn> fn)
    {
        if (fn is null)
        {
            throw new ArgumentNullException(nameof(fn));
        }

        m_stopwatch.Restart();

        TReturn result = fn();

        m_stopwatch.Stop();

        AddTiming_(key, m_stopwatch.Elapsed, m_stopwatch.ElapsedTicks);

        return result;
    }

    /// <summary>
    ///  Applies execute-around timing to an action and appends the
    ///  elapsed time under <paramref name="key"/>.
    /// </summary>
    /// <param name="key">
    ///  The key under which to record the timing.
    /// </param>
    /// <param name="fn">
    ///  The action whose execution time is measured. May not be
    ///  <c>null</c>.
    /// </param>
    public void Add(TKey key, Action fn)
    {
        if (fn is null)
        {
            throw new ArgumentNullException(nameof(fn));
        }

        m_stopwatch.Restart();

        fn();

        m_stopwatch.Stop();

        AddTiming_(key, m_stopwatch.Elapsed, m_stopwatch.ElapsedTicks);
    }

    /// <summary>
    ///  Returns the timing with the smallest <see cref="Timing.Ticks"/>
    ///  value in <paramref name="timings"/>.
    /// </summary>
    /// <param name="timings">
    ///  A non-empty sequence of timings. May not be <c>null</c>.
    /// </param>
    public static Timing TimingMin(IEnumerable<Timing> timings)
    {
        if (timings is null)
        {
            throw new ArgumentNullException(nameof(timings));
        }

        Timing result = default;
        bool assigned = false;

        foreach (Timing timing in timings)
        {
            if (!assigned)
            {
                result = timing;
                assigned = true;
            }
            else if (timing.Ticks < result.Ticks)
            {
                result = timing;
            }
        }

        if (!assigned)
        {
            throw new ArgumentException(
                "Sequence must contain at least one timing.",
                nameof(timings));
        }

        return result;
    }

    /// <summary>
    ///  Returns the timing with the largest <see cref="Timing.Ticks"/>
    ///  value in <paramref name="timings"/>.
    /// </summary>
    /// <param name="timings">
    ///  A non-empty sequence of timings. May not be <c>null</c>.
    /// </param>
    public static Timing TimingMax(IEnumerable<Timing> timings)
    {
        if (timings is null)
        {
            throw new ArgumentNullException(nameof(timings));
        }

        Timing result = default;
        bool assigned = false;

        foreach (Timing timing in timings)
        {
            if (!assigned)
            {
                result = timing;
                assigned = true;
            }
            else if (timing.Ticks > result.Ticks)
            {
                result = timing;
            }
        }

        if (!assigned)
        {
            throw new ArgumentException(
                "Sequence must contain at least one timing.",
                nameof(timings));
        }

        return result;
    }

    /// <summary>
    ///  Returns the arithmetic mean of <see cref="Timing.Ticks"/> in
    ///  <paramref name="timings"/>, with a corresponding
    ///  <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="timings">
    ///  A sequence of timings. May not be <c>null</c>.
    /// </param>
    public static Timing TimingMean(IEnumerable<Timing> timings)
    {
        if (timings is null)
        {
            throw new ArgumentNullException(nameof(timings));
        }

        long sumTicks = 0;
        int count = 0;

        foreach (Timing timing in timings)
        {
            sumTicks += timing.Ticks;
            ++count;
        }

        if (count == 0)
        {
            return default;
        }

        long meanTicks = sumTicks / count;

        return new Timing
        {
            Ticks = meanTicks,
            TimeSpan = new TimeSpan(
                (meanTicks * 10_000_000L) / Stopwatch.Frequency),
        };
    }

    /// <summary>
    ///  Counts how often each distinct <see cref="Timing"/> (by
    ///  <see cref="Timing.Ticks"/>) appears in
    ///  <paramref name="timings"/>.
    /// </summary>
    /// <param name="timings">
    ///  A sequence of timings. May not be <c>null</c>.
    /// </param>
    public static IDictionary<Timing, int> TimingFrequencies(
        IEnumerable<Timing> timings)
    {
        if (timings is null)
        {
            throw new ArgumentNullException(nameof(timings));
        }

        Dictionary<Timing, int> frequencies = new();

        foreach (Timing timing in timings)
        {
            if (!frequencies.ContainsKey(timing))
            {
                frequencies[timing] = 0;
            }

            frequencies[timing] = 1 + frequencies[timing];
        }

        return frequencies;
    }

    private void AddTiming_(TKey key, TimeSpan timeSpan, long ticks)
    {
        if (!m_dict.TryGetValue(key, out IList<Timing>? timings))
        {
            timings = new List<Timing>();
            m_dict.Add(key, timings);
        }

        timings.Add(new Timing { Ticks = ticks, TimeSpan = timeSpan });
    }
}
