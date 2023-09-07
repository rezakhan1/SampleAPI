using AutoMapper;
using SampleAPI.Entities;
using SampleAPI.Requests;
using SampleAPI.Response;

namespace SampleAPI.AutoMapperConfig
{
    /// <summary>
    /// MappingProfile
    /// </summary>
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            _ = CreateMap<CreateOrderRequest, Order>();
                CreateMap<Order, OrderResposne>(); 
        }
    }
}
