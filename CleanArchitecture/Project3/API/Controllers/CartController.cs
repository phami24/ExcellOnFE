using Application.Cart.Commands.AddCart;
using Application.Cart.Queries.GetCartByClientId;
using Application.Cart.Queries.GetTotalPrice;
using Application.Client.Queries.GetTotalClient;
using Application.DTOs.Cart;
using Application.ServiceCharges.Queries.GetServiceChargesByServiceId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart( [FromBody] AddToCartDto cartItem)
        {
            try
            {
                var command = new AddToCartCommand
                {
                   AddToCartDto = cartItem
                };

                await _mediator.Send(command);

                return Ok("Item added to cart successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("getByCustomerId/{clientId}")]
        public async Task<IActionResult> GetByClientId(int clientId)
        {
            try
            {
                var query = new GetCartByClientIdQuery()
                {
                    Id = clientId
                };
                var cart = await _mediator.Send(query);
                if (cart != null)
                {
                    return Ok(cart);
                }
                else
                {
                    return NotFound($"Client with ID {clientId} not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the request: {ex.Message}");
            }
        }
        [HttpGet("total-price/{clientId}")]
        public async Task<IActionResult> GetTotalPrice(int clientId)
        {
            var query = new GetTotalPriceQuery();
            var totalItem = await _mediator.Send(query);
            return Ok(totalItem);
        }
    }
}
