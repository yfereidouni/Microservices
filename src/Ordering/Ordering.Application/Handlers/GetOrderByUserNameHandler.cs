﻿using MediatR;
using Ordering.Application.Mapper;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers;

public class GetOrderByUserNameHandler : IRequestHandler<GetOrderByUserNameQuery, IEnumerable<OrderResponse>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByUserNameHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<OrderResponse>> Handle(GetOrderByUserNameQuery request, CancellationToken cancellationToken)
    {
        var orderList = await _orderRepository.GetOrderByUserName(request.UserName);

        var orderResponse = OrderMapper.Mapper.Map<IEnumerable<OrderResponse>>(orderList);

        return orderResponse;
    }
}
