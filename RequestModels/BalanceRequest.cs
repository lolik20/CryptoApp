using CryptoExchange.Entities;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.RequestModels
{
    public class BalanceRequest:IRequest<decimal>
    {
       
        public Guid UserId { get; set; }
        
    }
}
