using CryptoExchange.Exceptions;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Nethereum.Web3;

namespace CryptoExchange.Commands
{
    public class CreatePaymentCommand : IRequestHandler<CreatePaymentRequest, CreatePaymentResponse>
    {
        private readonly ApplicationContext _context;
        public CreatePaymentCommand(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<CreatePaymentResponse> Handle(CreatePaymentRequest request, CancellationToken cancellationToken)
        {
            var merchant = _context.Users.FirstOrDefault(x => x.Id == request.MerchantId);
            if (merchant == null)
            {
                return new CreatePaymentResponse(false, "Merchant not found");
            }
            var currency = _context.Currencies.FirstOrDefault(x => x.Code == request.CurrencyId.ToLower().Trim());
            if (currency == null) {
                throw new NotFoundException($"Currency {request.CurrencyId} not found");
            }

            var payment = _context.Payments.Add(new Entities.Payment { Amount = request.Amount,CurrencyId =currency.Id, MerchantId=request.MerchantId,Title=request.Title,PaymentData = new Entities.PaymentData() });
            
            _context.SaveChanges();
            return new CreatePaymentResponse(true, $"https://payments.com/{payment.Entity.Id}");
        }
    }
}
