using CSharpFunctionalExtensions;

namespace Varyence.ValueObjects.DataAccess.ValueObjects
{
    public class Age : ValueObject<Age>
    {
        public int Value { get; protected set; }

        protected Age()
        {
        }
        
        private Age(int value)
        {
            Value = value;
        }

        public static Result<Age> Create(int age) =>
            age switch
            {
                _ when age < 0 => Result.Failure<Age>("Age cannot be negative value."),
                _ when age > 120 => Result.Failure<Age>("Age is too big. No one leaves forever."),
                _ => new Age(age)
            };

        protected override bool EqualsCore(Age other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}