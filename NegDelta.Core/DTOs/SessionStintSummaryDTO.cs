namespace NegDelta.Core.DTOs;

public class SessionStintSummaryDTO
{
    public string Id { get; set; } = string.Empty;
    public List<StintSummaryDTO> Stints { get; set; } = new();
}
