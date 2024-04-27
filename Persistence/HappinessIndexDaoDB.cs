using Core.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class HappinessIndexDaoDB : IHappinessIndexDaoDB
{
    public DbContext Context { get; set; } = new ApplicationDbContext();

    public Task<IList<IHappinessIndex>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IHappinessIndex> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IHappinessIndex> AddAsync(IHappinessIndex entity)
    {
        throw new NotImplementedException();
    }

    public Task<IHappinessIndex> UpdateAsync(IHappinessIndex entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }
}
