using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ConversionAPI.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DataUnit
    {
        Byte,
        KB,
        MB,
        GB,
        TB,        
    }
}
