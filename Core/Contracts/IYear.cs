namespace Core.Contracts;

public interface IYear
{
    public int YearNumber { get; set; }
    public double? BirthRate { get; set; }
    public long? PopulationTotal { get; set; }
    public double? RuralPopulation { get; set; }
    public IHappinessIndex? HappinessIndex { get; set; }
}