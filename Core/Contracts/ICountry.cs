namespace Core.Contracts;

public interface ICountry
{
    public string Name { get; set; }

    public short BirthRate { get; set; }

    public long PopulationTotal { get; set; }

    public long RuralPopulation { get; set; }
}
