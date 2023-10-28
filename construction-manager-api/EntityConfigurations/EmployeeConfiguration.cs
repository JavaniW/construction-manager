using construction_manager_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace construction_manager_api.EntityConfigurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employees");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name ).HasColumnName("name").IsRequired();
        builder.Property(e => e.Title ).HasColumnName("title");
        builder.Property(e => e.Payroll ).HasColumnName("payroll");

        builder.HasOne<Department>(e => e.Department).WithMany().HasForeignKey(e => e.DepartmentId);
        builder.HasMany<Skill>(e => e.Skills).WithMany().UsingEntity("employees_skills");
    }
}