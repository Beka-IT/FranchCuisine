using Common.Entities;
using Domain.Enums;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string Login { get; set; }
    public string Password { get; set; }
    public UserType Type { get; set; }
    public int BranchId { get; set; }
}