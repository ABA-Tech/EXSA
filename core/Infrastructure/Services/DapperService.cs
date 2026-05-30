using Dapper;
using Domain.Services;
using System.Data;

namespace Infrastructure.Services
{
    public class DapperService: IDapperService
    {
        private readonly IDbConnection _dbConnection;
        public DapperService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public IEnumerable<T> Query<T>(string sql, object parameters = null)
        {
            return _dbConnection.Query<T>(sql, parameters);
        }
        public T QuerySingleOrDefault<T>(string sql, object parameters = null)
        {
            return _dbConnection.QuerySingleOrDefault<T>(sql, parameters);
        }
    }
}
