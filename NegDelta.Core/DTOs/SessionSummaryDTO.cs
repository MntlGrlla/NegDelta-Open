using NegDelta.Core.Enum;
namespace NegDelta.Core.DTOs;

public class SessionSummaryDTO
{
    public string Id { get; set; } = string.Empty;
    public string CarName { get; set; } = string.Empty;
    public string TrackName { get; set; } = string.Empty;
    public DateTime TimeCreated { get; set; }
    public SessionType Type { get; set; }
    public int StintCount { get; set; }
    public int LapCount { get; set; }
    public TimeSpan FastestLapTime { get; set; }
}
