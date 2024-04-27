using Microsoft.EntityFrameworkCore;

namespace Core.Contracts;

public interface IHappinessIndexDaoDB : IHappinessIndexDao
{
    public DbContext Context { get; set; }
}
