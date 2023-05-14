using Common.Entities;

namespace Domain.Entities;

public class Category : BaseEntity
{
    public string Title { get; set; }
    public ICollection<Food>? Foods { get; set; }
}