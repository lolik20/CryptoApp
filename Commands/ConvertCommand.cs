using CryptoExchange.Interfaces;
using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;

namespace CryptoExchange.Commands
{
    public class ConvertCommand : IRequestHandler<ConvertRequest, ConvertResponse>
    {
        private readonly ApplicationContext _context;
        private readonly ICurrencyService _currencyService;
        public ConvertCommand(ApplicationContext context, ICurrencyService currencyService)
        {
            _context = context;
            _currencyService = currencyService;
        }

        public async Task<ConvertResponse> Handle(ConvertRequest request, CancellationToken cancellationToken)
        {
            decimal rate = await _currencyService.GetRate(request.FromId, request.FromAmount, request.ToId);

            decimal toAmount = _currencyService.CalculateAmountWithComission(request.FromAmount, rate, request.Commission);

            
            throw new NotImplementedException();
        }
    }
}
