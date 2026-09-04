// Created: 7th May 2019
// Updated: 4th September 2026

namespace Diagnosticism.Diagnostics;

/// <summary>
///  Timing result for a measured operation.
/// </summary>
public struct Timing
    : IEquatable<Timing>
{
    /// <summary>
    ///  The <see cref="System.TimeSpan"/> of the measured
    ///  operation(s).
    /// </summary>
    public TimeSpan TimeSpan;

    /// <summary>
    ///  The number of ticks of the measured operation(s).
    /// </summary>
    public long Ticks;

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Ticks.GetHashCode();
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is Timing other && Equals(other);
    }

    /// <inheritdoc />
    public bool Equals(Timing other)
    {
        return Ticks == other.Ticks;
    }

    /// <summary>
    ///  Equality comparison based on <see cref="Ticks"/>.
    /// </summary>
    public static bool operator ==(Timing left, Timing right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///  Inequality comparison based on <see cref="Ticks"/>.
    /// </summary>
    public static bool operator !=(Timing left, Timing right)
    {
        return !left.Equals(right);
    }

    /// <summary>
    ///  Overridden for debugging / logging.
    /// </summary>
    public override string ToString()
    {
        return "{ " + Ticks.ToString() + ", " + TimeSpan.ToString() + " }";
    }
}
