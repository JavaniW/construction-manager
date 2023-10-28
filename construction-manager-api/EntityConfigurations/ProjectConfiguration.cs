using construction_manager_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace construction_manager_api.EntityConfigurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("projects");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.Expenses).IsRequired().HasColumnName("expenses");

        builder.HasOne<Location>(p => p.Location).WithMany().HasForeignKey(p => p.LocationId);
        builder.HasMany<Employee>(p => p.Employees).WithOne(e => e.Project).HasForeignKey(p => p.ProjectId);
        builder.HasMany<Skill>(p => p.RequiredSkills).WithMany().UsingEntity("projects_skills");
        builder.HasMany<Equipment>(p => p.RequiredEquipments).WithMany().UsingEntity("projects_equipments");
    }
}