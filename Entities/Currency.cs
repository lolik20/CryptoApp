﻿using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public CurrencyType Type { get; set; }
        [Required]
        public  string Code { get; set; }
        [Required]
        public  string Name { get; set; }
        public List<UserBalance>? Balances { get; set; }
        public List<BalanceTransaction>? Transactions { get; set; }
    }
}
