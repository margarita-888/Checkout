using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyStore.Data
{
    public class ProductRepository
    {
        public static List<Product> Products = new List<Product>()
        {
            new Product{ ProductCode = "1", ProductName = "Strawberries", PricePerUnit=2.70, Unit="pk", VolumeQuantity=2, VolumePrice=5.00 },
            new Product{ ProductCode = "2", ProductName = "Bluberries", PricePerUnit=3.50, Unit="pk", VolumeQuantity=3, VolumePrice=10.00  },
            new Product{ ProductCode = "3", ProductName = "Apples", PricePerUnit=4.50, Unit="kg" },
            new Product{ ProductCode = "4", ProductName = "Cheddar cheese", PricePerUnit=8.80, Unit="ea" },
            new Product{ ProductCode = "5",  ProductName = "Butter", PricePerUnit=5.00, Unit="ea" },
            new Product{ ProductCode = "6",  ProductName = "Milk", PricePerUnit=4.60, Unit="ea" },
            new Product{ ProductCode = "z",  ProductName = "Salmon", PricePerUnit=12.00, Unit="pk"  },
            new Product{ ProductCode = "8",  ProductName = "Chicken", PricePerUnit=10.50, Unit="ea", Discount = 1.00 },
            new Product{ ProductCode = "9",  ProductName = "Pistachio", PricePerUnit=8.00, Unit="pk", Discount = 3.00 },
        };

        public static Product GetProduct(string productCode)
        {
            if (Products == null)
                return null;

            return Products.Where(p => p.ProductCode == productCode).FirstOrDefault();
        }
    }
}