using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoExchange.Migrations
{
    public partial class UpdatePayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Currencies_CurrencyId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Networks_NetworkId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_NetworkId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "NetworkId",
                table: "Payments");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "Payments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Currencies_CurrencyId",
                table: "Payments",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Currencies_CurrencyId",
                table: "Payments");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "Payments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "NetworkId",
                table: "Payments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_NetworkId",
                table: "Payments",
                column: "NetworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Currencies_CurrencyId",
                table: "Payments",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Networks_NetworkId",
                table: "Payments",
                column: "NetworkId",
                principalTable: "Networks",
                principalColumn: "Id");
        }
    }
}
