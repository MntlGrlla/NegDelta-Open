﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NegDelta.Data;

#nullable disable

namespace NegDelta.Data.Migrations
{
    [DbContext(typeof(SessionDbContext))]
    [Migration("20250421214950_AddForeignKeysAndCascadeDeletes")]
    partial class AddForeignKeysAndCascadeDeletes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.4");

            modelBuilder.Entity("NegDelta.Core.Models.Lap", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("LapNumber")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("LapTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("StintId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("StintId");

                    b.ToTable("Laps");
                });

            modelBuilder.Entity("NegDelta.Core.Models.PositionPoint", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("LapId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<double>("X")
                        .HasColumnType("REAL");

                    b.Property<double>("Y")
                        .HasColumnType("REAL");

                    b.Property<double>("Z")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("LapId");

                    b.ToTable("PositionPoints");
                });

            modelBuilder.Entity("NegDelta.Core.Models.Sector", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("LapId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SectorNumber")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan>("SectorTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LapId");

                    b.ToTable("Sector");
                });

            modelBuilder.Entity("NegDelta.Core.Models.Session", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("CarName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("FastestLapTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("TrackName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("NegDelta.Core.Models.Stint", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("FastestLapID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<TimeSpan>("FastestLapTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("SessionId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("StintNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("Stints");
                });

            modelBuilder.Entity("NegDelta.Core.Models.TelemetryPoint", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<double>("BrakePosition")
                        .HasColumnType("REAL");

                    b.Property<string>("LapId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("SteeringAngle")
                        .HasColumnType("REAL");

                    b.Property<double>("ThrottlePosition")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LapId");

                    b.ToTable("TelemetryPoints");
                });

            modelBuilder.Entity("NegDelta.Core.Models.Lap", b =>
                {
                    b.HasOne("NegDelta.Core.Models.Stint", null)
                        .WithMany("Laps")
                        .HasForeignKey("StintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NegDelta.Core.Models.PositionPoint", b =>
                {
                    b.HasOne("NegDelta.Core.Models.Lap", null)
                        .WithMany("PositionPoints")
                        .HasForeignKey("LapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NegDelta.Core.Models.Sector", b =>
                {
                    b.HasOne("NegDelta.Core.Models.Lap", null)
                        .WithMany("SectorTimes")
                        .HasForeignKey("LapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NegDelta.Core.Models.Stint", b =>
                {
                    b.HasOne("NegDelta.Core.Models.Session", null)
                        .WithMany("Stints")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NegDelta.Core.Models.TelemetryPoint", b =>
                {
                    b.HasOne("NegDelta.Core.Models.Lap", null)
                        .WithMany("TelemetryPoints")
                        .HasForeignKey("LapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NegDelta.Core.Models.Lap", b =>
                {
                    b.Navigation("PositionPoints");

                    b.Navigation("SectorTimes");

                    b.Navigation("TelemetryPoints");
                });

            modelBuilder.Entity("NegDelta.Core.Models.Session", b =>
                {
                    b.Navigation("Stints");
                });

            modelBuilder.Entity("NegDelta.Core.Models.Stint", b =>
                {
                    b.Navigation("Laps");
                });
#pragma warning restore 612, 618
        }
    }
}
