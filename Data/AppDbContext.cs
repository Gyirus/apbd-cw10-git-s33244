using System.Xml;
using cw10.Models;
using Microsoft.EntityFrameworkCore;

namespace cw10.Data;

public class AppDbContext : DbContext
{
    public  AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}
    
    public DbSet<PC> PCs { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }
    public DbSet<PCComponent> PcComponents { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<PC>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Weight).HasPrecision(5);
        });
        
        modelBuilder.Entity<ComponentManufacturer>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Abbreviation).IsRequired().HasMaxLength(30);
            entity.Property(m => m.FullName).IsRequired().HasMaxLength(300);
            entity.Property(m => m.FoundationDate).IsRequired();
        });


        modelBuilder.Entity<ComponentType>(entity =>
        {
            entity.HasKey(t => t.Id);
    
            entity.Property(t => t.Abbreviation).IsRequired().HasMaxLength(30);
            entity.Property(t => t.Name).IsRequired().HasMaxLength(150);
        });


        modelBuilder.Entity<Component>(entity =>
        {
            entity.HasKey(c => c.Code);
            entity.Property(c => c.Code).HasMaxLength(10).IsFixedLength().IsRequired();
            entity.Property(c => c.Name).HasMaxLength(300).IsRequired();
            entity.Property(c => c.Description).IsRequired();
                
            entity.HasOne(c => c.Manufacturer)
                .WithMany(m => m.Components)
                .HasForeignKey(c => c.ComponentManufacturersId)
                .OnDelete(DeleteBehavior.Restrict);
                
            entity.HasOne(c => c.Type)
                .WithMany(t => t.Components)
                .HasForeignKey(c => c.ComponentTypesId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        
        
        modelBuilder.Entity<PCComponent>(entity =>
        {
            entity.HasKey(pc => new { pc.PCId, pc.ComponentCode });
    
            entity.Property(pc => pc.Amount).IsRequired();
    
            entity.HasOne(pc => pc.PC)
                .WithMany(p => p.PCComponents)
                .HasForeignKey(pc => pc.PCId);
    
            entity.HasOne(pc => pc.Component)
                .WithMany(c => c.PCComponents)
                .HasForeignKey(pc => pc.ComponentCode);
        });
    }
}