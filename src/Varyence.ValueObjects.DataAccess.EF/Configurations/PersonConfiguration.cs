using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Varyence.ValueObjects.DataAccess.EF.Metadata;
using Varyence.ValueObjects.DataAccess.Entities;

namespace Varyence.ValueObjects.DataAccess.EF.Configurations
{
    public sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder
                .ToTable(Tables.Person, Schemas.Dbo)
                .HasKey(p => p.Id);

            builder
                .Property(p => p.GithubAccountUri)
                .HasConversion(p => p.ToString(), str => new Uri(str));
            
            builder.OwnsOne(p => p.Age, x =>
            {
                x.Property(pp => pp.Value)
                    .HasColumnName("Age")
                    .HasColumnType("int")
                    .IsRequired();
            });

            builder.OwnsOne(p => p.Name, x =>
            {
                x.OwnsOne(pp => pp.FirstName, xx =>
                {
                    xx.Property(p => p.Value)
                        .HasColumnName("FirstName")
                        .HasColumnType("nvarchar(100)")
                        .IsRequired();
                });
                
                x.OwnsOne(pp => pp.LastName, xx =>
                {
                    xx.Property(p => p.Value)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("LastName")
                        .IsRequired();
                });

                x.Property<int>("NameSuffixId").HasColumnName("NameSuffixId");
                x.HasOne(pp => pp.Suffix).WithMany().HasForeignKey("NameSuffixId");
            });
        }
    }
}