using ConversionAPI.Enum;

namespace ConversionAPI.Services
{
    public interface ILengthConvertService
    {
        Task<double> ConvertLenghtAsync(LengthUnits from, LengthUnits to, double value);
    }
}
