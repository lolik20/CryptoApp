using CryptoExchange.Entities;
using CryptoExchange.Exceptions;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Queries
{
    public class GetUserPaymentQuery : IRequestHandler<GetUserPaymentRequest, UserPaymentResponse>
    {
        private readonly ApplicationContext _context;
        public GetUserPaymentQuery(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<UserPaymentResponse> Handle(GetUserPaymentRequest request, CancellationToken cancellationToken)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (payment == null)
            {
                throw new NotFoundException("Payment not found");
            }
            var fromCurrency = await _context.Currencies.FirstAsync(x => x.Id == payment!.CurrencyId);

            var result = new UserPaymentResponse
            {
                Id = request.Id,
                FromAmount = payment.Amount,
                FromCurrency = new CurrencyResponse
                {
                    Id = fromCurrency.Id,
                    Code = fromCurrency.Code,
                    Name = fromCurrency!.Name,
                    ImageUrl = fromCurrency.ImageUrl
                },
                MerchantId = payment.UserId,
                Title = payment.Title,
                Status = payment.PaymentStatus,
            };
            var paymentData = await _context.PaymentsData.FirstOrDefaultAsync(x=>x.PaymentId == request.Id);
            if (paymentData != null)
            {
                var network = await _context.Networks.FirstAsync(x => x.Id == paymentData.NetworkId);
                var toCurrency = await _context.Currencies.FirstAsync(x => x.Id == paymentData.CurrencyId);
                var currencyNetwork = await _context.CurrencyNetworks.FirstAsync(x => x.NetworkId == network.Id && x.CurrencyId == toCurrency.Id);

                result.ToNetwork = new NetworkResponse
                {
                    Id = network.Id,
                    ImageUrl = network.ImageUrl,
                    Name = network.Name,
                    Url = network.Url,
                    ExplorerUrl = network.ExplorerUrl,
                    ChainId = network.ChainId,
                };
                result.ToCurrency = new CurrencyResponse
                {
                    Id = toCurrency.Id,
                    Code = toCurrency.Code,
                    Name = toCurrency.Name,
                    ImageUrl = toCurrency.ImageUrl,
                };
                result.ToAmount = paymentData.ToAmount;
                result.WalletAddress = paymentData.WalletAddress;
                result.ToCurrency.ContractAddress = currencyNetwork.ContractAddress;

            }
            return result;
        }
    }
}
