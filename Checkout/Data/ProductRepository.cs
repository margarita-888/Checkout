using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyStore.Data
{
    public class ProductRepository
    {
        private static readonly List<Product> Products = new List<Product>()
        {
            new Product{ ProductCode = "1", ProductName = "Strawberries", PricePerUnit=2.70, Unit="pk", VolumeQuantity=2, VolumePrice=5.00 },
            new Product{ ProductCode = "2", ProductName = "Bluberries", PricePerUnit=3.50, Unit="pk", VolumeQuantity=3, VolumePrice=10.00  },
            new Product{ ProductCode = "3", ProductName = "Apples", PricePerUnit=4.50, Unit="kg" },
            new Product{ ProductCode = "4", ProductName = "Cheddar cheese", PricePerUnit=8.80, Unit="ea" },
            new Product{ ProductCode = "5",  ProductName = "Butter", PricePerUnit=5.00, Unit="ea" },
            new Product{ ProductCode = "6",  ProductName = "Milk", PricePerUnit=4.60, Unit="ea" },
            new Product{ ProductCode = "z",  ProductName = "Salmon", PricePerUnit=12.00, Unit="pk"  },
            new Product{ ProductCode = "8",  ProductName = "Chicken", PricePerUnit=11.50, Unit="ea", Discount = 1.00 },
            new Product{ ProductCode = "9",  ProductName = "Pistachio", PricePerUnit=11.00, Unit="pk", Discount = 3.00 },
        };

        public static IEnumerable<Product> GetProducts()
        {
            if (Products == null)
                return null;

            return Products;
        }

        public static Product GetProduct(string productCode)
        {
            if (string.IsNullOrEmpty(productCode))
            {
                Console.WriteLine("ProductRepository::GetProductsByProductCode. Product code must be provided.");
                return null;
            }

            return Products.Where(p => p.ProductCode == productCode).FirstOrDefault();
        }

        public static List<Product> GetProductsByProductCode(string[] codes)
        {
            if (codes == null)
            {
                Console.WriteLine("ProductRepository::GetProductsByProductCode. Product codes must be provided.");
                return null;
            }

            List<Product> products = new List<Product>();
            foreach (var code in codes)
            {
                products.Add(GetProduct(code));
            }
            return products;
        }
    }
}