using Contracts;
using Entities;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return FindAll()
                .OrderBy(o => o.OrderNo);
        }

        public Order GetOrderById(int orderId)
        {
            return FindByCondition(o => o.OrderId.Equals(orderId))
                    .FirstOrDefault();
        }

        public void CreateOrder(Order order)
        {            
            Create(order);
            Save();
        }

        public void UpdateOrder(Order dbOrder, Order order)
        {
            dbOrder.Name = order.Name;
            dbOrder.LastName = order.LastName;
            dbOrder.Price = order.Price;
            dbOrder.PostCode = order.PostCode;
            dbOrder.HouseNumber = order.HouseNumber;
            dbOrder.Street = order.Street;
            dbOrder.City = order.City;

            Update(dbOrder);
            Save();
        }
    }
}