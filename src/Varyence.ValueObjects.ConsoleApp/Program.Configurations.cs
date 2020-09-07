using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Varyence.ValueObjects.Common;
using Varyence.ValueObjects.DataAccess.EF;
using Varyence.ValueObjects.DataAccess.EF.Abstractions;
using Varyence.ValueObjects.DataAccess.EF.Repositories;
using Varyence.ValueObjects.DataAccess.Repositories;

namespace Varyence.ValueObjects.ConsoleApp
{
    public static partial class Program
    {
        private const string ConnectionString =
            "Server=localhost,1433;Database=VaryenceValueObjects;User=SA;Password=Your_password123;";

        private static async Task MigrateDatabase(IServiceScope scope)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.MigrateAsync();
        }
        
        private static ServiceProvider InitApplication() =>
            new ServiceCollection()
                .AddSingleton(sp => new DbConnectionString(ConnectionString))
                .AddLogging(builder => builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("Microsoft.EntityFrameworkCore.Database", LogLevel.Debug)
                    .AddConsole(t => t.DisableColors = true))
                .AddTransient<IPersonRepository, PersonRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<PersonController>()
                .AddDbContext<AppDbContext>()
                .BuildServiceProvider();
    }
}