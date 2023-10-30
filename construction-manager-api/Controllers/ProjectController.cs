using construction_manager_api.DTOs.Project;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using construction_manager_api.Models;

namespace construction_manager_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly ConstructionManagerDbContext _context;
    public ProjectController(ConstructionManagerDbContext context)
    {
        _context = context;
    }

    // GET: api/Project
    [HttpGet]
    public async Task<ActionResult<ICollection<ProjectDto>>> GetProjects()
    {
        var projects = await _context.Projects
            .OrderBy(n => n.Id)
            .Include(p => p.Employees)
            .Include(p => p.RequiredSkills)
            .Include(p => p.Location)
            .Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Expenses = p.Expenses,
                Employees = p.Employees.Select(e => e.Name).ToList(),
                Location = p.Location == null ? null : p.Location.Name,
                RequiredSkills = p.RequiredSkills.Select(s => s.Name).ToList()
            }).ToListAsync();
        return Ok(projects);
    }
    

    // GET: api/Project/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(Guid id)
    {
        //Return specific project by id
        var project = await _context.Projects
            .Include(p => p.Employees)
            .Include(p => p.RequiredSkills)
            .Include(p => p.Location)
            .FirstOrDefaultAsync();
        if (project == null) return NotFound();
        var projectDto = new ProjectDto()
        {
            Id = project.Id,
            Name = project.Name,
            Expenses = project.Expenses,
            Employees = project.Employees.Select(e => e.Name).ToList(),
            Location = project.Location?.Name,
            RequiredSkills = project.RequiredSkills.Select(s => s.Name).ToList()
        };
        return Ok(projectDto);
    }

    // PUT: api/Project/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProject(Guid id, ModifyProjectRequest request)
    {
        var projectUpdate = await _context.Projects.FindAsync(id);
        if (projectUpdate == null)
        {
            return NotFound();
        }

        var employees = await _context.Employees
            .Where(e => request.Employees != null && request.Employees.Contains(e.Id))
            .ToListAsync();
        var skills = await _context.Skills
            .Where(e => request.RequiredSkills != null && request.RequiredSkills.Contains(e.Id))
            .ToListAsync();
        var location = await _context.Locations
            .FindAsync(request.Location);
        projectUpdate.Expenses = request.Expenses;
        projectUpdate.Location = location;
        projectUpdate.RequiredSkills = skills;
        projectUpdate.Employees = employees;
        
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Project
    [HttpPost]
    public async Task<ActionResult> PostProject(CreateProjectRequest request)
    {
        var employees = await _context.Employees
            .Where(e => request.Employees != null && request.Employees.Contains(e.Id))
            .ToListAsync();
        var skills = await _context.Skills
            .Where(e => request.RequiredSkills != null && request.RequiredSkills.Contains(e.Id))
            .ToListAsync();
        var location = await _context.Locations
            .FindAsync(request.Location);
        //Create new project using attributes of project ogj
        var createProject = new Project
        {
            Name = request.Name,
            Expenses = request.Expenses,
            Location = location,
            RequiredSkills = skills,
            Employees = employees
        };

        _context.Projects.Add(createProject);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProject), new {id = createProject.Id});
    }

    // DELETE: api/Project/3
    [HttpDelete("{id}")]
    public async Task<ActionResult<Project>> DeleteProject(int id)
    {
        //Delete project with passed id
        var deleteProject = _context.Projects.Find(id);
        if (deleteProject == null)
        {
            return NotFound();
        }

        _context.Projects.Remove(deleteProject);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}