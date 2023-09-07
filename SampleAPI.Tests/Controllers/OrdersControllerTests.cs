using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SampleAPI.Controllers;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Requests;
using SampleAPI.Response;

namespace SampleAPI.Tests.Controllers
{
    public class OrdersControllerTests
    {
        private readonly Mock<ILogger<OrdersController>> _mocklogger;
        private readonly Mock<IOrderRepository> mockOrderRepository;
        public OrdersControllerTests() {
            mockOrderRepository = new Mock<IOrderRepository>();
            _mocklogger = new Mock<ILogger<OrdersController>>();          
        }
        [Fact]
        public async Task GetRecentOrders_ReturnsOkResult()
        {
            // Arrange
            mockOrderRepository.Setup(repo => repo.GetRecentOrdersAsync())
                .ReturnsAsync(new List<OrderResposne>());
            var controller = new OrdersController(mockOrderRepository.Object, _mocklogger.Object);

            // Act
            var result = await controller.GetRecentOrders();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SubmitOrder_WithValidOrder_ReturnsCreatedAtAction()
        {
            // Arrange 
            var validOrder = new Order { Name = "Test Order", Description = "Test Description" };
            var OrderRequest = new CreateOrderRequest { Name = "Test Order", Description = "Test Description" };
            mockOrderRepository.Setup(repo => repo.AddOrderAsync(It.IsAny<CreateOrderRequest>()))
                .ReturnsAsync(validOrder);

            var controller = new OrdersController(mockOrderRepository.Object, _mocklogger.Object);


            // Act
            var result = await controller.SubmitOrder(OrderRequest);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal("GetOrderById", createdAtActionResult.ActionName);
            Assert.Equal(validOrder.Id, createdAtActionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task SubmitOrder_WithNullOrder_ReturnsBadRequest()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var controller = new OrdersController(mockOrderRepository.Object, _mocklogger.Object);


            // Act
            var result = await controller.SubmitOrder(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task GetOrderById_WithExistingId_ReturnsOkResult()
        {
            // Arrange
            var orderId = 1;
            var existingOrder = new OrderResposne { Id = orderId };
            mockOrderRepository.Setup(repo => repo.GetOrderByIdAsync(orderId))
                .ReturnsAsync(existingOrder);

            var controller = new OrdersController(mockOrderRepository.Object, _mocklogger.Object);


            // Act
            var result = await controller.GetOrderById(orderId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = (OkObjectResult)result;
            Assert.Equal(existingOrder, okObjectResult.Value);
        }

        [Fact]
        public async Task GetOrderById_WithNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var orderId = 1;
            var orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock.Setup(repo => repo.GetOrderByIdAsync(orderId))
                .ReturnsAsync((OrderResposne)null);

            var controller = new OrdersController(mockOrderRepository.Object, _mocklogger.Object);


            // Act
            var result = await controller.GetOrderById(orderId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
