using System.Text.Json;
using System.Text.Json.Serialization;
using Core.Contracts;
using Core.Entities;
using Org.BouncyCastle.Security;

namespace Import;

public class HappinessIndexDaoJSON(string fileName) : IHappinessIndexDaoJSON
{
    public string FileName { get; set; } = fileName;

    private record HappinessIndexJSON
    (
        string CountryName, 
        string LadderScore, 
        string StandardErrorOfLadderScore, 
        string UpperWhisker, 
        string LowerWhisker, 
        string LoggedGDPPerCapita,
        string SocialSupport,
        string HealthyLifeExpectancy,
        string FreedomToMakeLifeChoices,
        string Generosity,
        string PerceptionsOfCorruption,
        string LadderScoreInDystopia,
        string ExplainedByLogGDPPerCapita,
        string ExplainedBySocialSupport,
        string ExplainedByHealthyLifeExpectancy,
        string ExplainedByFreedomToMakeLifeChoices,
        string ExplainedByGenerosity,
        string ExplainedByPerceptionsOfCorruption,
        string DystopiaResidual
    );

    public async Task<IList<ICountry>> GetAllAsync()
    {
        var text = await File.ReadAllTextAsync(FileName);
        var json = JsonSerializer.Deserialize<IList<HappinessIndexJSON>>(ReplaceJsonNamesWithObjNames(text));

        var happinessIndexes = json!
            .Select(h => new Country
            {
                Name = h.CountryName,
                Years = new List<IYear>
                {
                    ConvertHappinessIndexJsonToYear(h, 2023)
                }
            });
        
        return happinessIndexes.ToList<ICountry>();

    }

    public async Task<ICountry> GetCountryByCountryNameAsync(string countryName)
    {
        var countries = await GetAllAsync();
        return countries.Single(c => c.Name == countryName);
    }

    public Task<ICountry> AddCountryAsync(ICountry country)
    {
        throw new NotImplementedException();
    }

    public Task<ICountry> AddYearToCountryAsync(string countryName, IYear year)
    {
        throw new NotImplementedException();
    }

    public Task<ICountry> UpdateAsync(ICountry entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(string countryName)
    {
        var text = await File.ReadAllTextAsync(FileName);
        var json = JsonSerializer.Deserialize<IList<HappinessIndexJSON>>(ReplaceJsonNamesWithObjNames(text));
        json!.Remove(json.Single(c => c.CountryName == countryName));
        await File.WriteAllTextAsync(FileName, ReplaceObjNamesWithJsonNames(JsonSerializer.Serialize(json)));
    }

    public async Task<bool> ExistsAsync(string countryName)
    {
        var countries = await GetAllAsync();
        return countries.Any(c => c.Name == countryName);
    }

    public async Task<int> CountAsync()
    {
        var countries = await GetAllAsync();
        return countries.Count();
    }

    private string ReplaceJsonNamesWithObjNames(string json)
    {
        return json
            .Replace("Ladder score in Dystopia", "LadderScoreInDystopia")
            .Replace("Explained by: Log GDP per capita", "ExplainedByLogGDPPerCapita")
            .Replace("Explained by: Social support", "ExplainedBySocialSupport")
            .Replace("Explained by: Healthy life expectancy", "ExplainedByHealthyLifeExpectancy")
            .Replace("Explained by: Freedom to make life choices", "ExplainedByFreedomToMakeLifeChoices")
            .Replace("Explained by: Generosity", "ExplainedByGenerosity")
            .Replace("Explained by: Perceptions of corruption", "ExplainedByPerceptionsOfCorruption")
            .Replace("Country name", "CountryName")
            .Replace("Ladder score", "LadderScore")
            .Replace("Standard error of ladder score", "StandardErrorOfLadderScore")
            .Replace("upperwhisker", "UpperWhisker")
            .Replace("lowerwhisker", "LowerWhisker")
            .Replace("Logged GDP per capita", "LoggedGDPPerCapita")
            .Replace("Social support", "SocialSupport")
            .Replace("Healthy life expectancy", "HealthyLifeExpectancy")
            .Replace("Freedom to make life choices", "FreedomToMakeLifeChoices")
            .Replace("Generosity", "Generosity")
            .Replace("Perceptions of corruption", "PerceptionsOfCorruption")
            .Replace("Dystopia + residual", "DystopiaResidual");
    }

    private string ReplaceObjNamesWithJsonNames(string json)
    {
        return json
            .Replace("LadderScoreInDystopia", "Ladder score in Dystopia")
            .Replace("ExplainedByLogGDPPerCapita", "Explained by: Log GDP per capita")
            .Replace("ExplainedBySocialSupport", "Explained by: Social support")
            .Replace("ExplainedByHealthyLifeExpectancy", "Explained by: Healthy life expectancy")
            .Replace("ExplainedByFreedomToMakeLifeChoices", "Explained by: Freedom to make life choices")
            .Replace("ExplainedByGenerosity", "Explained by: Generosity")
            .Replace("ExplainedByPerceptionsOfCorruption", "Explained by: Perceptions of corruption")
            .Replace("CountryName", "Country name")
            .Replace("LadderScore", "Ladder score")
            .Replace("StandardErrorOfLadderScore", "Standard error of ladder score")
            .Replace("UpperWhisker", "upperwhisker")
            .Replace("LowerWhisker", "lowerwhisker")
            .Replace("LoggedGDPPerCapita", "Logged GDP per capita")
            .Replace("SocialSupport", "Social support")
            .Replace("HealthyLifeExpectancy", "Healthy life expectancy")
            .Replace("FreedomToMakeLifeChoices", "Freedom to make life choices")
            .Replace("Generosity", "Generosity")
            .Replace("PerceptionsOfCorruption", "Perceptions of corruption")
            .Replace("DystopiaResidual", "Dystopia + residual");
    }

    private Year ConvertHappinessIndexJsonToYear(HappinessIndexJSON h, short year)
    {
        return new Year
        {
            
            YearNumber = year,
            HappinessIndex = new HappinessIndex
            {
                LifeLadder = h.LadderScore == "" ? null : double.Parse(h.LadderScore.Replace(",", ".")),
                LifeLadderStandardError = h.StandardErrorOfLadderScore == "" ? null : double.Parse(h.StandardErrorOfLadderScore.Replace(",", ".")),
                UpperWhisker = h.UpperWhisker == "" ? null : double.Parse(h.UpperWhisker.Replace(",", ".")),
                LowerWhisker = h.LowerWhisker == "" ? null : double.Parse(h.LowerWhisker.Replace(",", ".")),
                LogGdpPerCapita = h.LoggedGDPPerCapita == "" ? null : double.Parse(h.LoggedGDPPerCapita.Replace(",", ".")),
                SocialSupport = h.SocialSupport == "" ? null : double.Parse(h.SocialSupport.Replace(",", ".")),
                HealthyLifeExpectancyAtBirth = h.HealthyLifeExpectancy == "" ? null : double.Parse(h.HealthyLifeExpectancy.Replace(",", ".")),
                FreedomToMakeLifeChoices = h.FreedomToMakeLifeChoices == "" ? null : double.Parse(h.FreedomToMakeLifeChoices.Replace(",", ".")),
                Generosity = h.Generosity == "" ? null : double.Parse(h.Generosity.Replace(",", ".")),
                PerceptionOfCorruption = h.PerceptionsOfCorruption == "" ? null : h.PerceptionsOfCorruption == "" ? null : double.Parse(h.PerceptionsOfCorruption.Replace(",", ".")),
                PositiveAffect = h.ExplainedByLogGDPPerCapita == "" ? null : double.Parse(h.ExplainedByLogGDPPerCapita.Replace(",", ".")),
                NegativeAffect = h.ExplainedBySocialSupport == "" ? null : double.Parse(h.ExplainedBySocialSupport.Replace(",", ".")),
                LifeLadderInDystopia = h.LadderScoreInDystopia == "" ? null : double.Parse(h.LadderScoreInDystopia.Replace(",", ".")),
                DystopiaPlusResidual = h.DystopiaResidual == "" ? null : double.Parse(h.DystopiaResidual.Replace(",", "."))
            }
        };
    }
}
