using System;
using System.Collections.Generic;

namespace construction_manager_api;

public class Location
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    // public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
