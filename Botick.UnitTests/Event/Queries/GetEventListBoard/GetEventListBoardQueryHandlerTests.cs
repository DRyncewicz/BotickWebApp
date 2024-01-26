using Application.UnitTests.Common;
using AutoMapper;
using BotickAPI.Application.Events.Queries.GetEventListForBoard;
using BotickAPI.Persistence.Context;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Event.Queries.GetEventListBoard
{
    [Collection("QueryCollection")]
    public class GetEventListBoardQueryHandlerTests
    {
        private readonly BotickDbContext _context;
        private readonly IMapper _mapper;
        private readonly Mock<ILogger<GetEventListBoardQueryHandler>> _logger;

        public GetEventListBoardQueryHandlerTests(QueryTestFixtures fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
            _logger = new Mock<ILogger<GetEventListBoardQueryHandler>>();
        }

        [Fact]
        public async Task CanGetEventListBoardForEventsInProgress()
        {
            var handler = new GetEventListBoardQueryHandler(_context, _mapper, _logger.Object);
        }
    }
}
