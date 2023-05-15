using Infrastructure.Persistance.Data;

namespace Infrastructure.Persistance.Configurations;

public class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();
    }
}