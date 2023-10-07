using System;
using System.Collections.Generic;

namespace Vault.Tests;

public class PropertyComparer : IEqualityComparer<Property>
{
    public static PropertyComparer Instance { get; } = new();
    public bool Equals(Property? x, Property? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Description == y.Description &&
            x.Is_builtin == y.Is_builtin &&
            x.Is_encrypted == y.Is_encrypted &&
            x.Is_index == y.Is_index && 
            x.Is_nullable == y.Is_nullable &&
            x.Is_readonly == y.Is_readonly &&
            x.Is_unique == y.Is_unique && 
            x.Name == y.Name && 
            x.Data_type_name == y.Data_type_name;
    }

    public int GetHashCode(Property obj)
    {
        var hashCode = new HashCode();
        hashCode.Add(obj.Description);
        hashCode.Add(obj.Is_builtin);
        hashCode.Add(obj.Is_encrypted);
        hashCode.Add(obj.Is_index);
        hashCode.Add(obj.Is_nullable);
        hashCode.Add(obj.Is_readonly);
        hashCode.Add(obj.Is_unique);
        hashCode.Add(obj.Name);
        hashCode.Add(obj.Data_type_name);
        return hashCode.ToHashCode();
    }
}