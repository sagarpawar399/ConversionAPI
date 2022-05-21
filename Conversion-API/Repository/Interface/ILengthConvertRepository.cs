using ConversionAPI.Model;

namespace ConversionAPI.Repository
{
    public interface ILengthConvertRepository
    {
        Task<IEnumerable<LengthUnitFactor>?> GetConversionFactors();
    }
}
