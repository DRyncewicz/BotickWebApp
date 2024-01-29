using BotickAPI.Application.Events.Queries.GetEventListForModificationAndApprovalPhase;
using Shouldly;
using WebApi.IntegrationTests.Common;
using Xunit;
using Xunit.Abstractions;

namespace WebApi.IntegrationTests.Controllers.Event
{
    public class GetForModificationAndApprovalPhase_Tests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        private readonly ITestOutputHelper _output;

        public GetForModificationAndApprovalPhase_Tests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output)
        {
            _factory = factory;
            _output = output;
        }

        [Fact]
        public async Task GivenPageSizeAndPageNumberWithValidUserRole_RetursEventsForValidUser()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            int pageSize = 15;
            int pageNumber = 1;
            var response = await client.GetAsync($"/user-events?pageSize={pageSize}&currentPage={pageNumber}");
            response.EnsureSuccessStatusCode();

            var vm = await Utilities.GetResponseContent<ListEventForListModificationAndApprovalPhase>(response);
            vm.ShouldNotBeNull();
            _output.WriteLine("Values:");
            _output.WriteLine($"{await response.Content.ReadAsStringAsync()}");
        }
    }
}
