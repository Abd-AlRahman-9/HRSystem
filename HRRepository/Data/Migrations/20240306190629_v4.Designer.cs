﻿// <auto-generated />
using System;
using HRRepository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HRRepository.Data.Migrations
{
    [DbContext(typeof(HRContext))]
    [Migration("20240306190629_v4")]
    partial class v4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HRDomain.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BonusHour")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("ComingTime")
                        .HasColumnType("time");

                    b.Property<int>("DeductHour")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("LeaveTime")
                        .HasColumnType("time");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<byte>("WorkDays")
                        .HasMaxLength(2)
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId")
                        .IsUnique()
                        .HasFilter("[ManagerId] IS NOT NULL");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("HRDomain.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int?>("DeptId")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<DateOnly>("HireData")
                        .HasColumnType("date");

                    b.Property<int?>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NationalID")
                        .IsRequired()
                        .HasColumnType("char(14)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PhoneNumber")
                        .HasMaxLength(25)
                        .HasColumnType("bigint");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<byte>("VacationsRecord")
                        .HasMaxLength(2)
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("DeptId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HRDomain.Entities.EmployeeAttendace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeOnly>("Attendance")
                        .HasColumnType("time");

                    b.Property<decimal>("Bonus")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("Leave")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeAttendaces");
                });

            modelBuilder.Entity("HRDomain.Entities.EmployeeVacation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId1")
                        .HasColumnType("int");

                    b.Property<int?>("VacationId")
                        .HasColumnType("int");

                    b.Property<int?>("VacationId1")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("EmployeeId1");

                    b.HasIndex("VacationId");

                    b.HasIndex("VacationId1");

                    b.ToTable("EmployeeVacations");
                });

            modelBuilder.Entity("HRDomain.Entities.Vacation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<bool>("Holiday")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Vacations");
                });

            modelBuilder.Entity("HRDomain.Entities.Department", b =>
                {
                    b.HasOne("HRDomain.Entities.Employee", "Manager")
                        .WithOne()
                        .HasForeignKey("HRDomain.Entities.Department", "ManagerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("HRDomain.Entities.Employee", b =>
                {
                    b.HasOne("HRDomain.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DeptId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("HRDomain.Entities.Employee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Department");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("HRDomain.Entities.EmployeeAttendace", b =>
                {
                    b.HasOne("HRDomain.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("HRDomain.Entities.EmployeeVacation", b =>
                {
                    b.HasOne("HRDomain.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("HRDomain.Entities.Employee", null)
                        .WithMany("EmployeeVacations")
                        .HasForeignKey("EmployeeId1");

                    b.HasOne("HRDomain.Entities.Vacation", "Vacation")
                        .WithMany()
                        .HasForeignKey("VacationId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("HRDomain.Entities.Vacation", null)
                        .WithMany("EmployeeVacations")
                        .HasForeignKey("VacationId1");

                    b.Navigation("Employee");

                    b.Navigation("Vacation");
                });

            modelBuilder.Entity("HRDomain.Entities.Employee", b =>
                {
                    b.Navigation("EmployeeVacations");
                });

            modelBuilder.Entity("HRDomain.Entities.Vacation", b =>
                {
                    b.Navigation("EmployeeVacations");
                });
#pragma warning restore 612, 618
        }
    }
}
