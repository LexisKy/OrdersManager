using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrdersManager.Models;

namespace OrdersManager.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Shop> Shops { get; set; }
    public DbSet<Order> Orders { get; set; }
}