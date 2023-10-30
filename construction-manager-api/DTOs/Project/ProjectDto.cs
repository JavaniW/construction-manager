using construction_manager_api.Models;

namespace construction_manager_api.DTOs.Project;

public record ProjectDto
{
    public required Guid Id { get; set; }
    
    public required string Name { get; init; } 

    public required decimal Expenses { get; init; } 

    public required ICollection<string> Employees { get; init; }

    public string? Location { get; init; }
    
    public ICollection<string>? Equipment { get; init; }
    
    public ICollection<string>? RequiredSkills { get; init; }
}