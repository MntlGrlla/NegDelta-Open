using NegDelta.Core.Models;

namespace NegDelta.Services.Runtime.Interfaces;

public interface ISimService
{
    event EventHandler<TelemetryPoint>? OnTelemetryUpdated;
    void StartTelemetry();
    void StopTelemetry();
}
