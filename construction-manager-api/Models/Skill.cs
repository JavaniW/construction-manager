﻿using System;
using System.Collections.Generic;

namespace construction_manager_api;

public class Skill
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    // public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    //
    // public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
