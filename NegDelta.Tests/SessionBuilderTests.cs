using NegDelta.Core.Enum;
using NegDelta.Core.Models;
using NegDelta.Services.Builders;

namespace NegDelta.Tests;

public class SessionBuilderTests
{
    [Fact]
    public void Constructor_CreatesEmptySession()
    {
        var builder = new SessionBuilder(SessionType.Practice, "Mazda MX-5", "Tsukuba");
        var session = builder.Build();

        Assert.Equal(SessionType.Practice, session.Type);
        Assert.Equal("Mazda MX-5", session.CarName);
        Assert.Equal("Tsukuba", session.TrackName);
        Assert.Empty(session.Stints);
    }

    [Fact]
    public void StartNewStint_Twice_ThrowsException()
    {
        var builder = new SessionBuilder(SessionType.Practice, "Mazda MX-5", "Tsukuba");

        builder.StartNewStint();

        var ex = Assert.Throws<InvalidOperationException>(() => builder.StartNewStint());
        Assert.Equal("A stint is already in progress. Complete the current stint before starting a new one.", ex.Message);
    }

    [Fact]
    public void AddLap_NoStint_ThrowsException()
    {
        var builder = new SessionBuilder(SessionType.Practice, "Mazda MX-5", "Tsukuba");

        var ex = Assert.Throws<InvalidOperationException>(() => builder.AddLap(TimeSpan.FromMinutes(1), new(), new(), new()));

        Assert.Equal("No stint is in progress. Start a new stint before adding laps.", ex.Message);
    }

    [Fact]
    public void SessionBuilder_CreatesSessionWithCorrectLapNumbers()
    {
        var builder = new SessionBuilder(SessionType.Race, "Mazda", "Tsukuba");

        builder.StartNewStint();
        builder.AddLap(TimeSpan.FromSeconds(90), new(), new(), new());
        builder.AddLap(TimeSpan.FromSeconds(91), new(), new(), new());
        builder.CompleteStint();

        builder.StartNewStint();
        builder.AddLap(TimeSpan.FromSeconds(92), new(), new(), new());
        builder.CompleteStint();

        var session = builder.Build();

        Assert.Equal(2, session.Stints.Count);
        Assert.Equal(2, session.Stints[0].Laps.Count);
        Assert.Equal(1, session.Stints[1].Laps.Count);
        Assert.Equal(1, session.Stints[0].Laps[0].LapNumber);
        Assert.Equal(2, session.Stints[0].Laps[1].LapNumber);
        Assert.Equal(1, session.Stints[1].Laps[0].LapNumber);
    }

    [Fact]
    public void CompleteStint_WithoutStarting_ThrowsException()
    {
        var builder = new SessionBuilder(SessionType.Practice, "Mazda MX-5", "Tsukuba");

        var ex = Assert.Throws<InvalidOperationException>(() => builder.CompleteStint());

        Assert.Equal("No active stint. Start a stint before completing one.", ex.Message);
    }

    [Fact]
    public void CompleteStint_SetsFastestLapCorrectly()
    {
        
        var builder = new SessionBuilder(SessionType.Practice, "Porsche 911 GT3", "Spa-Francorchamps");
        builder.StartNewStint();

        var telemetry = new List<TelemetryPoint>(); // Can be empty for testing
        var positions = new List<PositionPoint>();
        var sectors = new List<Sector>();

        builder.AddLap(TimeSpan.FromSeconds(82), telemetry, positions, sectors); // Lap 1
        builder.AddLap(TimeSpan.FromSeconds(78), telemetry, positions, sectors); // Lap 2 - fastest
        builder.AddLap(TimeSpan.FromSeconds(85), telemetry, positions, sectors); // Lap 3

        builder.CompleteStint();
        var session = builder.Build();
        var stint = session.Stints[0];
        var fastestLap = stint.Laps.Find(l => l.Id == stint.FastestLapID);

        Assert.NotNull(fastestLap);
        Assert.Equal(TimeSpan.FromSeconds(78), stint.FastestLapTime);
        Assert.Equal(fastestLap.Id, stint.FastestLapID);
    }
}