using Core.Contracts;

namespace Core.Entities;

public class Year: IYear
{
    public int YearNumber { get; set; }
    public double? BirthRate { get; set; }
    public long? PopulationTotal { get; set; }
    public double? RuralPopulation { get; set; }
    public IHappinessIndex? HappinessIndex { get; set; }
}