using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    CountryCode = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HappinessIndices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LifeLadder = table.Column<double>(type: "double", nullable: true),
                    LifeLadderStandardError = table.Column<double>(type: "double", nullable: true),
                    UpperWhisker = table.Column<double>(type: "double", nullable: true),
                    LowerWhisker = table.Column<double>(type: "double", nullable: true),
                    LogGdpPerCapita = table.Column<double>(type: "double", nullable: true),
                    SocialSupport = table.Column<double>(type: "double", nullable: true),
                    HealthyLifeExpectancyAtBirth = table.Column<double>(type: "double", nullable: true),
                    FreedomToMakeLifeChoices = table.Column<double>(type: "double", nullable: true),
                    Generosity = table.Column<double>(type: "double", nullable: true),
                    PerceptionOfCorruption = table.Column<double>(type: "double", nullable: true),
                    PositiveAffect = table.Column<double>(type: "double", nullable: true),
                    NegativeAffect = table.Column<double>(type: "double", nullable: true),
                    LifeLadderInDystopia = table.Column<double>(type: "double", nullable: true),
                    DystopiaPlusResidual = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HappinessIndices", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Message = table.Column<string>(type: "longtext", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Level = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Years",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    YearNumber = table.Column<int>(type: "int", nullable: false),
                    BirthRate = table.Column<double>(type: "double", nullable: true),
                    PopulationTotal = table.Column<long>(type: "bigint", nullable: true),
                    RuralPopulation = table.Column<double>(type: "double", nullable: true),
                    HappinessIndexId = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Years", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Years_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Years_HappinessIndices_HappinessIndexId",
                        column: x => x.HappinessIndexId,
                        principalTable: "HappinessIndices",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Years_CountryId",
                table: "Years",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Years_HappinessIndexId",
                table: "Years",
                column: "HappinessIndexId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Years");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "HappinessIndices");
        }
    }
}
