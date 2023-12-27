﻿using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using SagaStateMachineWorkerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaStateMachineWorkerService.DataAccess
{
	public class OrderStateDbContext : SagaDbContext
	{
		public OrderStateDbContext(DbContextOptions<OrderStateDbContext> options) : base(options)
		{
		}



		protected override IEnumerable<ISagaClassMap> Configurations
		{
			get { yield return new OrderStateMap(); }
		}
	}
}
