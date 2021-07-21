using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectsControl.Migrations
{
    public partial class DBProjectMigration : Migration
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
                    cost = table.Column<float>(type: "real", nullable: false),
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
                values: new object[] { "42fbf4fd-4877-4181-9362-8610119bcd9f", "SalemanTesting", "Private" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateofHiring", "Name", "Position" },
                values: new object[] { "8f0d9007-7f05-4c01-98d1-9d38b151fb8d", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sample of Employee", "Sample" });

            migrationBuilder.InsertData(
                table: "Salemans",
                columns: new[] { "SalemanId", "Name" },
                values: new object[] { "6ab592b9-0660-4982-b1c2-a7c6f5280888", "CustomerTesting" });

            migrationBuilder.InsertData(
                table: "Week",
                columns: new[] { "WeekId", "BeginOfWeek", "EndOfWeek", "NumberOfWeek" },
                values: new object[] { "39810adb-d340-4628-a7df-a6590a1b7739", new DateTime(2021, 7, 21, 8, 5, 3, 811, DateTimeKind.Local).AddTicks(5575), new DateTime(2021, 7, 21, 8, 5, 3, 813, DateTimeKind.Local).AddTicks(4637), "1-2011'" });

            migrationBuilder.InsertData(
                table: "Asistances",
                columns: new[] { "AsistanceId", "DateOfBegin", "DateOfEnd", "EmployeeId", "ProjectId", "WeekId" },
                values: new object[] { "b70dede3-8504-407b-aa09-e8551a686b72", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "8f0d9007-7f05-4c01-98d1-9d38b151fb8d", null, "39810adb-d340-4628-a7df-a6590a1b7739" });

            migrationBuilder.InsertData(
                table: "Contact",
                columns: new[] { "ContactId", "CustomerId", "Email", "Name", "PhoneNumber", "Position" },
                values: new object[] { "f2571f85-8758-4887-8956-4196c5d409fa", "42fbf4fd-4877-4181-9362-8610119bcd9f", "Sample@sample.com", "Contact Sample", 88888888, "Manager sample" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "ProjectId", "BeginDate", "CustomerId", "Details", "EndDate", "IsOver", "Name", "NumberOfTask", "OC", "OCDate", "OfferId", "SalemanId", "TypeOfJob", "Ubication" },
                values: new object[] { "70abe29b-c975-4917-945d-de3fc737944c", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "42fbf4fd-4877-4181-9362-8610119bcd9f", "Details sample", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Project Sample", 1234, "1234Sample", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1234Sample", "6ab592b9-0660-4982-b1c2-a7c6f5280888", "installation", null });

            migrationBuilder.InsertData(
                table: "ExtraHours",
                columns: new[] { "ExtraHourId", "AsistanceId", "BeginTime", "EmployeeId", "EndTime", "IsPaid", "Notes", "Reason", "TypeOfHour", "WeekId" },
                values: new object[] { "479727c2-6279-4fd8-9ed5-d391ff1ade11", "b70dede3-8504-407b-aa09-e8551a686b72", new DateTime(2021, 7, 21, 8, 5, 3, 814, DateTimeKind.Local).AddTicks(6249), "8f0d9007-7f05-4c01-98d1-9d38b151fb8d", new DateTime(2021, 7, 21, 8, 5, 3, 814, DateTimeKind.Local).AddTicks(6740), true, null, "SAMPLE", "double", "39810adb-d340-4628-a7df-a6590a1b7739" });

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
