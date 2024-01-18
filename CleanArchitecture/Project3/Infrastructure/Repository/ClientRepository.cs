using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class ClientRepository : GenericRepository<Client, int>, IClientRepository
    {
        public ClientRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
        public override async Task<Client?> GetById(int id)
        {
            try
            {
                return await _context.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.ClientId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
