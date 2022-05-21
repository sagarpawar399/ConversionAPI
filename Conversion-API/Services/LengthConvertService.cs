using ConversionAPI.Cache;
using ConversionAPI.Enum;
using ConversionAPI.Helper;
using ConversionAPI.Model;
using ConversionAPI.Repository;
using Dapper;
using Serilog;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ConversionAPI.Services
{
    public class LengthConvertService :  ILengthConvertService
    {
        private readonly ILengthConvertRepository _lengthConvertRepository;
        public LengthConvertService(ILengthConvertRepository lengthConvertRepository)
        {
            _lengthConvertRepository = lengthConvertRepository ?? throw new ArgumentNullException(nameof(lengthConvertRepository));
        }

        /// <summary>
        /// Converted length.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="valueToConvert"></param>
        /// <returns>Double</returns>
        public async Task<double> ConvertLenghtAsync(LengthUnits from, LengthUnits to, double valueToConvert)
        {
            string conversionFac = $"{from}To{to}".ToLower();
            IEnumerable<LengthUnitFactor>? lengthUnitFactor = await _lengthConvertRepository.GetConversionFactors();
            if (lengthUnitFactor == null)
            {
                throw new ArgumentNullException(nameof(lengthUnitFactor));
            }

            double? factor = lengthUnitFactor?.Where(l => l.Values != null && l.Values.Contains(conversionFac))?.FirstOrDefault()?.Factor;
            double convertedValue;
            if (factor != null)
            {
                double converstionFactor = (double)factor;
                convertedValue = conversionFac switch
                {
                    "kmtom" or "kmtocm" or "kmtomm" or "mtocm" or "mtomm" or "cmtomm" => valueToConvert * converstionFactor,
                    "mtokm" or "cmtom" or "cmtokm" or "mmtokm" or "mmtom" or "mmtocm" => valueToConvert / converstionFactor,
                    _ => valueToConvert,
                };

                return Math.Round(convertedValue, 6);
            }

            return Math.Round(valueToConvert, 2);
        }      
    }
}
