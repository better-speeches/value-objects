using System.Threading;
using System.Threading.Tasks;

namespace Varyence.ValueObjects.DataAccess.EF.Abstractions
{
    public interface IDbSeeder
    {
        Task SeedAsync(CancellationToken cancellationToken = default);
    }
}