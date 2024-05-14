# Übung: DAO

Sophie Haider, Lorenz Holzbauer, Luna Klatzer

## 1) Beschreibung der Datensätze
- Countries_1960-2023.csv: in dieser CSV werden die totale Population, die Prozentzahl der Landbevölkerung, Geburtsrate für 1960 bis 2023 gespeichert

- WorldHappinessIndex-Files:
    |CSV|JSON|Beschreibung|
    |--|--|--|
    |Country name|Country name| Name des Landes
    |year| \- | Jahr der Datenaufnahme, das Jahr ist 2023
    |Life Ladder|Ladder score|HappinessIndex|
    |Log GDP per capita|Logged GDP per capita|Brutto Inland Produkt pro Kopf| 
    |Social support|Social support| Soziale Unterstützung|
    |Healthy life expectancy at birth|Healthy life expectancy|Lebenserwartung|
    |Freedom to make life choices|Freedom to make life choices|Freiheit um Lebensentscheidungen zu machen|
    |Generosity|Generosity|Großzügigkeit|
    |Perceptions of corruption|Perceptions of corruption|Wahrnehmung von Korruption|
    |\-|Standard error of ladder score|Standardabweichung des HappinessIndexes|
    |\-|upperwhisker| oberer Whisker (Box-Plot) |
    |\-|lowerwhisker| unterer Whisker (Box-Plot)|
    |\-|Ladder score in Dystopia| HappinessIndex in Relation zur Dystopie |
    |\-|Explained by: Log GDP per capita| Grund: BIP pro Kopf|
    |\-|Explained by: Social support| Grund: soziale Unterstützung|
    |\-|Explained by: Healthy life expectancy|Grund: Lebenserwartung|
    |\-|Explained by: Freedom to make life choices|Grund: Freiheit um Lebensentscheidungen zu machen|
    |\-|Explained by: Generosity|Grund: Großzügigkeit|
    |\-|Explained by: Perceptions of corruption| Grund: Wahrnehmung von Korruption|
    |\-|Dystopia + residual| Dystopia und statistische Unbekanntheit|


## 2) Datenmodell und Technologien
**Technologien:**
- Backend: .NET, EntityFramework
- Datenbank: MySQL in Docker

**Datenmodell:**

Unser CSV und unser JSON Dao implementieren das Interface IHappinessIndexDao. Dieses Interface definiert, dass jeder Dao eine Funktion für das Importen benötigt. Im Import werden die Fileinhalte auf die gewünschte Struktur geparsed und in die Datenbank eingefügt:

ICountry:
- Name
- CountryCode
- Years

IYear:
- YearNumber
- BirthRate
- PopulationTotal
- RuralPopulation
- HappinessIndex

IHappinessIndex:
- LifeLadder
- LifeLadderStandardError
- UpperWhiskey
- LowerWhiskey
- LogGdpPerCapita
- SocialSupport
- HealthyLifeExpectancyAtBirth
- FreedomToMakeLifeChoices
- Generosity
- PerceptionOfCorruption
- PositiveAffect
- NegativeAffect
- LifeLadderInDystopia
- DystopiaPlusResidual

Die Klassen implementieren diese Interfaces. Wird ein HappinessIndex eingefügt, dessen Country in der Datenbank noch nicht existiert, wird dieses erstellt und das Event in die Log-Tabelle eingefügt.

## 3) Log-Entries

## 4) Query A

Welches Land hatte den größten Abfall im WHI? (Vergleich frühester Eintrag zu spätestem Eintrag) Welche anderen Werte sind in diesem Land ebenfalls abgefallen oder gestiegen? Erkennen Sie 
einen Zusammenhang?