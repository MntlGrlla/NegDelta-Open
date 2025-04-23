using Microsoft.EntityFrameworkCore;
using NegDelta.Core.Models;
using System.Security.Cryptography.X509Certificates;

namespace NegDelta.Data;
public class SessionDbContext : DbContext
{

    public DbSet<Session> Sessions { get; set; } 
    public DbSet<Stint> Stints { get; set; } 
    public DbSet<Lap> Laps { get; set; } 
    public DbSet<TelemetryPoint> TelemetryPoints { get; set; }
    public DbSet<PositionPoint> PositionPoints { get; set; }
    public DbSet<Sector> SectorTimes { get; set; } 

    public SessionDbContext(DbContextOptions<SessionDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        base.OnModelCreating(modelBuilder);

        // |----- Configure Relationships -----|

        // Configure Session -> Stint relationship 
        modelBuilder.Entity<Session>()
            .HasMany(s => s.Stints) // Session has many Stints
            .WithOne() // Stint is part of one Session, but no navigation back to it, therefore no parameter
            .HasForeignKey(st => st.SessionId) // Stint has foreign key to Session
            .OnDelete(DeleteBehavior.Cascade); // If Session is deleted, delete all Stints

        // Configure Stint -> Lap relationship
        modelBuilder.Entity<Stint>()
            .HasMany(s => s.Laps) // Stint has many Laps
            .WithOne() // Lap is part of one Stint, but no navigation back to it, therefore no parameter
            .HasForeignKey(l => l.StintId) // Lap has foreign key to Stint
            .OnDelete(DeleteBehavior.Cascade); // If Stint is deleted, delete all Laps

        // Configure Lap -> TelemetryPoint relationship
        modelBuilder.Entity<Lap>()
            .HasMany(l => l.TelemetryPoints) // Lap has many TelemetryPoints
            .WithOne() // TelemetryPoint is part of one Lap, but no navigation back to it, therefore now parameter
            .HasForeignKey(tp => tp.LapId) // TelemetryPoint has foreign key to Lap
            .OnDelete(DeleteBehavior.Cascade); // If Lap is deleted, delete all TelemetryPoints

        // Configure Lap -> PositionPoint relationship
        modelBuilder.Entity<Lap>()
            .HasMany(l => l.PositionPoints) // Lap has many PositionPoints
            .WithOne() // PositionPoint is part of one Lap, but no navigation back to it, therefore no parameter
            .HasForeignKey(pp => pp.LapId) // PositionPoint has foreign key to Lap
            .OnDelete(DeleteBehavior.Cascade); // If Lap is deleted, delete all PositionPoints

        // Configure Lap -> SectorTime relationship
        modelBuilder.Entity<Lap>()
            .HasMany(l => l.SectorTimes) // Lap has many SectorTimes
            .WithOne() // SectorTime is part of one Lap, but no navigation back to it, therefore no parameter
            .HasForeignKey(sect => sect.LapId) // SectorTime has foreign key to Lap
            .OnDelete(DeleteBehavior.Cascade); // If Lap is deleted, delete all SectorTimes

        
        
        // |----- Configure Indexes -----|

        // Configure index for SessionId in Stint
        modelBuilder.Entity<Stint>()
            .HasIndex(s => s.SessionId);

        // Configure index for StintId in Lap
        modelBuilder.Entity<Lap>()
            .HasIndex(l => l.StintId);

        // Configure index for LapId in TelemetryPoint
        modelBuilder.Entity<TelemetryPoint>()
            .HasIndex(tp => tp.LapId);

        // Configure index for LapId in PositionPoint
        modelBuilder.Entity<PositionPoint>()
            .HasIndex(pp => pp.LapId);

        // Configure index for LapId in SectorTime
        modelBuilder.Entity<Sector>()
            .HasIndex(sect => sect.LapId);
    }
}
