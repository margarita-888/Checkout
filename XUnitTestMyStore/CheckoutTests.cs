using MyStore;
using MyStore.Data;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestMyStore
{
    public class CheckoutTests
    {
        private readonly Checkout checkout = new Checkout();

        [Fact]
        public void TotalOfAllProduct_Equals5960()
        {
            //arrange
            List<Product> products = ProductRepository.GetProducts().ToList();

            //act
            foreach (var product in products)
            {
                checkout.Scan(product.ProductCode);
            }

            //assert
            Assert.Equal(59.60, checkout.TotalAmount, 2);
            Assert.Equal(4.00, checkout.TotalSaved, 2);
        }

        [Fact]
        public void TotalOfVolumeDeals_Equals15()
        {
            //arrange
            List<Product> products = ProductRepository.GetProductsByProductCode(new string[] { "1", "2", "2", "1", "2" });

            //act
            foreach (var product in products)
            {
                checkout.Scan(product.ProductCode);
            }

            //assert
            Assert.Equal(15.00, checkout.TotalAmount, 2);
            Assert.Equal(0.90, checkout.TotalSaved, 2);
        }

        [Fact]
        public void TotalOfVolumeDeals_PlusExtras_Equals2120()
        {
            //arrange
            List<Product> products = ProductRepository.GetProductsByProductCode(new string[] { "1", "2", "2", "1", "2", "1", "2" });

            //act
            foreach (var product in products)
            {
                checkout.Scan(product.ProductCode);
            }

            //assert
            Assert.Equal(21.20, checkout.TotalAmount, 2);
            Assert.Equal(0.90, checkout.TotalSaved, 2);
        }

        [Fact]
        public void GetProduct_WithEmptyProductCode_ReturnsNull()
        {
            //arrange

            //act
            Product product = ProductRepository.GetProduct("");

            //assert
            Assert.Null(product);
        }
    }
}