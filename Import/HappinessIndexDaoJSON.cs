using Core.Contracts;

namespace Import;

public class HappinessIndexDaoJSON(string fileName) : IHappinessIndexDaoJSON
{
    public string FileName { get; set; } = fileName;

    public async Task<IList<IHappinessIndex>> GetAllAsync()
    {
        var lines = await File.ReadAllLinesAsync(FileName);

        throw new NotImplementedException();
    }

    public async Task<IHappinessIndex> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IHappinessIndex> AddAsync(IHappinessIndex entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IHappinessIndex> UpdateAsync(IHappinessIndex entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }
}
