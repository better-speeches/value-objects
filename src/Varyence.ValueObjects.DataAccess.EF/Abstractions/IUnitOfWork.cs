using System.Threading.Tasks;

namespace Varyence.ValueObjects.DataAccess.EF.Abstractions
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}