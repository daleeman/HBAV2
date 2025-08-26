using HBA.Domain.Entities;

namespace HBA.Application.Interfaces.Services
{
    public interface IPropertyService
    {
        Task<IEnumerable<Property>> GetAllPropertiesAsync();
        Task<IEnumerable<Property>> GetFilteredPropertiesAsync(decimal? minPrice, decimal? maxPrice, int propertyTypeId);
        Task<Property?> GetPropertyByIdAsync(int id);
        Task AddPropertyAsync(Property property);
        Task UpdatePropertyAsync(Property property);
        Task DeletePropertyAsync(int id);
    }
}
