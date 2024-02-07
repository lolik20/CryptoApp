using CryptoExchange.ResponseModels;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.RequestModels
{
    public record TopUpRequest(Guid userId,decimal amount, int currencyId) : BalanceOperationRequest(userId,currencyId,amount), IRequest<TopUpResponse>;
  
}
