﻿using CryptoExchange.Entities;
using CryptoExchange.Exceptions;
using CryptoExchange.Models;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using CryptoExchange.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Queries
{
    public class BalanceQuery : IRequestHandler<BalanceRequest, List<UserBalance>>
    {
        private readonly ApplicationContext _context;
        public BalanceQuery(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<List<UserBalance>> Handle(BalanceRequest request, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found by id");
            }

            return new List<UserBalance>();
        }
    }
}
