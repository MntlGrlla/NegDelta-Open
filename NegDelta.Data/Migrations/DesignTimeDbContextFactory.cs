
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NegDelta.Data.Migrations;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SessionDbContext>
{
    // Creates a context on design time for migrations. 
    // This is used by the EF Core tools to create a context for migrations. 
    // If this wasn't here, the target would be the blazor project, which does not have the
    // .NET Core SDK project available for the tooling of the Package Manager Console.
    public SessionDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SessionDbContext>();
        // This path is relative to the project directory, and is not the same as the 
        // .db file that will be used. This is just to create a context. 
        optionsBuilder.UseSqlite("Data Source=sessions.db");
        return new SessionDbContext(optionsBuilder.Options);
    }
}
