﻿using Microsoft.EntityFrameworkCore;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories.Base;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(OrderContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<IEnumerable<Order>> GetOrderByUserName(string userName)
    {
        var orderList = await _dbContext.Orders
            .Where(o => o.UserName == userName) 
            .ToListAsync();

        return orderList;
    }
} 
