using ConversionAPI.Cache;
using ConversionAPI.Helper;
using ConversionAPI.Model;
using Dapper;
using Serilog;
using System.Data;
using System.Data.SqlClient;

namespace ConversionAPI.Repository
{
    public class LengthConvertRepository : SQLHelper, ILengthConvertRepository
    {
        private readonly ICacheService _cache;
        public LengthConvertRepository(IConfiguration configuration, ICacheService cache) : base(configuration)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        /// <summary>
        /// Get Conversion Factors for length. It will be cached for 1 week. 
        /// Send flush_cache = true to clear cache forcefully.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LengthUnitFactor>?> GetConversionFactors()
        {
            var cacheKey = "LengthUnitFactor";
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<LengthUnitFactor> lengthUnitFactor) && lengthUnitFactor == null)
            {

                using SqlConnection connection = GetConnection();
                try
                {
                    string query = $"SELECT * From dbo.LengthUnitFactor WITH(nolock)";
                    lengthUnitFactor = await connection.QueryAsync<LengthUnitFactor>
                                                        (
                                                            query,
                                                            commandType: CommandType.Text
                                                        );
                    if (lengthUnitFactor != null)
                    {
                        _cache.SetSlidingExpiration(cacheKey, lengthUnitFactor);
                    }
                    return lengthUnitFactor ?? new List<LengthUnitFactor>();
                }
                catch (Exception ex)
                {
                    Log.Error($"GetConversionFactors : Error occured while fetching length conversion factors.. Exception : {ex}");
                }
                finally
                {
                    CloseConnection(connection);
                }
            }
            return lengthUnitFactor;
        }
    }
}
