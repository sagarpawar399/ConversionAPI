using ConversionAPI.Cache;
using ConversionAPI.Helper;
using ConversionAPI.Model;
using Dapper;
using Serilog;
using System.Data;
using System.Data.SqlClient;

namespace ConversionAPI.Repository
{
    public class DataConvertRepository:SQLHelper, IDataConvertRepository
    {
        private readonly ICacheService _cache;
        public DataConvertRepository(IConfiguration configuration, ICacheService cache) : base(configuration)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        /// <summary>
        /// Get Conversion Factors for length. It will be cached for 1 week. 
        /// Send flush_cache = true to clear cache forcefully.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DataUnitFactor>?> GetConversionFactors()
        {
            var cacheKey = "DataUnitFactor";
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<DataUnitFactor> dataUnitFactor) && dataUnitFactor == null)
            {

                using SqlConnection connection = GetConnection();
                try
                {
                    string query = $"SELECT * From dbo.DataUnitFactor WITH(nolock)";
                    dataUnitFactor = await connection.QueryAsync<DataUnitFactor>
                                                        (
                                                            query,
                                                            commandType: CommandType.Text
                                                        );
                    if (dataUnitFactor != null)
                    {
                        _cache.SetSlidingExpiration(cacheKey, dataUnitFactor);
                    }
                    return dataUnitFactor ?? new List<DataUnitFactor>();
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
            return dataUnitFactor;
        }
    }
}
