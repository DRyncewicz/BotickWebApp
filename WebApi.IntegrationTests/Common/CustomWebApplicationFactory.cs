using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Infrastructure.Identity;
using BotickAPI.Persistence.Context;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApi.IntegrationTests.Common.DummyServices;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace WebApi.IntegrationTests.Common
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            try
            {
                builder
                .ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                        typeof(DbContextOptions<BotickDbContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<BotickDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });

                    services.AddScoped<ICurrentUserService, DummyCurrentUserService>();

                    var sp = services.BuildServiceProvider();

                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<BotickDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    try
                    {
                        Utilities.InitializeDbForTests(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            $"database with test messages. Error: {ex.Message}");
                    }
                })
                    .UseEnvironment("Test");
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<HttpClient> GetAuthenticatedClientAsync()
        {
            var client = CreateClient();

            var token = await GetAccessTokenAsync(client, "alice", "Pass123$");
            client.SetBearerToken(token);
            return client;
        }

        private async Task<string> GetAccessTokenAsync(HttpClient client, string userName, string userPassword)
        {
            var disco = await client.GetDiscoveryDocumentAsync();

            if (disco.IsError)
            {
                throw new Exception(disco.Error);
            }

            var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1",
                UserName = userName,
                Password = userPassword
            }) ;

            if (response.IsError)
            {
                throw new Exception(response.Error);
            }

            return response.AccessToken;
        }
    }
}
