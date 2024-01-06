using MassTransit;
using Microsoft.EntityFrameworkCore;
using SagaStateMachineWorkerService;
using SagaStateMachineWorkerService.DataAccess;
using SagaStateMachineWorkerService.Models;
using Shared;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
	.ConfigureServices((hostContext,services) =>
	{
		services.AddMassTransit( cfg=>
		{
			cfg.AddSagaStateMachine<OrderStateMachine, OrderStateInstance>().EntityFrameworkRepository(opt =>
			{
				opt.AddDbContext<DbContext, OrderStateDbContext>((provider,builder)=>
				{
					builder.UseNpgsql(hostContext.Configuration.GetConnectionString("Postgres"), m =>
					{
						m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
					});
				});
			});

			cfg.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(configure =>
			{
				configure.Host(hostContext.Configuration.GetConnectionString("RabbitMQ"));

				configure.ReceiveEndpoint(RabbitMQSettingsConst.OrderSaga, e =>
				{
					e.ConfigureSaga<OrderStateInstance>(provider);
				});
			}));
		
	});


		
		//services.AddMassTransitHostedService();
		services.AddHostedService<Worker>();
	})
	.Build();

host.Run();
