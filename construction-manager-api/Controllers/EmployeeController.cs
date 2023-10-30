using construction_manager_api.DTOs.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using construction_manager_api.Models;

namespace construction_manager_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly ConstructionManagerDbContext _context;
    public EmployeeController(ConstructionManagerDbContext context)
    {
        _context = context;
    }

    // GET: api/Employee
    [HttpGet]
    public async Task<ActionResult<List<EmployeeDto>>> GetEmployees() {
        //Create list of all employees from _context and return list
        return await _context.Employees
            .OrderBy(n => n.Name)
            .Include(e => e.Department)
            .Include(e => e.Project)
            .Include(e => e.Skills)
            .Select(e => new EmployeeDto
        {
            Id = e.Id,
            Name = e.Name,
            Title = e.Title,
            Payroll = e.Payroll,
            Department = e.Department == null ? null : e.Department.Name,
            Project = e.Project == null ? null : e.Project.Name,
            Skills = e.Skills.Select(s => s.Name).ToList()
        }).ToListAsync();
    }

    // GET: api/Employee/1
    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetEmployee(Guid id)
    {
        //Return specific employee by id
        var employee = await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Project)
            .Include(e => e.Skills)
            .FirstOrDefaultAsync(e => e.Id.Equals(id));
        
        if (employee is null) return NotFound();
        var employeeDto =new EmployeeDto
        {
            Id = employee.Id,
            Name = employee.Name,
            Title = employee.Title,
            Payroll = employee.Payroll,
            Department = employee.Department?.Name,
            Project = employee.Project?.Name,
            Skills = employee.Skills.Select(s => s.Name).ToList()
        };
        return Ok(employeeDto);
    }

    // PUT: api/Employee/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmployee(Guid id, ModifyEmployeeRequest request)
    {
        var employeeUpdate = await _context.Employees.FindAsync(id);
        if (employeeUpdate == null)
        {
            return NotFound();
        }
        
        var department =  await _context.Departments.FindAsync(request.DepartmentId);
        var project = await _context.Projects.FindAsync(request.ProjectId);

        employeeUpdate.Name = request.Name;
        employeeUpdate.Title = request.Title;
        employeeUpdate.Payroll = request.Payroll;
        //employeeUpdate.DepartmentId = employee.DepartmentId;
        employeeUpdate.Department = department;
        employeeUpdate.Project = project;
        //employeeUpdate.ProjectId = employee.ProjectId;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // POST: api/Employee
    [HttpPost]
    public async Task<ActionResult> PostEmployee(CreateEmployeeRequest request)
    {
        var department =  await _context.Departments.FindAsync(request.DepartmentId);
        var project = await _context.Projects.FindAsync(request.ProjectId);
        //Create new employee using attributes of employee ogj
        var employee = new Employee
        {
            Name = request.Name,
            Title = request.Title,
            Payroll = request.Payroll,
            Department = department,
            Project = project
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEmployee), new {id = employee.Id});
    }

    // DELETE: api/Employee/3
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEmployee(Guid id)
    {
        //Delete employee with passed id
        var deleteEmployee = await _context.Employees.FindAsync(id);
        if (deleteEmployee == null)
        {
            return NotFound();
        }

        _context.Employees.Remove(deleteEmployee);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}