using Domain.Interfaces;
using Domain.Repositories;

namespace Domain.Abstraction
{
    public interface IUnitOfWork
    {
        IClientRepository Clients { get; }
        IClientServiceRepository ClientServices { get; }
        IDepartmentRepository Departments { get; }
        IEmployeeRepository Employees { get; }
        IPaymentRepository Payments { get; }
        IReportRepository Reports { get; }
        IServiceRepository Services { get; }
        IServiceChargesRepository ServicesCharges { get; }
        Task CompleteAsync();
    }
}
