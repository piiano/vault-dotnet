using System.Reflection;
using System.Runtime.Serialization;

namespace Vault;

internal static class Extensions
{
    public static T To<T>(this object r) where T : struct // for enums
    {
        var names = ConvertToString(r);
        foreach (string name in names)
        {
            if (Enum.TryParse(name, out T v))
            {
                return v;
            }
        }

        throw new ArgumentException();
    }

    public static IEnumerable<U> ToAll<T, U>(this IEnumerable<T>? r)
        where T : struct
        where U : struct // for enums
    {
        return r == null 
            ? Array.Empty<U>() 
            : r.Select(item => item.To<U>());
    }

    public static IEnumerable<T>? ToOption<T>(this bool showBuiltins) where T : struct
    {
        return showBuiltins 
            ? new [] {ShowBuiltinsOption.ShowBuiltins.To<T>()} 
            : null;
    }

    private static IEnumerable<string> ConvertToString(object r)
    {
        var name = r.ToString()!;

        string? enumMemberValue = r.GetType()
            .GetField(name)?
            .GetCustomAttribute<EnumMemberAttribute>(true)?
            .Value;

        return enumMemberValue == null 
            ? new[] {name} 
            : new[] {enumMemberValue, name};
    }
}