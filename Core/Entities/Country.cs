using Core.Contracts;

namespace Core.Entities;

public class Country : ICountry
{
    public required string Name { get; set; }

    public string? CountryCode { get; set; }
    public IList<IYear> Years { get; set; } = [];
}
