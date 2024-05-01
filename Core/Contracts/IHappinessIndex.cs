namespace Core.Contracts;

public interface IHappinessIndex
{
    public double? LifeLadder { get; set; }

    public double? LifeLadderStandardError { get; set; }

    public double? UpperWhisker { get; set; }

    public double? LowerWhisker { get; set; }

    public double? LogGdpPerCapita { get; set; }

    public double? SocialSupport { get; set; }

    public double? HealthyLifeExpectancyAtBirth { get; set; }

    public double? FreedomToMakeLifeChoices { get; set; }

    public double? Generosity { get; set; }

    public double? PerceptionOfCorruption { get; set; }

    public double? PositiveAffect { get; set; }

    public double? NegativeEffort { get; set; }

    public double? LifeLadderInDystopia { get; set; }

    public double? DystopiaPlusResidual { get; set; }
}
