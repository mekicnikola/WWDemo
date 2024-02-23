﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using WWDemo.Api.Requests;
using WWDemo.Application.DTOs;
using WWDemo.Application.Products.Commands.AddProduct;
using WWDemo.Application.Products.Queries.GetAllProducts;

namespace WWDemo.Api.Controllers
{
    [Route("[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> AddProduct(AddProductRequest request)
		{
			var result = await _mediator.Send(new AddProductCommand
			{
				Category = request.Category,
				Name = request.Name,
				Price = request.Price,
				SerialNumber = request.SerialNumber,
            });

            return Ok();
        }

		[HttpGet]
        [ProducesResponseType(typeof(List<ProductRepresentation>), StatusCodes.Status200OK)]
        public async Task<List<ProductRepresentation>> GetAllProducts()
		{
			var result = await _mediator.Send(new GetAllProductsQuery());

			return result;
		}

		[HttpGet("serial-number")]
		public async Task<IActionResult> GetProductBySerialNumber([FromRoute(Name = "serial-number")]int serialNumber)
		{
			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteProduct()
		{
			return Ok();
		}
	}
}
