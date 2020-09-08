using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Varyence.ValueObjects.Common;
using Varyence.ValueObjects.DataAccess.Entities;

namespace Varyence.ValueObjects.DataAccess.EF
{
    public sealed class AppDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly DbConnectionString _connectionString;

        public AppDbContext(ILoggerFactory loggerFactory, DbConnectionString connectionString) =>
            (_loggerFactory, _connectionString) =
            (loggerFactory, connectionString);

        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(_loggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlServer(_connectionString.Value,
                    builder => builder
                        .EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), default)
                        .MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}