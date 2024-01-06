using MassTransit;
using Shared;
using Stock.API.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
	x.AddConsumer<OrderCreatedEventConsumer>();

	x.AddConsumer<StockRollBackMessageConsumer>();

	x.UsingRabbitMq((context, cfg) =>
	{
		cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"));

		cfg.ReceiveEndpoint(RabbitMQSettingsConst.StockOrderCreatedEventQueueName, e =>
		{
			e.ConfigureConsumer<OrderCreatedEventConsumer>(context);
		});

		cfg.ReceiveEndpoint(RabbitMQSettingsConst.StockRollBackMessageQueueName, e =>
		{
			e.ConfigureConsumer<StockRollBackMessageConsumer>(context);
		});
	});
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
