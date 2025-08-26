using HBA.Domain.Entities;
using HBA.Domain.Interfaces;
using HBA.Infrastructure.ApplicationDbContext;

namespace HBA.Infrastructure.Repository
{
    public class CommissionSetupRepository : Repository<CommissionSetup>, ICommissionSetupRepository
    {
        public CommissionSetupRepository(AppDbContext context) : base(context)
        {
        }
    }
}
