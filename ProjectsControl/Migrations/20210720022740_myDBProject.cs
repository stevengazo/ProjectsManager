using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectsControl.Migrations
{
    public partial class myDBProject : Migration
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
                    IsOver = table.Column<bool>(type: "bit", nullable: false),
                    TypeOfJob = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                    AsistanceId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Name", "Sector" },
                values: new object[] { "b2abc4a6-e855-4221-b355-532ac1aa3b7c", "SalemanTesting", "Private" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateofHiring", "Name", "Position" },
                values: new object[] { "56d2c825-78ba-45fe-af15-036dc1f095fb", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sample of Employee", "Sample" });

            migrationBuilder.InsertData(
                table: "Salemans",
                columns: new[] { "SalemanId", "Name" },
                values: new object[] { "b3175ee2-85de-4e92-9554-50bb8c5de282", "CustomerTesting" });

            migrationBuilder.InsertData(
                table: "Asistances",
                columns: new[] { "AsistanceId", "DateOfBegin", "DateOfEnd", "EmployeeId", "ProjectId" },
                values: new object[] { "01761896-68e6-4e21-856d-7c98f782977b", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "56d2c825-78ba-45fe-af15-036dc1f095fb", null });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "BeginDate", "CustomerId", "Details", "EndDate", "IsOver", "Name", "NumberOfTask", "OC", "OCDate", "OfferId", "SalemanId", "TypeOfJob", "Ubication" },
                values: new object[] { "4a8e7daf-8b09-44e0-a4b6-111a5b618258", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b2abc4a6-e855-4221-b355-532ac1aa3b7c", "Details sample", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Project Sample", 1234, "1234Sample", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1234Sample", "b3175ee2-85de-4e92-9554-50bb8c5de282", "installation", null });

            migrationBuilder.InsertData(
                table: "ExtraHours",
                columns: new[] { "ExtraHourId", "AsistanceId", "BeginTime", "EmployeeId", "EndTime", "IsPaid", "Notes", "Reason", "TypeOfHour" },
                values: new object[] { "4f91bb7a-0730-41eb-8570-a490e801c594", "01761896-68e6-4e21-856d-7c98f782977b", new DateTime(2021, 7, 19, 20, 27, 39, 470, DateTimeKind.Local).AddTicks(9955), "56d2c825-78ba-45fe-af15-036dc1f095fb", new DateTime(2021, 7, 19, 20, 27, 39, 472, DateTimeKind.Local).AddTicks(5243), true, null, "SAMPLE", "double" });

            migrationBuilder.CreateIndex(
                name: "IX_Asistances_EmployeeId",
                table: "Asistances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistances_ProjectId",
                table: "Asistances",
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
                name: "IX_Projects_CustomerId",
                table: "Projects",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SalemanId",
                table: "Projects",
                column: "SalemanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtraHours");

            migrationBuilder.DropTable(
                name: "Asistances");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Salemans");
        }
    }
}
