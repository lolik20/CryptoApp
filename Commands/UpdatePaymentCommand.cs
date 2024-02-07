using CryptoExchange.Exceptions;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;

namespace CryptoExchange.Commands
{
    public class UpdatePaymentCommand : IRequestHandler<UpdatePaymentRequest, UpdatePaymentResponse>
    {
        private readonly ApplicationContext _context;
        public UpdatePaymentCommand(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<UpdatePaymentResponse> Handle(UpdatePaymentRequest request, CancellationToken cancellationToken)
        {
            var payment = _context.Payments.FirstOrDefault(x => x.Id == request.Id);
            if (payment == null)
            {
                throw new NotFoundException("Платёжка с таким id не найдена");
            }
            payment.CurrencyId = request.CurrencyId;
            payment.NetworkId = request.NetworkId;
            _context.SaveChanges();
            return new UpdatePaymentResponse(true, "Информация по платёжке обновлена");
        }
    }
}
