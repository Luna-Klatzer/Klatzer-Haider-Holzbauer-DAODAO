using Core.Contracts;
using Core.Entities;

namespace Import;

public class HappinessIndexDaoCSV : IHappinessIndexDaoCSV
{
    public string FileNameCountries { get; set; }
    public string FileNameHappyIndexes { get; set; }


    public async Task<List<Country>> ImportAllCountriesAsync()
    {
        var countriesJson = await GetHappinessIndexesAsync();
        var countriesCsv = await GetAllCountriesFromCsvAsync();


        var merged = countriesJson
            .Join(countriesCsv,
                jsonCountry => jsonCountry.Name,
                csvCountry => csvCountry.Name,
                (jsonCountry, csvCountry) =>
                {
                    var newCountry = new Country
                    {
                        Name = jsonCountry.Name,
                        CountryCode = csvCountry.CountryCode,
                        Years = jsonCountry.Years
                    };
                    if (csvCountry.Years != null && newCountry.Years != null)
                    {
                        newCountry.Years.AddRange(csvCountry.Years);
                    }
                    return newCountry;
                })
            .ToList();

        var happinessIndexNullCount = merged
            .SelectMany(country => country.Years)
            .Count(year => year.HappinessIndex == null);
        Console.WriteLine(happinessIndexNullCount);

        var otherCountries = countriesJson
            .Where(jsonCountry => countriesCsv.All(csvCountry => csvCountry.Name != jsonCountry.Name))
            .ToList();

        merged.AddRange(otherCountries);
        return merged;
    }

    public async Task<List<Country>> GetHappinessIndexesAsync()
    {
        var countriesFromCsv = await GetAllCountriesFromCsvAsync();
        var countriesFromHappyIndexes = await GetHappinessIndexesFromCsvAsync();

        var joinedCountries = countriesFromHappyIndexes
            .Join(countriesFromCsv,
                happyIndex => happyIndex.Name,
                country => country.Name,
                (happyIndex, country) =>
                {
                    var newCountry = new Country
                    {
                        Name = happyIndex.Name,
                        CountryCode = country.CountryCode,
                        Years = happyIndex.Years,
                    };
                    if (country.Years != null && newCountry.Years != null)
                    {
                        foreach (var y in country.Years)
                        {
                            newCountry.Years.Add(y);
                        }
                    }
                    return newCountry;
                })
            .ToList();

        var countriesNotInCsv = countriesFromHappyIndexes
            .Where(happyIndex => countriesFromCsv.All(country => country.Name != happyIndex.Name))
            .ToList();
        
        joinedCountries.AddRange(countriesNotInCsv);
        return joinedCountries;
    }
    

    private async Task<List<Country>> GetHappinessIndexesFromCsvAsync()
    {
        var lines = await File.ReadAllLinesAsync(FileNameHappyIndexes);

        var preparedData = lines
            .Skip(1)
            .Select(parts =>
            {
                var values = parts.Split(",");
                return new
                {
                    Country = values[0],
                    Year = values[1],
                    LifeLadder = values[2],
                    LogGDPPerCapita = values[3],
                    SocialSupport = values[4],
                    HealthyLifeExpectancyAtBirth = values[5],
                    FreedomToMakeLifeChoices = values[6],
                    Generosity = values[7],
                    PerceptionsOfCorruption = values[8],
                    PositiveAffect = values[9],
                    NegativeAffect = values[10]
                };
            });

        var countries = preparedData
            .GroupBy(parts => parts.Country)
            .Select(country => new Country
            {
                Name = country.Key,
                Years = country
                    .Select(year => new Year
                    {
                        YearNumber = Convert.ToInt32(year.Year),
                        HappinessIndex = new HappinessIndex
                        {
                            LifeLadder = year.LifeLadder == "" ? null : Math.Round(double.Parse(year.LifeLadder), 5),
                            LogGdpPerCapita = year.LogGDPPerCapita == "" ? null : Math.Round(double.Parse(year.LogGDPPerCapita), 5),
                            SocialSupport = year.SocialSupport == "" ? null : Math.Round(double.Parse(year.SocialSupport), 5),
                            HealthyLifeExpectancyAtBirth = year.HealthyLifeExpectancyAtBirth == "" ? null : Math.Round(double.Parse(year.HealthyLifeExpectancyAtBirth), 5),
                            FreedomToMakeLifeChoices = year.FreedomToMakeLifeChoices == "" ? null : Math.Round(double.Parse(year.FreedomToMakeLifeChoices), 5),
                            Generosity = year.Generosity == "" ? null : Math.Round(double.Parse(year.Generosity), 5),
                            PerceptionOfCorruption = year.PerceptionsOfCorruption == "" ? null : Math.Round(double.Parse(year.PerceptionsOfCorruption), 5),
                            PositiveAffect = year.PositiveAffect == "" ? null : Math.Round(double.Parse(year.PositiveAffect), 5),
                            NegativeAffect = year.NegativeAffect == "" ? null : Math.Round(double.Parse(year.NegativeAffect), 5)
                        }
                    })
                    .ToList()
            })
            .ToList();
        return countries;
    }

    private async Task<List<Country>> GetAllCountriesFromCsvAsync()
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
            .ToList();
    }
}
