using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Varyence.ValueObjects.DataAccess.EF.Abstractions;
using Varyence.ValueObjects.DataAccess.Entities;

namespace Varyence.ValueObjects.DataAccess.EF.Seeder
{
    public class DbSeeder : IDbSeeder
    {
        private readonly AppDbContext _dbContext;

        public DbSeeder(AppDbContext dbContext) => 
            _dbContext = dbContext;

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.Database.EnsureDeletedAsync(cancellationToken);
            await _dbContext.Database.MigrateAsync(cancellationToken);
            
            await InitializeInternalAsync(cancellationToken);
        }

        private async Task InitializeInternalAsync(CancellationToken cancellationToken)
        {
            await _dbContext.Suffixes.AddRangeAsync(Suffix.AllSuffixes, cancellationToken);
            await _dbContext.SaveChangesAsync(true, cancellationToken);
        }
    }
}