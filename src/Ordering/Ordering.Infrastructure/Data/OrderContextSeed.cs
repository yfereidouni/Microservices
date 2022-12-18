using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data;

#region Course-Way
//public class OrderContextSeed
//{
//public static async Task SeedAsync(OrderContext orderContext, ILoggerFactory loggerFactory, int? retry = 0)
//{
//    int retryForAvailability = retry.Value;

//    try
//    {
//        orderContext.Database.Migrate();

//        if (!orderContext.Orders.Any())
//        {
//            orderContext.Orders.AddRange(GetPreconfiguredOrders());
//            await orderContext.SaveChangesAsync();
//        }
//    }
//    catch (Exception exception)
//    {
//        if (retryForAvailability < 3)
//        {
//            retryForAvailability++;
//            var log = loggerFactory.CreateLogger<OrderContextSeed>();
//            log.LogError(exception.Message);
//            await SeedAsync(orderContext, loggerFactory, retryForAvailability);
//        }
//        throw;
//    }
//}

//private static IEnumerable<Order> GetPreconfiguredOrders()
//{
//    return new List<Order>()
//    {
//        new Order(){UserName="YF",FirstName="Yasser",LastName="Fereidoui",EmailAddress="Yasser.Fereidouni@gmail.com",Country="Austria", PaymentMethod="Online",AddressLine="Gumpoldskirchen",CardName="Yasser Fereidouni" ,CardNumber="5555 5555 5555 4444",CVV="123",TotalPrice=1200,ZipCode="2352",State="Upper Austria" },
//        new Order(){UserName="MP",FirstName="Markus",LastName="Pinezich",EmailAddress="Markus.Pinezich@gmail.com",Country="Austria", PaymentMethod="Online",AddressLine="Gumpoldskirchen",CardName="Markus Pinezich" ,CardNumber="5555 3333 2121 4444",CVV="324",TotalPrice=1200,ZipCode="2432",State="Lower Austria" },
//    };
//}
//}
#endregion

public static class OrderContextSeed
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using (var context = new OrderContext(
             serviceProvider.GetRequiredService<DbContextOptions<OrderContext>>()))
        {
            if (!context.Orders.Any())
            {
                context.Database.Migrate();

                Console.WriteLine("--> Seeding Data...");

                context.Orders.AddRange(
                    new Order() { UserName = "YF", FirstName = "Yasser", LastName = "Fereidoui", EmailAddress = "Yasser.Fereidouni@gmail.com", Country = "Austria", PaymentMethod = "Online", AddressLine = "Gumpoldskirchen", CardName = "Yasser Fereidouni", CardNumber = "5555 5555 5555 4444", CVV = "123", TotalPrice = 1200, ZipCode = "2352", State = "Upper Austria",Expiration="06/2026" },
                    new Order() { UserName = "MP", FirstName = "Markus", LastName = "Pinezich", EmailAddress = "Markus.Pinezich@gmail.com", Country = "Austria", PaymentMethod = "Online", AddressLine = "Gumpoldskirchen", CardName = "Markus Pinezich", CardNumber = "5555 3333 2121 4444", CVV = "324", TotalPrice = 1200, ZipCode = "2432", State = "Lower Austria", Expiration = "04/2027" }
                );
                await context.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("--> We already have data!");
            }
        }

    }
}