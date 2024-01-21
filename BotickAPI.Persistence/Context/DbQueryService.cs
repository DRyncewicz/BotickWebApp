using BotickAPI.Application.Common.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Persistence.Context
{
    internal class DbQueryService : IDbQueryService, IDisposable
    {
        private readonly SqlConnection _db;

        public DbQueryService(ISqlConnectionFactory connectionFactory)
        {
            _db = connectionFactory.CreateConnection();
        }

        public async Task<T> GetAsync<T>(string command, object parms)
        {
            return (await _db.QueryAsync<T>(command, parms).ConfigureAwait(false)).FirstOrDefault();
        }

        public async Task<List<T>> GetAll<T>(string command, object parms)
        {
            return (await _db.QueryAsync<T>(command, parms)).ToList();
        }

        public async Task Execute(string command, object parms)
        {
            await _db.ExecuteAsync(command, parms, transaction: null, commandTimeout: 60, commandType: CommandType.Text);
        }

        public async Task<List<TReturn>> GetAll<TFirst, TSecond, TThird, TReturn>(
           string query, Func<TFirst, TSecond, TThird, TReturn> map, object parms, string splitOn)
        {
            var result = await _db.QueryAsync<TFirst, TSecond, TThird, TReturn>(
                query, map, parms, splitOn: splitOn);

            return result.Distinct().ToList();
        }

        public async void Dispose()
        {
            await _db.DisposeAsync();
        }
    }
}
