using construction_manager_api.DTOs.Location;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using construction_manager_api.Models;

namespace construction_manager_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly ConstructionManagerDbContext _context;
    public LocationController(ConstructionManagerDbContext context)
    {
        _context = context;
    }

    // GET: api/Location
    [HttpGet]
    public async Task<ActionResult<ICollection<LocationDto>>> GetLocations()
    {
        //Create list of all locations from _context and return list
        var locations = await _context.Locations.OrderBy(n => n.Name).Select(l => new LocationDto
        {
            Id = l.Id,
            Name = l.Name
        }).ToListAsync();
        return Ok(locations);
    }

    // GET: api/Location/1
    [HttpGet("{id}")]
    public async Task<ActionResult<LocationDto>> GetLocation(Guid id)
    {
        //Return specific location by id
        var location = await _context.Locations.FindAsync(id);
        if (location == null) return NotFound();
        var locationDto = new LocationDto()
        {
            Id = location.Id,
            Name = location.Name
        };
        return Ok(locationDto);
    }

    // PUT: api/Location/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLocation(Guid id, ModifyLocationRequest request)
    {
        var locationUpdate = await _context.Locations.FindAsync(id);
        if (locationUpdate == null)
        {
            return NotFound();
        }

        locationUpdate.Name = request.Name;
        
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // POST: api/Location
    [HttpPost]
    public async Task<ActionResult> PostLocation(CreateLocationRequest request)
    {
        //Create new location using attributes of location ogj
        var createLocation = new Location
        {
            Name = request.Name
        };

        _context.Locations.Add(createLocation);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLocation), new {id = createLocation.Id});
    }

    // DELETE: api/Location/3
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLocation(int id)
    {
        //Delete location with passed id
        var deleteLocation = await _context.Locations.FindAsync(id);
        if (deleteLocation == null)
        {
            return NotFound();
        }

        _context.Locations.Remove(deleteLocation);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}