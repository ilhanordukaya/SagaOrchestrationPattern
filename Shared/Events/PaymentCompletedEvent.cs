﻿using Shared.Interfaces;

namespace Shared.Events
{
	public class PaymentCompletedEvent : IPaymentCompletedEvent
	{
		public PaymentCompletedEvent(Guid correlationId)
		{
			CorrelationId = correlationId;
		}

		public Guid CorrelationId { get; }
	}
}
