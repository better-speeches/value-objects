using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Varyence.ValueObjects.Common;
using Varyence.ValueObjects.DataAccess.EF;
using Varyence.ValueObjects.DataAccess.EF.Abstractions;
using Varyence.ValueObjects.DataAccess.EF.Repositories;
using Varyence.ValueObjects.DataAccess.EF.Seeder;
using Varyence.ValueObjects.DataAccess.Repositories;

namespace Varyence.ValueObjects.ConsoleApp
{
    public static partial class Program
    {
        private static async Task MigrateDatabase(IServiceScope scope)
        {
            var seeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();

            await seeder.SeedAsync();
        }
        
        private static ServiceProvider InitApplication()
        {
            return new ServiceCollection()
                .AddSingleton(sp => new DbConnectionString())
                .AddLogging(ConfigureLogger)
                .AddTransient<IDbSeeder, DbSeeder>()
                .AddTransient<IPersonRepository, PersonRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<PersonController>()
                .AddDbContext<AppDbContext>()
                .BuildServiceProvider();
        }

        private static void ConfigureLogger(ILoggingBuilder builder)
        {
            builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                #if DEBUG
                .AddFilter("Microsoft.EntityFrameworkCore.Database", LogLevel.Debug)
                #endif
                .AddConsole(t => t.DisableColors = true);
        }
    }
}