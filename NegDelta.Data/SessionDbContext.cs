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

    // Constructor sets the database path
    public SessionDbContext(DbContextOptions<SessionDbContext> options) : base(options)
    {
        
    }
}
