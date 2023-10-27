namespace construction_manager_api.Models;

public class Project
{
    public Guid Id { get; private set; }
    public decimal Expenses { get; set; }
    public Location Location { get; set; }
    
    // relationships
    public ICollection<Skill> RequiredSkills { get; set; }
    public ICollection<Employee> Employees { get; set; }
    public ICollection<Equipment> RequiredEquipment { get; set; }
}