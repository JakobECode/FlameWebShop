using Microsoft.EntityFrameworkCore;
using WebApi.Models.Entities;

namespace WebApi.Context
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ProductEntity>()
            //    .ToContainer("Products")
            //    .HasPartitionKey(x => x.PartitionKey);

            //modelBuilder.Entity<CategoryEntity>()
            //    .ToContainer("Categories")
            //    .HasPartitionKey(x => x.PartitionKey);
            //modelBuilder.Entity<ReviewEntity>()
            //    .ToContainer("Reviews")
            //    .HasPartitionKey(x => x.PartitionKey);

            //modelBuilder.Entity<OrderEntity>()
            //    .ToContainer("Orders")
            //    .HasPartitionKey(x => x.PartitionKey);
        }
    }
}
