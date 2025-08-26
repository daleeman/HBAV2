using HBA.Domain.Entities;

namespace HBA.Domain.Interfaces
{
    public interface IPropertyRepository : IRepository<Property>
    {
        Task<IEnumerable<Property>> GetFilteredPropertiesAsync(decimal? minPrice, decimal? maxPrice, int propertyTypeId);
    }
}
