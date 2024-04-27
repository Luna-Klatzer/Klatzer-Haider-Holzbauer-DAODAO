namespace Core.Contracts;

public interface IHappinessIndexDaoJSON : IHappinessIndexDao
{
    public string FileName { get; set; }
}
