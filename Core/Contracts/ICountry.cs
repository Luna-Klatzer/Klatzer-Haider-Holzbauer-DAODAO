namespace Core.Contracts;

public interface ICountry
{
    public string Name { get; set; }

    public string? CountryCode { get; set; }

    public IList<IYear> Years { get; set; }
}
