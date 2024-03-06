using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoExchange.Migrations
{
    public partial class ChainProtocol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChainProtocol",
                table: "Networks",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChainProtocol",
                table: "Networks");
        }
    }
}
