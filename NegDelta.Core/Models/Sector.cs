namespace NegDelta.Core.Models;

public class Sector
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public int SectorNumber { get; set; }
    public TimeSpan SectorTime { get; set; }

}
