namespace construction_manager_api.DTOs.Equipment;

public record EquipmentDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; } 
}