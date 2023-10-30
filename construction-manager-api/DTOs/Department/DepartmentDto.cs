namespace construction_manager_api.DTOs.Department;

public record DepartmentDto
{
    public required int Id { get; init; }

    public required string Name { get; init; }
}