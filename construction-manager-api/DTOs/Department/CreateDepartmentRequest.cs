namespace construction_manager_api.DTOs.Department;

public record CreateDepartmentRequest
{
    public required string Name { get; init; }
};