using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Domain.Common;
using BotickAPI.Domain.Exceptions;
using BotickAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;

namespace BotickAPI.Persistence.Repositories
{
    public class BaseCommandRepository<T>(BotickDbContext dbContext) : IBaseCommandRepository<T> where T : AuditableEntity
    {
        public async Task<int> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await dbContext.Set<T>().AddAsync(entity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task DeleteAsync(int recordId, CancellationToken cancellationToken)
        {
                var item = await dbContext.Set<T>().FirstOrDefaultAsync(d => d.Id == recordId, cancellationToken);

                if (item != null)
                {
                    throw new NotFoundException("Item selected to be deleted has been not found in database");
                }

                dbContext.Set<T>().Remove(item);
                await dbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            dbContext.Set<T>().Update(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
