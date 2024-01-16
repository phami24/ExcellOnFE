using Domain.Abstraction;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IClientRepository : IGenericRepository<Client,int>
    {
    }
}
