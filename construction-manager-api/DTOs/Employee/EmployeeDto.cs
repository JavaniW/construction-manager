using construction_manager_api.Models;

namespace construction_manager_api.DTOs.Employee;

public record EmployeeDto
{
    public required Guid Id { get; set; }
    public required string Name { get; init; } 

    public required string Title { get; init; } 

    public required decimal Payroll { get; init; }

    public string? Department { get; init; }
    
    public string? Project { get; init; }
    
    public ICollection<string>? Skills { get; init; }
}