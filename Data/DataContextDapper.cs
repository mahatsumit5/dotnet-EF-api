using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DotnetApi.Data
{
    class DataContextDapper(IConfiguration config)
    {
        private readonly IConfiguration _config = config;

        public IEnumerable<G> LoadData<G>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(
                _config.GetConnectionString("DefaultConnection")
            );

            return dbConnection.Query<G>(sql);
        }

        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(
                _config.GetConnectionString("DefaultConnection")
            );

            return dbConnection.QuerySingle<T>(sql);
        }

        public bool ExecuteSql(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(
                _config.GetConnectionString("DefaultConnection")
            );
            return dbConnection.Execute(sql) > 0;
        }

        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(
                _config.GetConnectionString("DefaultConnection")
            );
            return dbConnection.Execute(sql);
        }
    }
}
