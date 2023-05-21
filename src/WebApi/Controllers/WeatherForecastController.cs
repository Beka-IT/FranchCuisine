using Domain.Entities;
using Infrastructure.Persistance.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly AppDbContext _db;

    public WeatherForecastController(AppDbContext context)
    {
        _db = context;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<Branch> Get()
    {
        var branch = new Branch()
        {
            Title = "Айни",
            CreatedAt = DateTime.Now
        };

        _db.Add(branch);
        _db.SaveChanges();

        return _db.Branches.ToArray();
    }
}