using Basket.API.Data;
using Basket.API.Data.Interfaces;
using Basket.API.Mapping;
using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using EventBus.RabbitMQ;
using EventBus.RabbitMQ.Producer;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Basket.API",
        Description = "Basket.API",
        Contact = new OpenApiContact
        {
            Name = "Yasser FEREIDOUNI",
            Email = "Yasser.Fereidouni@gmail.com",
        }
    });
});

builder.Services.AddTransient<IBasketContext, BasketContext>();
builder.Services.AddTransient<IBasketRepository, BasketRepository>();
builder.Services.AddAutoMapper(typeof(BasketMapping));

///Configure REDIS -------------------------------
builder.Services.AddSingleton<ConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("RedisCnnStr"), true);
    return ConnectionMultiplexer.Connect(configuration);
});
///-----------------------------------------------


///RabbitMQ Connection Configuration -------------
builder.Services.AddSingleton<IRabbitMQConnection>(sp =>
{
    var factory = new ConnectionFactory()
    {
        HostName = builder.Configuration.GetSection("EventBus:HostName").Value,
        UserName = "guest",
        Password = "guest"
    };
    return new RabbitMQConnection(factory);
});
builder.Services.AddSingleton<EventBusRabbitMQProducer>();
///-----------------------------------------------



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseAuthorization();

//app.UseHttpsRedirection();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.Run();
