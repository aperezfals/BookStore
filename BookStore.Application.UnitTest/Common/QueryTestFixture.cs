using AutoMapper;
using BookStore.Application.Common.Mappings;
using BookStore.Persistence;
using System;

namespace BookStore.Application.UnitTest.Common
{
    public class QueryTestFixture : IDisposable
    {
        public BookStoreDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestFixture()
        {
            Context = BookStoreContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            BookStoreContextFactory.Destroy(Context);
        }
    }
}
