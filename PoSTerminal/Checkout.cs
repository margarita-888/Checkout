using MyStore.Data;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyStore
{
    public class Checkout
    {
        public List<Product> ScannedProducts { get; private set; }

        public double TotalAmount { get; private set; }

        public Checkout()
        {
            ScannedProducts = new List<Product>();
        }

        public void Scan(string productCode)
        {
            if (string.IsNullOrEmpty(productCode))
            {
                Console.WriteLine("Valid product code must be provided.");
                return;
            }

            var product = ProductRepository.GetProduct(productCode);

            if (product == null)
                return;

            ScannedProducts.Add(product);
            if (product.Discount.HasValue)
                Console.WriteLine(product.ProductDescription + " " + product.DiscountDescription);
            else
                Console.WriteLine(product.ProductDescription);
            UpdateTotal(productCode);
        }

        public void UpdateTotal(string productCode)
        {
            if (string.IsNullOrEmpty(productCode))
            {
                Console.WriteLine("Valid product code must be provided.");
                return;
            }

            var product = ProductRepository.GetProduct(productCode);

            if (product == null)
                return;

            TotalAmount += product.PricePerUnit;

            if (IsVolumeDiscountQuantityReached(product))
            {
                TotalAmount -= product.VolumeDiscount;
                Console.WriteLine(product.VolumeDealDescription);
            }
        }

        public double GetTotal()
        {
            return TotalAmount;
        }

        private int ProductScannedCount(string productCode)
        {
            if (string.IsNullOrEmpty(productCode))
                return 0;

            var count = ScannedProducts.Where(p => p.ProductName == productCode).Count();
            return count;
        }

        private bool IsVolumeDiscountQuantityReached(Product product)
        {
            if (!product.HasVolumeDeal)
                return false;

            var count = ProductScannedCount(product.ProductName);

            if (count % product.VolumeQuantity == 0)
                return true;
            else return false;
        }
    }
}