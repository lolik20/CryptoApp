using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoExchange.Migrations
{
    public partial class getBalanceProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string command = @"CREATE OR REPLACE PROCEDURE getbalance(IN merchantId uuid)
AS $$
BEGIN
    
    SELECT
        pd.""CurrencyId"",
        SUM(pd.""ToAmount"") AS ""TotalAmount""
    FROM
        public.""Payments"" AS p
    JOIN
        public.""PaymentsData"" AS pd ON p.""Id"" = pd.""PaymentId""
    WHERE
        p.""PaymentStatus"" = 2
        AND p.""MerchantId"" = merchantId 
    GROUP BY
        pd.""CurrencyId"",
        p.""MerchantId"";
END;
$$ LANGUAGE plpgsql;
";
            migrationBuilder.Sql(command);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string command = "DROP PROCEDURE getbalance";
            migrationBuilder.Sql(command);
        }
    }
}
