using Bybit.Net.Clients;
using CryptoExchange.Exceptions;
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
        private readonly BybitRestClient _bybitRestClient;
        private Nethereum.Web3.Accounts.Account _account;
        private readonly IConfiguration _configuration;
        public UpdatePaymentCommand(ApplicationContext context, IConfiguration configuration)
        {
            _context = context;
            _bybitRestClient = new BybitRestClient();
            _configuration = configuration;
        }
        public async Task<UpdatePaymentResponse> Handle(UpdatePaymentRequest request, CancellationToken cancellationToken)
        {
            var payment = _context.Payments.Include(x => x.PaymentData).FirstOrDefault(x => x.Id == request.Id);
            if (payment == null)
            {
                throw new NotFoundException("Payment not found");
            }
            if (payment.PaymentStatus != Entities.PaymentStatus.Created)
            {
                throw new AlreadyExistException("Cannot switch currency and network");

            }
            var currencyNetwork = _context.CurrencyNetworks.Include(x => x.Network).Include(x => x.Currency).FirstOrDefault(x => x.CurrencyId == request.CurrencyId && x.NetworkId == request.NetworkId);
            if (currencyNetwork == null)
            {
                throw new NotFoundException("CurrencyNetwork not found");
            }
            var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
            var privateKey = ecKey.GetPrivateKeyAsBytes().ToHex();
            _account = new Nethereum.Web3.Accounts.Account(privateKey);
            _web3 = new Web3(currencyNetwork!.Network!.Url);

            payment.PaymentData!.WalletAddress = _account.Address.ToLower();
            payment.PaymentData!.PrivateKey = privateKey;
            payment.PaymentData!.CurrencyId = request.CurrencyId;
            payment.PaymentData!.NetworkId = request.NetworkId;
            decimal comission = 1.015m;
            switch (payment.Currency!.Type)
            {
                case Entities.CurrencyType.Fiat:
                    //p2p exchange rate
                    payment.PaymentData!.ToAmount = payment.Amount *comission;

                    break;
                case Entities.CurrencyType.Stable:
                    payment.PaymentData!.ToAmount = payment.Amount *comission;
                    break;
            }

            payment.PaymentStatus = Entities.PaymentStatus.InProgress;
            _context.SaveChanges();
            return new UpdatePaymentResponse(true, "Updated");
        }
    }
}
