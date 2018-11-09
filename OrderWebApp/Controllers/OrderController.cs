using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderWebApp.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private IRepositoryWrapper _repository;

        public OrderController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            try
            {
                var orders = _repository.Order.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                //Log exception
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name= "OrderById")]
        public IActionResult GetOrderById(int id)
        {
            try
            {
                var order = _repository.Order.GetOrderById(id);

                if (order == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(order);
                }
            }
            catch (Exception ex)
            {
                //Log exception
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody]Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest("Order object is null");
                }

                if (!ModelState.IsValid)
                {                    
                    return BadRequest("Invalid model state");
                }

                _repository.Order.CreateOrder(order);

                return CreatedAtRoute("OrderById", new { id = order.OrderId }, order);
            }
            catch (Exception ex)
            {
                //Log exception
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody]Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest("Order object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model state");
                }

                var dbOrder = _repository.Order.GetOrderById(id);
                if (dbOrder == null)
                {
                    return NotFound();
                }

                _repository.Order.UpdateOrder(dbOrder, order);

                return NoContent();
            }
            catch (Exception ex)
            {
                //Log exception
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
