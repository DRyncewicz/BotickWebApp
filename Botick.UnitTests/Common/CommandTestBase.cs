using BotickAPI.Persistence.Context;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly BotickDbContext _context;
        protected readonly Mock<BotickDbContext> _contextMock;

        public CommandTestBase()
        {
            _contextMock = BotickDbContextFactory.Create();
            _context = _contextMock.Object;
        }

        public void Dispose()
        {
            BotickDbContextFactory.Destroy(_context);
        }
    }
}
