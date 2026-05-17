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
        
        
        
          modelBuilder.Entity<ComponentType>().HasData(
        new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Processor" },
        new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Card" },
        new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Memory" }
    );

    modelBuilder.Entity<ComponentManufacturer>().HasData(
        new ComponentManufacturer 
        { 
            Id = 1, 
            Abbreviation = "AMD", 
            FullName = "Advanced Micro Devices", 
            FoundationDate = new DateTime(1969, 5, 1) 
        },
        new ComponentManufacturer 
        { 
            Id = 2, 
            Abbreviation = "NV", 
            FullName = "NVIDIA Corporation", 
            FoundationDate = new DateTime(1993, 4, 5) 
        },
        new ComponentManufacturer 
        { 
            Id = 3, 
            Abbreviation = "COR", 
            FullName = "Corsair Gaming Inc.", 
            FoundationDate = new DateTime(1994, 1, 1) 
        }
    );

    modelBuilder.Entity<Component>().HasData(
        new Component 
        { 
            Code = "CPU0000001", 
            Name = "Ryzen 7 7800X3D", 
            Description = "8-core gaming processor, 3D V-Cache", 
            ComponentManufacturersId = 1,  
            ComponentTypesId = 1            
        },
        new Component 
        { 
            Code = "GPU0000001", 
            Name = "RTX 4080 Super", 
            Description = "High-end gaming graphics card with 16GB VRAM", 
            ComponentManufacturersId = 2, 
            ComponentTypesId = 2           
        },
        new Component 
        { 
            Code = "RAM0000001", 
            Name = "Corsair Vengeance DDR5 16GB", 
            Description = "DDR5 RAM module 16GB 5200MHz", 
            ComponentManufacturersId = 3,  
            ComponentTypesId = 3            
        },
        new Component 
        { 
            Code = "SSD0000001", 
            Name = "Samsung 980 Pro 1TB", 
            Description = "NVMe M.2 SSD, read speed 7000 MB/s", 
            ComponentManufacturersId = 3,  
            ComponentTypesId = 3           
        }
    );

    modelBuilder.Entity<PC>().HasData(
        new PC 
        { 
            Id = 1, 
            Name = "Gaming Beast X", 
            Weight = 12.5, 
            Warranty = 36, 
            CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0), 
            Stock = 5 
        },
        new PC 
        { 
            Id = 2, 
            Name = "Office Mini Pro", 
            Weight = 4.2, 
            Warranty = 24, 
            CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0), 
            Stock = 12 
        },
        new PC 
        { 
            Id = 3, 
            Name = "Workstation Ultra", 
            Weight = 15.8, 
            Warranty = 48, 
            CreatedAt = new DateTime(2026, 3, 20, 10, 0, 0), 
            Stock = 3 
        }
    );

    modelBuilder.Entity<PCComponent>().HasData(
        new { PCId = 1, ComponentCode = "CPU0000001", Amount = 1 },
        new { PCId = 1, ComponentCode = "GPU0000001", Amount = 1 },
        new { PCId = 1, ComponentCode = "RAM0000001", Amount = 2 },
        
        new { PCId = 2, ComponentCode = "CPU0000001", Amount = 1 },
        new { PCId = 2, ComponentCode = "RAM0000001", Amount = 1 },
        
        new { PCId = 3, ComponentCode = "CPU0000001", Amount = 1 },
        new { PCId = 3, ComponentCode = "GPU0000001", Amount = 1 },
        new { PCId = 3, ComponentCode = "RAM0000001", Amount = 4 }
    );
}
    }
