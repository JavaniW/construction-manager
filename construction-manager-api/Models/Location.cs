namespace construction_manager_api.Models;

public class Location
{
    public int Id { get; private set; }
    public string Name { get; set; }

    public Location(int id, string name)
    {
        Id = id;
        Name = name;
    }
}