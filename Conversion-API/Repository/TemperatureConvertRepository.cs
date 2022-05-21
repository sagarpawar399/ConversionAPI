using ConversionAPI.Cache;
using ConversionAPI.Helper;
using ConversionAPI.Model;
using Dapper;
using Serilog;
using System.Data;
using System.Data.SqlClient;

namespace ConversionAPI.Repository
{
    public class TemperatureConvertRepository : SQLHelper, ITemperatureConvertRepository
    {
        private readonly ICacheService _cache;
        public TemperatureConvertRepository(IConfiguration configuration, ICacheService cache) : base(configuration)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        /// <summary>
        /// Get Conversion Factors for temp. It will be cached for 1 week. 
        /// Send flush_cache = true to clear cache forcefully.
        /// </summary>
        /// <returns></returns>
        public async Task<TempUnitFactors?> GetConversionFactors()
        {
            var cacheKey = "TempUnitFactors";
            if (!_cache.TryGetValue(cacheKey, out TempUnitFactors tempUnitFactors) && tempUnitFactors == null)
            {

                using SqlConnection connection = GetConnection();
                try
                {
                    string query = $"SELECT * From dbo.TempUnitFactor WITH(nolock)";
                    tempUnitFactors = await connection.QueryFirstOrDefaultAsync<TempUnitFactors>
                                                        (
                                                            query,
                                                            commandType: CommandType.Text
                                                        );
                    if (tempUnitFactors != null)
                    {
                        _cache.SetSlidingExpiration(cacheKey, tempUnitFactors);
                    }
                    return tempUnitFactors ?? new TempUnitFactors();
                }
                catch (Exception ex)
                {
                    Log.Error($"GetConversionFactors : Error occured while fetching temperature conversion factors.. Exception : {ex}");
                }
                finally
                {
                    CloseConnection(connection);
                }
            }
            return tempUnitFactors;
        }
    }
}
