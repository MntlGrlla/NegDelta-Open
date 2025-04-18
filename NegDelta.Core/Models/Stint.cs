namespace NegDelta.Core.Models;

public class Stint
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public int StintNumber { get; set; }
    public string FastestLapID { get; set; } = string.Empty;
    public TimeSpan FastestLapTime { get; set; }
    public List<Lap> Laps { get; set; } = new();
    
}
