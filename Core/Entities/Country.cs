using Core.Contracts;

namespace Core.Entities;

public class Country : BaseEntity,ICountry
{
    public required string Name { get; set; }

    public string? CountryCode { get; set; }

    public List<Year>? Years { get; set; } = [];
}
