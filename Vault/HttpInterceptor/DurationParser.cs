using System.Text.RegularExpressions;
using Tomlyn.Model;

namespace Vault;

internal static class DurationParser
{
    public static void ReplaceDurationStringWithSecondsInteger(TomlTable rootTable, string property)
    {
        // Considering the property, which has the dotted format, as a path,
        // find the sub-table of toml that corresponds to the directory of the property. 

        string[] propertyPath = property.Split('.');
        string propertyKey = propertyPath.Last();
        
        TomlTable propertyTable = propertyPath
            .SkipLast(1)
            .Aggregate(rootTable, (current, key) => (TomlTable) current[key]);

        // If the property's table contains the property, convert its value from duration string to seconds.  
        if (propertyTable.ContainsKey(propertyKey))
        {
            propertyTable[propertyKey] = ConvertDurationToSeconds(propertyTable[propertyKey].ToString()!);
        }
    }

    private static int ConvertDurationToSeconds(string stringValue)
    {
        TimeSpan duration = ParseDuration(stringValue);
        return (int) duration.TotalSeconds;
    }

    private static TimeSpan ParseDuration(string durationString)
    {
        string pattern = @"^(-)?(\d+d)?(\d+h)?(\d+m)?(\d+s)?(\d+ms)?$";
        
        Match match = Regex.Match(durationString, pattern);

        if (!match.Success)
        {
            throw new FormatException("Invalid duration format.");
        }

        int sign = match.Groups[1].Success ? -1 : 1;
        TimeSpan days = TimeSpan.FromDays(
            GetGroupValueOrDefault(match.Groups[2]));
        TimeSpan hours = TimeSpan.FromHours(
            GetGroupValueOrDefault(match.Groups[3]));
        TimeSpan minutes = TimeSpan.FromMinutes(
            GetGroupValueOrDefault(match.Groups[4]));
        TimeSpan seconds = TimeSpan.FromSeconds(
            GetGroupValueOrDefault(match.Groups[5]));
        TimeSpan milliseconds = TimeSpan.FromMilliseconds(
            GetGroupValueOrDefault(match.Groups[6]));
            
        return sign * (days + hours + minutes + seconds + milliseconds);
    }

    private static int GetGroupValueOrDefault(Group group)
    {
        if (!group.Success)
        {
            return 0;
        }
        
        string value = group.Value;

        value = value.EndsWith("ms") 
            ? value[..^2] 
            : value[..^1];

        return int.Parse(value);
    }
}