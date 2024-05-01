namespace Core.Contracts;

public interface IHappinessIndexDao
{
    public Task<IList<ICountry>> GetAllAsync();
    public Task<ICountry> GetCountryByCountryNameAsync(string countryName);
    public Task<ICountry> AddCountryAsync(ICountry country);
    public Task<ICountry> AddYearToCountryAsync(string countryName, IYear year);
    public Task<ICountry> UpdateAsync(ICountry entity);
    public Task DeleteAsync(string countryName);
    public Task<bool> ExistsAsync(string countryName);
    public Task<int> CountAsync();
}
