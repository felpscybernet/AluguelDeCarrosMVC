using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AluguelDeCarrosMVC.Models;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Car> Carros { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Aluguel> Alugueis { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 

        
        modelBuilder.Entity<Car>()
            .Property(c => c.PrecoDiaria)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<Aluguel>()
            .Property(a => a.ValorTotal)
            .HasColumnType("decimal(18, 2)");
    }
}