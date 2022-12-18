using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Ordering.Application.Mapper;
using Ordering.Application.Responses;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Commands;

public class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, OrderResponse>
{
    private readonly IOrderRepository _orderRepository;

    public CheckoutOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderResponse> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = OrderMapper.Mapper.Map<Order>(request);
        
        if (orderEntity == null)
        {
            throw new ApplicationException("Not mapped!");
        }

        var newOrder = await _orderRepository.AddAsync(orderEntity);

        var orderResponse = OrderMapper.Mapper.Map<OrderResponse>(newOrder);
        
        return orderResponse;
    }
}
