using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using construction_manager_api.Models;

namespace construction_manager_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly ConstructionManagerContext _context;
    public DepartmentController(ConstructionManagerContext context)
    {
        _context = context;
    }

    // GET: api/Department
    [HttpGet]
    public Task<List<Department>> GetDepartments() =>
        //Create list of all departments from _context and return list
        _context.Departments.OrderBy(n => n.Name).ToListAsync();

    // GET: api/Department/1
    [HttpGet("{id}")]
    public IActionResult GetDepartment(int id)
    {
        //Return specific department by id
        var department = _context.Departments.Find(id);
        return department == null ? NotFound() : Ok(department);
    }

    // PUT: api/Department/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDepartment(long id, Department department)
    {
        //Udate department with passed id using the attributes of the passed department obj
        if (id != department.Id)
        {
            return BadRequest();
        }

        var departmentUpdate = await _context.Departments.FindAsync(id);
        if (departmentUpdate == null)
        {
            return NotFound();
        }

        departmentUpdate.Name = department.Name;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!DepartmentExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Department
    [HttpPost]
    public ActionResult<Department> PostDepartment(Department department)
    {
        //Create new department using attributes of department ogj
        var createDepartment = new Department(department.Id, department.Name);

        _context.Departments.Add(createDepartment);
        _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDepartment), new {id = createDepartment.Id});
    }

    // DELETE: api/Department/3
    [HttpDelete("{id}")]
    public ActionResult<Department> DeleteDepartment(int id)
    {
        //Delete department with passed id
        var deleteDepartment = _context.Departments.Find(id);
        if (deleteDepartment == null)
        {
            return NotFound();
        }

        _context.Departments.Remove(deleteDepartment);
        _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DepartmentExists(long id)
    {
        return _context.Departments.Any(e => e.Id == id);
    }
}