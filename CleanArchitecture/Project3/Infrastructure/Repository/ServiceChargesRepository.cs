using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    internal class ServiceChargesRepository : GenericRepository<ServiceCharges, int>, IServiceChargesRepository
    {
        public ServiceChargesRepository(AppDbContext context, ILogger<ServiceChargesRepository> logger) : base(context, logger)
        {
        }

        public override async Task<ServiceCharges?> GetById(int id)
        {
            try
            {
                return await _context.ServiceCharges.AsNoTracking().FirstOrDefaultAsync(sc => sc.ServiceChargesId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public async Task<ServiceCharges?> GetByName(string serviceName)
        {
            try
            {
                return await _context.ServiceCharges.AsNoTracking().FirstOrDefaultAsync(sc => sc.ServiceChargesName == serviceName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
