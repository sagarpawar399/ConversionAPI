using ConversionAPI.Configurations;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace ConversionAPI.Helper
{
    public class SQLHelper
    {
        private readonly string _connectionString;

        public SQLHelper(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"];
        }

        internal SqlConnection GetConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        internal void CloseConnection(SqlConnection sqlConnection)
        {
            if (sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }
    }
}
