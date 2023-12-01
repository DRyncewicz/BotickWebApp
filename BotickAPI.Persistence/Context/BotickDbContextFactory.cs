using BotickAPI.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BotickAPI.Persistence.Context
{
    public class BotickDbContextFactory : IDesignTimeDbContextFactory<BotickDbContext>
    {
        private readonly IDateTime _dateTime;
        public BotickDbContextFactory()
        {

        }

        public BotickDbContextFactory(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }
        public BotickDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("C:\\Users\\ereen\\source\\repos\\Botick\\BotickAPI\\appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<BotickDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));

            return new BotickDbContext(optionsBuilder.Options, _dateTime);
        }
    }
}
