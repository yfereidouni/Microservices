using System.Security.Cryptography.X509Certificates;
using AutoFixture;
using AutoMapper;
using Basket.API.Controllers;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using EventBus.RabbitMQ;
using EventBus.RabbitMQ.Producer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Basket.API.Tests
{
    [TestClass]
    public class BasketControllerTests
    {
        private Mock<IBasketRepository> _basketRepositoryMock;
        private Mock<IMapper> _mapperMock;
        //private Mock<IRabbitMQConnection> _rabbitMQConnectionMock;
        private Fixture _fixture;
        private BasketController _controller;
        private Mock<EventBusRabbitMQProducer> _eventBusRabbitMQProducerMock;

        public BasketControllerTests()
        {
            _fixture = new Fixture();
            _basketRepositoryMock = new Mock<IBasketRepository>();
            _mapperMock = new Mock<IMapper>();
            _eventBusRabbitMQProducerMock = new Mock<EventBusRabbitMQProducer>();

            _controller = new BasketController(_basketRepositoryMock.Object, _mapperMock.Object, _eventBusRabbitMQProducerMock.Object);
        }

        [TestMethod]
        public void GetBasket_Return_Ok()
        {
            var basketCart = _fixture.Create<Task<BasketCart>>();

            _basketRepositoryMock.Setup(x => x.GetBasket(It.IsAny<string>()))
                .Returns(basketCart);

            var result = _controller.GetBasket(It.IsAny<string>());

            var obj = new OkObjectResult(result);

            Assert.AreEqual(200, obj.StatusCode);
        }
    }
}