using HBA.Application.Interfaces.Services;
using HBA.Domain.Entities;
using HBA.Domain.Interfaces;

namespace HBA.Application.Services
{
    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly IPropertyTypeRepository _repository;
        public PropertyTypeService(IPropertyTypeRepository repository)
        {
            _repository = repository;
        }

        public Task<PropertyType?> GetAllPropertyTypeByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<IEnumerable<PropertyType>> GetAllPropertyTypesAsync()
        {
            return _repository.GetAllAsync();
        }
    }
}
