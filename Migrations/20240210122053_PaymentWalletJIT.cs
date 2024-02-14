using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoExchange.Migrations
{
    public partial class PaymentWalletJIT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyNetwork_Currencies_CurrencyId",
                table: "CurrencyNetwork");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyNetwork_Networks_NetworkId",
                table: "CurrencyNetwork");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrencyNetwork",
                table: "CurrencyNetwork");

            migrationBuilder.RenameTable(
                name: "CurrencyNetwork",
                newName: "CurrencyNetworks");

            migrationBuilder.RenameColumn(
                name: "ToAddress",
                table: "PaymentsData",
                newName: "WalletAddress");

            migrationBuilder.RenameColumn(
                name: "WalletAddress",
                table: "Networks",
                newName: "Url");

            migrationBuilder.RenameIndex(
                name: "IX_CurrencyNetwork_NetworkId",
                table: "CurrencyNetworks",
                newName: "IX_CurrencyNetworks_NetworkId");

            migrationBuilder.AddColumn<string>(
                name: "PrivateKey",
                table: "PaymentsData",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrencyNetworks",
                table: "CurrencyNetworks",
                columns: new[] { "CurrencyId", "NetworkId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyNetworks_Currencies_CurrencyId",
                table: "CurrencyNetworks",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyNetworks_Networks_NetworkId",
                table: "CurrencyNetworks",
                column: "NetworkId",
                principalTable: "Networks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyNetworks_Currencies_CurrencyId",
                table: "CurrencyNetworks");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyNetworks_Networks_NetworkId",
                table: "CurrencyNetworks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrencyNetworks",
                table: "CurrencyNetworks");

            migrationBuilder.DropColumn(
                name: "PrivateKey",
                table: "PaymentsData");

            migrationBuilder.RenameTable(
                name: "CurrencyNetworks",
                newName: "CurrencyNetwork");

            migrationBuilder.RenameColumn(
                name: "WalletAddress",
                table: "PaymentsData",
                newName: "ToAddress");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Networks",
                newName: "WalletAddress");

            migrationBuilder.RenameIndex(
                name: "IX_CurrencyNetworks_NetworkId",
                table: "CurrencyNetwork",
                newName: "IX_CurrencyNetwork_NetworkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrencyNetwork",
                table: "CurrencyNetwork",
                columns: new[] { "CurrencyId", "NetworkId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyNetwork_Currencies_CurrencyId",
                table: "CurrencyNetwork",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyNetwork_Networks_NetworkId",
                table: "CurrencyNetwork",
                column: "NetworkId",
                principalTable: "Networks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
