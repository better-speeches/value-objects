using CSharpFunctionalExtensions;
using Varyence.ValueObjects.DataAccess.Entities;

namespace Varyence.ValueObjects.DataAccess.ValueObjects
{
    public class PersonName : ValueObject<PersonName>
    {
        public Name FirstName { get; protected set; }
        public Name LastName { get; protected set; }
        
        public virtual Suffix Suffix { get; protected set; }

        protected PersonName()
        {
        }
        
        private PersonName(Name firstName, Name lastName, Suffix suffix)
        {
            FirstName = firstName;
            LastName = lastName;
            Suffix = suffix;
        }

        public static Result<PersonName> Create(
            Maybe<Name> maybeFirstName, 
            Maybe<Name> maybeLastName, 
            Maybe<Suffix> maybeSuffix)
        {
            var firstNameResult = maybeFirstName.ToResult("FirstName is null.");
            var lastNameResult = maybeLastName.ToResult("LastName is null.");
            var suffixResult = maybeSuffix.ToResult("Suffix is null");

            return Result
                .Combine(firstNameResult, lastNameResult, suffixResult)
                .Map(() => new PersonName(firstNameResult.Value, lastNameResult.Value, suffixResult.Value));
        }
        
        protected override bool EqualsCore(PersonName other)
        {
            return FirstName == other.FirstName && LastName == other.LastName;
        }

        protected override int GetHashCodeCore()
        {
            return FirstName.GetHashCode() ^ LastName.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Suffix.Name} {FirstName.Value} {LastName.Value}";
        }
    }
}