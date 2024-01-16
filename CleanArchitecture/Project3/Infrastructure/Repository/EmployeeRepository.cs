using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbSet<Employee> _employee;
        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _employee = _appDbContext.Employees;
        }
        public async Task<bool> Add(Employee entity)
        {
            await _employee.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Employee>> All()
        {
            return await _employee.AsNoTracking().ToListAsync();
        }

        public async Task<int> Count()
        {
            return await _employee.CountAsync();
        }

        public async Task<bool> Delete(Employee entity)
        {
            _employee.Remove(entity);
            return true;
        }

        public Task<Employee?> GetById(int id)
        {
            return _employee
                       .Include(e => e.Department)
                       .SingleOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<IEnumerable<Employee>> GetByPage(int page, int pageSize)
        {
            return await _employee.Skip((page - 1) * pageSize)
                             .Take(pageSize)
                             .ToListAsync();
        }

        public async Task<bool> Update(Employee entity)
        {
            _employee.Update(entity);
            return true;
        }
    }
}
