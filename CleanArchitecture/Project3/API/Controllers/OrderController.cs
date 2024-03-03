using Application.Cart.Commands.AddCart;
using Application.Client.Commands.DeleteClient;
using Application.Client.Queries.GetAllClient;
using Application.DTOs.Cart;
using Application.DTOs.Order;
using Application.DTOs.OrderDetail;
using Application.Order.Commands.AddOrder;
using Application.Order.Commands.DeleteOrder;
using Application.Order.Queries.GetOrder;
using Application.OrderDetail.Commands.CreateOrderDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] AddOrderDto order)
        {
            try
            {
                var command = new AddOrderCommand
                {
                    AddOrderDto = order
                };

                await _mediator.Send(command);

                return Ok("Order successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            throw new NotImplementedException();
        }
        [HttpPost("order-detail")]
        public async Task<IActionResult> AddOrderDetail([FromBody] AddOrderDetailDto orderItem)
        {
            try
            {
                var command = new CreateOrderDetailCommand
                {
                    AddOrderDetailDto = orderItem
                };

                await _mediator.Send(command);

                return Ok("Order successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            throw new NotImplementedException();
        }
        [HttpGet]
        public async Task<IActionResult> GetOrder()
        {
            var orderQuery = new GetOrderQuery();
            var orders = await _mediator.Send(orderQuery);
            return Ok(orders);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var command = new DeleteOrderCommand()
            {
                OrderId = id
            };
            var result = await _mediator.Send(command);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound($"Client {id} not found!");
        }
    }
}
