using CSharpFunctionalExtensions;

namespace Varyence.ValueObjects.DataAccess.ValueObjects
{
    public class PersonName : ValueObject<PersonName>
    {
        public Name FirstName { get; protected set; }
        public Name LastName { get; protected set; }

        protected PersonName()
        {
        }
        
        private PersonName(Name firstName, Name lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static Result<PersonName> Create(Maybe<Name> maybeFirstName, Maybe<Name> maybeLastName)
        {
            var firstNameResult = maybeFirstName.ToResult("FirstName is null.");
            var lastNameResult = maybeLastName.ToResult("LastName is null.");

            return Result
                .Combine(firstNameResult, lastNameResult)
                .Map(() => new PersonName(maybeFirstName.Value, maybeLastName.Value));
        }
        
        protected override bool EqualsCore(PersonName other)
        {
            return FirstName == other.FirstName && LastName == other.LastName;
        }

        protected override int GetHashCodeCore()
        {
            return FirstName.GetHashCode() ^ LastName.GetHashCode();
        }
    }
}