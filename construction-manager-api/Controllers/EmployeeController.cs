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
    public Task<List<Employee>> GetEmployees() =>
        //Create list of all employees from _context and return list
        _context.Employees.OrderBy(n => n.Name).ToListAsync();

    // GET: api/Employee/1
    [HttpGet("{id}")]
    public IActionResult GetEmployee(int id)
    {
        //Return specific employee by id
        var employee = _context.Employees.Find(id);
        return employee == null ? NotFound() : Ok(employee);
    }

    // PUT: api/Employee/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmployee(Guid id, Employee employee)
    {
        //Udate employee with passed id using the attributes of the passed employee obj
        if (!id.Equals(employee.Id))
        {
            return BadRequest();
        }

        var employeeUpdate = await _context.Employees.FindAsync(id);
        if (employeeUpdate == null)
        {
            return NotFound();
        }

        employeeUpdate.Name = employee.Name;
        employeeUpdate.Title = employee.Title;
        employeeUpdate.Payroll = employee.Payroll;
        //employeeUpdate.DepartmentId = employee.DepartmentId;
        employeeUpdate.Department = employee.Department;
        //employeeUpdate.ProjectId = employee.ProjectId;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!EmployeeExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Employee
    [HttpPost]
    public ActionResult<Employee> PostEmployee(Employee employee)
    {
        //Create new employee using attributes of employee ogj
        var createEmployee = new Employee();

        _context.Employees.Add(createEmployee);
        _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEmployee), new {id = createEmployee.Id});
    }

    // DELETE: api/Employee/3
    [HttpDelete("{id}")]
    public ActionResult<Employee> DeleteEmployee(int id)
    {
        //Delete employee with passed id
        var deleteEmployee = _context.Employees.Find(id);
        if (deleteEmployee == null)
        {
            return NotFound();
        }

        _context.Employees.Remove(deleteEmployee);
        _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EmployeeExists(Guid id)
    {
        return _context.Employees.Any(e => e.Id == id);
    }
}