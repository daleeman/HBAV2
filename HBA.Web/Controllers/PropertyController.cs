using HBA.Application.Interfaces.Services;
using HBA.Application.Services;
using HBA.Domain.Entities;
using HBA.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HBA.Web.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly ICommissionSetupService _commissionSetupService;

        public PropertyController(IPropertyService propertyRepository, IPropertyTypeService propertyTypeRepository, ICommissionSetupService commissionSetupRepository)
        {
            _propertyService = propertyRepository;
            _propertyTypeService = propertyTypeRepository;
            _commissionSetupService = commissionSetupRepository;
        }

        [Authorize(Roles = "Broker,Seeker")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var properties = await _propertyService.GetAllPropertiesAsync();
            var commissionSetup = await _commissionSetupService.GetAllCommissionSetupsAsync();
            var propertyTypes = await _propertyTypeService.GetAllPropertyTypesAsync();

            ViewBag.PropertyTypes = propertyTypes.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Type
            });

            List<PropertyViewModel> viewModel = properties.Select(x => new PropertyViewModel
            {
                Id = x.Id,
                PropertyName = x.PropertyName,
                PropertyTypeName = x.PropertyType!.Type,
                Price = x.Price,
                Location = x.Location,
                CommissionValue = CommissionCalculator.Calculate(x.Price, commissionSetup)
            }).ToList();

            return View(viewModel);


        }
        [Authorize(Roles = "Broker,Seeker")]
        [HttpGet]
        public async Task<IActionResult> FilteredProperties(decimal? minPrice, decimal? maxPrice, int propertyTypeId)
        {

            var properties = await _propertyService.GetFilteredPropertiesAsync(minPrice,maxPrice,propertyTypeId);
            var commissionSetup = await _commissionSetupService.GetAllCommissionSetupsAsync();

            var model = properties.Select(x => new PropertyViewModel
            {
                Id = x.Id,
                PropertyName = x.PropertyName,
                PropertyTypeName = x.PropertyType!.Type,
                Price = x.Price,
                Location = x.Location,
                CommissionValue = CommissionCalculator.Calculate(x.Price, commissionSetup)
            }).ToList();

            return PartialView("_PropertyList", model);
        }


        [Authorize(Roles = "Broker")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var propertyTypes = await _propertyTypeService.GetAllPropertyTypesAsync();

            ViewBag.PropertyTypes = propertyTypes.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Type
            });
            return View();
        }

        [Authorize(Roles = "Broker")]
        [HttpPost]
        public async Task<IActionResult> Create(PropertyViewModel propertyvm)
        {
            if (ModelState.IsValid)
            {
                var property = new Property
                {
                    PropertyName = propertyvm.PropertyName,
                    PropertyTypeId = propertyvm.PropertyTypeId,
                    Location = propertyvm.Location,
                    Price = propertyvm.Price
                };

                await _propertyService.AddPropertyAsync(property);
                return RedirectToAction("Index");
            }
            return View(propertyvm);
        }
        [Authorize(Roles = "Broker")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            var property = await _propertyService.GetPropertyByIdAsync(id);
            var propertyTypes = await _propertyTypeService.GetAllPropertyTypesAsync();

            ViewBag.PropertyTypes = propertyTypes.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Type
            });

            if (property == null) return View("Error");


            var propertyvm = new PropertyViewModel
            {
                PropertyName = property.PropertyName,
                PropertyTypeId = property.PropertyTypeId,
                Location = property.Location,
                Price = property.Price
            };

            return View(propertyvm);
        }

        [Authorize(Roles = "Broker")]
        [HttpPost]
        public async Task<IActionResult> Edit(PropertyViewModel propertyvm)
        {
            if (ModelState.IsValid)
            {
                var result = await _propertyService.GetPropertyByIdAsync(propertyvm.Id);
                if (result == null)
                    return View("Error");


                result.PropertyName = propertyvm.PropertyName;
                result.PropertyTypeId = propertyvm.PropertyTypeId;
                result.Location = propertyvm.Location;
                result.Price = propertyvm.Price;

                await _propertyService.UpdatePropertyAsync(result);
                return RedirectToAction("Index");
            }

            return View(propertyvm);
        }

        [Authorize(Roles = "Broker")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _propertyService.DeletePropertyAsync(id);
            return Json(new { success = true });
        }


    }
}
