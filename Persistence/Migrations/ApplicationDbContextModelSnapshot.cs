﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Core.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CountryCode")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Core.Entities.HappinessIndex", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double?>("DystopiaPlusResidual")
                        .HasColumnType("double");

                    b.Property<double?>("FreedomToMakeLifeChoices")
                        .HasColumnType("double");

                    b.Property<double?>("Generosity")
                        .HasColumnType("double");

                    b.Property<double?>("HealthyLifeExpectancyAtBirth")
                        .HasColumnType("double");

                    b.Property<double?>("LifeLadder")
                        .HasColumnType("double");

                    b.Property<double?>("LifeLadderInDystopia")
                        .HasColumnType("double");

                    b.Property<double?>("LifeLadderStandardError")
                        .HasColumnType("double");

                    b.Property<double?>("LogGdpPerCapita")
                        .HasColumnType("double");

                    b.Property<double?>("LowerWhisker")
                        .HasColumnType("double");

                    b.Property<double?>("NegativeAffect")
                        .HasColumnType("double");

                    b.Property<double?>("PerceptionOfCorruption")
                        .HasColumnType("double");

                    b.Property<double?>("PositiveAffect")
                        .HasColumnType("double");

                    b.Property<double?>("SocialSupport")
                        .HasColumnType("double");

                    b.Property<double?>("UpperWhisker")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("HappinessIndices");
                });

            modelBuilder.Entity("Core.Entities.Year", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double?>("BirthRate")
                        .HasColumnType("double");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<int?>("HappinessIndexId")
                        .HasColumnType("int");

                    b.Property<long?>("PopulationTotal")
                        .HasColumnType("bigint");

                    b.Property<double?>("RuralPopulation")
                        .HasColumnType("double");

                    b.Property<int>("YearNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("HappinessIndexId");

                    b.ToTable("Years");
                });

            modelBuilder.Entity("Persistence.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Core.Entities.Year", b =>
                {
                    b.HasOne("Core.Entities.Country", null)
                        .WithMany("Years")
                        .HasForeignKey("CountryId");

                    b.HasOne("Core.Entities.HappinessIndex", "HappinessIndex")
                        .WithMany()
                        .HasForeignKey("HappinessIndexId");

                    b.Navigation("HappinessIndex");
                });

            modelBuilder.Entity("Core.Entities.Country", b =>
                {
                    b.Navigation("Years");
                });
#pragma warning restore 612, 618
        }
    }
}
