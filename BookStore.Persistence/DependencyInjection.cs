using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using BookStore.Application.Common.Interfaces;

namespace BookStore.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookStoreDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BookStoreDatabase")));

            services.AddScoped<IBookStoreDbContext>(provider => provider.GetService<BookStoreDbContext>());

            return services;
        }
    }
}
