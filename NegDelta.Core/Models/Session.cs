using NegDelta.Core.Enum;
namespace NegDelta.Core.Models;

public class Session
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime TimeCreated { get; set; } 
    public string CarName { get; set; } = string.Empty;
    public string TrackName { get; set; } = string.Empty;
    public SessionType Type { get; set; }
    public List<Session>? ChildSessions { get; set; } // necessary for race sessions that hold practice, qualifying, and the race
    public List<Stint> Stints { get; set; } = new();
    public TimeSpan FastestLapTime { get; set; }
}
