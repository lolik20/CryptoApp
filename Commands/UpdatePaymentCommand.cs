﻿using Bybit.Net.Clients;
using Bybit.Net.Interfaces.Clients;
using CryptoExchange.Entities;
using CryptoExchange.Exceptions;
using CryptoExchange.Interfaces;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using CryptoExchange.Services;
using Google.Type;
using Isopoh.Cryptography.Argon2;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Model;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using TradePack;
using TradePack.Interfaces;

namespace CryptoExchange.Commands
{
    public class UpdatePaymentCommand : IRequestHandler<UpdatePaymentRequest, UpdatePaymentResponse>
    {
        private readonly ApplicationContext _context;
        private readonly ILogger _logger;
        private readonly IEthService _ethService;
        private readonly IByBitService _byBitService;

        public UpdatePaymentCommand(ApplicationContext context, ILogger<UpdatePaymentCommand> logger, IEthService ethService, IByBitService byBitService)
        {
            _context = context;
            _logger = logger;
            _ethService = ethService;
            _byBitService = byBitService;
        }
        public async Task<UpdatePaymentResponse> Handle(UpdatePaymentRequest request, CancellationToken cancellationToken)
        {

            using (var transaction = await _context.Database.BeginTransactionAsync(isolationLevel: System.Data.IsolationLevel.Serializable, cancellationToken))
            {
                try
                {
                    var payment = await _context.Payments.Include(x=>x.Currency).ThenInclude(x=>x.Bank).FirstOrDefaultAsync(x => x.Id == request.Id);
                    if (payment == null)
                    {
                        throw new NotFoundException("Payment not found");
                    }
                    if (payment.PaymentStatus != PaymentStatus.Created)
                    {
                        throw new AlreadyExistException("Cannot switch currency and network");

                    }
                    var currencyNetwork = await _context.CurrencyNetworks.Include(x => x.Network).Include(x => x.Currency).FirstOrDefaultAsync(x => x.CurrencyId == request.CurrencyId && x.NetworkId == request.NetworkId);
                    if (currencyNetwork == null)
                    {
                        throw new NotFoundException("CurrencyNetwork not found");
                    }
                    var wallet = _ethService.CreateWallet(currencyNetwork!.Network!.Url);
                   
                    var paymentData = new PaymentData
                    {
                        WalletAddress = wallet.address,
                        PrivateKey = wallet.privateKey,
                        CurrencyId = request.CurrencyId,
                        NetworkId = request.NetworkId,
                        PaymentId = payment.Id,

                    };
                    decimal comission = 1.015m;
                    var fromCurrency = payment.Currency;
                    switch (fromCurrency!.Type)
                    {
                        case CurrencyType.Fiat:
                            var bank = fromCurrency.Bank;
                            var rateResult = await _byBitService.GetRate(payment.Amount, $"{bank!.ByBitId}", TradePack.Entities.OperationType.Sell,fromCurrency.Code.ToUpper());
                            if (rateResult.FirstOrDefault() == 0m)
                            {
                                throw new CalculatingException("Get rate error");
                            };
                            decimal rate = rateResult.First();
                            paymentData.ToAmount = (payment.Amount / rate) * comission;
                            break;
                        case CurrencyType.Stable:
                            paymentData.ToAmount = payment.Amount * comission;
                            break;
                    }

                    payment.PaymentStatus = PaymentStatus.InProgress;
                    await _context.PaymentsData.AddAsync(paymentData);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new UpdatePaymentResponse(true, "Updated");
                }
                catch (DbUpdateException ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error while update payment: {0}", request.Id);
                    throw new Exception("Update payment exception");
                }
            }

        }
    }
}
