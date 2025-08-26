using HBA.Application.Interfaces.Services;
using HBA.Application.Services;
using HBA.Domain.Entities;
using HBA.Domain.Interfaces;
using HBA.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HBA.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly ICommissionSetupService _commissionSetupService;

        public PropertyController(
            IPropertyService propertyService,
            IPropertyTypeService propertyTypeService,
            ICommissionSetupService commissionSetupService)
        {
            _propertyService = propertyService;
            _propertyTypeService = propertyTypeService;
            _commissionSetupService = commissionSetupService;
        }

        // GET: api/property
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAllProperties()
        {
            var properties = await _propertyService.GetAllPropertiesAsync();
            var commissionSetup = await _commissionSetupService.GetAllCommissionSetupsAsync();

            var result = properties.Select(p => new
            {
                p.Id,
                p.PropertyName,
                p.Price,
                p.Location,
                CommissionValue = CommissionCalculator.Calculate(p.Price, commissionSetup)
            });

            return Ok(result);
        }

        // POST: api/property
        [HttpPost]
        public async Task<ActionResult> CreateProperty([FromBody] PropertyViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var property = new Property()
            {
                PropertyName = model.PropertyName,
                PropertyTypeId = model.PropertyTypeId,
                Location = model.Location,
                Price = model.Price
            };

            await _propertyService.AddPropertyAsync(property);
            return Ok(property);
        }

        // GET: api/property/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPropertyById(int id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            var commissionSetup = await _commissionSetupService.GetAllCommissionSetupsAsync();

            if (property == null)
                return NotFound();

            var result = new
            {
                property.Id,
                property.PropertyName,
                property.Price,
                property.Location,
                CommissionValue = CommissionCalculator.Calculate(property.Price, commissionSetup)
            };

            return Ok(result);
        }

        // PUT: api/property/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditProperty([FromBody] PropertyViewModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _propertyService.GetPropertyByIdAsync(model.Id);
            if (existing == null)
                return NotFound();

            existing.PropertyName = model.PropertyName;
            existing.PropertyTypeId = model.PropertyTypeId;
            existing.Location = model.Location;
            existing.Price = model.Price;

            await _propertyService.UpdatePropertyAsync(existing);
            return Ok(existing);
        }

        // DELETE: api/property/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var property = await _propertyService.GetPropertyByIdAsync(id);
            if (property == null)
                return NotFound();

            await _propertyService.DeletePropertyAsync(id);
            return Ok("Deleted Successfully");
        }
    }
}
