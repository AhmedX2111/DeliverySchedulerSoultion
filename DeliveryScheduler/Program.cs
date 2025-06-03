using DeliveryScheduler.Application.Interfaces;
using DeliveryScheduler.Application.Services;
using DeliveryScheduler.Application.UseCases;
using DeliveryScheduler.Domain.Interfaces;
using DeliveryScheduler.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddSingleton<IDeliverySlotService, DeliverySlotService>();
builder.Services.AddScoped<GetAvailableDeliverySlots>();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAngularDevServer",
		builder => builder
			.WithOrigins("http://localhost:4200") // Angular dev server
			.AllowAnyMethod()
			.AllowAnyHeader());
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

//app.UseHttpsRedirection();

app.UseCors("AllowAngularDevServer");

app.UseAuthorization();

app.MapControllers();

app.Run();
