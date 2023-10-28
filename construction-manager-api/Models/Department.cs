namespace construction_manager_api.Models;

public class Department
{
    public int Id { get; private set; }
    public string Name { get; set; }

    public Department(int id, string name)
    {
        Id = id;
        Name = name;
    }
}