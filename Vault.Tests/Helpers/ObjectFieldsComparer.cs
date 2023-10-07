using System.Collections.Generic;
using System.Linq;

namespace Vault.Tests;

public class ObjectFieldsComparer : IEqualityComparer<ObjectFields>
{
    public static readonly ObjectFieldsComparer Instance = new ObjectFieldsComparer();
    
    public bool Equals(ObjectFields? x, ObjectFields? y)
    {
        if (x == null && y == null)
        {
            return true;
        }
        if (x == null || y == null)
        {
            return false;
        }
        
        return x.AdditionalProperties.SequenceEqual(y.AdditionalProperties);
    }

    public int GetHashCode(ObjectFields obj)
    {
        return obj.AdditionalProperties.GetHashCode();
    }
}