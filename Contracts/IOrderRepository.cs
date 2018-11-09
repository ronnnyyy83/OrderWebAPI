using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int orderId);
        void CreateOrder(Order order);
        void UpdateOrder(Order dbOrder, Order order);
    }
}
