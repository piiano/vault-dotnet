using System;
using System.Collections.Generic;
using System.Linq;

namespace Vault.Tests;

public class CollectionComparer : IEqualityComparer<Collection>
{
    public static CollectionComparer Instance { get; } = new CollectionComparer();

    public bool Equals(Collection? x, Collection? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (ReferenceEquals(x, null))
        {
            return false;
        }

        if (ReferenceEquals(y, null))
        {
            return false;
        }

        if (x.GetType() != y.GetType())
        {
            return false;
        }

        IEnumerable<Property> xProperties = GetProperties(x);
        IEnumerable<Property> yProperties = GetProperties(y);
        
        return x.Name == y.Name &&
            x.Type == y.Type &&
            xProperties
                .SequenceEqual(
                    yProperties,
                    PropertyComparer.Instance);
    }

    public int GetHashCode(Collection obj)
    {
        return HashCode.Combine(obj.Name, obj.Type);
    }
    
    private IEnumerable<Property> GetProperties(Collection y)
    {
        return y.Properties.OrderBy(property => property.Name);
    }
}