using ConversionAPI.Enum;

namespace ConversionAPI.Services
{
    public interface IDataConvertService
    {
        Task<double> ConvertDataAsync(DataUnit from, DataUnit to, double value);
    }
}
