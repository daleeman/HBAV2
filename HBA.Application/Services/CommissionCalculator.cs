using HBA.Domain.Entities;

namespace HBA.Application.Services
{
    public static class CommissionCalculator
    {
        public static decimal Calculate(decimal price, IEnumerable<CommissionSetup> commissionSetups)
        {
            var slab = commissionSetups.FirstOrDefault(x =>
                price >= x.FromAmount &&
                (x.ToAmount == null || price <= x.ToAmount));

            if (slab == null)
                return 0M;

            return price * (slab.CommissionValue / 100);
        }
    }
}
