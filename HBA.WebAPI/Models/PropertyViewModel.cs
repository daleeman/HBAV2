namespace HBA.WebAPI.Models
{
    public class PropertyViewModel
    {
        public int Id { get; set; }
        public int PropertyTypeId { get; set; }
        public string PropertyName { get; set; } = null!;
        public string? PropertyTypeName { get; set; }
        public string Location { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal CommissionValue { get; set; }
    }
}
