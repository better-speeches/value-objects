using System.Threading.Tasks;

namespace Varyence.ValueObjects.DataAccess.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}