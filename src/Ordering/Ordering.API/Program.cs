using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderContext>(opt => 
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("OrderConnection"));
});



var app = builder.Build();

///Database Auto-Migration -------------------------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<OrderContext>();

    //Auto-Migrate Database
    context.Database.Migrate();

    // Preparing default values for DB
    await OrderContextSeed.SeedAsync(services);


    //var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    //try
    //{
    //    ///SeedDatabase------------------------------------------------------------------------------------
    //    await OrderContextSeed.SeedAsync(context, loggerFactory);
    //}
    //catch (Exception ex)
    //{
    //    var logger = loggerFactory.CreateLogger<Program>();
    //    logger.LogError(ex.Message);
    //}
}


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
