using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SampleAPI.AutoMapperConfig;
using SampleAPI.Entities;
using SampleAPI.Requests;
using SampleAPI.Response;

namespace SampleAPI.Repositories
{
    /// <summary>
    /// OrderRepository
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new();
        private readonly IMapper _mapper;
        private int _nextId = 1;

        /// <summary>
        /// OrderRepository
        /// </summary>
        public OrderRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

              _mapper = config.CreateMapper();
        }

        /// <summary>
        /// GetRecentOrdersAsync
        /// </summary>
        /// <returns>OrderResposne</returns>
        public async Task<IEnumerable<OrderResposne>> GetRecentOrdersAsync()
        {
            var cutoffDate = DateTime.Now.AddDays(-1);

            var orders=  _orders
                .Where(order => !order.IsDeleted && order.EntryDate >= cutoffDate)
                .OrderByDescending(order => order.EntryDate).ToList();
            return _mapper.Map<List<OrderResposne>>(orders);
        }

        /// <summary>
        /// GetOrderByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns>OrderResposne</returns>
        public async Task<OrderResposne> GetOrderByIdAsync(int id)
        {
            var order= _orders.FirstOrDefault(order => order.Id == id);
            return _mapper.Map<OrderResposne>(order);
        }

        /// <summary>
        /// GetOrderByIdAsync
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Order</returns>
        public async Task<Order> AddOrderAsync(CreateOrderRequest order)
        {          
            order.Id = _nextId++;
            order.EntryDate = DateTime.Now;
            var createdOrder = _mapper.Map<Order>(order); 
            _orders.Add(createdOrder);
            return createdOrder;
        }

       
    }
}
