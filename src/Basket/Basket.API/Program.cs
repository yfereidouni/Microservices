using Basket.API.Data;
using Basket.API.Data.Interfaces;
using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Description = "Basket.API",
        Contact = new OpenApiContact()
        {
            Name = "Yasser FEREIDOUNI",
            Email = "Yasser.Fereidouni@gmail.com",
        }
    });
});

builder.Services.AddTransient<IBasketContext, BasketContext>();
builder.Services.AddTransient<IBasketRepository, BasketRepository>();

///Configure REDIS -------------------------------
builder.Services.AddSingleton<ConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("RedisCnnStr"), true);
    return ConnectionMultiplexer.Connect(configuration);
});
///-----------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket-API-V1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
