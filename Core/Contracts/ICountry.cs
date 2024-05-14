using Core.Entities;

namespace Core.Contracts;

public interface ICountry : IBaseEntity
{
    public string Name { get; set; }

    public string? CountryCode { get; set; }

    public List<Year>? Years { get; set; }
}
