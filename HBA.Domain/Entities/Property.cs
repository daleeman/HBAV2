namespace HBA.Domain.Entities
{
    public class Property
    {
        public int Id { get; set; }
        public string PropertyName { get; set; } = null!;
        public int PropertyTypeId { get; set; }
        public string Location { get; set; } = null!;
        public decimal Price { get; set; }
        public PropertyType? PropertyType { get; set; }
    }
}
