using CryptoExchange.ResponseModels;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CryptoExchange.RequestModels
{
    public record WithdrawRequest(Guid userId,decimal amount,int currencyId) : BalanceOperationRequest(userId,currencyId,amount), IRequest<WithdrawResponse>
    {
        
    }
}
