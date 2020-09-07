using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Varyence.ValueObjects.DataAccess.Entities;

namespace Varyence.ValueObjects.DataAccess.Repositories
{
    public interface IPersonRepository
    {
        Task<Maybe<Person>> GetByIdAsync(int personId);
        Task<Maybe<Person>> GetWith(Expression<Func<Person, bool>> predicate);
        void Save(Person person);
        void Remove(Person person);
    }
}