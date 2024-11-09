using Microsoft.EntityFrameworkCore;
using Server.Domain.Models;
using Server.Infrastructure.Configurations;

namespace Server.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
    
    public DbSet<User> Users => Set<User>();
}