// Created: 15th May 2019
// Updated: 4th September 2026

namespace Diagnosticism.Testing;

using System.IO;

/// <summary>
///  Assistance methods for testing diagnostic and console-oriented
///  code.
/// </summary>
public static class Assist
{
    /// <summary>
    ///  Applies execute-around method to <paramref name="action"/>,
    ///  supplying a <see cref="TextWriter"/> whose written content is
    ///  returned as a string.
    /// </summary>
    /// <param name="action">
    ///  The action to execute. May not be <c>null</c>.
    /// </param>
    /// <returns>
    ///  All text written to the supplied
    ///  <see cref="TextWriter"/>.
    /// </returns>
    public static string ExecuteAroundWriter(Action<TextWriter> action)
    {
        if (action is null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        using StringWriter writer = new();

        action(writer);

        return writer.ToString();
    }
}
