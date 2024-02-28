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
            var payment = _context.Payments.Include(x=>x.Currency).Include(x => x.PaymentData).ThenInclude(x => x.Network).Include(x => x.PaymentData).ThenInclude(x => x.Currency).FirstOrDefault(x => x.Id == request.Id);
            if (payment == null)
            {
                throw new NotFoundException("Payment not found");
            }
            var result = new PaymentResponse
            {
                Id = request.Id,
                FromAmount = payment.Amount,
                FromCurrency =new CurrencyResponse
                {
                    Id = payment!.Currency!.Id,
                    Code = payment!.Currency!.Code,
                    Name = payment!.Currency!.Name,
                    ImageUrl = payment!.Currency.ImageUrl
                },
                

                MerchantId = payment.MerchantId,
           
                Title = payment.Title,
                Status = payment.PaymentStatus,


            };
            if (payment.PaymentData!.Network != null && payment.PaymentData.Currency!=null)
            {
                result.ToNetwork = new NetworkResponse
                {
                    Id = payment.PaymentData.Network.Id,
                    ImageUrl = payment.PaymentData.Network.ImageUrl,
                    Name = payment.PaymentData.Network.Name,
                    Url = payment.PaymentData.Network.Url,
                    ExplorerUrl = payment.PaymentData.Network.ExplorerUrl,
                    ChainId = payment.PaymentData.Network.ChainId,
                };
                result.ToCurrency = new CurrencyResponse
                {
                    Id = payment.PaymentData.Currency.Id,
                    Code = payment.PaymentData.Currency.Code,
                    Name = payment.PaymentData.Currency.Name,
                    ImageUrl = payment.PaymentData.Currency.ImageUrl,
                };  
                result.ToAmount = payment.PaymentData.ToAmount;
                result.WalletAddress = payment.PaymentData.WalletAddress;
                var currencyNetwork = _context.CurrencyNetworks.Include(x=>x.Network).Include(x=>x.Currency).FirstOrDefault(x => x.CurrencyId == payment.PaymentData.CurrencyId && x.NetworkId == payment.PaymentData.NetworkId);
                if(currencyNetwork != null)
                {
                    result.ToCurrency.ContractAddress = currencyNetwork.ContractAddress;
                }
            }
            return result;
        }
    }
}
