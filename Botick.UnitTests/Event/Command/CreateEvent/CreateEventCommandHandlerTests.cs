using Application.UnitTests.Common;
using Application.UnitTests.Mapping;
using AutoMapper;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Application.Events.Commands.CreateEvent;
using Castle.Core.Logging;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
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
    public class CreateEventCommandHandlerTests : IClassFixture<MappingTestFixture>
    {
        private readonly CreateEventCommandHandler _handler;

        private readonly TestRepository<BotickAPI.Domain.Entities.Event> _repo = new TestRepository<BotickAPI.Domain.Entities.Event>();

        private readonly IConfigurationProvider _configuration;

        private readonly IMapper _mapp;

        private readonly Mock<IFileSaver> _fileSaver;

        private readonly Mock<ICurrentUserService> _currentUserService;

        private readonly Mock<IDbQueryService> _dbQueryService;

        private readonly Mock<ILogger<CreateEventCommandHandler>> _logger;

        public CreateEventCommandHandlerTests(MappingTestFixture fixture) : base()
        {
            _configuration = fixture.ConfigurationProvider;
            _mapp = fixture.Mapper;
            _fileSaver = new Mock<IFileSaver>();
            _currentUserService = new Mock<ICurrentUserService>();
            _dbQueryService = new Mock<IDbQueryService>();
            _logger = new Mock<ILogger<CreateEventCommandHandler>>();
            _handler = new CreateEventCommandHandler(_repo, _mapp, _fileSaver.Object, _currentUserService.Object, _dbQueryService.Object, _logger.Object);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldInsertEvent()
        {
            //Arrange
            _fileSaver.Setup(fs => fs.SaveImageFile(It.IsAny<byte[]>(), It.IsAny<string>()))
                .Returns("fake/path/to/image.jpg");
            _currentUserService.Setup(v => v.Email).Returns("user@user.pl");

            byte[] image = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13 };

            var command = new CreateEventCommand()
            {
                ArtistsId = [1],
                LocationsId = [1],
                Description = "Test description with 100 chars Test description with 100 chars Test description with 100 chars Test description with 100 chars Test description with 100 chars",
                StartTime = DateTime.Now.AddDays(14),
                EndTime = DateTime.Now.AddDays(15),
                Image = image,
                Name = "name",
                EventType = "type"
            };

            //Act
            var result = await _handler.Handle(command, CancellationToken.None);
            var resultCheck = await _repo._cont.Events.FirstAsync(x => x.Id == result, CancellationToken.None);

            //Assert
            result.ShouldBe<int>(2);
            resultCheck.ShouldNotBeNull();
        }

        public void Dispose()
        {
            _repo.Dispose();
        }
    }
}
