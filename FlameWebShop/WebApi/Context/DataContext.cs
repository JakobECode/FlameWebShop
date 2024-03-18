using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi.Models.Entities;

namespace WebApi.Context
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        DbSet<UserProfileEntity> UserProfileEntities { get; set; }
        DbSet<UserProfileAddressItemEntity> UserAddressItems { get; set; }
        DbSet<AddressEntity> AddressEntities { get; set; }
        DbSet<AddressItemEntity> AddressItems { get; set; }
        DbSet<CreditCardEntity> CreditCardEntities { get; set; }
        DbSet<UserProfileCreditCardEntity> UserProfileCreditCards { get; set; }
        DbSet<ProductEntity> Products { get; set; }
        DbSet<CategoryEntity> Categories { get; set; }
        DbSet<ReviewEntity> Reviews { get; set; }
        DbSet<OrderEntity> Orders { get; set; }

    }
}

