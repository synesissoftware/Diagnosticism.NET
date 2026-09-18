// Created: 27th April 2019
// Updated: 4th September 2026

namespace Diagnosticism.Reflection;

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

/// <summary>
///  Utility class for assisting with working with anonymous structures.
/// </summary>
public static class AnonymousUtil
{
    /// <summary>
    ///  Determines whether <paramref name="type"/> is a compiler-generated
    ///  anonymous type.
    /// </summary>
    /// <param name="type">
    ///  The type to evaluate. May not be <c>null</c>.
    /// </param>
    /// <returns>
    ///  <c>true</c> if the type is anonymous; <c>false</c> otherwise.
    /// </returns>
    public static bool IsAnonymousType(Type type)
    {
        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        string? fullName = type.FullName;

        if (fullName is null ||
            fullName.IndexOf("AnonymousType", StringComparison.Ordinal) < 0)
        {
            return false;
        }

        return type.GetCustomAttributes(
            typeof(CompilerGeneratedAttribute),
            inherit: false).Length > 0;
    }

    /// <summary>
    ///  Converts an anonymous-type instance to a dictionary whose keys are
    ///  property names and whose values are property values (nested
    ///  anonymous types are converted recursively).
    /// </summary>
    /// <param name="instance">
    ///  The anonymous-type instance. May not be <c>null</c>.
    /// </param>
    /// <param name="options">
    ///  Options that moderate the conversion behaviour.
    /// </param>
    /// <returns>
    ///  A new dictionary of property names to values.
    /// </returns>
    public static IDictionary<string, object?> ConvertToDictionary(
        object instance,
        StructureConversionOptions options)
    {
        if (instance is null)
        {
            throw new ArgumentNullException(nameof(instance));
        }

        _ = options;

        Dictionary<string, object?> result = new();
        Type type = instance.GetType();

        foreach (PropertyInfo property in type.GetProperties(
            BindingFlags.Instance | BindingFlags.Public))
        {
            object? value = property.GetValue(instance);

            if (value is not null && IsAnonymousType(property.PropertyType))
            {
                result.Add(property.Name, ConvertToDictionary(value, options));
            }
            else
            {
                result.Add(property.Name, value);
            }
        }

        return result;
    }
}
