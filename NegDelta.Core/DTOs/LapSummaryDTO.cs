namespace NegDelta.Core.DTOs;

public class LapSummaryDTO
{
    public string Id { get; set; } = string.Empty;
    public TimeSpan LapTime { get; set; }
    public int LapNumber { get; set; }
}
