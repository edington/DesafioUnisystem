using DesafioUnisystem.Domain.Entities;
using DesafioUnisystem.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace DesafioUnisystem.Infrastructure;

public class AppDBContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AppDBContext(DbContextOptions<AppDBContext> options)
       : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
    }
}
