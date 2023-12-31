﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LoncotesLibrary.Migrations
{
    [DbContext(typeof(LoncotesLibraryDbContext))]
    [Migration("20230919190324_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LoncotesLibrary.Models.Checkout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CheckoutDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("MaterialId")
                        .HasColumnType("integer");

                    b.Property<bool?>("Paid")
                        .HasColumnType("boolean");

                    b.Property<int>("PatronId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("PatronId");

                    b.ToTable("Checkouts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CheckoutDate = new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 1,
                            PatronId = 1,
                            ReturnDate = new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CheckoutDate = new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 2,
                            Paid = false,
                            PatronId = 1
                        },
                        new
                        {
                            Id = 3,
                            CheckoutDate = new DateTime(2023, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 3,
                            PatronId = 1
                        },
                        new
                        {
                            Id = 4,
                            CheckoutDate = new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 4,
                            Paid = true,
                            PatronId = 2,
                            ReturnDate = new DateTime(2023, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            CheckoutDate = new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MaterialId = 5,
                            Paid = false,
                            PatronId = 2,
                            ReturnDate = new DateTime(2023, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Fantasy"
                        },
                        new
                        {
                            Id = 2,
                            Name = "History"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Theology"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Poetry"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Period Piece"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Fiction"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Pop Punk"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Horror"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Textbook"
                        });
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GenreId")
                        .HasColumnType("integer");

                    b.Property<string>("MaterialName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaterialTypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("OutOfCirculationSince")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("MaterialTypeId");

                    b.ToTable("Materials");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GenreId = 6,
                            MaterialName = "Harry Potter and the Sorcerer's Stone",
                            MaterialTypeId = 1
                        },
                        new
                        {
                            Id = 2,
                            GenreId = 6,
                            MaterialName = "Harry Potter and the Chamber of Secrets",
                            MaterialTypeId = 1
                        },
                        new
                        {
                            Id = 3,
                            GenreId = 6,
                            MaterialName = "Harry Potter and the Prisoner of Azkaban",
                            MaterialTypeId = 1
                        },
                        new
                        {
                            Id = 4,
                            GenreId = 6,
                            MaterialName = "Harry Potter and the Goblet of Fire",
                            MaterialTypeId = 1
                        },
                        new
                        {
                            Id = 5,
                            GenreId = 6,
                            MaterialName = "Harry Potter and the Half-Blood Prince",
                            MaterialTypeId = 1
                        },
                        new
                        {
                            Id = 6,
                            GenreId = 3,
                            MaterialName = "Presbyterion, vol. 1, Spring 1986",
                            MaterialTypeId = 3,
                            OutOfCirculationSince = new DateTime(1986, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 7,
                            GenreId = 7,
                            MaterialName = "Commit This To Memory",
                            MaterialTypeId = 2,
                            OutOfCirculationSince = new DateTime(2006, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 8,
                            GenreId = 2,
                            MaterialName = "Hirohito's War",
                            MaterialTypeId = 1
                        },
                        new
                        {
                            Id = 9,
                            GenreId = 8,
                            MaterialName = "Peewee's Big Adventure",
                            MaterialTypeId = 4,
                            OutOfCirculationSince = new DateTime(2000, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 10,
                            GenreId = 9,
                            MaterialName = "Operating System Concepts, 8th ed",
                            MaterialTypeId = 1,
                            OutOfCirculationSince = new DateTime(2008, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("LoncotesLibrary.Models.MaterialType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CheckoutDays")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MaterialTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CheckoutDays = 14,
                            Name = "Book"
                        },
                        new
                        {
                            Id = 2,
                            CheckoutDays = 7,
                            Name = "CD"
                        },
                        new
                        {
                            Id = 3,
                            CheckoutDays = 14,
                            Name = "Academic Journal"
                        },
                        new
                        {
                            Id = 4,
                            CheckoutDays = 7,
                            Name = "DVD"
                        });
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Patron", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Patrons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "123 Street Rd",
                            Email = "john@john.com",
                            FirstName = "John",
                            IsActive = true,
                            LastName = "McJohn"
                        },
                        new
                        {
                            Id = 2,
                            Address = "123 Road St",
                            Email = "mark@mark.com",
                            FirstName = "Mark",
                            IsActive = true,
                            LastName = "McMark"
                        },
                        new
                        {
                            Id = 3,
                            Address = "321 Road St",
                            Email = "jake@jake.com",
                            FirstName = "Jake",
                            IsActive = false,
                            LastName = "McJake"
                        });
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Checkout", b =>
                {
                    b.HasOne("LoncotesLibrary.Models.Material", "Material")
                        .WithMany("Checkouts")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LoncotesLibrary.Models.Patron", "Patron")
                        .WithMany("Checkouts")
                        .HasForeignKey("PatronId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Patron");
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Material", b =>
                {
                    b.HasOne("LoncotesLibrary.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LoncotesLibrary.Models.MaterialType", "MaterialType")
                        .WithMany()
                        .HasForeignKey("MaterialTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("MaterialType");
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Material", b =>
                {
                    b.Navigation("Checkouts");
                });

            modelBuilder.Entity("LoncotesLibrary.Models.Patron", b =>
                {
                    b.Navigation("Checkouts");
                });
#pragma warning restore 612, 618
        }
    }
}
