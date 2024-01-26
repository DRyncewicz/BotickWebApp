using AutoMapper;
using BotickAPI.Application.Common.Mappings;
using BotickAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Common
{
    public class QueryTestFixtures : IDisposable
    {
        public BotickDbContext Context { get; private set; }

        public IMapper Mapper { get; private set; }

        public QueryTestFixtures()
        {
            Context = BotickDbContextFactory.Create().Object;

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            BotickDbContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryColletion")]
    public class QueryCollection : ICollectionFixture<QueryTestFixtures>
    {

    }
}
