using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class CartRepository : GenericRepository<CartDetail, int>, ICartRepository
    {
        public CartRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
       

        public async Task<List<CartDetail>> GetCartById(int id)
        {
            try
            {
                return await _context.CartDetail.AsNoTracking().Where(c => c.ClientId == id).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while getting cart by ID: {Id}", id);
                return null;
            }
        }

        public async Task<double> CalculateTotalPrice(int clientId)
        {
            try
            {
                var cartItems = await _context.CartDetail
                    .AsNoTracking()
                    .Include(c => c.ServiceCharges)
                    .Where(item => item.ClientId == clientId)
                    .ToListAsync();

                double totalPrice = cartItems.Sum(item => item.ServiceCharges.Price); 

                return totalPrice;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while calculating total price of cart");
                throw;
            }
        }
    }
}
