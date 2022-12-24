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
using Ordering.API.Extensions;
using Ordering.API.RabbitMQ;

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
        Title = "Ordering.API",
        Description= "Ordering.API", 
        Contact = new OpenApiContact
        {
            Name = "Yasser FEREIDOUNI",
            Email = "Yasser.Fereidouni@gmail.com"
        }
    });
});

builder.Services.AddDbContext<OrderContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("OrderConnection")), ServiceLifetime.Singleton);

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(typeof(CheckoutOrderHandler).GetTypeInfo().Assembly);

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
builder.Services.AddSingleton<EventBusRabbitMQConsumer>();
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
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRabbitListener();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.Run();
