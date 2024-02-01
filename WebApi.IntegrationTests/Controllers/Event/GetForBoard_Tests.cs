
using BotickAPI.Application.Events.Queries.GetEventListBoard;
using BotickAPI.Application.Events.Queries.GetEventListForBoard;
using BotickAPI.Application.Events.Queries.GetEventListForModificationAndApprovalPhase;
using Shouldly;
using WebApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace WebApi.IntegrationTests.Controllers.Event
{
    public class GetForBoard_Tests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        private readonly ITestOutputHelper _output;

        public GetForBoard_Tests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;
        }

        [Fact]
        public async Task GivenPageSizeAndPageNumber_ReturnsEventsWithValidStatus()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            int pageSize = 15;
            int pageNumber = 1;
            var response = await client.GetAsync($"/api/events?pageSize={pageSize}&currentPage={pageNumber}");
            response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<ListEventForListBoardVm>(response);
            vm.ShouldNotBeNull();
            _output.WriteLine("Values:");
            _output.WriteLine($"{response.Headers}");
            _output.WriteLine($"{await response.Content.ReadAsStringAsync()}");
            //Just test for git flow
        }
    }
}
