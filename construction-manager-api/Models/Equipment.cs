namespace construction_manager_api.Models;

public class Equipment
{
    public Guid Id { get; } = Guid.NewGuid();

    public string Name { get; set; } = null!;
}
