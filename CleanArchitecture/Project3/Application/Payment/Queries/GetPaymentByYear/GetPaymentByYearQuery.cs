using Application.DTOs.Payment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payment.Queries.GetPaymentByYear
{
    public class GetPaymentByYearQuery : IRequest<List<RevenueDto>>
    {
        public int Year { get; set; }
    }
}
