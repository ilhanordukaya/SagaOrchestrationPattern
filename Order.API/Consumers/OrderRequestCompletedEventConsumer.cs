﻿using MassTransit;
using Order.API.DataAccess;
using Order.API.Models;
using Shared.Interfaces;

namespace Order.API.Consumers
{
	public class OrderRequestCompletedEventConsumer : IConsumer<IOrderRequestCompletedEvent>
	{
		private readonly OrderAppDbContext _context;

		private readonly ILogger<OrderRequestCompletedEventConsumer> _logger;

		public OrderRequestCompletedEventConsumer(OrderAppDbContext context, ILogger<OrderRequestCompletedEventConsumer> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task Consume(ConsumeContext<IOrderRequestCompletedEvent> context)
		{
			var order = await _context.Orders.FindAsync(context.Message.OrderId);

			if (order != null)
			{
				order.Status = OrderStatus.Complete;
				await _context.SaveChangesAsync();

				_logger.LogInformation($"Order (Id={context.Message.OrderId}) status changed : {order.Status}");
			}
			else
			{
				_logger.LogError($"Order (Id={context.Message.OrderId}) not found");
			}
		}
	}
}
