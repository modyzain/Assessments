using Dapper;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace App.Infrastructure.Services
{
    public static class Dapper
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Initialize(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static async Task<T> ExecuteScalarCommandAsync<T>(this IDbConnection connection, string query, object parameter = null, IDbTransaction transaction = null, int? timeOut = null, CommandType? commandType = null)
        {
            return await connection.ExecuteScalarAsync<T>(query, parameter, transaction, timeOut, commandType);
        }

        public static async Task<IEnumerable<T>> ExecuteAsync<T>(this IDbConnection connection, string query, object parameter = null, IDbTransaction transaction = null, int? timeOut = null, CommandType? commandType = null)
        {
            return await connection.QueryAsync<T>(query, parameter, transaction, timeOut, commandType);
        }

        public static async Task<T> FirstOrDefaultExecuteAsync<T>(this IDbConnection connection, string query, object parameter = null, IDbTransaction transaction = null, int? timeOut = null, CommandType? commandType = null)
        {
            return await connection.QueryFirstOrDefaultAsync<T>(query, parameter, transaction, timeOut, commandType);
        }
    }
}
