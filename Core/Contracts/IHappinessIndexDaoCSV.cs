namespace Core.Contracts;

public interface IHappinessIndexDaoCSV : IHappinessIndexDao
{
    public string FileName { get; set; }
}
