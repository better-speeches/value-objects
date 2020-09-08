using Microsoft.EntityFrameworkCore.Design;
using Varyence.ValueObjects.Common;

namespace Varyence.ValueObjects.DataAccess.EF
{
    public sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var dbConnectionString = new DbConnectionString();
            return new AppDbContext(null, dbConnectionString);
        }
    }
}