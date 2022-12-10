using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data;

public class CatalogContextSeed
{
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        bool existProduct = productCollection.Find(p => true).Any();
        if (!existProduct)
        {
            productCollection.InsertManyAsync(GetPreConfigureProducts());
        }
    }

    private static IEnumerable<Product> GetPreConfigureProducts()
    {
        return new List<Product>()
        {
            new Product()
            {
                Name ="IPhone X",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Repudiandae dolorum assumenda cumque a. Tempora nulla ipsa ab asperiores atque ratione.",
                ImageFile = "product-1.png",
                Price = 950.00M,
                Category = "Smart Phone"
            },
            new Product()
            {
                Name ="NOKIA A10",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Repudiandae dolorum assumenda cumque a. Tempora nulla ipsa ab asperiores atque ratione.",
                ImageFile = "product-2.png",
                Price = 950.00M,
                Category = "Smart Phone"
            },
            new Product()
            {
                Name ="Samsung 10",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Repudiandae dolorum assumenda cumque a. Tempora nulla ipsa ab asperiores atque ratione.",
                ImageFile = "product-3.png",
                Price = 950.00M,
                Category = "Smart Phone"
            },
            new Product()
            {
                Name ="Huawei Plus",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Repudiandae dolorum assumenda cumque a. Tempora nulla ipsa ab asperiores atque ratione.",
                ImageFile = "product-3.png",
                Price = 950.00M,
                Category = "Smart Phone"
            },
            new Product()
            {
                Name ="Xiaomi Mi 9",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Repudiandae dolorum assumenda cumque a. Tempora nulla ipsa ab asperiores atque ratione.",
                ImageFile = "product-4.png",
                Price = 950.00M,
                Category = "White Appliances"
            },
            new Product()
            {
                Name ="HTC U11+ Plus",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Repudiandae dolorum assumenda cumque a. Tempora nulla ipsa ab asperiores atque ratione.",
                ImageFile = "product-6.png",
                Price = 950.00M,
                Category = "Smart Phone"
            },
            new Product()
            {
                Name ="LG G7 ThinQ",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Repudiandae dolorum assumenda cumque a. Tempora nulla ipsa ab asperiores atque ratione.",
                ImageFile = "product-5.png",
                Price = 950.00M,
                Category = "Home Kitchen"
            },
            
        };
    }
}
