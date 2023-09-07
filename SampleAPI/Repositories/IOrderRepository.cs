using SampleAPI.Entities;
using SampleAPI.Requests;
using SampleAPI.Response;

namespace SampleAPI.Repositories
{
    public interface IOrderRepository
    {
        /// <summary>
        /// GetRecentOrdersAsync
        /// </summary>
        /// <returns>OrderResposne</returns>
        Task<IEnumerable<OrderResposne>> GetRecentOrdersAsync();

        /// <summary>
        /// Order
        /// </summary>
        /// <param name="id"></param>
        /// <returns>OrderResposne</returns>
        Task<OrderResposne> GetOrderByIdAsync(int id);

        /// <summary>
        /// AddOrderAsync
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Order</returns>
        Task<Order> AddOrderAsync(CreateOrderRequest order);

    }
}
