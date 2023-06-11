using System.Collections.Generic;
using ProductUIAutomationTests.Model;

namespace ProductUIAutomationTests.TestData
{
    public class ProductDataGenerator
    {
        public static IEnumerable<object[]> NewProduct()
        {
            yield return new object[]
            {
                new Product()
                {
                    Name = "Pendrive",
                    Description = "Low capacity Pendrive",
                    Price = 700,
                    ProductType = ProductType.EXTERNAL
                }
            };
        }

        public static IEnumerable<object[]> UpdateProduct()
        {
            yield return new object[]
            {
                new Product()
                {
                    Name = "Pendrive",
                    Description = "High capacity Pendrive",
                    Price = 1400,
                    ProductType = ProductType.EXTERNAL
                }
            };
        }
    }
}
