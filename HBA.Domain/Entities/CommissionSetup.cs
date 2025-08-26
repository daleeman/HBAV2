namespace HBA.Domain.Entities
{
    public class CommissionSetup
    {
        public int Id { get; set; }
        public decimal FromAmount { get; set; }
        public decimal? ToAmount { get; set; }
        public decimal CommissionValue { get; set; }
        public static decimal CalculateCommission(decimal price, IEnumerable<CommissionSetup> commissionSetups)
        {
            var slab = commissionSetups.FirstOrDefault(x => price >= x.FromAmount &&
            (x.ToAmount == null || price <= x.ToAmount));

            if (slab == null)
                return 0M;

            return price * (slab.CommissionValue / 100);
        }
    }
}
