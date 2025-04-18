using IRSDKSharper;
using NegDelta.Core.Models;
using NegDelta.Services.Runtime.Interfaces;
using System.Diagnostics;

namespace NegDelta.Services.Runtime;

public class IRacingService : ISimService
{
    public event EventHandler<TelemetryPoint>? OnTelemetryUpdated;

    private System.Timers.Timer _timer;

    public void StartTelemetry()
    {
        _timer = new System.Timers.Timer(1000); // TODO: change to 60 Hz
        _timer.Elapsed += (sender, e) =>
        {
            // Simulate telemetry data
            var telemetryData = new TelemetryPoint
            {
                Timestamp = DateTime.Now,
                ThrottlePosition = new Random().NextDouble(),
                BrakePosition = new Random().NextDouble()
            };
            Debug.WriteLine("About to invoke OnTelemetryUpdated");
            OnTelemetryUpdated?.Invoke(this, telemetryData);
        };
        _timer.Start();
    }

    public void StopTelemetry()
    {
        _timer.Stop();
        _timer.Dispose();
    }
}
