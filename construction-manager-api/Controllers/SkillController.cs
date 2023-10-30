using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using construction_manager_api.Models;

namespace construction_manager_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SkillController : ControllerBase
{
    private readonly ConstructionManagerDbContext _context;
    public SkillController(ConstructionManagerDbContext context)
    {
        _context = context;
    }

    // GET: api/Skill
    [HttpGet]
    public async Task<ActionResult<ICollection<SkillDto>>> GetSkills()
    {
        var skills = await _context.Skills
            .OrderBy(n => n.Id)
            .Select(s => new SkillDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
        return Ok(skills);
    }
        //Create list of all skills from _context and return list

    // GET: api/Skill/1
    [HttpGet("{id}")]
    public async Task<ActionResult<SkillDto>> GetSkill(int id)
    {
        //Return specific skill by id
        var skill = await _context.Skills.FindAsync(id);
        if (skill is null) return NotFound();
        var skillDto = new SkillDto
        {
            Id = skill.Id,
            Name = skill.Name
        };
        return Ok(skillDto);
    }

    // PUT: api/Skill/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSkill(long id, Skill skill)
    {
        //Udate skill with passed id using the attributes of the passed skill obj

        var skillUpdate = await _context.Skills.FindAsync(id);
        if (skillUpdate == null)
        {
            return NotFound();
        }

        skillUpdate.Name = skill.Name;
        
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Skill
    [HttpPost]
    public async Task<ActionResult> PostSkill(CreateSkillRequest request)
    {
        //Create new skill using attributes of skill ogj
        var createSkill = new Skill
        {
            Name = request.Name
        };

        _context.Skills.Add(createSkill);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSkill), new {id = createSkill.Id});
    }

    // DELETE: api/Skill/3
    [HttpDelete("{id}")]
    public async Task<ActionResult<SkillDto>> DeleteSkill(int id)
    {
        //Delete skill with passed id
        var deleteSkill = await _context.Skills.FindAsync(id);
        if (deleteSkill == null)
        {
            return NotFound();
        }

        _context.Skills.Remove(deleteSkill);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}