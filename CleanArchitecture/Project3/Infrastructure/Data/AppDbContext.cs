using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientService> ClientServices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceCharges> ServiceCharges { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
