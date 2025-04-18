using NegDelta.Data;
using NegDelta.Core.DTOs;
using NegDelta.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace NegDelta.Services.Storage;

public class SessionStorageService
{
    private readonly IDbContextFactory<SessionDbContext> _contextFactory;
    
    public SessionStorageService(IDbContextFactory<SessionDbContext> contextFactory)
    {
        _contextFactory = contextFactory;     
    }

    public void SaveSesison(Session s)
    {
        
    }

    public async Task<Session?> GetSessionFullByIdAsync(string sessionId)
    {
        var context = await _contextFactory.CreateDbContextAsync();

        
    }

    // this method retrieves summaries of all sessions stored in the database.
    public async Task<List<SessionSummaryDTO>> GetSessionSummariesAsync()
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        return await context.Sessions
            .OrderByDescending(s => s.TimeCreated) // order by time created descending (most recent first)
            .Select(s => new SessionSummaryDTO
            {
                Id = s.Id,
                CarName = s.CarName,
                TrackName = s.TrackName,
                TimeCreated = s.TimeCreated,
                Type = s.Type,
                StintCount = s.Stints.Count,
                LapCount = s.Stints.SelectMany(stint => stint.Laps).Count(),
                FastestLapTime = s.FastestLapTime
            })
            .ToListAsync();
    }

    public async Task<SessionStintSummaryDTO?> GetSessionStintsByIdAsync(string sessionId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Sessions
            .Where(s => s.Id == sessionId)
            .Include(s => s.Stints)
                .ThenInclude(stint => stint.Laps)
            // Creating DTOs here to avoid loading unnecessary data
            // using projection to create DTOs 
            // This will create a SessionStintSummaryDTO for the session with the given ID
            .Select(s => new SessionStintSummaryDTO
            {
                // This is the session ID
                Id = s.Id,
                // This is the list of stints for the session
                Stints = s.Stints.Select(stint => new StintSummaryDTO
                {
                    // Stint ID, number, and fastest lap time
                    Id = stint.Id,
                    StintNumber = stint.StintNumber,
                    FastestLapTime = stint.FastestLapTime,
                    // creating list of lap summaries for each stint
                    Laps = stint.Laps.Select(l => new LapSummaryDTO
                    {
                        Id = l.Id,
                        LapNumber = l.LapNumber,
                        LapTime = l.LapTime
                    }).ToList()

                }).ToList()
            })
            .FirstOrDefaultAsync(); // selecting only the session with the given ID or null if not found
    }

    // This method retrieves a lap by its Id.
    public async Task<Lap?> GetLapByIdAsync(string lapId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Laps
            .Where(l => l.Id == lapId)
            // .Include is for navigation layers.
            // LapTime and LapNumber are simple scalars and don't need to be navigated to.
            // Lists of objects need to be navigated to. 
            .Include(l => l.TelemetryPoints)
            .Include(l => l.PositionPoints)
            .Include(l => l.SectorTimes)
            .FirstOrDefaultAsync();
    }
    
    public async Task SaveSessionAsync(Session session)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        // check if the session already exists in the database
        var exists = await context.Sessions.AnyAsync(s => s.Id == session.Id);
        if (exists)
        {
            throw new InvalidOperationException("Session with the same ID already exists.");
        }

        // add the session to the context
        context.Sessions.Add(session);
        // save the changes to the database
        await context.SaveChangesAsync();
    }

}
