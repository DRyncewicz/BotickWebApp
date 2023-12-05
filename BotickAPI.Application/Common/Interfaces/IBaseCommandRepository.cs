using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Application.Common.Interfaces
{
    public interface IBaseCommandRepository<T> where T : class
    {
        Task<int> AddAsync(T entity, CancellationToken cancellationToken);

        Task DeleteAsync(int recordId, CancellationToken cancellationToken);

        Task UpdateAsync(T entity, CancellationToken cancellationToken);
    }
}
