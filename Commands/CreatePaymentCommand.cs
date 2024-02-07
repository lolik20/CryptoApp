using CryptoExchange.Exceptions;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;

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
                return new CreatePaymentResponse(false, "Мерчант не найден");
            }
            var currency = _context.Currencies.FirstOrDefault(x => x.Code == request.CurrencyId.ToLower().Trim());
            if (currency == null) {
                throw new NotFoundException($"Валюта {request.CurrencyId} не найдена");
            }
            var payment = _context.Payments.Add(new Entities.Payment { Amount = request.Amount,MerchantId=request.MerchantId,Title=request.Title });
            _context.SaveChanges();
            return new CreatePaymentResponse(true, $"https://payments.com/{payment.Entity.Id}");
        }
    }
}
