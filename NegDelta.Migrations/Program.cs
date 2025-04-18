using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NegDelta.Data;

var serviceProvider = new ServiceCollection()
    .AddDbContext<SessionDbContext>(options =>
    options.UseSqlite("Data Source=sessions.db"))
    .BuildServiceProvider();

var dbContext = serviceProvider.GetRequiredService<SessionDbContext>();

// Ensure the database is created and apply any pending migrations
dbContext.Database.Migrate();
