﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Order.API.Models
{
	public class OrderItem
	{
		public int Id { get; set; }
		public int ProductId { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

		public int OrderId { get; set; }

		public Orderr Order { get; set; }

		public int Count { get; set; }
	}
}
