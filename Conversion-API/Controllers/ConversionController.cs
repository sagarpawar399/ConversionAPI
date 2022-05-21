using ConversionAPI.Enum;
using ConversionAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConversionAPI.Controllers
{
    [ApiController]
    public class ConversionController : ControllerBase
    {
        private readonly ILogger<ConversionController> _logger;
        private readonly ITemperatureConvertService _temperatureConvertService;
        private readonly ILengthConvertService _lengthConvertService;
        private readonly IDataConvertService _dataConvertService;

        public ConversionController(ILogger<ConversionController> logger, 
                ITemperatureConvertService temperatureConvertService,
                ILengthConvertService lengthConvertService,
                IDataConvertService dataConvertService)
        {
            _logger = logger;
            _temperatureConvertService = temperatureConvertService;
            _lengthConvertService = lengthConvertService;
            _dataConvertService = dataConvertService;
        }

        /// <summary>
        /// Convert Temperature
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpGet("convert/temperature")]
        public async Task<IActionResult> ConvertTemperatureAsync(TemperatureUnit from = TemperatureUnit.Celsius, TemperatureUnit to = TemperatureUnit.Kelvin, double value = 10)
        {
            return Ok($"{value} {from} = {await _temperatureConvertService.ConvertTemperatureAsync(from, to, value)} {to}");
        }

        /// <summary>
        /// Convert Lenght
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpGet("convert/length")]
        public async Task<IActionResult> ConvertLenghtAsync(LengthUnits from, LengthUnits to , double value = 10)
        {
            if (from == to)
            {
                return BadRequest("From and To should not be same.");
            }
            return Ok($"{value} {from} = {await _lengthConvertService.ConvertLenghtAsync(from, to, value)} {to}");
        }

        /// <summary>
        /// Convert Data
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpGet("convert/data")]
        public async Task<IActionResult> ConvertDataAsync(DataUnit from, DataUnit to, double value = 10)
        {
            return Ok($"{value} {from} = {await _dataConvertService.ConvertDataAsync(from, to, value)} {to}");
        }
    }
}
