using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoExchange.Migrations
{
    /// <inheritdoc />
    public partial class InsertData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO public.""Currencies""(
	""Id"", ""Type"", ""Code"", ""Name"")
	VALUES (1, 0, 'BTC', 'Bitcoin'),(2,2,'USD','US Dollar')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
