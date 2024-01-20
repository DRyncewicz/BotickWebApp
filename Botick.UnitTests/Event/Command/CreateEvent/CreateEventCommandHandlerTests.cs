using Application.UnitTests.Common;
using AutoMapper;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Application.Events.Commands.CreateEvent;
using Castle.Core.Logging;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Event.Command.CreateEvent
{
    public class CreateEventCommandHandlerTests : CommandTestBase
    {
        private readonly CreateEventCommandHandler _handler;

        private readonly Mock<IMapper> _mapper;

        private readonly Mock<IFileSaver> _fileSaver;

        private readonly Mock<ICurrentUserService> _currentUserService;

        private readonly Mock<ISqlConnectionFactory> _connectionFactory;

        private readonly Mock<ILogger<CreateEventCommandHandler>> _logger;

        private readonly Mock<IBaseCommandRepository<BotickAPI.Domain.Entities.Event>> _repo;


        public CreateEventCommandHandlerTests() : base()
        {
            _mapper = new Mock<IMapper>();
            _fileSaver = new Mock<IFileSaver>();
            _currentUserService = new Mock<ICurrentUserService>();
            _connectionFactory = new Mock<ISqlConnectionFactory>();
            _repo = new Mock<IBaseCommandRepository<BotickAPI.Domain.Entities.Event>>();
            _logger = new Mock<ILogger<CreateEventCommandHandler>>();
            _handler = new CreateEventCommandHandler(_repo.Object, _mapper.Object, _fileSaver.Object, _currentUserService.Object, _connectionFactory.Object, _logger.Object);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldInsertEvent()
        {
            //Arrange
            _fileSaver.Setup(fs => fs.SaveImageFile(It.IsAny<byte[]>(), It.IsAny<string>()))
                .Returns("fake/path/to/image.jpg");
            _mapper.Setup(mapper => mapper.Map<BotickAPI.Domain.Entities.Event>(It.IsAny<CreateEventCommand>()))
                .Returns(new BotickAPI.Domain.Entities.Event());
            _repo.Setup(repo => repo.AddAsync(It.IsAny<BotickAPI.Domain.Entities.Event>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(1));
            var mockDbConnection = new Mock<IDbConnection>();
            _connectionFactory.Setup(factory => factory.CreateConnection()).Returns(mockDbConnection.Object);
            byte[] image = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13 };

            var command = new CreateEventCommand()
            {
                ArtistsId = [1],
                LocationsId = [1],
                Description = "description",
                StartTime = DateTime.Now.AddDays(14),
                EndTime = DateTime.Now.AddDays(15),
                Image = image,
                Name = "name",
                EventType = "type"
            };

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);

            var dir = await _context.EventReviews.FirstAsync(x => x.Id == result, CancellationToken.None);

            //Assert
            dir.ShouldNotBeNull();
        }
    }
}
