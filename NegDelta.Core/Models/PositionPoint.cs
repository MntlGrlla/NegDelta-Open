using System.Security.Cryptography.X509Certificates;

namespace NegDelta.Core.Models;

public class PositionPoint
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string LapId { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
}
