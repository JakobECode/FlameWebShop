﻿using WebApi.Context;
using WebApi.Helpers.Repositories.Base;
using WebApi.Models.Entities;

namespace WebApi.Helpers.Repositories
{
    public class OrderRepository : SqlRepository<OrderEntity>
    {
        public OrderRepository(SqlContext context) : base(context) 
        {
        }
    }
}
