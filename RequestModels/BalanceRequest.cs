﻿using CryptoExchange.Entities;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.RequestModels
{
    public class BalanceRequest:IRequest<List<decimal>>
    {
        [FromRoute]
        public Guid UserId { get; set; }
        
    }
}
