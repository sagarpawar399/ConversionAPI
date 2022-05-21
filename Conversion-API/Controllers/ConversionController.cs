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

        public ConversionController(ILogger<ConversionController> logger, 
                ITemperatureConvertService temperatureConvertService,
                ILengthConvertService lengthConvertService)
        {
            _logger = logger;
            _temperatureConvertService = temperatureConvertService;
            _lengthConvertService = lengthConvertService;
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
            if (from == to)
            {
                return BadRequest("From and To should not be same.");
            }
            return Ok($"From {from} to {to} : {await _temperatureConvertService.ConvertTemperatureAsync(from, to, value)}");
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
            return Ok($"From {from} to {to} : {await _lengthConvertService.ConvertLenghtAsync(from, to, value)}");
        }
    }
}
