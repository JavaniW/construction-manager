using construction_manager_api.Models;

namespace construction_manager_api.DTOs.Employee;

public record CreateEmployeeRequest
{
    public required string Name { get; init; } = null!;

    public required string Title { get; init; } = null!;

    public required decimal Payroll { get; init; }

    public int DepartmentId { get; init; }
    
    public Guid ProjectId { get; init; }
    
    public ICollection<Skill>? Skills { get; init; }
}