using HBA.Domain.Entities;

namespace HBA.Application.Interfaces.Services
{
    public interface IPropertyTypeService
    {
        Task<IEnumerable<PropertyType>> GetAllPropertyTypesAsync();
        Task<PropertyType?> GetAllPropertyTypeByIdAsync(int id);
    }
}
