using Catalog.API.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Catalog.API.Data;

public class CatalogContextSeed
{
    public static async void SeedData(IMongoCollection<Product> productCollection)
    {
        bool existProduct = productCollection.Find(p => true).Any();
        if (!existProduct)
        {
            await productCollection.InsertManyAsync(GetPreConfigureProducts());
        }
        //else
        //{
        //    await productCollection.DeleteManyAsync("{}");
        //    await productCollection.InsertManyAsync(GetPreConfigureProducts());
        //}
    }

    private static IEnumerable<Product> GetPreConfigureProducts()
    {
        return new List<Product>()
        {
            new Product()
            {
                Name ="IPhone X",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Repudiandae dolorum assumenda cumque ratione.",
                ImageFile = "product-1.png",
                Price = 599.99M,
                Category = "Smart Phone"
            },
            new Product()
            {
                Name ="NOKIA A10",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Repudiandae dolorum assumenda cumque ratione.",
                ImageFile = "product-2.png",
                Price = 1124.99M,
                Category = "Smart Phone"
            },
            new Product()
            {
                Name ="Samsung 10",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Repudiandae dolorum assumenda cumque ratione.",
                ImageFile = "product-3.png",
                Price = 760.99M,
                Category = "Smart Phone"
            },
            new Product()
            {
                Name ="Huawei Plus",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Repudiandae dolorum assumenda cumque ratione.",
                ImageFile = "product-3.png",
                Price = 936.11M,
                Category = "White Appliances"
            },
            new Product()
            {
                Name ="Xiaomi Mi 9",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Repudiandae dolorum assumenda cumque ratione.",
                ImageFile = "product-4.png",
                Price = 453.25M,
                Category = "White Appliances"
            },
            new Product()
            {
                Name ="HTC U11+ Plus",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Repudiandae dolorum assumenda cumque ratione.",
                ImageFile = "product-6.png",
                Price = 785.50M,
                Category = "Smart Phone"
            },
            new Product()
            {
                Name ="LG G7 ThinQ",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Repudiandae dolorum assumenda cumque ratione.",
                ImageFile = "product-5.png",
                Price = 1200.99M,
                Category = "Home Kitchen"
            },
        };
    }
}