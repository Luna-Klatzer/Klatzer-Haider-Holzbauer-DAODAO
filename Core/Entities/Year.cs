using Core.Contracts;

namespace Core.Entities;

public class Year : BaseEntity, IYear
{
    public int YearNumber { get; set; }
    public double? BirthRate { get; set; }
    public long? PopulationTotal { get; set; }
    public double? RuralPopulation { get; set; }
    public int? HappinessIndexId { get; set; }
    public HappinessIndex? HappinessIndex { get; set; }
}