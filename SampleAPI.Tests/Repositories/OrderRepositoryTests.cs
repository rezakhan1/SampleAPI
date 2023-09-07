using AutoMapper;
using FluentAssertions;
using FluentAssertions.Extensions;
using Moq;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Requests;
using SampleAPI.Response;

namespace SampleAPI.Tests.Repositories
{
    public class OrderRepositoryTests
    {
        private readonly OrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderRepositoryTests()
        {
            // Create a mock IMapper
            var mockMapper = new Mock<IMapper>();
            _mapper = mockMapper.Object;

            // Initialize the OrderRepository with the mock IMapper
            _orderRepository = new OrderRepository();
        }


        [Fact]
        public async Task GetRecentOrdersAsync_ShouldReturnRecentOrders()
        {
            // Arrange
            var cutoffDate = DateTime.Now.AddDays(-1);
            _ = _orderRepository.AddOrderAsync(new CreateOrderRequest { Name = "Order1", EntryDate = DateTime.Now });
            _ = _orderRepository.AddOrderAsync(new CreateOrderRequest { Name = "Order2", EntryDate = DateTime.Now.AddDays(-2) });

            // Act
            var recentOrders = await _orderRepository.GetRecentOrdersAsync();

            // Assert
            Assert.Equal(2, recentOrders.Count());
            Assert.Equal("Order2", recentOrders.First().Name);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnsOrderById()
        {
            // Arrange
            var createdOrder = await _orderRepository.AddOrderAsync(new CreateOrderRequest { Name = "Order1" });

            // Act
            var retrievedOrder = await _orderRepository.GetOrderByIdAsync(createdOrder.Id);

            // Assert
            Assert.Equal(createdOrder.Id, retrievedOrder.Id);
            Assert.Equal(createdOrder.Name, retrievedOrder.Name);
        }

        [Fact]
        public async Task AddOrderAsync_AddsOrderToRepository()
        {
            // Arrange
            var orderToAdd = new CreateOrderRequest { Name = "NewOrder" };

            // Act
            var addedOrder = await _orderRepository.AddOrderAsync(orderToAdd);

            // Assert
            Assert.NotNull(addedOrder);
            Assert.Equal(orderToAdd.Name, addedOrder.Name);
        }
    }
}