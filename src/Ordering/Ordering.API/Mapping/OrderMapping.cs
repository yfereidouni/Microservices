using AutoMapper;
using EventBus.RabbitMQ.Events;
using Ordering.Application.Commands;

namespace Ordering.API.Mapping
{
    public class OrderMapping:Profile
    {
        public OrderMapping()
        {
            CreateMap<BasketCheckoutEvent, CheckoutOrderCommand>().ReverseMap();

        }
    }
}
