namespace construction_manager_api.Models;

public class Project
{
    public Guid Id { get; private set; }
    public decimal Expenses { get; set; }
    
    // relationships
    public Guid LocationId { get; set; }
    public Location Location { get; set; }
    
    public ICollection<Skill> RequiredSkills { get; set; }
    public ICollection<Employee> Employees { get; set; }
    public ICollection<Equipment> RequiredEquipments { get; set; }

    public Project(Guid id, decimal expenses, Guid locationId, Location location, ICollection<Skill> requiredSkills, ICollection<Employee> employees, ICollection<Equipment> requiredEquipments)
    {
        Id = Guid.NewGuid();
        Expenses = expenses;
        LocationId = locationId;
        Location = location;
        RequiredSkills = requiredSkills;
        Employees = employees;
        RequiredEquipments = requiredEquipments;
    }
}