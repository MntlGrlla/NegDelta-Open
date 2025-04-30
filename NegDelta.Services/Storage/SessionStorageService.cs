using NegDelta.Data;
using NegDelta.Core.DTOs;
using NegDelta.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace NegDelta.Services.Storage;

public class SessionStorageService
{
    public Action? OnDbChanged { get; set; } // Event to notify when the db has been modified.

    private readonly IDbContextFactory<SessionDbContext> _contextFactory;
    
    // Constructor with context factory DI
    public SessionStorageService(IDbContextFactory<SessionDbContext> contextFactory)
    {
        _contextFactory = contextFactory;     
    }



    // |---- Create ----|

    /// <summary>
    /// Saves a session to the database
    /// </summary>
    /// <param name="session"><see cref="Session"/> object to save to database</param>
    /// <exception cref="InvalidOperationException">Thrown when a session with the same ID already exists in the database.</exception>
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
        OnDbChanged?.Invoke();
    }



    // |---- Read ----|


    /// <summary>
    /// Retrieves Session data by ID
    /// </summary>
    /// <param name="sessionId">ID of desired session</param>
    /// <returns>A <see cref="Session"/> object, or null if not found</returns>
    public async Task<Session?> GetSessionFullByIdAsync(string sessionId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();

        var session = await context.Sessions
            .Where(s => s.Id == sessionId)
            .Include(s => s.Stints)
                .ThenInclude(stint => stint.Laps)
                    .ThenInclude(lap => lap.TelemetryPoints)
            .Include(s => s.Stints)
                .ThenInclude(stint => stint.Laps)
                    .ThenInclude(lap => lap.PositionPoints)
            .Include(s => s.Stints)
                .ThenInclude(stint => stint.Laps)
                    .ThenInclude(lap => lap.SectorTimes)
            .FirstOrDefaultAsync();

        if (session != null)
        {
            session = OrderSession(session);
            return session;
        }

        return null;

    }


    /// <summary>
    /// Retrieves summaries for all sessions in database
    /// </summary>
    /// <returns>A list of <see cref="SessionSummaryDTO"/>s.</returns>
    public async Task<List<SessionSummaryDTO>> GetAllSessionSummariesAsync()
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


    /// <summary>
    /// Retrieves paginated session summaries from the database.
    /// </summary>
    /// <param name="pageNumber">The 1-based index of the page to retrieve</param>
    /// <param name="pageSize">Number of session summaries per page</param>
    /// <returns>An ordered list of <see cref="SessionSummaryDTO"/>s with up to <paramref name="pageSize"/> entries. 
    /// Ordered by <see cref="Session.TimeCreated"/> descending.</returns>
    public async Task<List<SessionSummaryDTO>> GetSessionSummariesAsync(int pageNumber, int pageSize)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Sessions
            .OrderByDescending(s => s.TimeCreated) // order by time created descending (most recent first)
            .Skip((pageNumber - 1) * pageSize) // skip the previous pages
            .Take(pageSize) // take the current page
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


    /// <summary>
    /// Retrieves a session summary by ID, and including stint and lap summaries
    /// </summary>
    /// <param name="sessionId">ID of desired session</param>
    /// <returns>A <see cref="SessionStintSummaryDTO"/> containing stints and lap information, or null if not found.</returns>
    public async Task<SessionStintSummaryDTO?> GetSessionStintSummaryByIdAsync(string sessionId)
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


    /// <summary>
    /// Retrieves the total number of sessions in the database.
    /// </summary>
    /// <returns>An integer representing the number of sessions in the database.</returns>
    public async Task<int> GetTotalSessionCountAsync()
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        return await context.Sessions.CountAsync();
    }


    /// <summary>
    /// Retrieves full lap data by lap ID
    /// </summary>
    /// <param name="lapId">ID of desired lap</param>
    /// <returns><see cref="Lap"/> object, or null if not found.</returns>
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



    // |---- Delete ----|

    /// <summary>
    /// Deletes a session from the database by ID
    /// </summary>
    /// <param name="sessionId">The ID of <see cref="Session"/> to be deleted.</param>
    /// <returns>An integer representing the number of <see cref="Session"/>s deleted.</returns>
    public async Task<int> DeleteSessionAsync(string sessionId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var numDeleted = await context.Sessions.Where(s => s.Id == sessionId).ExecuteDeleteAsync();
        OnDbChanged?.Invoke();
        return numDeleted;
    }


    /// <summary>
    /// Deletes a stint from the database by ID
    /// </summary>
    /// <param name="stintId">ID of <see cref="Stint"/> to delete</param>
    /// <returns>An integer representing the number of <see cref="Stint"/>s deleted.</returns>
    public async Task<int> DeleteStintAsync(string stintId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var numDeleted = await context.Stints.Where(st => st.Id == stintId).ExecuteDeleteAsync();
        OnDbChanged?.Invoke();
        return numDeleted;

    }


    /// <summary>
    /// Deletes a lap from the database by ID
    /// </summary>
    /// <param name="lapId">ID of <see cref="Lap"/> to delete</param>
    /// <returns>An integer representing the number of <see cref="Lap"/>s deleted.</returns>
    public async Task<int> DeleteLapAsync(string lapId)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var numDeleted = await context.Laps.Where(l => l.Id == lapId).ExecuteDeleteAsync();
        OnDbChanged?.Invoke();
        return numDeleted;
    }

    // |----- Helpers -----|

    private Session OrderSession(Session session)
    {
        session.Stints = session.Stints
                .OrderBy(stint => stint.StintNumber)
                .ToList();
        foreach (var st in session.Stints)
        {
            st.Laps = st.Laps
                .OrderBy(l => l.LapNumber)
                .ToList();
            foreach (var l in st.Laps)
            {
                l.TelemetryPoints = l.TelemetryPoints
                    .OrderBy(tp => tp.Timestamp)
                    .ToList();
                l.PositionPoints = l.PositionPoints
                    .OrderBy(pp => pp.Timestamp)
                    .ToList();
                l.SectorTimes = l.SectorTimes
                    .OrderBy(st => st.SectorNumber)
                    .ToList();
            }
        }

        return session;
    }

}
