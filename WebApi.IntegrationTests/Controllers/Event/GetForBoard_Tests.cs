
using BotickAPI.Application.Events.Queries.GetEventListForBoard;
using Shouldly;
using WebApi.IntegrationTests.Common;
using Xunit;

namespace WebApi.IntegrationTests.Controllers.Event
{
    public class GetForBoard_Tests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public GetForBoard_Tests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenPageSizeAndPageNumber_ReturnsEventsWithValidStatus()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            int pageSize = 15;
            int pageNumber = 1;
            var response = await client.GetAsync($"/api/events?pageSize={pageSize}&pageNo={pageNumber}");
            response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<EventForListBoardVm>(response);
            vm.ShouldNotBeNull();
        }
    }
}
