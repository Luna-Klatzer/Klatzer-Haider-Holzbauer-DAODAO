namespace Core.Contracts;

public interface IHappinessIndexDaoCSV : IHappinessIndexDao
{
    public string FileNameCountries { get; set; }
    public string FileNameHappyIndexes { get; set; }
}
