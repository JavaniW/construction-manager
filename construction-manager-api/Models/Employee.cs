namespace construction_manager_api.Models;

public class Employee
{
    public Guid Id { get;} = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public string Title { get; set; } = null!;

    public decimal Payroll { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Project? Project { get; set; }

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
