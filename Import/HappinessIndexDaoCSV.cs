using Core.Contracts;
using Core.Entities;

namespace Import;

public class HappinessIndexDaoCSV : IHappinessIndexDaoCSV
{
    public string FileNameCountries { get; set; }
    public string FileNameHappyIndexes { get; set; }

    public HappinessIndexDaoCSV(string fileNameCountries, string fileNameHappyIndexes)
    {
        FileNameCountries = fileNameCountries;
        FileNameHappyIndexes = fileNameHappyIndexes;
    }

    public Task<ICountry> AddCountryAsync(ICountry country)
    {
        throw new NotImplementedException();
    }

    public Task<ICountry> AddYearToCountryAsync(string countryName, IYear year)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string countryName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(string countryName)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<ICountry>> GetAllAsync()
    {
        return await GetAllCountriesAsync();
    }

    public Task<ICountry> GetCountryByCountryNameAsync(string countryName)
    {
        throw new NotImplementedException();
    }

    public Task<ICountry> UpdateAsync(ICountry entity)
    {
        throw new NotImplementedException();
    }

    private async Task<IList<ICountry>> GetAllCountriesAsync()
    {
        var lines = await File.ReadAllLinesAsync(FileNameCountries);
        return lines
            .Skip(1)
            .Select(line => line
                .Replace("Birth rate, crude (per 1,000 people)", "BirthRate")
                .Replace("Population, total", "PopulationTotal")
                .Replace("Bahamas, The", "Bahamas")
                .Replace("Congo, Dem. Rep.", "Dem. Rep. Congo")
                .Replace("Congo, Rep.", "Congo")
                .Replace("Gambia, The", "Gambia")
                .Replace("Egypt, Arab Rep.", "Egypt")
                .Replace("Iran, Islamic Rep.", "Iran")
                .Replace("Korea, Rep.", "South Korea")
                .Replace("Korea, Dem. People's Rep.", "North Korea")
                .Replace("Hong Kong SAR, China", "Hong Kong")
                .Replace("Macao SAR, China", "Macao")
                .Replace("Micronesia, Fed. Sts.", "Micronesia")
                .Replace("Venezuela, RB", "Venezuela")
                .Replace("Yemen, Rep.", "Yemen")
            )
            .Select(line => line.Split(","))
            .GroupBy(parts => parts[0].Replace("\"", ""))
            .Select((group) =>
            {
                var dataLines = group.Select(values => values).ToList();
                var country = new Country
                {
                    Name = group.Key,
                    CountryCode = group.First()[1],
                    Years = []
                };
                var countOfYears = group.First().Count() - 4;
                for (var i = 0; i < countOfYears; i++)
                {
                    var birthRate = dataLines[0][4 + i];
                    var populationTotal = dataLines[1][4 + i];
                    var ruralPopulation = dataLines[2][4 + i];
                    country.Years.Add(new Year
                    {
                        YearNumber = 1960 + i,
                        BirthRate = birthRate == ".." ? 0 : Math.Round(double.Parse(birthRate), 5),
                        PopulationTotal = populationTotal == ".." ? 0 : long.Parse(populationTotal),
                        RuralPopulation = ruralPopulation == ".." ? 0 : Math.Round(double.Parse(ruralPopulation), 5)
                    });
                }
                return country;
            })
            .ToList<ICountry>();
    }
}
