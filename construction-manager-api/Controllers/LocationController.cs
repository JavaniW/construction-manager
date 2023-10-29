using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using construction_manager_api.Models;

namespace construction_manager_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly ConstructionManagerContext _context;
    public LocationController(ConstructionManagerContext context)
    {
        _context = context;
    }

    // GET: api/Location
    [HttpGet]
    public Task<List<Location>> GetLocations() =>
        //Create list of all locations from _context and return list
        _context.Locations.OrderBy(n => n.Name).ToListAsync();

    // GET: api/Location/1
    [HttpGet("{id}")]
    public IActionResult GetLocation(int id)
    {
        //Return specific location by id
        var location = _context.Locations.Find(id);
        return location == null ? NotFound() : Ok(location);
    }

    // PUT: api/Location/2
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLocation(long id, Location location)
    {
        //Udate location with passed id using the attributes of the passed location obj
        if (id != location.Id)
        {
            return BadRequest();
        }

        var locationUpdate = await _context.Locations.FindAsync(id);
        if (locationUpdate == null)
        {
            return NotFound();
        }

        locationUpdate.Name = location.Name;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!LocationExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }

    // POST: api/Location
    [HttpPost]
    public ActionResult<Location> PostLocation(Location location)
    {
        //Create new location using attributes of location ogj
        var createLocation = new Location(location.Id, location.Name);

        _context.Locations.Add(createLocation);
        _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLocation), new {id = createLocation.Id});
    }

    // DELETE: api/Location/3
    [HttpDelete("{id}")]
    public ActionResult<Location> DeleteLocation(int id)
    {
        //Delete location with passed id
        var deleteLocation = _context.Locations.Find(id);
        if (deleteLocation == null)
        {
            return NotFound();
        }

        _context.Locations.Remove(deleteLocation);
        _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LocationExists(long id)
    {
        return _context.Locations.Any(e => e.Id == id);
    }
}