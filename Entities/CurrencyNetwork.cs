using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoExchange.Entities
{
    public class CurrencyNetwork
    {
        [ForeignKey(nameof(Currency))]
        public int CurrencyId { get; set; }
        public Currency? Currency { get; set; }

        [ForeignKey(nameof(Network))]
        public int NetworkId { get; set; }
        public Network? Network { get; set; }
        public string? ContractAddress { get; set; }
    }
}
