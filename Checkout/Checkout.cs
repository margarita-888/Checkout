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
        private readonly List<Product> scannedProducts = new List<Product>();

        public double TotalAmount { get; private set; }
        public double TotalSaved { get; private set; }

        public void Scan(string productCode)
        {
            if (string.IsNullOrEmpty(productCode))
            {
                Console.WriteLine("Valid product code must be provided.");
                return;
            }

            try
            {
                var product = ProductRepository.GetProduct(productCode);

                if (product == null)
                    return;

                scannedProducts.Add(product);
                if (product.Discount.HasValue)
                    Console.WriteLine(product.ProductDescription + " " + product.DiscountDescription);
                else
                    Console.WriteLine(product.ProductDescription);
                UpdateTotal(productCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Checkout::Scan. Error {ex}.");
            }
        }

        // Total amount is calculated by adding product price to the existing total, minus any discount whether product or volume if the volume quantity has been reached
        public void UpdateTotal(string productCode)
        {
            if (string.IsNullOrEmpty(productCode))
            {
                Console.WriteLine("Valid product code must be provided.");
                return;
            }

            try
            {
                var product = ProductRepository.GetProduct(productCode);

                if (product == null)
                {
                    Console.WriteLine($"Product with code {productCode} was not found.");
                    return;
                }

                TotalAmount += product.PricePerUnit;

                if (product.Discount.HasValue)
                {
                    TotalAmount -= product.Discount.Value;
                    TotalSaved += product.Discount.Value;
                }

                if (IsVolumeDiscountQuantityReached(product))
                {
                    TotalAmount -= product.VolumeDiscount;
                    TotalSaved += product.VolumeDiscount;
                    Console.WriteLine(product.VolumeDealDescription);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Checkout::UpdateTotal. Error {ex}.");
            }
        }

        public double GetTotal()
        {
            return TotalAmount;
        }

        public double GetTotalSavings()
        {
            return TotalSaved;
        }

        private int ProductScannedCount(string productCode)
        {
            if (string.IsNullOrEmpty(productCode))
                return 0;

            var count = scannedProducts.Where(p => p.ProductName == productCode).Count();
            return count;
        }

        private bool IsVolumeDiscountQuantityReached(Product product)
        {
            if (product == null)
            {
                return false;
            }

            if (!product.HasVolumeDeal)
                return false;

            var count = ProductScannedCount(product.ProductName);

            if (count % product.VolumeQuantity == 0)
                return true;
            else return false;
        }
    }
}