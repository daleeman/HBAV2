using HBA.Application.Interfaces.Services;
using HBA.Domain.Entities;
using HBA.Domain.Interfaces;

namespace HBA.Application.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _repository;
        public PropertyService(IPropertyRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Property?> GetPropertyByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddPropertyAsync(Property property)
        {
            await _repository.AddAsync(property);
        }

        public async Task UpdatePropertyAsync(Property property)
        {
            await _repository.UpdateAsync(property);
        }

        public async Task DeletePropertyAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Property>> GetFilteredPropertiesAsync(decimal? minPrice, decimal? maxPrice, int propertyTypeId)
        {
           return await _repository.GetFilteredPropertiesAsync(minPrice, maxPrice, propertyTypeId);
        }
    }
}
