using construction_manager_api.Models;

namespace construction_manager_api.DTOs.Project;

public record ModifyProjectRequest
{
    public required string Name { get; init; } 

    public required decimal Expenses { get; init; } 

    public required ICollection<Guid>? Employees { get; init; }

    public Guid? Location { get; init; }
    
    public ICollection<Guid>? Equipment { get; init; }
    
    public ICollection<int>? RequiredSkills { get; init; }
}