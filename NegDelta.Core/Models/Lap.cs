namespace NegDelta.Core.Models;

public class Lap
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public TimeSpan LapTime { get; set; }
    public int LapNumber { get; set; }
    public List<TelemetryPoint> TelemetryPoints { get; set; } = new();
    public List<PositionPoint> PositionPoints { get; set; } = new();
    public List<Sector> SectorTimes { get; set; } = new();
}
