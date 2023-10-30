using construction_manager_api.Models;

namespace construction_manager_api.DTOs.Employee;

public record ModifyEmployeeRequest
{
    public string Name { get; init; } = null!;

    public string Title { get; init; } = null!;

    public decimal Payroll { get; init; }

    public int DepartmentId { get; init; }
    
    public Guid ProjectId { get; init; }
    
    public ICollection<Skill>? Skills { get; init; }
}