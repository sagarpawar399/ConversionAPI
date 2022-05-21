using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ConversionAPI.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TemperatureUnit
    {
        Fahrenheit,
        Celsius,
        Kelvin,
    }
}
