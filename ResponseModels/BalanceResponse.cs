using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoExchange.ResponseModels
{
    public class BalanceResponse
    {
       
            public int CurrencyId { get; set; }
            public decimal TotalAmount { get; set; }
        
    }
}
