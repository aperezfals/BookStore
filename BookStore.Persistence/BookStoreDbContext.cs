using BookStore.Application.Common.Interfaces;
using BookStore.Common;
using BookStore.Domain.Common;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Persistence
{
    public class BookStoreDbContext : DbContext, IBookStoreDbContext
    {
        private readonly IDateTime dateTime;
        private readonly ICurrentUserService currentUserService;

        public DbSet<Book> Books { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Order> Orders { get; set; }

        public BookStoreDbContext(IDateTime dateTime,
            ICurrentUserService currentUserService,
            DbContextOptions<BookStoreDbContext> options) : base(options)
        {
            this.dateTime = dateTime;
            this.currentUserService = currentUserService;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.LastUpdatedBy = currentUserService.UserId;
                        entry.Entity.LastUpdated = dateTime.Now;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedBy = currentUserService.UserId;
                        entry.Entity.Created = dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookStoreDbContext).Assembly);
        }
    }
}
