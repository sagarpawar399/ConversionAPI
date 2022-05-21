using ConversionAPI.Model;

namespace ConversionAPI.Repository
{
    public interface IDataConvertRepository
    {
        Task<IEnumerable<DataUnitFactor>?> GetConversionFactors();
    }
}
