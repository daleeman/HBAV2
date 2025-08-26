using HBA.Domain.Entities;
using HBA.Domain.Interfaces;
using HBA.Infrastructure.ApplicationDbContext;

namespace HBA.Infrastructure.Repository
{
    public class PropertyTypeRepository : Repository<PropertyType>, IPropertyTypeRepository
    {
        public PropertyTypeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
