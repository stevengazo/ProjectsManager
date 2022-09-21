using System;
using Microsoft.EntityFrameworkCore.Migrations;
using ProjectsControl.Models.StoredProcedures;

#nullable disable

namespace ProjectsControl.Migrations
{
    public partial class mymigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DNIOfCustomer = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Sector = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeDNI = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateofHiring = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfFired = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MobileNumber = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    OfferId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumberOfOffer = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaleManName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastEdition = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.OfferId);
                    table.ForeignKey(
                        name: "FK_Offers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    ActionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfAction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(340)", maxLength: 340, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.ActionId);
                    table.ForeignKey(
                        name: "FK_Actions_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumberOfProject = table.Column<int>(type: "int", nullable: false),
                    NumberOfTask = table.Column<int>(type: "int", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OCDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Manager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Technician = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOver = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PendingAmount = table.Column<double>(type: "float", nullable: false),
                    TypeOfJob = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ubication = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfOffer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                    table.ForeignKey(
                        name: "FK_Projects_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "Salary",
                columns: table => new
                {
                    SalaryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SalaryAmount = table.Column<double>(type: "float", nullable: false),
                    DayOfApplication = table.Column<DateTime>(type: "datetime2", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salary", x => x.SalaryId);
                    table.ForeignKey(
                        name: "FK_Salary_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.CreateTable(
                name: "Asistances",
                columns: table => new
                {
                    AsistanceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfBegin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NumberOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistances", x => x.AsistanceId);
                    table.ForeignKey(
                        name: "FK_Asistances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                    table.ForeignKey(
                        name: "FK_Asistances_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    BillId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumberOfBill = table.Column<int>(type: "int", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.BillId);
                    table.ForeignKey(
                        name: "FK_Bill_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "Expensives",
                columns: table => new
                {
                    ExpensiveId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModification = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expensives", x => x.ExpensiveId);
                    table.ForeignKey(
                        name: "FK_Expensives_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NotesId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    NoteDescription = table.Column<string>(type: "nvarchar(340)", maxLength: 340, nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NotesId);
                    table.ForeignKey(
                        name: "FK_Notes_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumberOfReport = table.Column<int>(type: "int", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Report_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateTable(
                name: "ExtraHours",
                columns: table => new
                {
                    ExtraHourId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BeginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeOfHour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    AceptedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AsistanceId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NumberOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraHours", x => x.ExtraHourId);
                    table.ForeignKey(
                        name: "FK_ExtraHours_Asistances_AsistanceId",
                        column: x => x.AsistanceId,
                        principalTable: "Asistances",
                        principalColumn: "AsistanceId");
                    table.ForeignKey(
                        name: "FK_ExtraHours_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId");
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "DNIOfCustomer", "Name", "Sector" },
                values: new object[] { "6e390172-fbf0-4e1e-a7bb-b2cd23a7f4b6", 110, "Sample", "Private" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateOfBirth", "DateOfFired", "DateofHiring", "Email", "EmployeeDNI", "IsActive", "MobileNumber", "Name", "Position", "Salary" },
                values: new object[] { "51d9992c-dd67-4eaf-916b-bec1dd0c219b", new DateTime(2022, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), "sample@grupomecsa.net", 1171292, true, 888, "Sample of name", "d", 0f });

            migrationBuilder.InsertData(
                table: "Actions",
                columns: new[] { "ActionId", "Author", "DateOfCreation", "Description", "EmployeeId", "IsActive", "Title", "TypeOfAction" },
                values: new object[] { "c853b3fd-ab96-40fa-86e8-24ab3c96a063", "Sample of author", new DateTime(2022, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), "sample of description", "51d9992c-dd67-4eaf-916b-bec1dd0c219b", true, "Sample of title", "sample of type" });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "OfferId", "Author", "CustomerId", "DateOfCreation", "Description", "LastEdition", "NumberOfOffer", "SaleManName", "Title", "Type" },
                values: new object[] { "f0f87a60-c1f8-4a56-bc18-6af58b43c5f3", "Sample of Author", "6e390172-fbf0-4e1e-a7bb-b2cd23a7f4b6", new DateTime(2022, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), "sample of description", new DateTime(2022, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), 1, "Sample of name", "Title Sample", "installation" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "Amount", "BeginDate", "Currency", "CustomerId", "Details", "EmployeeId", "EndDate", "Estatus", "IsOver", "Manager", "NumberOfOffer", "NumberOfProject", "NumberOfTask", "OC", "OCDate", "PendingAmount", "ProjectName", "Technician", "TypeOfJob", "Ubication" },
                values: new object[] { "fed6b256-4e1b-426f-97a1-ce8976e5d10b", 100f, new DateTime(2022, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), "Dolar", "6e390172-fbf0-4e1e-a7bb-b2cd23a7f4b6", "Sample of details", "51d9992c-dd67-4eaf-916b-bec1dd0c219b", new DateTime(2022, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), "In progress", false, "Sample of Name", "PS1", 1, 1, "Oc Id Sample", new DateTime(2022, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), 0.0, "Sample Of Project", "Sample", "Sample of Job", "San JoseCosta Rica" });

            migrationBuilder.InsertData(
                table: "Asistances",
                columns: new[] { "AsistanceId", "DateOfBegin", "DateOfEnd", "EmployeeId", "NumberOfWeek", "ProjectId" },
                values: new object[] { "ece3af32-30aa-48b3-912b-e84d39a6149a", new DateTime(2022, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), "51d9992c-dd67-4eaf-916b-bec1dd0c219b", "01", "fed6b256-4e1b-426f-97a1-ce8976e5d10b" });

            migrationBuilder.InsertData(
                table: "Bill",
                columns: new[] { "BillId", "Amount", "Author", "Currency", "DateOfCreation", "Notes", "NumberOfBill", "ProjectId" },
                values: new object[] { "96e460ff-e949-47f8-a2ff-d2a7732757d1", 1f, "Sample", "Dolar", new DateTime(2022, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), "Sample of notes", 1, "fed6b256-4e1b-426f-97a1-ce8976e5d10b" });

            migrationBuilder.InsertData(
                table: "Expensives",
                columns: new[] { "ExpensiveId", "Amount", "Author", "Currency", "LastModification", "Note", "ProjectId", "Type" },
                values: new object[] { "0dd145f1-8a55-495e-9452-69b648318644", 1.12f, "Sample Of authot", "Dolar", new DateTime(2022, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), "Sample", "fed6b256-4e1b-426f-97a1-ce8976e5d10b", "Km Cost" });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "NotesId", "Author", "DateOfCreation", "NoteDescription", "ProjectId", "Title" },
                values: new object[] { "d2631946-81f8-4b25-ac17-43bd916a295b", "Sample", new DateTime(2022, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), "Description of the action", "fed6b256-4e1b-426f-97a1-ce8976e5d10b", "Sample" });

            migrationBuilder.InsertData(
                table: "Report",
                columns: new[] { "ReportId", "Author", "BeginDate", "EndDate", "Notes", "NumberOfReport", "ProjectId", "Status" },
                values: new object[] { "8431a55c-0413-489b-a505-3b28259c12d0", "Sample of author", new DateTime(2022, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2022, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), "sample of notes", 1, "fed6b256-4e1b-426f-97a1-ce8976e5d10b", "sample of estatus" });

            migrationBuilder.InsertData(
                table: "ExtraHours",
                columns: new[] { "ExtraHourId", "AceptedBy", "AsistanceId", "BeginTime", "EmployeeId", "EndTime", "IsPaid", "Notes", "NumberOfWeek", "Reason", "TypeOfHour" },
                values: new object[] { "ba96ff28-8492-4989-85e1-a49319e4fce9", "Nyree", "ece3af32-30aa-48b3-912b-e84d39a6149a", new DateTime(2022, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), "51d9992c-dd67-4eaf-916b-bec1dd0c219b", new DateTime(2022, 9, 19, 0, 0, 0, 0, DateTimeKind.Local), false, "as", "01", "ad", "double" });

            migrationBuilder.CreateIndex(
                name: "IX_Actions_EmployeeId",
                table: "Actions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistances_EmployeeId",
                table: "Asistances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistances_ProjectId",
                table: "Asistances",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_ProjectId",
                table: "Bill",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Expensives_ProjectId",
                table: "Expensives",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraHours_AsistanceId",
                table: "ExtraHours",
                column: "AsistanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraHours_EmployeeId",
                table: "ExtraHours",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ProjectId",
                table: "Notes",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CustomerId",
                table: "Offers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CustomerId",
                table: "Projects",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_EmployeeId",
                table: "Projects",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectId",
                table: "Report",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Salary_EmployeeId",
                table: "Salary",
                column: "EmployeeId");
            SP_Project.BuildStoredProcedures(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "Expensives");

            migrationBuilder.DropTable(
                name: "ExtraHours");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Salary");

            migrationBuilder.DropTable(
                name: "Asistances");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
