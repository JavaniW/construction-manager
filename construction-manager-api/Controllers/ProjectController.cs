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
    public Task<List<Project>> GetProjects() =>
        //Create list of all projects from _context and return list
        _context.Projects.OrderBy(n => n.Id).ToListAsync();

    // GET: api/Project/1
    [HttpGet("{id}")]
    public IActionResult GetProject(int id)
    {
        //Return specific project by id
        var project = _context.Projects.Find(id);
        return project == null ? NotFound() : Ok(project);
    }

    // PUT: api/Project/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProject(Guid id, Project project)
    {
        //Udate project with passed id using the attributes of the passed project obj
        if (!id.Equals(project.Id))
        {
            return BadRequest();
        }

        var projectUpdate = await _context.Projects.FindAsync(id);
        if (projectUpdate == null)
        {
            return NotFound();
        }

        projectUpdate.Expenses = project.Expenses;
        //projectUpdate.LocationId = project.LocationId;
        projectUpdate.Location = project.Location;
        projectUpdate.RequiredSkills = project.RequiredSkills;
        projectUpdate.Employees = project.Employees;
        //projectUpdate.RequiredEquipments = project.RequiredEquipments;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!ProjectExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Project
    [HttpPost]
    public ActionResult<Project> PostProject(Project project)
    {
        //Create new project using attributes of project ogj
        var createProject = new Project();

        _context.Projects.Add(createProject);
        _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProject), new {id = createProject.Id});
    }

    // DELETE: api/Project/3
    [HttpDelete("{id}")]
    public ActionResult<Project> DeleteProject(int id)
    {
        //Delete project with passed id
        var deleteProject = _context.Projects.Find(id);
        if (deleteProject == null)
        {
            return NotFound();
        }

        _context.Projects.Remove(deleteProject);
        _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProjectExists(Guid id)
    {
        return _context.Projects.Any(e => e.Id == id);
    }
}