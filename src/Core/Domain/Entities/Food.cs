using Common.Entities;

namespace Domain.Entities;

public class Food : BaseEntity
{
    public string Title { get; set; }
    public decimal Price { get; set; }
    public byte[] Photo { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}