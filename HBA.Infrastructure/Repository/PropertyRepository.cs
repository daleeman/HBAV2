using HBA.Domain.Entities;
using HBA.Domain.Interfaces;
using HBA.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace HBA.Infrastructure.Repository
{
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        public PropertyRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Property>> GetFilteredPropertiesAsync(decimal? minPrice, decimal? maxPrice, int propertyTypeId)
        {
            var query = _context.Property
        .Include(p => p.PropertyType)
        .Select(p => new Property()
        {
            Id = p.Id,
            PropertyName = p.PropertyName,
            Price = p.Price,
            Location = p.Location,
            PropertyType = p.PropertyType,
            PropertyTypeId = p.PropertyTypeId
        });

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (propertyTypeId > 0)
                query = query.Where(p => p.PropertyTypeId == propertyTypeId);

            return await query
                .Select(p => new Property
                {
                    Id = p.Id,
                    PropertyName = p.PropertyName,
                    PropertyType = p.PropertyType,
                    Price = p.Price,
                    Location = p.Location
                })
                .ToListAsync();
        }
    }
}
