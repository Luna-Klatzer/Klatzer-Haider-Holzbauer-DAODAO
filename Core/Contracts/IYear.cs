using Core.Entities;

namespace Core.Contracts;

public interface IYear : IBaseEntity
{
    public int YearNumber { get; set; }
    public double? BirthRate { get; set; }
    public long? PopulationTotal { get; set; }
    public double? RuralPopulation { get; set; }
    public int? HappinessIndexId { get; set; }
    public HappinessIndex? HappinessIndex { get; set; }
}