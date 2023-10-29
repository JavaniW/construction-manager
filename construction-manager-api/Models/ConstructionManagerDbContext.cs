using construction_manager_api.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace construction_manager_api.Models;

public partial class ConstructionManagerDbContext : DbContext
{
    public ConstructionManagerDbContext()
    {
    }

    public ConstructionManagerDbContext(DbContextOptions<ConstructionManagerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }
    
    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Equipment> Equipments { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        modelBuilder.ApplyConfiguration(new SkillConfiguration());
        modelBuilder.ApplyConfiguration(new LocationConfiguration());
        modelBuilder.ApplyConfiguration(new EquipmentConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        base.OnModelCreating(modelBuilder);
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}