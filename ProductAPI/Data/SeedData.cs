using ProductAPI.Models;

namespace ProductAPI.Data
{
    public static class SeedData
    {
        public static void Seed(this ProductDbContext context)
        {
            if (!context.Products.Any())
            {
                var products =
                    new List<Product>()
                    {
                        new Product()
                        {
                            Name = "Keyboard",
                            Description = "Gaming Keyboard with lights",
                            Price = 2000,
                            ProductType = ProductType.PERIPHARALS
                        },
                        new Product()
                        {
                            Name = "Mouse",
                            Description = "Gaming Mouse",
                            Price = 1000,
                            ProductType = ProductType.PERIPHARALS
                        },
                        new Product()
                        {
                            Name = "Monitor",
                            Description = "HD Monitor",
                            Price = 5000,
                            ProductType = ProductType.MONITOR
                        },
                        new Product()
                        {
                            Name = "Cabinet",
                            ProductType = ProductType.EXTERNAL,
                            Description = "PC Cabinet with RGB lights",
                            Price = 3000
                        }
                    };

                context.Products.AddRange (products);
                context.SaveChanges();
            }
        }
    }
}
