namespace NegDelta.Core.Models;

public class TelemetryPoint
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string LapId { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public double ThrottlePosition { get; set; }
    public double BrakePosition { get; set; }
    public double SteeringAngle { get; set; }
}
