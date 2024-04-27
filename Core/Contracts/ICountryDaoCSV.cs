namespace Core.Contracts;

public interface ICountryDaoCSV : ICountryDao
{
    public string FileName { get; set; }
}
