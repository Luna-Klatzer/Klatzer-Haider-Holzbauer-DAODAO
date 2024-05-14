using Import;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;


var context = new ApplicationDbContext();
context.Database.EnsureDeleted();
await context.Database.MigrateAsync();
await context.SaveChangesAsync();

var jsonDao = new HappinessIndexDaoJSON
{
    FileName = "./Data/WorldHappinessIndex_2023.json"
};
var countriesJson = await jsonDao.ImportAllCountriesAsync();

var csvDao = new HappinessIndexDaoCSV
{
    FileNameCountries = "./Data/Countries_1960-2023.csv",
    FileNameHappyIndexes = "./Data/WorldHappinessIndex_2005-2020.csv"
};
var countriesCsv = await csvDao.ImportAllCountriesAsync();


var countries = countriesJson
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

var otherCountries = countriesJson
    .Where(jsonCountry => countriesCsv.All(csvCountry => csvCountry.Name != jsonCountry.Name))
    .ToList();

var logs = otherCountries.Select(c => new Log
{
    Message = $"Country {c.Name} was not found in CSV file",
    CreatedAt = DateTime.Now,
    Level = "Info"
});

context.Logs.AddRange(logs);

countries.AddRange(otherCountries);

context.Countries.AddRange(countries);
await context.SaveChangesAsync();


Console.WriteLine();

static Country GetCountryWithMaxHappinessIndexDrop(List<Country> countries)
{
    Country? countryWithMaxDrop = null;
    double maxDrop = double.MinValue;

    foreach (var country in countries)
    {
        var years = country.Years!.OrderBy(y => y.YearNumber).ToList();

        var initialHappiness = years.First().HappinessIndex?.LifeLadder ?? 0;
        var latestHappiness = years.Last().HappinessIndex?.LifeLadder ?? 0;

        if (initialHappiness - latestHappiness > maxDrop)
        {
            maxDrop = initialHappiness - latestHappiness;
            countryWithMaxDrop = country;
        }
    }
    return countryWithMaxDrop!;
}

var countryWithMaxDrop = GetCountryWithMaxHappinessIndexDrop(countries);
Console.WriteLine($"Country with max happiness index drop: {countryWithMaxDrop.Name}");