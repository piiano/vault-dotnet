using System.Runtime.Serialization;

namespace Vault;

internal enum ObjectOptions
{
    [EnumMember(Value = "archived")]
    Archived,
    
    [EnumMember(Value = "show_builtins")]
    ShowBuiltins,
    
    [EnumMember(Value = "unsafe")]
    Unsafe
}