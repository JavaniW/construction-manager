namespace construction_manager_api.Models;

public class Employee
{
    public Guid Id { get; private set; } = new Guid();
    public string Name { get; set; }
    public string Title { get; set; }
    public decimal Payroll { get; set; }

    // relationships
    public Department Department { get; set; } //M-1
    public Project Project { get; set; } //M-1
    public ICollection<Skill> Skills { get; private set; } //1-M
    
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