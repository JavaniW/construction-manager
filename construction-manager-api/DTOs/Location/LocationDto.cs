using construction_manager_api.Models;

namespace construction_manager_api.DTOs.Location;

public record LocationDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; } 
}