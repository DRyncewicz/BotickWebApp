using BotickAPI.Application.Common.Exceptions;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Domain.Common;
using BotickAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Common
{
    public class TestRepository<T> : IDisposable, IBaseCommandRepository<T> where T : AuditableEntity
    {
        protected readonly BotickDbContext _context;

        protected readonly Mock<BotickDbContext> _contextMock;

        internal BotickDbContext _cont { get; set; }

        public TestRepository()
        {
            _contextMock = BotickDbContextFactory.Create();
            _context = _contextMock.Object;
            _cont = _contextMock.Object;
        }

        public async Task<int> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task DeleteAsync(int recordId, CancellationToken cancellationToken)
        {
            var item = await _context.Set<T>().FirstOrDefaultAsync(d => d.Id == recordId, cancellationToken);

            if (item != null)
            {
                throw new NotFoundException("Item selected to be deleted has been not found in database");
            }

            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            BotickDbContextFactory.Destroy(_cont);
            BotickDbContextFactory.Destroy(_context);
        }
    }
}
