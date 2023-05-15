using Domain.Entities;
using Infrastructure.Persistance.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Configurations;

public class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext context)
    {
        await context.Database.MigrateAsync();
    }
}