using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class OrderDetailRepository : GenericRepository<OrderDetail, int>, IOrderDetailRepository
    {
        public OrderDetailRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
