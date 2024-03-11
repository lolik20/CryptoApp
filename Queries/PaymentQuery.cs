using CryptoExchange.Entities;
using CryptoExchange.Exceptions;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Queries
{
    public class PaymentQuery : IRequestHandler<GetPaymentRequest, PaymentResponse>
    {
        private readonly ApplicationContext _context;
        public PaymentQuery(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<PaymentResponse> Handle(GetPaymentRequest request, CancellationToken cancellationToken)
        {
            var payment = await _context.Payments.Include(x => x.PaymentData).FirstOrDefaultAsync(x => x.Id == request.Id);
            if (payment == null)
            {
                throw new NotFoundException("Payment not found");
            }
            var fromCurrency = await _context.Currencies.FirstAsync(x => x.Id == payment!.CurrencyId);

            var result = new PaymentResponse
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
                MerchantId = payment.MerchantId,
                Title = payment.Title,
                Status = payment.PaymentStatus,
            };
            if (payment.PaymentData!.NetworkId != null && payment.PaymentData!.CurrencyId != null)
            {
                var network = await _context.Networks.FirstAsync(x => x.Id == payment.PaymentData!.NetworkId);
                var toCurrency = await _context.Currencies.FirstAsync(x => x.Id == payment.PaymentData!.CurrencyId);
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
                result.ToAmount = payment.PaymentData!.ToAmount;
                result.WalletAddress = payment.PaymentData!.WalletAddress;
                result.ToCurrency.ContractAddress = currencyNetwork.ContractAddress;

            }
            return result;
        }
    }
}
