namespace construction_manager_api.DTOs.Equipment;

public record ModifyEquipmentRequest
{
    public required string Name { get; init; }
}