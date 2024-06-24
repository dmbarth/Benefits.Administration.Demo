using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Benefits.Administration.Infrastructure.Migrations
{
  /// <inheritdoc />
  public partial class AddBenefitDiscountJoinTable : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
        name: "FK_Discounts_Benefits_BenefitId",
        table: "Discounts");

      migrationBuilder.DropIndex(
        name: "IX_Discounts_BenefitId",
        table: "Discounts");

      migrationBuilder.DropColumn(
        name: "BenefitId",
        table: "Discounts");

      migrationBuilder.DropColumn(
        name: "IsActive",
        table: "Discounts");

      migrationBuilder.CreateTable(
        name: "BenefitDiscount",
        columns: table => new
        {
          Id = table.Column<long>(type: "bigint", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
          IsActive = table.Column<bool>(type: "bit", nullable: false),
          BenefitId = table.Column<long>(type: "bigint", nullable: false),
          DiscountId = table.Column<long>(type: "bigint", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_BenefitDiscount", x => x.Id);
          table.ForeignKey(
            name: "FK_BenefitDiscount_Benefits_BenefitId",
            column: x => x.BenefitId,
            principalTable: "Benefits",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
            name: "FK_BenefitDiscount_Discounts_DiscountId",
            column: x => x.DiscountId,
            principalTable: "Discounts",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
        });

      migrationBuilder.CreateIndex(
        name: "IX_BenefitDiscount_BenefitId",
        table: "BenefitDiscount",
        column: "BenefitId");

      migrationBuilder.CreateIndex(
        name: "IX_BenefitDiscount_DiscountId",
        table: "BenefitDiscount",
        column: "DiscountId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
        name: "BenefitDiscount");

      migrationBuilder.AddColumn<long>(
        name: "BenefitId",
        table: "Discounts",
        type: "bigint",
        nullable: false,
        defaultValue: 0L);

      migrationBuilder.AddColumn<bool>(
        name: "IsActive",
        table: "Discounts",
        type: "bit",
        nullable: false,
        defaultValue: false);

      migrationBuilder.UpdateData(
        table: "Discounts",
        keyColumn: "Id",
        keyValue: 1L,
        columns: new[] { "BenefitId", "IsActive" },
        values: new object[] { 1L, true });

      migrationBuilder.CreateIndex(
        name: "IX_Discounts_BenefitId",
        table: "Discounts",
        column: "BenefitId");

      migrationBuilder.AddForeignKey(
        name: "FK_Discounts_Benefits_BenefitId",
        table: "Discounts",
        column: "BenefitId",
        principalTable: "Benefits",
        principalColumn: "Id");
    }
  }
}
