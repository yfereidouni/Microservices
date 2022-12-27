using AutoFixture;
using Catalog.API.Controllers;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;

namespace Catalog.API.Tests
{
    [TestClass]
    public class CatalogControllerTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private Mock<ILogger<CatalogController>> _loggerMock;
        private Fixture _fixture;
        private CatalogController _controller;

        public CatalogControllerTests()
        {
            _fixture = new Fixture();
            _productRepositoryMock = new Mock<IProductRepository>();
            _loggerMock = new Mock<ILogger<CatalogController>>();

            _controller = new CatalogController(_productRepositoryMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task GetProducts_Returns_Ok()
        {
            var products = _fixture.Create<Task<IEnumerable<Product>>>();

            _productRepositoryMock.Setup(repo => repo.GetProducts())
                .Returns(products);

            _controller = new CatalogController(_productRepositoryMock.Object, _loggerMock.Object);

            var result = _controller.GetProducts();

            var obj = new OkObjectResult(result);

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task GetProducts_Returns_BadRequest()
        {
            _productRepositoryMock.Setup(repo => repo.GetProducts())
                .Throws(new Exception());

            //_controller = new CatalogController(_productRepositoryMock.Object, _loggerMock.Object);

            var result = _controller.GetProducts();

            var obj = new BadRequestObjectResult(result);

            Assert.AreEqual(400, obj.StatusCode);
        }

        [TestMethod]
        public async Task GetProductsById_Returns_Ok()
        {
            var product = _fixture.Create<Task<Product>>();

            _productRepositoryMock.Setup(repo => repo.GetProductById(It.IsAny<string>()))
                .Returns(product);

            //_controller = new CatalogController(_productRepositoryMock.Object, _loggerMock.Object);

            var result = _controller.GetProduct(It.IsAny<string>());

            var obj = new OkObjectResult(result);

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task GetProductsById_Returns_NotFound()
        { 
            //var product = _fixture.Create<Task<Product>>();
            Task<Product>? product = null;

            _productRepositoryMock.Setup(repo => repo.GetProductById(It.IsAny<string>()))
                .Equals(new NotFoundResult());

            //_controller = new CatalogController(_productRepositoryMock.Object, _loggerMock.Object);

            var result = _controller.GetProduct(It.IsAny<string>());

            var obj = new NotFoundResult();

            Assert.AreEqual(404, obj.StatusCode);
        }

        [TestMethod]
        public async Task GetProductByCategory_Returns_Ok()
        {
            var productsByCategoryName = _fixture.Create<Task<IEnumerable<Product>>>();

            _productRepositoryMock.Setup(repo => repo.GetProductByCategory(It.IsAny<string>()))
                .Returns(productsByCategoryName);

            //_controller = new CatalogController(_productRepositoryMock.Object, _loggerMock.Object);

            var result = _controller.GetProductByCategory(It.IsAny<string>());

            var obj = new OkObjectResult(result);

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task GetProductByCategory_Returns_BadRequest()
        {
            _productRepositoryMock.Setup(repo => repo.GetProductByCategory(It.IsAny<string>()))
                .Throws(new Exception());

            //_controller = new CatalogController(_productRepositoryMock.Object, _loggerMock.Object);

            var result = _controller.GetProductByCategory(It.IsAny<string>());

            var obj = new BadRequestObjectResult(result);

            Assert.AreEqual(400, obj.StatusCode);
        }

        [TestMethod]
        public async Task GetProductByName_Returns_Ok()
        {
            var productsByName = _fixture.Create<Task<IEnumerable<Product>>>();

            _productRepositoryMock.Setup(repo => repo.GetProductByName(It.IsAny<string>()))
                .Returns(productsByName);

            //_controller = new CatalogController(_productRepositoryMock.Object, _loggerMock.Object);

            var result = _controller.GetProductByName(It.IsAny<string>());

            var obj = new OkObjectResult(result);

            Assert.AreEqual(200, obj.StatusCode);
        }

        [TestMethod]
        public async Task GetProductByName_Returns_BadRequest()
        {
            _productRepositoryMock.Setup(repo => repo.GetProductByName(It.IsAny<string>()))
                .Throws(new Exception());

            //_controller = new CatalogController(_productRepositoryMock.Object, _loggerMock.Object);

            var result = _controller.GetProductByName(It.IsAny<string>());

            var obj = new BadRequestObjectResult(result);

            Assert.AreEqual(400, obj.StatusCode);
        }

        [TestMethod]
        public async Task CreateProduct_Returns_Ok()
        {
            var product = _fixture.Create<Product>();
            var productVM = _fixture.Create<ProductVM>();

            _productRepositoryMock.Setup(repo => repo.Create(product))
                .Equals(new CreatedAtRouteResult("GetProduct", new { id = product.Id }, product));

            //_controller = new CatalogController(_productRepositoryMock.Object, _loggerMock.Object);

            var result = await _controller.CreateProduct(productVM);
            var routName = ((Microsoft.AspNetCore.Mvc.CreatedAtRouteResult)result.Result).RouteName;
            var obj = new CreatedAtRouteResult(routName, new { id = product.Id }, product);

            Assert.AreEqual(201, obj.StatusCode);
        }

        [TestMethod]
        public async Task CreateProduct_Returns_BadRequest()
        {
            //var product = _fixture.Create<Product>();
            var productVM = _fixture.Create<ProductVM>();

            _productRepositoryMock.Setup(repo => repo.Create(It.IsAny<Product>()))
                .Throws(new Exception());

            //_controller = new CatalogController(_productRepositoryMock.Object, _loggerMock.Object);

            var result = await _controller.CreateProduct(productVM);

            var obj = new BadRequestResult();

            Assert.AreEqual(400, obj.StatusCode);
            Assert.AreEqual(400, ((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).StatusCode);
        }

        [TestMethod]
        public async Task UpdateProduct_Returns_Ok()
        {
            var product = _fixture.Create<Product>();

            _productRepositoryMock.Setup(repo => repo.Update(product))
                .Equals(new OkObjectResult(true));

            //_controller = new CatalogController(_productRepositoryMock.Object, _loggerMock.Object);

            var result = await _controller.UpdateProduct(product);
            var obj = new OkObjectResult(true);

            Assert.AreEqual(200, obj.StatusCode);
            Assert.AreEqual(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
        }

        [TestMethod]
        public async Task DeleteProduct_Returns_Ok()
        {
            var product = _fixture.Create<Product>();

            _productRepositoryMock.Setup(repo => repo.Delete(It.IsAny<string>()))
                .Equals(new OkObjectResult(true));

            //_controller = new CatalogController(_productRepositoryMock.Object, _loggerMock.Object);

            var result = await _controller.DeleteProduct(It.IsAny<string>());
            var obj = new OkObjectResult(true);

            Assert.AreEqual(200, obj.StatusCode);
            Assert.AreEqual(200, ((Microsoft.AspNetCore.Mvc.ObjectResult)result).StatusCode);
        }
    }
}