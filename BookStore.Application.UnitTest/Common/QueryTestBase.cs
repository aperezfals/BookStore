using AutoMapper;
using BookStore.Application.Common.Mappings;
using BookStore.Persistence;
using System;

namespace BookStore.Application.UnitTest.Common
{
    public class QueryTestBase : IDisposable
    {
        public BookStoreDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestBase()
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
