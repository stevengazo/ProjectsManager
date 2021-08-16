﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectsControl.Models;

namespace ProjectsControl.Migrations
{
    [DbContext(typeof(DBProjectContext))]
    [Migration("20210816135138_mymigration")]
    partial class mymigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectsControl.Models.Asistance", b =>
                {
                    b.Property<string>("AsistanceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateOfBegin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfEnd")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProjectId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("WeekId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AsistanceId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("WeekId");

                    b.ToTable("Asistances");

                    b.HasData(
                        new
                        {
                            AsistanceId = "8ead276b-3331-40e3-b3e5-04ed2dd33978",
                            DateOfBegin = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateOfEnd = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = "9dbb5ec0-cc28-43fe-b298-d1e45595c78f",
                            ProjectId = "9a17fa75-4c12-48d3-b16b-8bc8d29fb27f",
                            WeekId = "31d45921-e610-437d-93e3-ca328705225e"
                        });
                });

            modelBuilder.Entity("ProjectsControl.Models.Bill", b =>
                {
                    b.Property<string>("BillId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberOfBill")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("BillId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Bill");
                });

            modelBuilder.Entity("ProjectsControl.Models.Contact", b =>
                {
                    b.Property<string>("ContactId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ContactId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Contact");

                    b.HasData(
                        new
                        {
                            ContactId = "b021d9f8-3cde-4036-b54c-f22806f19474",
                            CustomerId = "5c0dc7b9-6fe3-4395-99ef-94aa0e6d5029",
                            Email = "Sample@sample.com",
                            Name = "Contact Sample",
                            PhoneNumber = 88888888,
                            Position = "Manager sample"
                        });
                });

            modelBuilder.Entity("ProjectsControl.Models.Customer", b =>
                {
                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Sector")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = "5c0dc7b9-6fe3-4395-99ef-94aa0e6d5029",
                            Name = "SalemanTesting",
                            Sector = "Private"
                        });
                });

            modelBuilder.Entity("ProjectsControl.Models.Employee", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateofHiring")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = "9dbb5ec0-cc28-43fe-b298-d1e45595c78f",
                            DateofHiring = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Sample of Employee",
                            Position = "Sample"
                        });
                });

            modelBuilder.Entity("ProjectsControl.Models.ExtraHour", b =>
                {
                    b.Property<string>("ExtraHourId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AsistanceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BeginTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOfHour")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WeekId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ExtraHourId");

                    b.HasIndex("AsistanceId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("WeekId");

                    b.ToTable("ExtraHours");

                    b.HasData(
                        new
                        {
                            ExtraHourId = "37f35666-213c-45d8-9374-9748ce60bd41",
                            AsistanceId = "8ead276b-3331-40e3-b3e5-04ed2dd33978",
                            BeginTime = new DateTime(2021, 8, 16, 7, 51, 37, 744, DateTimeKind.Local).AddTicks(2638),
                            EmployeeId = "9dbb5ec0-cc28-43fe-b298-d1e45595c78f",
                            EndTime = new DateTime(2021, 8, 16, 7, 51, 37, 744, DateTimeKind.Local).AddTicks(3109),
                            IsPaid = true,
                            Reason = "SAMPLE",
                            TypeOfHour = "double",
                            WeekId = "31d45921-e610-437d-93e3-ca328705225e"
                        });
                });

            modelBuilder.Entity("ProjectsControl.Models.Project", b =>
                {
                    b.Property<string>("ProjectId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Estatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOver")
                        .HasColumnType("bit");

                    b.Property<string>("Manager")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfTask")
                        .HasColumnType("int");

                    b.Property<string>("OC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OCDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OfferId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PendingAmount")
                        .HasColumnType("float");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("SalemanId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TypeOfJob")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ubication")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("SalemanId");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            ProjectId = "9a17fa75-4c12-48d3-b16b-8bc8d29fb27f",
                            Amount = 0f,
                            BeginDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Currency = "dolar",
                            CustomerId = "5c0dc7b9-6fe3-4395-99ef-94aa0e6d5029",
                            Details = "Details sample",
                            EndDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Estatus = "sample",
                            IsOver = false,
                            Manager = "sample",
                            NumberOfTask = 1234,
                            OC = "1234Sample",
                            OCDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OfferId = "1234Sample",
                            PendingAmount = 0.0,
                            ProjectName = "Project Sample",
                            SalemanId = "f73ad4d0-d2b5-4943-9974-510370f0ea9d",
                            TypeOfJob = "installation"
                        });
                });

            modelBuilder.Entity("ProjectsControl.Models.Report", b =>
                {
                    b.Property<string>("ReportId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BeginDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfReport")
                        .HasColumnType("int");

                    b.Property<string>("ProjectId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReportId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("ProjectsControl.Models.Saleman", b =>
                {
                    b.Property<string>("SalemanId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("SalemanId");

                    b.ToTable("Salemans");

                    b.HasData(
                        new
                        {
                            SalemanId = "f73ad4d0-d2b5-4943-9974-510370f0ea9d",
                            Name = "CustomerTesting"
                        });
                });

            modelBuilder.Entity("ProjectsControl.Models.Week", b =>
                {
                    b.Property<string>("WeekId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BeginOfWeek")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndOfWeek")
                        .HasColumnType("datetime2");

                    b.Property<string>("NumberOfWeek")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WeekId");

                    b.ToTable("Week");

                    b.HasData(
                        new
                        {
                            WeekId = "31d45921-e610-437d-93e3-ca328705225e",
                            BeginOfWeek = new DateTime(2021, 8, 16, 7, 51, 37, 740, DateTimeKind.Local).AddTicks(6714),
                            EndOfWeek = new DateTime(2021, 8, 16, 7, 51, 37, 742, DateTimeKind.Local).AddTicks(8045),
                            NumberOfWeek = "1-2011'"
                        });
                });

            modelBuilder.Entity("ProjectsControl.Models.Asistance", b =>
                {
                    b.HasOne("ProjectsControl.Models.Employee", "Employee")
                        .WithMany("Asistances")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("ProjectsControl.Models.Project", "Project")
                        .WithMany("Asistances")
                        .HasForeignKey("ProjectId");

                    b.HasOne("ProjectsControl.Models.Week", "Week")
                        .WithMany("Asistances")
                        .HasForeignKey("WeekId");

                    b.Navigation("Employee");

                    b.Navigation("Project");

                    b.Navigation("Week");
                });

            modelBuilder.Entity("ProjectsControl.Models.Bill", b =>
                {
                    b.HasOne("ProjectsControl.Models.Project", "Project")
                        .WithMany("Bills")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectsControl.Models.Contact", b =>
                {
                    b.HasOne("ProjectsControl.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ProjectsControl.Models.ExtraHour", b =>
                {
                    b.HasOne("ProjectsControl.Models.Asistance", "Asistance")
                        .WithMany("ExtraHours")
                        .HasForeignKey("AsistanceId");

                    b.HasOne("ProjectsControl.Models.Employee", "Employee")
                        .WithMany("ExtraHours")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("ProjectsControl.Models.Week", "Week")
                        .WithMany("ExtraHours")
                        .HasForeignKey("WeekId");

                    b.Navigation("Asistance");

                    b.Navigation("Employee");

                    b.Navigation("Week");
                });

            modelBuilder.Entity("ProjectsControl.Models.Project", b =>
                {
                    b.HasOne("ProjectsControl.Models.Customer", "Customer")
                        .WithMany("Projects")
                        .HasForeignKey("CustomerId");

                    b.HasOne("ProjectsControl.Models.Saleman", "Saleman")
                        .WithMany("Projects")
                        .HasForeignKey("SalemanId");

                    b.Navigation("Customer");

                    b.Navigation("Saleman");
                });

            modelBuilder.Entity("ProjectsControl.Models.Report", b =>
                {
                    b.HasOne("ProjectsControl.Models.Project", "Project")
                        .WithMany("Reports")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectsControl.Models.Asistance", b =>
                {
                    b.Navigation("ExtraHours");
                });

            modelBuilder.Entity("ProjectsControl.Models.Customer", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("ProjectsControl.Models.Employee", b =>
                {
                    b.Navigation("Asistances");

                    b.Navigation("ExtraHours");
                });

            modelBuilder.Entity("ProjectsControl.Models.Project", b =>
                {
                    b.Navigation("Asistances");

                    b.Navigation("Bills");

                    b.Navigation("Reports");
                });

            modelBuilder.Entity("ProjectsControl.Models.Saleman", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("ProjectsControl.Models.Week", b =>
                {
                    b.Navigation("Asistances");

                    b.Navigation("ExtraHours");
                });
#pragma warning restore 612, 618
        }
    }
}