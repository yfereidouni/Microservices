using EventBus.RabbitMQ.Producer;
using EventBus.RabbitMQ;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Ordering.Application.Commands;
using Ordering.Core.Repositories;
using Ordering.Core.Repositories.Base;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories;
using Ordering.Infrastructure.Repositories.Base;
using RabbitMQ.Client;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Ordering.API",
        Description= "Ordering.API", 
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Yasser Fereidouni",
            Email = "Yasser.Fereidouni@gmail.com"
        }
    });
});

builder.Services.AddDbContext<OrderContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("OrderConnection"));
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(typeof(CheckoutOrderHandler).GetTypeInfo().Assembly);
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));


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

///Database Auto-Migration -------------------------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<OrderContext>();
    context.Database.Migrate();
    await OrderContextSeed.SeedAsync(services);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
