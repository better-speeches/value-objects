using System;

namespace Varyence.ValueObjects.Common
{
    public sealed class DbConnectionString
    {
        #if DEBUG
        private const string ConnectionString =
            "Server=localhost,1433;Database=VaryenceValueObjects;User=SA;Password=Your_password123;";
        #else
        private const string ConnectionString =
            "Server=db;Database=VaryenceValueObjects;User=SA;Password=Your_password123;";
        #endif
        
        //public DbConnectionString(string value)
        public DbConnectionString()
        {
            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new ArgumentNullException(nameof(ConnectionString));
            
            Value = ConnectionString;
        }

        public string Value { get; }
    }
}