using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Application.Common.Interfaces
{
    public interface IDbQueryService
    {
        Task<T> GetAsync<T>(string command, object parms);

        Task<List<T>> GetAll<T>(string command, object parms);

        Task Execute(string command, object parms);

        Task<List<TReturn>> GetAll<TFirst, TSecond, TThird, TReturn>(
           string query, Func<TFirst, TSecond, TThird, TReturn> map, object parms, string splitOn);
    }
}
