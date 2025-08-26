using HBA.Application.Interfaces.Services;
using HBA.Domain.Entities;
using HBA.Domain.Interfaces;

namespace HBA.Application.Services
{
    public class CommissionSetupService : ICommissionSetupService
    {
        private readonly ICommissionSetupRepository _repository;
        public CommissionSetupService(ICommissionSetupRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<CommissionSetup>> GetAllCommissionSetupsAsync()
        {
            return _repository.GetAllAsync();
        }
    }
}
