using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using Microsoft.EntityFrameworkCore;
using NegDelta.Data;
using NegDelta.Services.Runtime;
using NegDelta.Services.Runtime.Interfaces;
using NegDelta.Core.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;


namespace NegDelta;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
		builder.Services.AddMudServices();

		builder.Services.AddSingleton<ISimService, IRacingService>();

        // Creating the db context at the desired path
        builder.Services.AddDbContextFactory<SessionDbContext>(options =>
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sessions.db");
            options.UseSqlite($"Data Source={dbPath}");
        });

        using (var scope = builder.Services.BuildServiceProvider().CreateScope())
        {
            var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SessionDbContext>>();
			var dbContext = factory.CreateDbContext();
            dbContext.Database.Migrate();
        }
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
