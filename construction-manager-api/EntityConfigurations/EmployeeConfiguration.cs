using construction_manager_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace construction_manager_api.EntityConfigurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("employees");

        builder.HasIndex("DepartmentId");

        builder.HasIndex("ProjectId");

        builder.Property<int?>("DepartmentId").HasColumnName("department_id");
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        builder.Property(e => e.Payroll)
                .HasPrecision(10)
                .HasColumnName("payroll");
        builder.Property<Guid>("ProjectId").HasColumnName("project_id");
        builder.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");

        builder.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey("DepartmentId")
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_employees_department_id_departments");

        builder.HasOne(d => d.Project).WithMany(p => p.Employees)
                .HasForeignKey("ProjectId")
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_employees_project_id_projects");

        builder.HasMany(d => d.Skills).WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeesSkills",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillId")
                        .HasConstraintName("fk_employees_skills_skill_id_skills"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("fk_employees_skills_employee_id_employees"),
                    j =>
                    {
                        j.HasKey("EmployeeId", "SkillId").HasName("PRIMARY");
                        j.ToTable("employees_skills");
                        j.HasIndex(new[] { "SkillId" }, "fk_employees_skills_skill_id_skills");
                        j.IndexerProperty<Guid>("EmployeeId").HasColumnName("employee_id");
                        j.IndexerProperty<int>("SkillId").HasColumnName("skill_id");
                    });
                    // builder.ToTable("employees");
                    //
                    // builder.HasKey(e => e.Id);
                    //
                    // builder.Property(e => e.Name ).HasColumnName("name").IsRequired();
                    // builder.Property(e => e.Title ).HasColumnName("title");
                    // builder.Property(e => e.Payroll ).HasColumnName("payroll");
                    // // builder.Property(e => e.DepartmentId).HasColumnName("department_id");
                    // // builder.Property(e => e.ProjectId).HasColumnName("project_id");
                    //
                    // builder.HasOne<Project>(e => e.Project).WithMany(p => p.Employees).HasForeignKey("project_id");
                    // builder.HasOne<Department>(e => e.Department).WithMany().HasForeignKey("department_id");
                    //
                    // builder.HasMany<Skill>(e => e.Skills).WithMany().UsingEntity(
                    // l => l.HasOne(typeof(Employee)).WithMany().HasForeignKey("employee_id").HasPrincipalKey(nameof(Employee.Id)),
                    // r => r.HasOne(typeof(Skill)).WithMany().HasForeignKey("skill_id").HasPrincipalKey(nameof(Skill.Id)),
                    // j => j.ToTable("employees_skills").HasKey("employee_id", "skill_id"));
    }
}