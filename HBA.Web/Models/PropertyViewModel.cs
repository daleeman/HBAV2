using System.ComponentModel;

namespace HBA.Web.Models
{
    public class PropertyViewModel
    {
        public int Id { get; set; }
        public int PropertyTypeId { get; set; }
        [DisplayName("Property Name")]
        public string PropertyName { get; set; } = null!;
        [DisplayName("Property Type")]
        public string? PropertyTypeName { get; set; }
        public string Location { get; set; } = null!;
        public decimal Price { get; set; }
        [DisplayName("Commission Value")]
        public decimal CommissionValue { get; set; }
    }
}
