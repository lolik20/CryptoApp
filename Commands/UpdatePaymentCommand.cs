using Bybit.Net.Clients;
using Bybit.Net.Interfaces.Clients;
using CryptoExchange.Entities;
using CryptoExchange.Exceptions;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using Isopoh.Cryptography.Argon2;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Model;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace CryptoExchange.Commands
{
    public class UpdatePaymentCommand : IRequestHandler<UpdatePaymentRequest, UpdatePaymentResponse>
    {
        private readonly ApplicationContext _context;
        private Web3 _web3;
        private Nethereum.Web3.Accounts.Account _account;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public UpdatePaymentCommand(ApplicationContext context, IConfiguration configuration, ILogger<UpdatePaymentCommand> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<UpdatePaymentResponse> Handle(UpdatePaymentRequest request, CancellationToken cancellationToken)
        {

            using (var transaction = await _context.Database.BeginTransactionAsync(isolationLevel: System.Data.IsolationLevel.Serializable, cancellationToken))
            {
                try
                {
                    var payment = await _context.Payments.FirstOrDefaultAsync(x => x.Id == request.Id);
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
                    var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
                    var privateKey = ecKey.GetPrivateKeyAsBytes().ToHex();
                    _account = new Nethereum.Web3.Accounts.Account(privateKey);
                    _web3 = new Web3(currencyNetwork!.Network!.Url);
                    var paymentData = new PaymentData
                    {
                        WalletAddress = _account.Address.ToLower(),
                        PrivateKey = privateKey,
                        CurrencyId = request.CurrencyId,
                        NetworkId = request.NetworkId
                    };

                    decimal comission = 1.015m;
                    switch (payment.Currency!.Type)
                    {
                        case CurrencyType.Fiat:
                            paymentData.ToAmount = payment.Amount * comission;

                            break;
                        case CurrencyType.Stable:
                            paymentData.ToAmount = payment.Amount * comission;
                            break;
                    }
                    payment.PaymentStatus = PaymentStatus.InProgress;
                    _context.PaymentsData.Update(paymentData);

                    await _context.SaveChangesAsync();
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
