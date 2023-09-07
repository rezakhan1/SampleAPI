using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Requests;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrdersController> _logger;

        /// <summary>
        /// OrdersController
        /// </summary>
        /// <param name="orderRepository"></param>
        /// <param name="logger"></param>
        public OrdersController(IOrderRepository orderRepository,
            ILogger<OrdersController> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        /// <summary>
        /// GetRecentOrders
        /// </summary>
        /// <returns>Order Response</returns>
        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentOrders()
        {
            try
            {
                var recentOrders = await _orderRepository.GetRecentOrdersAsync().ConfigureAwait(false);
                return Ok(recentOrders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching recent orders.");
                return StatusCode(500, $"An error occurred while retrieving recent orders. message={ex.Message}");
            }
        }

        /// <summary>
        /// SubmitOrder
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Order Response</returns>
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitOrder([FromBody] CreateOrderRequest order)
        {
            if (order == null)
            {
                return BadRequest("Invalid order data.");
            }

            try
            {
                var addedOrder = await _orderRepository.AddOrderAsync(order);
                return CreatedAtAction(nameof(GetOrderById), new { id = addedOrder.Id }, addedOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while submitting an order.");
                return StatusCode(500, "An error occurred while submitting the order.");
            }
        }

        /// <summary>
        /// GetOrderById
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Order Response</returns>

        [HttpGet("{id}", Name = "GetOrderById")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
    }
}
