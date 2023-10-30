using construction_manager_api.DTOs.Department;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using construction_manager_api.Models;

namespace construction_manager_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly ConstructionManagerDbContext _context;
    public DepartmentController(ConstructionManagerDbContext context)
    {
        _context = context;
    }

    // GET: api/Department
    [HttpGet]
    public async Task<ICollection<DepartmentDto>> GetDepartments()
    {
        //Create list of all departments from _context and return list
        return await _context.Departments.OrderBy(n => n.Name).Select(d => new DepartmentDto
        {
            Id = d.Id,
            Name = d.Name
        }).ToListAsync();
    }

    // GET: api/Department/1
    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentDto>> GetDepartment(int id)
    {
        //Return specific department by id
        var department = await _context.Departments.FindAsync(id);
        if (department is null) return NotFound();
        var departmentDto = new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name
        };
        return Ok(departmentDto);
    }

    // PUT: api/Department/2
    [HttpPut("{id}")]
    public async Task<ActionResult<DepartmentDto>> PutDepartment(long id, ModifyDepartmentRequest request)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null)
        {
            return NotFound();
        }

        department.Name = request.Name;
        var departmentDto = new DepartmentDto()
        {
            Id = department.Id,
            Name = department.Name
        };
        await _context.SaveChangesAsync();
        return Ok(departmentDto);
    }

    // POST: api/Department
    [HttpPost]
    public async Task<ActionResult> PostDepartment(CreateDepartmentRequest department)
    {
        //Create new department using attributes of department ogj
        var createDepartment = new Department()
        {
            Name = department.Name
        };

        _context.Departments.Add(createDepartment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDepartment), new {id = createDepartment.Id});
    }

    // DELETE: api/Department/3
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDepartment(int id)
    {
        //Delete department with passed id
        var deleteDepartment = await _context.Departments.FindAsync(id);
        if (deleteDepartment == null)
        {
            return NotFound();
        }

        _context.Departments.Remove(deleteDepartment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}