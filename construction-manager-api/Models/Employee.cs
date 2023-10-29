using System;
using System.Collections.Generic;

namespace construction_manager_api;

public class Employee
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Title { get; set; } = null!;

    public decimal Payroll { get; set; }

    // public int? DepartmentId { get; set; }

    // public Guid? ProjectId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Project? Project { get; set; }

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
