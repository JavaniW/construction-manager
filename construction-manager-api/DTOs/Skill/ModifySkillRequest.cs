
using construction_manager_api;
using construction_manager_api.Models;

public record ModifySkillRequest
{
    public string Name { get; init; } = null!;
}