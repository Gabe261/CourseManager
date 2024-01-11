﻿// <auto-generated />
using System;
using CourseManager.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CourseManager.Migrations
{
    [DbContext(typeof(CourseManagerDbContext))]
    partial class CourseManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CourseManager.Entities.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("Instructor")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            CourseId = 1,
                            Instructor = "David",
                            Name = "See Sharp",
                            RoomNumber = "4G15",
                            StartDate = new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            CourseId = 2,
                            Instructor = "Ryan",
                            Name = "Sequel",
                            RoomNumber = "2C09",
                            StartDate = new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            CourseId = 3,
                            Instructor = "Owen",
                            Name = "GitHub",
                            RoomNumber = "1G15",
                            StartDate = new DateTime(2023, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            CourseId = 4,
                            Instructor = "Liam",
                            Name = "Web Dynamics",
                            RoomNumber = "4G18",
                            StartDate = new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            CourseId = 5,
                            Instructor = "Eddy",
                            Name = "Game Programming",
                            RoomNumber = "3B11",
                            StartDate = new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("CourseManager.Entities.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("StudentEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("StudentId");

                    b.HasIndex("CourseId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            StudentId = 1,
                            CourseId = 1,
                            Status = 0,
                            StudentEmail = "gabed.siewert@hotmail.com",
                            StudentName = "John"
                        },
                        new
                        {
                            StudentId = 2,
                            CourseId = 1,
                            Status = 0,
                            StudentEmail = "gsiewert2384@conestogac.on.ca",
                            StudentName = "Greg"
                        },
                        new
                        {
                            StudentId = 3,
                            CourseId = 2,
                            Status = 0,
                            StudentEmail = "gabesiewert@hotmail.com",
                            StudentName = "Simon"
                        },
                        new
                        {
                            StudentId = 4,
                            CourseId = 2,
                            Status = 0,
                            StudentEmail = "gabed.siewert@hotmail.com",
                            StudentName = "Thomas"
                        },
                        new
                        {
                            StudentId = 5,
                            CourseId = 3,
                            Status = 0,
                            StudentEmail = "gabed.siewert@hotmail.com",
                            StudentName = "Jason"
                        });
                });

            modelBuilder.Entity("CourseManager.Entities.Student", b =>
                {
                    b.HasOne("CourseManager.Entities.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("CourseManager.Entities.Course", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}