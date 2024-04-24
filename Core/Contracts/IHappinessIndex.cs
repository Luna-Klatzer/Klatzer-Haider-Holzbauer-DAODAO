namespace Core.Contracts;

public interface IHappinessIndex
{
    public ICountry Country { get; set; }

    public short Year { get; set; }

    public short? LifeLadder { get; set; }

    public short? LifeLadderStandardError { get; set; }

    public short? UpperWhisker { get; set; }

    public short? LowerWhisker { get; set; }

    public short? LogGdpPerCapita { get; set; }

    public short? SocialSupport { get; set; }

    public short? HealthyLifeExpectancyAtBirth { get; set; }

    public short? FreedomToMakeLifeChoices { get; set; }

    public short? Generosity { get; set; }

    public short? PerceptionOfCorruption { get; set; }

    public short? PositiveAffect { get; set; }

    public short? NegativeEffort { get; set; }

    public short? LifeLadderInDystopia { get; set; }

    public short? DystopiaPlusResidual { get; set; }
}
