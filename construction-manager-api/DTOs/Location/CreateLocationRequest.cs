namespace construction_manager_api.DTOs.Location;

public record CreateLocationRequest
{
    public required string Name { get; init; }
}