using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using construction_manager_api.Models;

namespace construction_manager_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EquipmentController : ControllerBase
{
    private readonly ConstructionManagerContext _context;
    public EquipmentController(ConstructionManagerContext context)
    {
        _context = context;
    }

    // GET: api/Equipment
    [HttpGet]
    public Task<List<Equipment>> GetEquipments() =>
        //Create list of all equipments from _context and return list
        _context.Equipments.OrderBy(n => n.Name).ToListAsync();

    // GET: api/Equipment/1
    [HttpGet("{id}")]
    public IActionResult GetEquipment(int id)
    {
        //Return specific equipment by id
        var equipment = _context.Equipments.Find(id);
        return equipment == null ? NotFound() : Ok(equipment);
    }

    // PUT: api/Equipment/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEquipment(Guid id, Equipment equipment)
    {
        //Udate equipment with passed id using the attributes of the passed equipment obj
        if (!id.Equals(equipment.Id))
        {
            return BadRequest();
        }

        var equipmentUpdate = await _context.Equipments.FindAsync(id);
        if (equipmentUpdate == null)
        {
            return NotFound();
        }

        equipmentUpdate.Name = equipment.Name;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!EquipmentExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Equipment
    [HttpPost]
    public ActionResult<Equipment> PostEquipment(Equipment equipment)
    {
        //Create new equipment using attributes of equipment ogj
        var createEquipment = new Equipment(equipment.Id, equipment.Name);

        _context.Equipments.Add(createEquipment);
        _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEquipment), new {id = createEquipment.Id});
    }

    // DELETE: api/Equipment/3
    [HttpDelete("{id}")]
    public ActionResult<Equipment> DeleteEquipment(int id)
    {
        //Delete equipment with passed id
        var deleteEquipment = _context.Equipments.Find(id);
        if (deleteEquipment == null)
        {
            return NotFound();
        }

        _context.Equipments.Remove(deleteEquipment);
        _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EquipmentExists(Guid id)
    {
        return _context.Equipments.Any(e => e.Id == id);
    }
}