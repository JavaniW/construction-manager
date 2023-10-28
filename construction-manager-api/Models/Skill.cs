namespace construction_manager_api.Models;

public class Skill
{
    public int Id { get; private set; }
    public string Name { get; set; }

    public Skill(int id, string name)
    {
        Id = id;
        Name = name;
    }
}