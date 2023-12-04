using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Domain.Exceptions;
using Dapper;

namespace BotickAPI.Persistence.Repositories
{
    public class BaseQueryRepository<T>(IDbConnection dbConnection, string tableName) : IBaseQueryRepository<T> where T : class
    {
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = $"SELECT * FROM {tableName}";
            return await dbConnection.QueryAsync<T>(query);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var query = $"SELECT * FROM {tableName} WHERE Id = @Id";
            try
            {
                return await dbConnection.QuerySingleOrDefaultAsync<T>(query, new { Id = id });
            }
            catch
            {
                throw new NotFoundException();
            }
        }
    }
}
