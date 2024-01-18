using Domain.Interfaces;
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInFrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)),
                ServiceLifetime.Transient);
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientServiceRepository, ClientServiceRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IServiceChargesRepository, ServiceChargesRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            return services;
        }
    }
}
