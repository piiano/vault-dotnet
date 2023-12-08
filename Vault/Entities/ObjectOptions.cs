using System.Runtime.Serialization;

namespace Vault;

internal enum ObjectOptions
{
    [EnumMember(Value = "archived")]
    Archived,
    
    [EnumMember(Value = "show_builtins")]
    Show_builtins,
    
    [EnumMember(Value = "unsafe")]
    Unsafe
}