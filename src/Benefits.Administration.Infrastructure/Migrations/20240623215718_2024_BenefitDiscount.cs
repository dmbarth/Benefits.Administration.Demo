using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Benefits.Administration.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class _2024_BenefitDiscount : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql(@"
        INSERT INTO BenefitDiscount
        VALUES
          (1, 1, 1),
          (1, 5, 1)
      ");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql(@"
        DELETE FROM BenefitDiscount
        WHERE BenefitId = 1
          AND BenefitId = 5
      ");
    }
  }
}
