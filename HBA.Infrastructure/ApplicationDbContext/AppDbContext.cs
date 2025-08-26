using HBA.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HBA.Infrastructure.ApplicationDbContext
{
    public class AppDbContext :  IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Property> Property { get; set; } 
        public DbSet<PropertyType> PropertyType { get; set; }
        public DbSet<CommissionSetup> CommissionSetup { get; set; }
    }
}
