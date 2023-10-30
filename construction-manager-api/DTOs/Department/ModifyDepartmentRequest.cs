namespace construction_manager_api.DTOs.Department;

public record ModifyDepartmentRequest
{
    public required string Name { get; init; }
};