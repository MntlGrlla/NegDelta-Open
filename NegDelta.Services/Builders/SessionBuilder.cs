using NegDelta.Core.Models;
using NegDelta.Core.Enum;
using NegDelta.Services.Factories;

namespace NegDelta.Services.Builders;

public class SessionBuilder
{
    private readonly Session _currentSession;
    private Stint? _currentStint;
    private int _currentStintNumber = 1;
    private int _currentLapNumber = 1;

    public SessionBuilder(SessionType type, string carName, string trackName)
    {
        _currentSession = new Session
        {
            Type = type,
            CarName = carName,
            TrackName = trackName,
            TimeCreated = DateTime.Now,
        };
    }

    public void StartNewStint()
    {
        if (_currentStint is not null)
        {
            throw new InvalidOperationException("A stint is already in progress. " +
                "Complete the current stint before starting a new one.");
        }

        _currentStint = new Stint
        {
            StintNumber = _currentStintNumber,
        };

        _currentSession.Stints.Add(_currentStint);
    }

    public void AddLap(
        TimeSpan lapTime,
        List<TelemetryPoint> telemetryPoints,
        List<PositionPoint> positionPoints,
        List<Sector> sectorTimes)
    {
        if (_currentStint is null)
        {
            throw new InvalidOperationException("No stint is in progress. " +
                "Start a new stint before adding laps.");
        }

        var lap = LapFactory.CreateLap(
            _currentLapNumber,
            lapTime,
            telemetryPoints,
            positionPoints,
            sectorTimes);

        _currentStint.Laps.Add(lap);
        _currentLapNumber++;
    }

    public void CompleteStint()
    {
        if (_currentStint is null)
        {
            throw new InvalidOperationException("No active stint. Start a stint before completing one."); 
        }

        Lap fastestLap = _currentStint.Laps.MinBy(l => l.LapTime)!; // We know it's not going to be null. (! operator)
        _currentStint.FastestLapID = fastestLap.Id;
        _currentStint.FastestLapTime = fastestLap.LapTime;

        _currentStint = null;
        _currentStintNumber++;
        _currentLapNumber = 1;
    }

    public Session Build()
    {
        if (_currentStint is not null)
        {
            throw new InvalidOperationException("A stint is still in progress. " +
                "Complete the current stint before building the session.");
        }

        List<Lap> laps = new List<Lap>();
        foreach (var s in _currentSession.Stints)
        {
            laps.Add(s.Laps.MinBy(l => l.LapTime)!); // We know it's not going to be null. 
        } 

        return _currentSession;
    }
}
