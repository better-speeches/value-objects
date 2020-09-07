﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Varyence.ValueObjects.DataAccess.EF;

namespace Varyence.ValueObjects.DataAccess.EF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Varyence.ValueObjects.DataAccess.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GithubAccountUri")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Person","Dbo");
                });

            modelBuilder.Entity("Varyence.ValueObjects.DataAccess.Entities.Person", b =>
                {
                    b.OwnsOne("Varyence.ValueObjects.DataAccess.ValueObjects.Age", "Age", b1 =>
                        {
                            b1.Property<int>("PersonId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("Value")
                                .HasColumnName("Age")
                                .HasColumnType("int");

                            b1.HasKey("PersonId");

                            b1.ToTable("Person");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.OwnsOne("Varyence.ValueObjects.DataAccess.ValueObjects.PersonName", "Name", b1 =>
                        {
                            b1.Property<int>("PersonId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.HasKey("PersonId");

                            b1.ToTable("Person");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");

                            b1.OwnsOne("Varyence.ValueObjects.DataAccess.ValueObjects.Name", "FirstName", b2 =>
                                {
                                    b2.Property<int>("PersonNamePersonId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int")
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnName("FirstName")
                                        .HasColumnType("nvarchar(100)");

                                    b2.HasKey("PersonNamePersonId");

                                    b2.ToTable("Person");

                                    b2.WithOwner()
                                        .HasForeignKey("PersonNamePersonId");
                                });

                            b1.OwnsOne("Varyence.ValueObjects.DataAccess.ValueObjects.Name", "LastName", b2 =>
                                {
                                    b2.Property<int>("PersonNamePersonId")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("int")
                                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                                    b2.Property<string>("Value")
                                        .IsRequired()
                                        .HasColumnName("LastName")
                                        .HasColumnType("nvarchar(100)");

                                    b2.HasKey("PersonNamePersonId");

                                    b2.ToTable("Person");

                                    b2.WithOwner()
                                        .HasForeignKey("PersonNamePersonId");
                                });
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
