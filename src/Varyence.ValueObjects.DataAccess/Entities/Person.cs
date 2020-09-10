using System;
using CSharpFunctionalExtensions;
using Varyence.ValueObjects.DataAccess.Entities.Base;
using Varyence.ValueObjects.DataAccess.ValueObjects;

#nullable enable

namespace Varyence.ValueObjects.DataAccess.Entities
{
    public class Person : Entity
    {
        public virtual PersonName Name { get; private set; }
        public virtual Age Age { get; private set; }
        public Uri? GithubAccountUri { get; private set; }

        protected Person()
        {
        }

        private Person(PersonName name, Age age)
        {
            Name = name;
            Age = age;
        }

        public static Result<Person> Create(Maybe<PersonName> maybePersonName, Maybe<Age> maybeAge)
        {
            var personNameResult = maybePersonName.ToResult("PersonName is null.");
            var ageResult = maybeAge.ToResult("Person Age is null");

            return Result
                .Combine(personNameResult, ageResult)
                .Map(() => new Person(personNameResult.Value, ageResult.Value));
        }

        public Result UpdateAge(Maybe<Age> maybeAge) =>
            maybeAge
                .ToResult("Person Age is null.")
                .Tap(age => Age = age);
 
        public Result Rename(Maybe<PersonName> maybePersonName) =>
            maybePersonName
                .ToResult("PersonName is null.")
                .Tap(personName => Name = personName);

        public void UpdateGithubUri(Maybe<Uri> maybeUri)
        {
            GithubAccountUri = maybeUri.HasValue
                ? maybeUri.Value
                : null;
        }
    }
}