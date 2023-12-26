using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order.API.DataAccess;
using Order.API.DTOs;
using Order.API.Models;

namespace Order.API.Controllers
{
	public class OrderController:ControllerBase
	{

		private readonly AppDbContext _context;

		public OrderController(AppDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> Create(OrderCreateDto orderCreate)
		{
			var newOrder = new Models.Orderr
			{
				BuyerId = orderCreate.BuyerId,
				Status = OrderStatus.Suspend,
				Address = new Address { Line = orderCreate.Address.Line, Province = orderCreate.Address.Province, District = orderCreate.Address.District },
				CreatedDate = DateTime.Now
			};

			orderCreate.orderItems.ForEach(item =>
			{
				newOrder.Items.Add(new OrderItem() { Price = item.Price, ProductId = item.ProductId, Count = item.Count });
			});

			await _context.AddAsync(newOrder);

			await _context.SaveChangesAsync();

			var orderCreatedRequestEvent = new OrderCreatedRequestEvent()
			{
				BuyerId = orderCreate.BuyerId,
				OrderId = newOrder.Id,
				Payment = new PaymentMessage
				{
					CardName = orderCreate.payment.CardName,
					CardNumber = orderCreate.payment.CardNumber,
					Expiration = orderCreate.payment.Expiration,
					CVV = orderCreate.payment.CVV,
					TotalPrice = orderCreate.orderItems.Sum(x => x.Price * x.Count)
				},
			};

			orderCreate.orderItems.ForEach(item =>
			{
				orderCreatedRequestEvent.OrderItems.Add(new OrderItemMessage { Count = item.Count, ProductId = item.ProductId });
			});

			var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettingsConst.OrderSaga}"));

			await sendEndpoint.Send<IOrderCreatedRequestEvent>(orderCreatedRequestEvent);

			return Ok();
		}
	}
}
