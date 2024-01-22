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
    public class BaseCommandRepository<T>(BotickDbContext dbContext)
    {
        public async Task<int> AddAsync(T entity, CancellationToken cancellationToken)
        {

            await dbContext.SaveChangesAsync(cancellationToken);
            return 1;
        }

     
    }
}
