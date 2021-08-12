using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectsControl.Migrations
{
    public partial class MyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Sector = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DateofHiring = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Salemans",
                columns: table => new
                {
                    SalemanId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salemans", x => x.SalemanId);
                });

            migrationBuilder.CreateTable(
                name: "Week",
                columns: table => new
                {
                    WeekId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumberOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeginOfWeek = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndOfWeek = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Week", x => x.WeekId);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    ContactId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactId);
                    table.ForeignKey(
                        name: "FK_Contact_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumberOfTask = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    OfferId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OCDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Manager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOver = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PendingAmount = table.Column<double>(type: "float", nullable: false),
                    TypeOfJob = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ubication = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SalemanId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Salemans_SalemanId",
                        column: x => x.SalemanId,
                        principalTable: "Salemans",
                        principalColumn: "SalemanId",
                        onDelete: ReferentialAction.Restrict);
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
                    WeekId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistances", x => x.AsistanceId);
                    table.ForeignKey(
                        name: "FK_Asistances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asistances_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Asistances_Week_WeekId",
                        column: x => x.WeekId,
                        principalTable: "Week",
                        principalColumn: "WeekId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    BillId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumberOfBill = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
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
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AsistanceId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    WeekId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraHours", x => x.ExtraHourId);
                    table.ForeignKey(
                        name: "FK_ExtraHours_Asistances_AsistanceId",
                        column: x => x.AsistanceId,
                        principalTable: "Asistances",
                        principalColumn: "AsistanceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtraHours_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtraHours_Week_WeekId",
                        column: x => x.WeekId,
                        principalTable: "Week",
                        principalColumn: "WeekId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Name", "Sector" },
                values: new object[] { "bee46401-0d98-42cb-86be-e8f3faa0820c", "SalemanTesting", "Private" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateofHiring", "Name", "Position" },
                values: new object[] { "f10759fb-fdb6-4191-abb6-b896ec0d3ba2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sample of Employee", "Sample" });

            migrationBuilder.InsertData(
                table: "Salemans",
                columns: new[] { "SalemanId", "Name" },
                values: new object[] { "51783fb6-1590-4093-b04b-70a6f29bfa97", "CustomerTesting" });

            migrationBuilder.InsertData(
                table: "Week",
                columns: new[] { "WeekId", "BeginOfWeek", "EndOfWeek", "NumberOfWeek" },
                values: new object[] { "a1e0620b-f0ad-4d41-82ee-9b3eaabd2418", new DateTime(2021, 8, 12, 12, 38, 36, 473, DateTimeKind.Local).AddTicks(8455), new DateTime(2021, 8, 12, 12, 38, 36, 475, DateTimeKind.Local).AddTicks(5515), "1-2011'" });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "ContactId", "CustomerId", "Email", "Name", "PhoneNumber", "Position" },
                values: new object[] { "b40a147a-91c7-4b57-a69d-97a136b5207a", "bee46401-0d98-42cb-86be-e8f3faa0820c", "Sample@sample.com", "Contact Sample", 88888888, "Manager sample" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "Amount", "BeginDate", "Currency", "CustomerId", "Details", "EndDate", "Estatus", "IsOver", "Manager", "Name", "NumberOfTask", "OC", "OCDate", "OfferId", "PendingAmount", "SalemanId", "TypeOfJob", "Ubication" },
                values: new object[] { "34b10602-9994-481b-961a-8ab506efaf32", 0f, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dolar", "bee46401-0d98-42cb-86be-e8f3faa0820c", "Details sample", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sample", false, "sample", "Project Sample", 1234, "1234Sample", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1234Sample", 0.0, "51783fb6-1590-4093-b04b-70a6f29bfa97", "installation", null });

            migrationBuilder.InsertData(
                table: "Asistances",
                columns: new[] { "AsistanceId", "DateOfBegin", "DateOfEnd", "EmployeeId", "ProjectId", "WeekId" },
                values: new object[] { "544339f0-8ff0-4dc1-b165-97509649648c", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "f10759fb-fdb6-4191-abb6-b896ec0d3ba2", "34b10602-9994-481b-961a-8ab506efaf32", "a1e0620b-f0ad-4d41-82ee-9b3eaabd2418" });

            migrationBuilder.InsertData(
                table: "ExtraHours",
                columns: new[] { "ExtraHourId", "AsistanceId", "BeginTime", "EmployeeId", "EndTime", "IsPaid", "Notes", "Reason", "TypeOfHour", "WeekId" },
                values: new object[] { "411a1f70-bfea-4ffb-a0bc-3bf8a1e320b8", "544339f0-8ff0-4dc1-b165-97509649648c", new DateTime(2021, 8, 12, 12, 38, 36, 476, DateTimeKind.Local).AddTicks(9079), "f10759fb-fdb6-4191-abb6-b896ec0d3ba2", new DateTime(2021, 8, 12, 12, 38, 36, 476, DateTimeKind.Local).AddTicks(9520), true, null, "SAMPLE", "double", "a1e0620b-f0ad-4d41-82ee-9b3eaabd2418" });

            migrationBuilder.CreateIndex(
                name: "IX_Asistances_EmployeeId",
                table: "Asistances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistances_ProjectId",
                table: "Asistances",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistances_WeekId",
                table: "Asistances",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_ProjectId",
                table: "Bill",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CustomerId",
                table: "Contact",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraHours_AsistanceId",
                table: "ExtraHours",
                column: "AsistanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraHours_EmployeeId",
                table: "ExtraHours",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraHours_WeekId",
                table: "ExtraHours",
                column: "WeekId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CustomerId",
                table: "Projects",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SalemanId",
                table: "Projects",
                column: "SalemanId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProjectId",
                table: "Report",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "ExtraHours");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Asistances");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Week");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Salemans");
        }
    }
}
