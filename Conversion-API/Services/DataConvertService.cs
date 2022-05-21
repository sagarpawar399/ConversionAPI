using ConversionAPI.Model;
using ConversionAPI.Enum;
using ConversionAPI.Repository;

namespace ConversionAPI.Services
{
    public class DataConvertService : IDataConvertService
    {
        private readonly IDataConvertRepository _dataConvertRepository;
        public DataConvertService(IDataConvertRepository dataConvertRepository)
        {
            _dataConvertRepository = dataConvertRepository ?? throw new ArgumentNullException(nameof(dataConvertRepository));
        }

        /// <summary>
        /// Converted data.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="valueToConvert"></param>
        /// <returns>Double</returns>
        public async Task<double> ConvertDataAsync(DataUnit from, DataUnit to, double valueToConvert)
        {
            string conversionFac = $"{from}To{to}".ToLower();
            IEnumerable<DataUnitFactor>? dataUnitFactors = await _dataConvertRepository.GetConversionFactors();
            if (dataUnitFactors == null)
            {
                throw new ArgumentNullException(nameof(dataUnitFactors));
            }

            double? factor = dataUnitFactors?.Where(l => l.Values != null && l.Values.Contains(conversionFac))?.FirstOrDefault()?.Factor;
            double convertedValue;
            if (factor != null)
            {
                double converstionFactor = (double)factor;
                convertedValue = conversionFac switch
                {
                    "bytetokb" or "kbtomb" or "mbtogb" or "gbtotb" => valueToConvert / Math.Pow(1024, converstionFactor),
                    "kbtobyte" or "mbtokb" or "gbtomb" or "tbtogb" => valueToConvert * Math.Pow(1024, converstionFactor),
                    "bytetomb" or "kbtogb" or "mbtotb" => valueToConvert / Math.Pow(1024, converstionFactor),
                    "mbtobyte" or "gbtokb" or "tbtomb" => valueToConvert * Math.Pow(1024, converstionFactor),
                    "bytetogb" or "kbtotb" => valueToConvert / Math.Pow(1024, converstionFactor),
                    "gbtobyte" or "tbtokb" => valueToConvert * Math.Pow(1024, converstionFactor),
                    "bytetotb" => valueToConvert / Math.Pow(1024, converstionFactor),
                    "tbtobyte" => valueToConvert * Math.Pow(1024, converstionFactor),
                    _ => valueToConvert,
                };

                return Math.Round(convertedValue, 8);
            }

            return Math.Round(valueToConvert, 2);
        }
    }
}
