﻿using NegDelta.Core.Models;
namespace NegDelta.Services.Factories;

public static class LapFactory
{
    public static Lap CreateLap(
        string parentStintId,
        int lapNumber,
        TimeSpan LapTime,
        List<TelemetryPoint> telemetryPoints,
        List<PositionPoint> positionPoints,
        List<Sector> sectorTimes)
    {
        return new Lap
        {
            StintId = parentStintId,
            LapNumber = lapNumber,
            LapTime = LapTime,
            TelemetryPoints = telemetryPoints,
            PositionPoints = positionPoints,
            SectorTimes = sectorTimes
        };
    }
}
