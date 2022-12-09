﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectsControl.Models;

#nullable disable

namespace ProjectsControl.Migrations
{
    [DbContext(typeof(DBProjectContext))]
    [Migration("20221209195642_projectdbMigration")]
    partial class projectdbMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ProjectsControl.Models.Action", b =>
                {
                    b.Property<string>("ActionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(340)
                        .HasColumnType("nvarchar(340)");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOfAction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActionId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Actions");

                    b.HasData(
                        new
                        {
                            ActionId = "c305e6c9-972f-4551-9d95-60663c1d5006",
                            Author = "Sample of author",
                            DateOfCreation = new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "sample of description",
                            EmployeeId = "f677dafe-2a72-4b6c-9976-277caaf2efff",
                            IsActive = true,
                            Title = "Sample of title",
                            TypeOfAction = "sample of type"
                        });
                });

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

                    b.Property<string>("NumberOfWeek")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AsistanceId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Asistances");

                    b.HasData(
                        new
                        {
                            AsistanceId = "5c4c26c5-1007-4060-963f-906f01c159b9",
                            DateOfBegin = new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            DateOfEnd = new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            EmployeeId = "f677dafe-2a72-4b6c-9976-277caaf2efff",
                            NumberOfWeek = "01",
                            ProjectId = "cab19fb2-a49b-47e7-8a5a-1168ee3450cd"
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

                    b.Property<int>("NumberOfBill")
                        .HasColumnType("int");

                    b.Property<string>("ProjectId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("BillId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Bill");

                    b.HasData(
                        new
                        {
                            BillId = "f7794f38-9206-43a0-8b5e-cad5ca3918e7",
                            Amount = 1f,
                            Author = "Sample",
                            Currency = "Dolar",
                            DateOfCreation = new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Notes = "Sample of notes",
                            NumberOfBill = 1,
                            ProjectId = "cab19fb2-a49b-47e7-8a5a-1168ee3450cd"
                        });
                });

            modelBuilder.Entity("ProjectsControl.Models.Customer", b =>
                {
                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("DNIOfCustomer")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Sector")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = "9458082e-df30-4edd-bf45-78d758ad1b43",
                            DNIOfCustomer = 110,
                            Name = "Sample",
                            Sector = "Private"
                        });
                });

            modelBuilder.Entity("ProjectsControl.Models.Employee", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfFired")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateofHiring")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeDNI")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("MobileNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Salary")
                        .HasColumnType("real");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = "f677dafe-2a72-4b6c-9976-277caaf2efff",
                            DateOfBirth = new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            DateOfFired = new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            DateofHiring = new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Email = "sample@grupomecsa.net",
                            EmployeeDNI = 1171292,
                            IsActive = true,
                            MobileNumber = 888,
                            Name = "Sample of name",
                            Position = "d",
                            Salary = 0f
                        });
                });

            modelBuilder.Entity("ProjectsControl.Models.Expensive", b =>
                {
                    b.Property<string>("ExpensiveId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModification")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExpensiveId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Expensives");

                    b.HasData(
                        new
                        {
                            ExpensiveId = "70d2d552-b460-4801-ac7f-d628651f2964",
                            Amount = 1.12f,
                            Author = "Sample Of authot",
                            Currency = "Dolar",
                            LastModification = new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Note = "Sample",
                            ProjectId = "cab19fb2-a49b-47e7-8a5a-1168ee3450cd",
                            Type = "Km Cost"
                        });
                });

            modelBuilder.Entity("ProjectsControl.Models.ExtraHour", b =>
                {
                    b.Property<string>("ExtraHourId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AceptedBy")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<string>("NumberOfWeek")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOfHour")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExtraHourId");

                    b.HasIndex("AsistanceId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("ExtraHours");

                    b.HasData(
                        new
                        {
                            ExtraHourId = "ac7d68e0-c829-417e-b890-cea7d1abe2d8",
                            AceptedBy = "Nyree",
                            AsistanceId = "5c4c26c5-1007-4060-963f-906f01c159b9",
                            BeginTime = new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            EmployeeId = "f677dafe-2a72-4b6c-9976-277caaf2efff",
                            EndTime = new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            IsPaid = false,
                            Notes = "as",
                            NumberOfWeek = "01",
                            Reason = "ad",
                            TypeOfHour = "double"
                        });
                });

            modelBuilder.Entity("ProjectsControl.Models.Notes", b =>
                {
                    b.Property<string>("NotesId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoteDescription")
                        .IsRequired()
                        .HasMaxLength(340)
                        .HasColumnType("nvarchar(340)");

                    b.Property<string>("ProjectId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("NotesId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Notes");

                    b.HasData(
                        new
                        {
                            NotesId = "03f3c3a1-0566-4af7-8aba-4a257773895b",
                            Author = "Sample",
                            DateOfCreation = new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            NoteDescription = "Description of the action",
                            ProjectId = "cab19fb2-a49b-47e7-8a5a-1168ee3450cd",
                            Title = "Sample"
                        });
                });

            modelBuilder.Entity("ProjectsControl.Models.Offer", b =>
                {
                    b.Property<string>("OfferId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastEdition")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfOffer")
                        .HasColumnType("int");

                    b.Property<string>("SaleManName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TypeCurrency")
                        .HasMaxLength(10)
                        .HasColumnType("real");

                    b.HasKey("OfferId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Offers");

                    b.HasData(
                        new
                        {
                            OfferId = "2b10e1f4-ee65-41ac-999e-56bcf3033b6b",
                            Amount = 100.3f,
                            Author = "Sample of Author",
                            CustomerId = "9458082e-df30-4edd-bf45-78d758ad1b43",
                            DateOfCreation = new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Description = "sample of description",
                            LastEdition = new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            NumberOfOffer = 1,
                            SaleManName = "Sample of name",
                            Title = "Title Sample",
                            Type = "installation",
                            TypeCurrency = 0f
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

                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Estatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOver")
                        .HasColumnType("bit");

                    b.Property<string>("Manager")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberOfOffer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfProject")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfTask")
                        .HasColumnType("int");

                    b.Property<string>("OC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OCDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OfferId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("PendingAmount")
                        .HasColumnType("float");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Technician")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOfJob")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ubication")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("OfferId");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            ProjectId = "cab19fb2-a49b-47e7-8a5a-1168ee3450cd",
                            Amount = 100f,
                            BeginDate = new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Currency = "Dolar",
                            CustomerId = "9458082e-df30-4edd-bf45-78d758ad1b43",
                            Details = "Sample of details",
                            EmployeeId = "f677dafe-2a72-4b6c-9976-277caaf2efff",
                            EndDate = new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Estatus = "In progress",
                            IsOver = false,
                            Manager = "Sample of Name",
                            NumberOfOffer = "PS1",
                            NumberOfProject = 1,
                            NumberOfTask = 1,
                            OC = "Oc Id Sample",
                            OCDate = new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            OfferId = "2b10e1f4-ee65-41ac-999e-56bcf3033b6b",
                            PendingAmount = 0.0,
                            ProjectName = "Sample Of Project",
                            Technician = "Sample",
                            TypeOfJob = "Sample of Job",
                            Ubication = "San JoseCosta Rica"
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

                    b.HasData(
                        new
                        {
                            ReportId = "e858869a-6bc8-49f0-b359-97e51cff7c36",
                            Author = "Sample of author",
                            BeginDate = new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            EndDate = new DateTime(2022, 12, 9, 0, 0, 0, 0, DateTimeKind.Local),
                            Notes = "sample of notes",
                            NumberOfReport = 1,
                            ProjectId = "cab19fb2-a49b-47e7-8a5a-1168ee3450cd",
                            Status = "sample of estatus"
                        });
                });

            modelBuilder.Entity("ProjectsControl.Models.Salary", b =>
                {
                    b.Property<string>("SalaryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DayOfApplication")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("SalaryAmount")
                        .HasColumnType("float");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<string>("notes")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SalaryId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Salary");
                });

            modelBuilder.Entity("ProjectsControl.Models.Action", b =>
                {
                    b.HasOne("ProjectsControl.Models.Employee", "Employee")
                        .WithMany("Actions")
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("ProjectsControl.Models.Asistance", b =>
                {
                    b.HasOne("ProjectsControl.Models.Employee", "Employee")
                        .WithMany("Asistances")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("ProjectsControl.Models.Project", "Project")
                        .WithMany("Asistances")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Employee");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectsControl.Models.Bill", b =>
                {
                    b.HasOne("ProjectsControl.Models.Project", "Project")
                        .WithMany("Bills")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectsControl.Models.Expensive", b =>
                {
                    b.HasOne("ProjectsControl.Models.Project", "Project")
                        .WithMany("Expensives")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectsControl.Models.ExtraHour", b =>
                {
                    b.HasOne("ProjectsControl.Models.Asistance", "Asistance")
                        .WithMany("ExtraHours")
                        .HasForeignKey("AsistanceId");

                    b.HasOne("ProjectsControl.Models.Employee", "Employee")
                        .WithMany("ExtraHours")
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Asistance");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("ProjectsControl.Models.Notes", b =>
                {
                    b.HasOne("ProjectsControl.Models.Project", "Project")
                        .WithMany("Notes")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectsControl.Models.Offer", b =>
                {
                    b.HasOne("ProjectsControl.Models.Customer", "Customer")
                        .WithMany("Offers")
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ProjectsControl.Models.Project", b =>
                {
                    b.HasOne("ProjectsControl.Models.Customer", "Customer")
                        .WithMany("Projects")
                        .HasForeignKey("CustomerId");

                    b.HasOne("ProjectsControl.Models.Employee", "Employee")
                        .WithMany("Projects")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("ProjectsControl.Models.Offer", "Offer")
                        .WithMany("Projects")
                        .HasForeignKey("OfferId");

                    b.Navigation("Customer");

                    b.Navigation("Employee");

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("ProjectsControl.Models.Report", b =>
                {
                    b.HasOne("ProjectsControl.Models.Project", "Project")
                        .WithMany("Reports")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectsControl.Models.Salary", b =>
                {
                    b.HasOne("ProjectsControl.Models.Employee", "Employee")
                        .WithMany("Salaries")
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("ProjectsControl.Models.Asistance", b =>
                {
                    b.Navigation("ExtraHours");
                });

            modelBuilder.Entity("ProjectsControl.Models.Customer", b =>
                {
                    b.Navigation("Offers");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("ProjectsControl.Models.Employee", b =>
                {
                    b.Navigation("Actions");

                    b.Navigation("Asistances");

                    b.Navigation("ExtraHours");

                    b.Navigation("Projects");

                    b.Navigation("Salaries");
                });

            modelBuilder.Entity("ProjectsControl.Models.Offer", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("ProjectsControl.Models.Project", b =>
                {
                    b.Navigation("Asistances");

                    b.Navigation("Bills");

                    b.Navigation("Expensives");

                    b.Navigation("Notes");

                    b.Navigation("Reports");
                });
#pragma warning restore 612, 618
        }
    }
}