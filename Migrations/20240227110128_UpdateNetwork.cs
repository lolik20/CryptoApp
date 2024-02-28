using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoExchange.Migrations
{
    public partial class UpdateNetwork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChainId",
                table: "Networks",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ExplorerUrl",
                table: "Networks",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChainId",
                table: "Networks");

            migrationBuilder.DropColumn(
                name: "ExplorerUrl",
                table: "Networks");
        }
    }
}
