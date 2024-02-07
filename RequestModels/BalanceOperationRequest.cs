using CryptoExchange.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.RequestModels
{
    public record BalanceOperationRequest(
      [FromRoute]  Guid UserId,

        [Range(0, int.MaxValue)]
         int CurrencyId,

        [Range(0, double.MaxValue, ErrorMessage = $"Invalid amount")]
          decimal Amount);


}
