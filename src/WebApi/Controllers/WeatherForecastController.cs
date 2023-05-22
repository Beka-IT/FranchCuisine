using Domain.Entities;
using Infrastructure.Persistance.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
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
        var branches = new List<Branch>()
        {
            new Branch()
            {
                Title = "Айни",
                CreatedAt = DateTime.Now
            }
        };


        return branches;
    }
}