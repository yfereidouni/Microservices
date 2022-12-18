using AutoMapper;
using Ordering.Application.Commands;
using Ordering.Application.Responses;
using Ordering.Core.Entities;

namespace Ordering.Application.Mapper;

public class OrderMappingprofile:Profile
{
	public OrderMappingprofile()
	{
		CreateMap<Order, OrderResponse>().ReverseMap();
		CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
	}
}
