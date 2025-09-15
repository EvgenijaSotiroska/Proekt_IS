using CoffeeEShop.Domain.DomainModels;
using CoffeeEShop.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffeeEShop.Repository;

public class ApplicationDbContext : IdentityDbContext<SystemUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Domain.DomainModels.Shop> CoffeeShops { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<ProductInCoffeeShop> ProductInCoffeeShops { get; set; }
    public virtual DbSet<ProductInOrder> ProductInOrder { get; set; }
}
