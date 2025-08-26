using HBA.Domain.Entities;

namespace HBA.Application.Interfaces.Services
{
    public interface ICommissionSetupService
    {
        Task<IEnumerable<CommissionSetup>> GetAllCommissionSetupsAsync();
    }
}
