using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using construction_manager_api.Models;

namespace construction_manager_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SkillController : ControllerBase
{
    private readonly ConstructionManagerContext _context;
    public SkillController(ConstructionManagerContext context)
    {
        _context = context;
    }

    // GET: api/Skill
    [HttpGet]
    public Task<List<Skill>> GetSkills() =>
        //Create list of all skills from _context and return list
        _context.Skills.OrderBy(n => n.Name).ToListAsync();

    // GET: api/Skill/1
    [HttpGet("{id}")]
    public IActionResult GetSkill(int id)
    {
        //Return specific skill by id
        var skill = _context.Skills.Find(id);
        return skill == null ? NotFound() : Ok(skill);
    }

    // PUT: api/Skill/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSkill(long id, Skill skill)
    {
        //Udate skill with passed id using the attributes of the passed skill obj
        if (id != skill.Id)
        {
            return BadRequest();
        }

        var skillUpdate = await _context.Skills.FindAsync(id);
        if (skillUpdate == null)
        {
            return NotFound();
        }

        skillUpdate.Name = skill.Name;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!SkillExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Skill
    [HttpPost]
    public ActionResult<Skill> PostSkill(Skill skill)
    {
        //Create new skill using attributes of skill ogj
        var createSkill = new Skill(skill.Id, skill.Name);

        _context.Skills.Add(createSkill);
        _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSkill), new {id = createSkill.Id});
    }

    // DELETE: api/Skill/3
    [HttpDelete("{id}")]
    public ActionResult<Skill> DeleteSkill(int id)
    {
        //Delete skill with passed id
        var deleteSkill = _context.Skills.Find(id);
        if (deleteSkill == null)
        {
            return NotFound();
        }

        _context.Skills.Remove(deleteSkill);
        _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SkillExists(long id)
    {
        return _context.Skills.Any(e => e.Id == id);
    }
}