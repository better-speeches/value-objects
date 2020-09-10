using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Varyence.ValueObjects.DataAccess.Entities;
using Varyence.ValueObjects.DataAccess.Repositories;

namespace Varyence.ValueObjects.DataAccess.EF.Repositories
{
    public sealed class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _dbContext;

        public PersonRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Maybe<Person>> GetByIdAsync(int personId)
        {
            return await _dbContext.Persons.FindAsync(personId);
        }

        public async Task<Maybe<Person>> GetWithAsync(Expression<Func<Person, bool>> predicate)
        {
            return await _dbContext.Persons.FirstOrDefaultAsync(predicate);
        }

        public void Save(Person person)
        {
            _dbContext.Persons.Attach(person);
        }

        public void Remove(Person person)
        {
            _dbContext.Persons.Remove(person);
        }
    }
}