using construction_manager_api.Models;

namespace construction_manager_api.DTOs.Equipment;

public record CreateEquipmentRequest
{
    public required string Name { get; init; }
}