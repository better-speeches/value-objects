using System;

namespace Varyence.ValueObjects.Common
{
    public sealed class DbConnectionString
    {
        public DbConnectionString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));
            
            Value = value;
        }

        public string Value { get; }
    }
}