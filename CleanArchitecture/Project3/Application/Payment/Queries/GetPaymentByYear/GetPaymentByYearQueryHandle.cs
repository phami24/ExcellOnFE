using Application.DTOs.Payment;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payment.Queries.GetPaymentByYear
{
    public class GetPaymentByYearQueryHandle : IRequestHandler<GetPaymentByYearQuery, List<RevenueDto>>
    {
        public IPaymentRepository _paymentRepository;
        public GetPaymentByYearQueryHandle(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<List<RevenueDto>> Handle(GetPaymentByYearQuery request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Payment> payments = await _paymentRepository.GetPaymentsByYear(request.Year);
            List<RevenueDto> revenueDtos = new List<RevenueDto>();
            foreach (var payment in payments)
            {
                if (payment != null)
                {
                    var revenueDto = new RevenueDto()
                    {
                        Amount = payment.Amount,
                        PaymentDate = payment.PaymentDate
                    };
                    revenueDtos.Add(revenueDto);
                }
            }
            return revenueDtos;
        }
    }
}
