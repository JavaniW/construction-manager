namespace construction_manager_api.Models;

public class Equipment
{
    public Guid Id { get; private set; }
    public string Name { get; set; }

    public Equipment(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}