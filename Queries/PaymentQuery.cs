using CryptoExchange.Exceptions;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;

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
            var payment = _context.Payments.FirstOrDefault(x => x.Id == request.Id);
            if (payment == null)
            {
                throw new NotFoundException("Payment not found");
            }
            return new PaymentResponse
            {
                Id = request.Id,
                Amount = payment.Amount,
                CurrencyId = payment.CurrencyId,
                MerchantId = payment.MerchantId,
                NetworkId = payment.NetworkId,
                Title = payment.Title
            };
        }
    }
}
