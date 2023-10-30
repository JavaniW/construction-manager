using construction_manager_api;
using construction_manager_api.Models;

public record CreateSkillRequest
{
    public required string Name { get; init; } = null!;
}