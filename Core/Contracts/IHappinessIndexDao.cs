using Core.Entities;

namespace Core.Contracts;

public interface IHappinessIndexDao
{
    public Task<List<Country>> ImportAllCountriesAsync();
}
