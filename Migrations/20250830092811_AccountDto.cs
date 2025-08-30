using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensePlanner.Api.Migrations
{
    /// <inheritdoc />
    public partial class AccountDto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalExpense",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalIncome",
                table: "Accounts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalExpense",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TotalIncome",
                table: "Accounts");
        }
    }
}
