using construction_manager_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace construction_manager_api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ConstructionManagerDbContext db;

    public WeatherForecastController(ConstructionManagerDbContext db, ILogger<WeatherForecastController> logger)
    {
        this.db = db;
        _logger = logger;
    }

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    

    [HttpGet(Name = "GetWeatherForecast")]
    public Employee Get()
    {
        var employee = db.Employees.Single(x => x.Id.Equals(new Guid("c941f441-d47a-46cb-b0c9-01679ba500dd")));
        _logger.LogWarning("Employee is: {employee}", employee.Id);
        return employee;
    }
}

