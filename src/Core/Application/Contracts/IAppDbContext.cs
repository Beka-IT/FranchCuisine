using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Contracts;

public interface IAppDbContext
{
    DbSet<User> Users { get; set; } 
    DbSet<Branch> Branches { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Food> Foods { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}