namespace construction_manager_api.Models;

public class Employee
{
    public Guid Id { get; private set; } = new Guid();
    public string Name { get; set; }
    public string Title { get; set; }
    public decimal Payroll { get; set; }

    // relationships
    
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; } //M-1
    
    public Guid ProjectId { get; set; }
    public virtual Project? Project { get; set; } //M-1
    public virtual ICollection<Skill> Skills { get; private set; } //1-M

    public Employee(string name, string title, decimal payroll, Guid departmentId, Department department, Guid projectId, ICollection<Skill> skills)
    {
        Name = name;
        Title = title;
        Payroll = payroll;
        DepartmentId = departmentId;
        Department = department;
        ProjectId = projectId;
        Skills = skills;
    }

    // helpers
    public void AddSkill(Skill skill)
    {
        Skills.Add(skill);
    }

    public void RemoveSkill(int skillId)
    {
        Skill toRemove = Skills.Single(s => s.Id == skillId);
        Skills.Remove(toRemove);
    }

    public void ChangeDepartment(Department dept)
    {
        Department = dept;
    }
}