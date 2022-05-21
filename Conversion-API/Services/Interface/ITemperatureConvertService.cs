using ConversionAPI.Enum;

namespace ConversionAPI.Services
{
    public interface ITemperatureConvertService
    {
        Task<double> ConvertTemperatureAsync(TemperatureUnit from, TemperatureUnit to, double value);
    }
}
