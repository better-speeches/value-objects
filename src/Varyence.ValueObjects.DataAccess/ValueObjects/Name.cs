using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace Varyence.ValueObjects.DataAccess.ValueObjects
{
    public class Name : ValueObject<Name>
    {
        public string Value { get; protected set; }

        private Name(string value)
        {
            Value = value;
        }

        public static Result<Name> Create(string name) =>
            name switch
            {
                _ when string.IsNullOrWhiteSpace(name) =>
                    Result.Failure<Name>("Name should not be empty"),

                _ when name.Length < 2 =>
                    Result.Failure<Name>("Name is too short"),
                
                _ when name.Length > 100 =>
                    Result.Failure<Name>("Name is too long"),

                _ when !Regex.IsMatch(name, @"^[\p{L}\p{M}\p{N}]{1,100}\z") =>
                    Result.Failure<Name>("Name is invalid"),

                _ => new Name(name)
            };

        public static implicit operator string(Name name)
        {
            return name.Value;
        }
        
        protected override bool EqualsCore(Name other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}