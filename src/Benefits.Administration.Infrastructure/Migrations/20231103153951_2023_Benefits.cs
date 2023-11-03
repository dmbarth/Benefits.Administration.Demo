using Microsoft.EntityFrameworkCore.Migrations;

namespace Benefits.Administration.Infrastructure.Migrations
{
  public partial class _2023_Benefits : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql(@"
        INSERT INTO Benefits
        VALUES
          (2021, 26, 1000, 500),
          (2022, 26, 1000, 500),
          (2023, 26, 1000, 500),
          (2024, 26, 1000, 500)
      ");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql(@"
        DELETE FROM Benefits
        WHERE Year > 2020
          AND Year < 2025
      ");
    }
  }
}
