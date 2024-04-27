using Core.Contracts;

namespace Core.Entities;

public class Country : ICountry
{
    public string Name { get; set; }

    public string CountryCode { get; set; }

    public int Year { get; set; }

    public double? BirthRate { get; set; }

    public long? PopulationTotal { get; set; }

    public double? RuralPopulation { get; set; }
}
