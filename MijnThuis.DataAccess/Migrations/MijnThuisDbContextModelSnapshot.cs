﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MijnThuis.DataAccess;

#nullable disable

namespace MijnThuis.DataAccess.Migrations
{
    [DbContext(typeof(MijnThuisDbContext))]
    partial class MijnThuisDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MijnThuis.DataAccess.Entities.BatteryEnergyHistoryEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AvailableEnergy")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CalculatedStateOfHealth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DataCollected")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("RatedEnergy")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("StateOfCharge")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("StateOfHealth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("SysId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("SysId"));

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("Date");

                    b.HasIndex("SysId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("SysId"));

                    b.ToTable("BATTERY_ENERGY_HISTORY", (string)null);
                });

            modelBuilder.Entity("MijnThuis.DataAccess.Entities.CarChargingEnergyHistoryEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ChargingAmps")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("ChargingDuration")
                        .HasColumnType("time");

                    b.Property<Guid>("ChargingSessionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("EnergyCharged")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<long>("SysId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("SysId"));

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("ChargingSessionId");

                    b.HasIndex("SysId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("SysId"));

                    b.HasIndex("Timestamp");

                    b.ToTable("CAR_CHARGING_HISTORY", (string)null);
                });

            modelBuilder.Entity("MijnThuis.DataAccess.Entities.EnergyHistoryEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("ActiveTarrif")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("GasCoefficient")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<decimal>("MonthlyPowerPeak")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<long>("SysId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("SysId"));

                    b.Property<decimal>("Tarrif1Export")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<decimal>("Tarrif1ExportDelta")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<decimal>("Tarrif1Import")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<decimal>("Tarrif1ImportDelta")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<decimal>("Tarrif2Export")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<decimal>("Tarrif2ExportDelta")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<decimal>("Tarrif2Import")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<decimal>("Tarrif2ImportDelta")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<decimal>("TotalExport")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<decimal>("TotalExportDelta")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<decimal>("TotalGas")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<decimal>("TotalGasDelta")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<decimal>("TotalGasKwh")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<decimal>("TotalGasKwhDelta")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<decimal>("TotalImport")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.Property<decimal>("TotalImportDelta")
                        .HasPrecision(9, 3)
                        .HasColumnType("decimal(9,3)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("Date");

                    b.HasIndex("SysId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("SysId"));

                    b.ToTable("ENERGY_HISTORY", (string)null);
                });

            modelBuilder.Entity("MijnThuis.DataAccess.Entities.Flag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("SysId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("SysId"));

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("Name");

                    b.HasIndex("SysId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("SysId"));

                    b.ToTable("FLAGS", (string)null);
                });

            modelBuilder.Entity("MijnThuis.DataAccess.Entities.SolarEnergyHistoryEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Consumption")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ConsumptionFromBattery")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ConsumptionFromGrid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ConsumptionFromSolar")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DataCollected")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Export")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Import")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Production")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ProductionToBattery")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ProductionToGrid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ProductionToHome")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("SysId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("SysId"));

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("Date");

                    b.HasIndex("SysId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("SysId"));

                    b.ToTable("SOLAR_ENERGY_HISTORY", (string)null);
                });

            modelBuilder.Entity("MijnThuis.DataAccess.Entities.SolarPowerHistoryEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Consumption")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ConsumptionFromBattery")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ConsumptionFromGrid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ConsumptionFromSolar")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DataCollected")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Export")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Import")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Production")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ProductionToBattery")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ProductionToGrid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ProductionToHome")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("StorageLevel")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("SysId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("SysId"));

                    b.HasKey("Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("Date");

                    b.HasIndex("SysId");

                    SqlServerIndexBuilderExtensions.IsClustered(b.HasIndex("SysId"));

                    b.ToTable("SOLAR_POWER_HISTORY", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
