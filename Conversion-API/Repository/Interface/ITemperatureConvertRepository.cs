using ConversionAPI.Model;

namespace ConversionAPI.Repository
{
    public interface ITemperatureConvertRepository
    {
        Task<TempUnitFactors?> GetConversionFactors();
    }
}
