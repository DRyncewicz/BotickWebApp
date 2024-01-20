using BotickAPI.Persistence.Context;
using Moq;

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
