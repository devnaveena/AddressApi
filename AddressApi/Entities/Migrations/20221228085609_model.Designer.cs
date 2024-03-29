﻿// <auto-generated />
using System;
using AddressApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AddressApi.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20221228085609_model")]
    partial class model
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AddressApi.Address", b =>
                {
                    b.Property<Guid>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Line1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Line2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Type")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ZipCode")
                        .HasColumnType("int");

                    b.Property<Guid>("country")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("AddressApi.Email", b =>
                {
                    b.Property<Guid>("EmailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("Type")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EmailId");

                    b.HasIndex("UserId");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("AddressApi.FileModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("file")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("File");
                });

            modelBuilder.Entity("AddressApi.PhoneNumber", b =>
                {
                    b.Property<Guid>("phoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Type")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("phoneId");

                    b.HasIndex("UserId");

                    b.ToTable("PhoneNumber");
                });

            modelBuilder.Entity("AddressApi.RefSet", b =>
                {
                    b.Property<Guid>("RefSetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RefSetId");

                    b.ToTable("RefSet");

                    b.HasData(
                        new
                        {
                            RefSetId = new Guid("1398ff0d-2062-4594-23d4-08dac5f97924"),
                            Description = "For  personal",
                            Name = "PERSONAL"
                        },
                        new
                        {
                            RefSetId = new Guid("1398ff0d-2062-4594-23d4-08dac5f97923"),
                            Description = "For the work",
                            Name = "WORK"
                        },
                        new
                        {
                            RefSetId = new Guid("1398ff0d-2062-4594-23d4-08dac5f97922"),
                            Description = "For the alternate",
                            Name = "ALTERNATE"
                        },
                        new
                        {
                            RefSetId = new Guid("1398ff0d-2062-4594-23d4-08dac5f97914"),
                            Description = "For the country India",
                            Name = "INDIA"
                        },
                        new
                        {
                            RefSetId = new Guid("1298ff0d-2062-4594-23d4-08dac5f97924"),
                            Description = "For the country USA",
                            Name = "USA"
                        },
                        new
                        {
                            RefSetId = new Guid("1398ff0d-2062-4594-23d4-08dac5f97921"),
                            Description = "for the country UK",
                            Name = "UK"
                        },
                        new
                        {
                            RefSetId = new Guid("1398ff0d-2062-5594-23d4-08dac5f97924"),
                            Description = "for the country Japan",
                            Name = "JAPAN"
                        });
                });

            modelBuilder.Entity("AddressApi.Refterm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Refterm");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            key = "ADDRESS TYPE"
                        },
                        new
                        {
                            Id = 2,
                            key = "PHONE NUMBER TYPE"
                        },
                        new
                        {
                            Id = 3,
                            key = "EMAIL ADDRESS TYPE"
                        },
                        new
                        {
                            Id = 4,
                            key = "Country"
                        },
                        new
                        {
                            Id = 5,
                            key = "India"
                        },
                        new
                        {
                            Id = 6,
                            key = "UNITED STATES"
                        });
                });

            modelBuilder.Entity("AddressApi.SetRef", b =>
                {
                    b.Property<Guid>("RefId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RefSetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RefType")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RefId");

                    b.ToTable("Setref");
                });

            modelBuilder.Entity("AddressApi.Types", b =>
                {
                    b.Property<Guid>("RefsetType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RefsetType");

                    b.ToTable("Type");
                });

            modelBuilder.Entity("AddressApi.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1398ff0d-2062-4594-33d4-08dac5f97924"),
                            FirstName = "Naveena",
                            IsActive = true,
                            LastName = "T",
                            Password = "Navee@123",
                            UserName = "Naveena"
                        });
                });

            modelBuilder.Entity("AddressApi.Address", b =>
                {
                    b.HasOne("AddressApi.User", null)
                        .WithMany("Address")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AddressApi.Email", b =>
                {
                    b.HasOne("AddressApi.User", null)
                        .WithMany("Email")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AddressApi.FileModel", b =>
                {
                    b.HasOne("AddressApi.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AddressApi.PhoneNumber", b =>
                {
                    b.HasOne("AddressApi.User", null)
                        .WithMany("PhoneNumber")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AddressApi.User", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Email");

                    b.Navigation("PhoneNumber");
                });
#pragma warning restore 612, 618
        }
    }
}
