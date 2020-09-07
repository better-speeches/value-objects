using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Varyence.ValueObjects.DataAccess.EF.Abstractions;
using Varyence.ValueObjects.DataAccess.Entities;
using Varyence.ValueObjects.DataAccess.Repositories;
using Varyence.ValueObjects.DataAccess.ValueObjects;

namespace Varyence.ValueObjects.ConsoleApp
{
    public sealed class PersonController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonRepository _repository;
        private readonly ILogger<PersonController> _logger;

        public PersonController(
            IUnitOfWork unitOfWork, 
            IPersonRepository repository, 
            ILogger<PersonController> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task PresentPerson(int personId)
        {
            await _repository.GetByIdAsync(personId)
                .ToResult($"Person was not found for ID: {personId}")
                .Tap(person => _logger.LogInformation(FormatPerson(person)))
                .OnFailure(error => _logger.LogError(error));
        }

        public async Task Create(string firstName, string lastName, int age)
        {
            var firstNameResult = Name.Create(firstName);
            var lastNameResult = Name.Create(lastName);
            var ageResult = Age.Create(age);

            await Result
                .Combine(firstNameResult, lastNameResult, ageResult)
                .Bind(() => PersonName.Create(firstNameResult.Value, lastNameResult.Value))
                .Bind(personName => Person.Create(personName, ageResult.Value))
                .Tap(person => _repository.Save(person))
                .Tap(() => _unitOfWork.CommitAsync())
                .Tap(() => _logger.LogInformation($"Person was stored to db: {firstName} {lastName}"))
                .OnFailure(error => _logger.LogError(error));
            
            /*
            var firstNameResult = Name.Create(firstName);
            var lastNameResult = Name.Create(lastName);
            var ageResult = Age.Create(age);
            var personName = PersonName.Create(firstNameResult.Value, lastNameResult.Value);
            var personResult = Person.Create(personName.Value, ageResult.Value);

            _repository.Save(personResult.Value);
            await _unitOfWork.CommitAsync();
            */
        }
        
        public async Task Rename(int personId, string firstName, string lastName)
        {
            var firstNameResult = Name.Create(firstName);
            var lastNameResult = Name.Create(lastName);

            await Result
                .Combine(firstNameResult, lastNameResult)
                .Bind(() => _repository
                    .GetByIdAsync(personId)
                    .ToResult($"Person was not found for ID: {personId}"))
                .Bind(person => PersonName
                    .Create(firstNameResult.Value, lastNameResult.Value)
                    .Tap(personName => person.Rename(personName)))
                .Tap(() => _unitOfWork.CommitAsync())
                .Tap(() => _logger.LogInformation($"Person was renamed to {firstName} {lastName}"))
                .OnFailure(error => _logger.LogError(error));
            
            /*
            var firstNameResult = Name.Create(firstName);
            var lastNameResult = Name.Create(lastName);
            var personNameResult = PersonName.Create(firstNameResult.Value, lastNameResult.Value);
            
            var maybePerson = await _repository.GetById(personId);
            maybePerson.Value.Rename(personNameResult.Value);
            */
        }
        
        public async Task UpdateAge(int personId, int personAge)
        {
            await _repository.GetByIdAsync(personId)
                .ToResult($"Person was not found for ID: {personId}")
                .Bind(person => Age
                    .Create(personAge)
                    .Tap(age => person.UpdateAge(age)))
                .Tap(() => _unitOfWork.CommitAsync())
                .Tap(() => _logger.LogInformation($"Person age updated to {personAge}"))
                .OnFailure(error => _logger.LogError(error));

            /*
            var maybePerson = await _repository.GetById(personId);
            var ageResult = Age.Create(personAge);
            maybePerson.Value.UpdateAge(ageResult.Value);

            await _unitOfWork.CommitAsync();
            */
        }

        public async Task UpdateGithubUrl(int personId, string url)
        {
            Uri uri = null;
            
            try
            {
                uri = new Uri(url);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
            }
            catch (UriFormatException e)
            {
                Console.WriteLine(e);
            }

            await _repository.GetByIdAsync(personId)
                .ToResult($"Person was not found for ID: {personId}")
                .Tap(person => person.UpdateGithubUri(uri))
                .Tap(() => _unitOfWork.CommitAsync())
                .Tap(() => _logger.LogInformation($"Person github url updated to {url}"))
                .OnFailure(error => _logger.LogError(error));
        }

        public async Task Remove(int personId)
        {
            await _repository.GetByIdAsync(personId)
                .ToResult($"Person was not found for ID: {personId}")
                .Tap(person => _repository.Remove(person))
                .Tap(() => _unitOfWork.CommitAsync())
                .Tap(() => _logger.LogInformation($"Person was removed with ID: {personId}"))
                .OnFailure(error => _logger.LogError(error));
        }

        private static string FormatPerson(Person person)
        {
            var result = $"{person.Name.FirstName.Value} {person.Name.LastName.Value} has age {person.Age.Value}"; 
            return person.GithubAccountUri != null
                ? result + $" and github {person.GithubAccountUri}"
                : result;
        }
    }
}