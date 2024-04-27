using Core.Contracts;
using Core.Entities;

namespace Import;

public class CountryDaoCSV(string fileName) : ICountryDaoCSV
{
    public string FileName { get; set; } = fileName;

    public async Task<IList<ICountry>> GetAllAsync()
    {
        var lines = await File.ReadAllLinesAsync(FileName);
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
            .SelectMany((group) =>
            {
                var dataLines = group.Select(values => values).ToList();
                var countries = new List<ICountry>();
                var countOfYears = group.First().Count() - 4;
                for (var i = 0; i < countOfYears; i++)
                {
                    var birthRate = dataLines[0][4 + i];
                    countries.Add(new Country
                    {
                        Name = group.Key,
                        CountryCode = group.First()[1],
                        Year = 1960 + i,
                        BirthRate = birthRate == ".." ? 0 : Math.Round(double.Parse(birthRate), 5),
                    });
                }
                for (var i = 0; i < countOfYears; i++)
                {
                    var populationTotal = dataLines[1][4 + i];
                    countries[i].PopulationTotal = populationTotal == ".." ? 0 : long.Parse(populationTotal);
                }
                for (var i = 0; i < countOfYears; i++)
                {
                    var ruralPopulation = dataLines[2][4 + i];
                    countries[i].RuralPopulation = ruralPopulation == ".." ? 0 : Math.Round(double.Parse(ruralPopulation), 5);
                }
                return countries;
            })
            .ToList();
    }

    public async Task<ICountry> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ICountry> AddAsync(ICountry entity)
    {
        throw new NotImplementedException();
    }

    public async Task<ICountry> UpdateAsync(ICountry entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CountAsync()
    {
        throw new NotImplementedException();
    }
}
