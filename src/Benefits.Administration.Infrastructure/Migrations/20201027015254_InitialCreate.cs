using Microsoft.EntityFrameworkCore.Migrations;

namespace Benefits.Administration.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Benefits",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(maxLength: 4, nullable: false),
                    PayPeriods = table.Column<int>(maxLength: 2, nullable: false),
                    EmployeeCost = table.Column<double>(nullable: false),
                    DependentCost = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benefits", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Income = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    BenefitID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Discounts_Benefits_BenefitID",
                        column: x => x.BenefitID,
                        principalTable: "Benefits",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Dependents",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    EmployeeID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependents", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Dependents_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Benefits",
                columns: new[] { "ID", "DependentCost", "EmployeeCost", "PayPeriods", "Year" },
                values: new object[] { 1L, 500.0, 1000.0, 26, 2020 });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "FirstName", "Income", "LastName", "MiddleName" },
                values: new object[,]
                {
                    { 1L, "Anakin", 52000.0, "Skywalker", null },
                    { 2L, "Sarah", 52000.0, "Connor", "J" },
                    { 3L, "Bruce", 52000.0, "Wayne", "Thomas" }
                });

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "ID", "EmployeeID", "FirstName", "LastName", "MiddleName", "Type" },
                values: new object[,]
                {
                    { 1L, 1L, "Padme", "Skywalker", null, 1 },
                    { 2L, 1L, "Luke", "Skywalker", null, 2 },
                    { 3L, 1L, "Leah", "Skywalker", null, 2 },
                    { 4L, 2L, "John", "Connor", null, 2 }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "ID", "Amount", "BenefitID", "IsActive", "Type" },
                values: new object[] { 1L, 0.10000000000000001, 1L, true, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Dependents_EmployeeID",
                table: "Dependents",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_BenefitID",
                table: "Discounts",
                column: "BenefitID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dependents");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Benefits");
        }
    }
}
