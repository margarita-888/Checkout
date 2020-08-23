using MyStore.Data;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyStore
{
    internal class Program
    {
        private static List<Product> products = ProductRepository.Products;

        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Checkout!\nAvailable today:");
            Console.WriteLine("====================================");
            products.ForEach(p => Console.WriteLine(p.ToString()));
            Console.WriteLine("====================================");

            Console.WriteLine("To scan your next product enter product code 1,...z,...9 as per list above.\nTo PAY enter P.");

            try
            {
                ScanProducts();
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong product code. Try again");
                ScanProducts();
            }
        }

        private static void ScanProducts()
        {
            Checkout terminal = new Checkout();

            var answer = Console.ReadLine().ToLower();
            while (answer != "p")
            {
                if (!(products.Select(p => p.ProductCode.ToString()).Contains(answer)) && !(products.Select(p => p.ProductCode.ToString()).Contains(answer.ToUpper())))
                    Console.WriteLine("Wrong product code. Try again");
                else
                    terminal.Scan(answer);

                answer = Console.ReadLine().ToLower();
            }

            Console.WriteLine("====================================");
            Console.Write($"\tTotal: ${string.Format("{0:0.00}", terminal.GetTotal())}\n");
        }
    }
}