using construction_manager_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace construction_manager_api.EntityConfigurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("projects");

        builder.HasIndex("LocationId");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Expenses)
            .HasPrecision(10)
            .HasColumnName("expenses");
        builder.Property<Guid?>("LocationId").HasColumnName("location_id"); //
        // builder.Property(e => e.LocationId).HasColumnName("location_id");

        builder.HasOne(d => d.Location).WithMany()
            .HasForeignKey("LocationId")
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("fk_projects_location_id_locations");

        builder.HasMany(d => d.Equipment).WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProjectsEquipments",
                r => r.HasOne<Equipment>().WithMany()
                    .HasForeignKey("EquipmentId")
                    .HasConstraintName("fk_projects_equipments_equipment_id_equipments"),
                l => l.HasOne<Project>().WithMany()
                    .HasForeignKey("ProjectId")
                    .HasConstraintName("fk_projects_equipments_project_id_projects"),
                j =>
                {
                    j.HasKey("ProjectId", "EquipmentId").HasName("PRIMARY");
                    j.ToTable("projects_equipments");
                    j.HasIndex(new[] { "EquipmentId" }, "fk_projects_equipments_equipment_id_equipments");
                    j.IndexerProperty<Guid>("ProjectId").HasColumnName("project_id");
                    j.IndexerProperty<Guid>("EquipmentId").HasColumnName("equipment_id");
                });

        builder.HasMany(d => d.RequiredSkills).WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "ProjectsSkills",
                r => r.HasOne<Skill>().WithMany()
                    .HasForeignKey("SkillId")
                    .HasConstraintName("fk_projects_skills_skill_id_skills"),
                l => l.HasOne<Project>().WithMany()
                    .HasForeignKey("ProjectId")
                    .HasConstraintName("fk_projects_skills_project_id_projects"),
                j =>
                {
                    j.HasKey("ProjectId", "SkillId").HasName("PRIMARY");
                    j.ToTable("projects_skills");
                    j.HasIndex(new[] { "SkillId" }, "fk_projects_skills_skill_id_skills");
                    j.IndexerProperty<Guid>("ProjectId").HasColumnName("project_id");
                    j.IndexerProperty<int>("SkillId").HasColumnName("skill_id");
                });
    }
}