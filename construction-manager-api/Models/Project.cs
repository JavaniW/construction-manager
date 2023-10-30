namespace construction_manager_api.Models;

public class Project
{
    public Guid Id { get; } = Guid.NewGuid();
    
    public string Name { get; set; }

    public decimal Expenses { get; set; }
    
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Location? Location { get; set; }

    public virtual ICollection<Equipment> Equipment { get; set; } = new List<Equipment>();

    public virtual ICollection<Skill> RequiredSkills { get; set; } = new List<Skill>();
}
