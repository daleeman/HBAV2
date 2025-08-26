using HBA.Application.Interfaces.Services;
using HBA.Domain.Entities;
using HBA.WebAPI.Controllers;
using HBA.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HBA.Tests
{
    public class PropertyControllerTests
    {
        private readonly Mock<IPropertyService> _propertyServiceMock;
        private readonly Mock<IPropertyTypeService> _propertyTypeServiceMock;
        private readonly Mock<ICommissionSetupService> _commissionSetupServiceMock;
        private readonly PropertyController _controller;

        public PropertyControllerTests()
        {
            _propertyServiceMock = new Mock<IPropertyService>();
            _propertyTypeServiceMock = new Mock<IPropertyTypeService>();
            _commissionSetupServiceMock = new Mock<ICommissionSetupService>();

            _controller = new PropertyController(
                _propertyServiceMock.Object,
                _propertyTypeServiceMock.Object,
                _commissionSetupServiceMock.Object);
        }

        // GET: api/property
        [Fact]
        public async Task GetAllProperties_ReturnsOk_WithList()
        {
            // Arrange
            var properties = new List<Property>
            {
                new Property { Id = 1, PropertyName = "Villa", Price = 2000, Location = "Beach", PropertyTypeId = 1, PropertyType = new PropertyType { Type = "House" } }
            };
            _propertyServiceMock.Setup(p => p.GetAllPropertiesAsync()).ReturnsAsync(properties);
            _commissionSetupServiceMock.Setup(c => c.GetAllCommissionSetupsAsync()).ReturnsAsync(new List<CommissionSetup>());

            // Act
            var result = await _controller.GetAllProperties();

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<object>>(ok.Value);
        }

        // GET: api/property/{id}
        [Fact]
        public async Task GetPropertyById_ReturnsOk_WhenFound()
        {
            var property = new Property { Id = 1, PropertyName = "Condo", Price = 1500, Location = "City" };
            _propertyServiceMock.Setup(x => x.GetPropertyByIdAsync(1)).ReturnsAsync(property);
            _commissionSetupServiceMock.Setup(c => c.GetAllCommissionSetupsAsync()).ReturnsAsync(new List<CommissionSetup>());

            var result = await _controller.GetPropertyById(1);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var data = Assert.IsType<object>(ok.Value);
        }

        [Fact]
        public async Task GetPropertyById_ReturnsNotFound_WhenNull()
        {
            _propertyServiceMock.Setup(x => x.GetPropertyByIdAsync(1)).ReturnsAsync((Property)null);

            var result = await _controller.GetPropertyById(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        // POST: api/property
        [Fact]
        public async Task CreateProperty_ReturnsOk_WhenValid()
        {
            var model = new PropertyViewModel
            {
                PropertyName = "Test Property",
                PropertyTypeId = 1,
                Location = "Town",
                Price = 1000
            };

            var result = await _controller.CreateProperty(model);

            _propertyServiceMock.Verify(x => x.AddPropertyAsync(It.IsAny<Property>()), Times.Once);
            var ok = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateProperty_ReturnsBadRequest_WhenModelInvalid()
        {
            _controller.ModelState.AddModelError("Price", "Required");
            var result = await _controller.CreateProperty(new PropertyViewModel());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // PUT: api/property/{id}
        [Fact]
        public async Task EditProperty_ReturnsOk_WhenUpdated()
        {
            var existing = new Property { Id = 1, PropertyName = "Old Name" };
            _propertyServiceMock.Setup(x => x.GetPropertyByIdAsync(1)).ReturnsAsync(existing);

            var model = new PropertyViewModel
            {
                Id = 1,
                PropertyName = "New Name",
                PropertyTypeId = 1,
                Location = "Updated",
                Price = 3000
            };

            var result = await _controller.EditProperty(model);

            _propertyServiceMock.Verify(x => x.UpdatePropertyAsync(It.Is<Property>(p => p.PropertyName == "New Name")), Times.Once);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task EditProperty_ReturnsNotFound_WhenNotExists()
        {
            _propertyServiceMock.Setup(x => x.GetPropertyByIdAsync(It.IsAny<int>())).ReturnsAsync((Property)null);

            var result = await _controller.EditProperty(new PropertyViewModel { Id = 5 });

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task EditProperty_ReturnsBadRequest_WhenInvalidModel()
        {
            _controller.ModelState.AddModelError("Name", "Required");

            var result = await _controller.EditProperty(new PropertyViewModel());

            Assert.IsType<BadRequestObjectResult>(result);
        }

        // DELETE: api/property/{id}
        [Fact]
        public async Task DeleteProperty_ReturnsOk_WhenDeleted()
        {
            _propertyServiceMock.Setup(x => x.GetPropertyByIdAsync(1)).ReturnsAsync(new Property { Id = 1 });

            var result = await _controller.DeleteProperty(1);

            _propertyServiceMock.Verify(x => x.DeletePropertyAsync(1), Times.Once);
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Deleted Successfully", ok.Value);
        }

        [Fact]
        public async Task DeleteProperty_ReturnsNotFound_WhenMissing()
        {
            _propertyServiceMock.Setup(x => x.GetPropertyByIdAsync(1)).ReturnsAsync((Property)null);

            var result = await _controller.DeleteProperty(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}