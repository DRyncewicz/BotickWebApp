using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Persistence.Context;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace WebApi.IntegrationTests.Common
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            try
            {

                builder.ConfigureServices(services =>
                {
                    var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();
                    
                    services.AddDbContext<BotickDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDatabase");
                        options.UseInternalServiceProvider(serviceProvider);
                    });
                    services.AddScoped<IBotickDbContext>(provider => provider.GetService<BotickDbContext>());

                    var sp = services.BuildServiceProvider();

                    using var scope = sp.CreateScope();
                    var scopedService = scope.ServiceProvider;
                    var context = scopedService.GetRequiredService<BotickDbContext>();
                    var logger = scopedService.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    context.Database.EnsureCreated();

                    try
                    {
                        Utilities.InitializeDbForTests(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the" +
                            $"databgase with test messages. Error: {ex.Message}");
                    }
                })
                    .UseEnvironment("Test");
            }
            catch (Exception ex)
            {

            }
            base.ConfigureWebHost(builder);
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
            });

            if (response.IsError)
            {
                throw new Exception(response.Error);
            }

            return response.AccessToken;
        }
    }
}
