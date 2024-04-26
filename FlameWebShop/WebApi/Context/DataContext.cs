using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using WebApi.Models.Entities;

namespace WebApi.Context
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {        
        }

        public DbSet<UserProfileEntity> UserProfileEntities { get; set; }
       // public DbSet<UserProfileAddressItemEntity> UserAddressItems { get; set; }
        //public DbSet<AddressEntity> AddressEntities { get; set; }
        //public DbSet<AddressItemEntity> AddressItems { get; set; }
        public DbSet<CreditCardEntity> CreditCardEntities { get; set; }
        public DbSet<ProductEntity> ProductEntities { get; set; }
        public DbSet<CategoryEntity> CategoryEntities { get; set; }
        public DbSet<ReviewEntity> ReviewEntities { get; set; }
        public DbSet<OrderEntity> OrderEntities { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<AspNetRoles>().ToTable(nameof(TableNames), t =>
        //    t.ExcludeFromMigrations());
        //}

    }
}

