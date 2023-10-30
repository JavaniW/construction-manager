using construction_manager_api.DTOs.Employee;
using construction_manager_api.DTOs.Equipment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using construction_manager_api.Models;

namespace construction_manager_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EquipmentController : ControllerBase
{
    private readonly ConstructionManagerDbContext _context;
    public EquipmentController(ConstructionManagerDbContext context)
    {
        _context = context;
    }

    // GET: api/Equipment
    [HttpGet]
    public async Task<ActionResult<ICollection<EquipmentDto>>> GetEquipments() {
        //Create list of all equipments from _context and return list
        var equipments = await _context.Equipments.OrderBy(n => n.Name).Select(e => new EquipmentDto
        {
            Id = e.Id,
            Name = e.Name
        }).ToListAsync();
        return Ok(equipments);
    }

    // GET: api/Equipment/1
    [HttpGet("{id}")]
    public async Task<ActionResult<ICollection<EquipmentDto>>> GetEquipment(Guid id)
    {
        //Return specific equipment by id
        var equipment = await _context.Equipments.FindAsync(id);
        if (equipment == null) return NotFound();
        var equipmentDto = new EquipmentDto
        {
            Id = equipment.Id,
            Name = equipment.Name
        };
        return Ok(equipmentDto);
    }

    // PUT: api/Equipment/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEquipment(Guid id, ModifyEquipmentRequest request)
    {
        var equipmentUpdate = await _context.Equipments.FindAsync(id);
        if (equipmentUpdate == null)
        {
            return NotFound();
        }
        equipmentUpdate.Name = request.Name;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/Equipment
    [HttpPost]
    public async Task<ActionResult> PostEquipment(CreateEquipmentRequest request)
    {
        //Create new equipment using attributes of equipment ogj
        var createEquipment = new Equipment
        {
            Name = request.Name
        };

        _context.Equipments.Add(createEquipment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEquipment), new {id = createEquipment.Id});
    }

    // DELETE: api/Equipment/3
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEquipment(Guid id)
    {
        //Delete equipment with passed id
        var deleteEquipment = await _context.Equipments.FindAsync(id);
        if (deleteEquipment == null)
        {
            return NotFound();
        }

        _context.Equipments.Remove(deleteEquipment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}