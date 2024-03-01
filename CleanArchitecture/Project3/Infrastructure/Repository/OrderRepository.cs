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
    public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
    {
        public OrderRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
