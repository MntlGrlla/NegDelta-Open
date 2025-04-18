namespace NegDelta.Core.DTOs;

public class StintSummaryDTO
{
    public string Id { get; set; } = string.Empty;
    public int StintNumber { get; set; }
    public TimeSpan FastestLapTime { get; set; }
    public List<LapSummaryDTO> Laps { get; set; } = new();
}
