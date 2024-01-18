using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Common;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class EmployeeRepository : GenericRepository<Employee, int>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
        public override async Task<Employee?> GetById(int id)
        {
            try
            {
                return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.EmployeeId == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
