using ConversionAPI.Cache;
using ConversionAPI.Enum;
using ConversionAPI.Helper;
using ConversionAPI.Model;
using ConversionAPI.Repository;
using Dapper;
using Serilog;
using System.Data;
using System.Data.SqlClient;

namespace ConversionAPI.Services
{
    public class TemperatureConvertService : ITemperatureConvertService
    {
        private readonly ITemperatureConvertRepository _temperatureConvertRepository;
        public TemperatureConvertService(ITemperatureConvertRepository temperatureConvertRepository)
        {
            _temperatureConvertRepository = temperatureConvertRepository ?? throw new ArgumentNullException(nameof(temperatureConvertRepository));
        }

        /// <summary>
        /// Converted temperature.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="valueToConvert"></param>
        /// <returns>Double</returns>
        public async Task<double> ConvertTemperatureAsync(TemperatureUnit from, TemperatureUnit to, double valueToConvert)
        {
            string conversionFac = $"{from}To{to}";
            TempUnitFactors? tempUnitFactors = await _temperatureConvertRepository.GetConversionFactors();
            if (tempUnitFactors == null)
            {
                throw new ArgumentNullException(nameof(tempUnitFactors));
            }
            double convertedValue = conversionFac switch
            {
                "FahrenheitToCelsius" => (valueToConvert - tempUnitFactors.Fac1) * (tempUnitFactors.Fac2 / tempUnitFactors.Fac3),
                "FahrenheitToKelvin" => (valueToConvert - tempUnitFactors.Fac1) * (tempUnitFactors.Fac2 / tempUnitFactors.Fac3) + tempUnitFactors.Fac4,
                "CelsiusToFahrenheit" => valueToConvert * (tempUnitFactors.Fac3 / tempUnitFactors.Fac2) + tempUnitFactors.Fac1,
                "CelsiusToKelvin" => valueToConvert + tempUnitFactors.Fac4,
                "KelvinToFahrenheit" => (valueToConvert - tempUnitFactors.Fac4) * (tempUnitFactors.Fac3 / tempUnitFactors.Fac2) + tempUnitFactors.Fac1,
                "KelvinToCelsius" => valueToConvert - tempUnitFactors.Fac4,
                _ => valueToConvert,
            };
            return Math.Round(convertedValue, 2);
        }
    }
}
