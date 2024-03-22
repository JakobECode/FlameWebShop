using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;
using WebApi.Controllers;
using WebApi.Helpers.Interfaces;
using WebApi.Models.Dtos;
using WebApi.Models.Entities;
using WebApi.Models.Interfaces;

namespace UnitTests
{
    public class ProductControllerTests
    {
        //positiv
        [Fact]
        public async Task GetAsync_WhenProductExists_ShouldReturnOk()
        {
            // Arrange
            var mockService = new Mock<IProductService>();
            var testProduct = new ProductDTO
            {
                Id = 1,
                Title = "Testprodukt",
                Description = "Det här är en testbeskrivning",
                Price = 99.99m,
                StarRating = 5,
                ImageUrl = "http://example.com/image.jpg",
                CategoryId = 1,
                Tag = "Testtagg"
            };
            mockService.Setup(s => s.GetAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>()))
                       .ReturnsAsync(testProduct);
            var controller = new ProductsController(mockService.Object);

            // Act
            var result = await controller.GetAsync(1);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var product = Assert.IsType<ProductDTO>(actionResult.Value);

            Assert.Equal(1, product.Id);
            Assert.Equal("Testprodukt", product.Title);
            Assert.Equal("Det här är en testbeskrivning", product.Description);
            Assert.Equal(99.99m, product.Price);
            Assert.Equal(5, product.StarRating);
            Assert.Equal("http://example.com/image.jpg", product.ImageUrl);
            Assert.Equal(1, product.CategoryId);
            Assert.Equal("Testtagg", product.Tag);
        }


        [Fact]
        public async Task GetAsync_WhenExceptionOccurs_ShouldReturnInternalServerError()
        {
            // Arrange
            var mockService = new Mock<IProductService>();
            mockService.Setup(s => s.GetAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>()))
                       .ThrowsAsync(new Exception("Ett oväntat fel inträffade")); // Simulera ett undantag
            var controller = new ProductsController(mockService.Object);

            // Act
            var result = await controller.GetAsync(1); // Använd ett giltigt ID för testet

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode); // Verifiera att statuskoden är 500 (InternalServerError)
        }

        //negative

        //[Fact]
        //public async Task GetAsync_WhenProductDoesNotExist_ShouldReturnNotFound()
        //{
        //    // Arrange
        //    var mockService = new Mock<IProductService>();
        //    mockService.Setup(s => s.GetAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>()))
        //               .ReturnsAsync((Product)null); // Simulera att ingen produkt hittas

        //    var controller = new ProductsController(mockService.Object);

        //    // Act
        //    var result = await controller.GetAsync(999); // Använd ett ID som inte finns

        //    // Assert
        //    Assert.IsType<NotFoundObjectResult>(result); // Verifiera att resultatet är NotFound
        //}

        //positiv

       

        //negativ
    

    }
}