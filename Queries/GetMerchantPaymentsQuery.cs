using CryptoExchange.RequestModels;
using CryptoExchange.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Queries
{
    public class GetMerchantPaymentsQuery : IRequestHandler<GetMerchantPaymentsRequest, List<MerchantPaymentResponse>>
    {
        private readonly ApplicationContext _context;
        public GetMerchantPaymentsQuery(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<List<MerchantPaymentResponse>> Handle(GetMerchantPaymentsRequest request, CancellationToken cancellationToken)
        {
            var payments =await _context.Payments.Where(x => x.UserId == request.MerchantId).Select(x => new MerchantPaymentResponse
            {
                Id = x.Id,
                Amount = x.Amount,
                Created = x.Created,
                Status = x.PaymentStatus,
                Title = x.Title
            }).ToListAsync();

            return payments;
        }
    }
}
