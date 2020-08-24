using MyStore.Data;
using MyStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyStore
{
    internal class Program
    {
        private static List<Product> products = ProductRepository.GetProducts().ToList();

        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Checkout!\nAvailable today:");
            Console.WriteLine("=====================================");
            products.ForEach(p => Console.WriteLine(p.ToString()));
            Console.WriteLine("=====================================");

            Console.WriteLine("To scan your next product enter product code 1,...z,...9 as per list above.\nTo PAY enter P.");
            Checkout checkout = new Checkout();
            try
            {
                ScanProduct(checkout);
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong product code. Try again");
                ScanProduct(checkout);
            }
        }

        private static void ScanProduct(Checkout terminal)
        {
            var answer = Console.ReadLine().ToLower();
            while (answer != "p")
            {
                if (!(products.Select(p => p.ProductCode.ToString()).Contains(answer)) && !(products.Select(p => p.ProductCode.ToString()).Contains(answer.ToUpper())))
                    Console.WriteLine("Wrong product code. Try again");
                else
                    terminal.Scan(answer);

                answer = Console.ReadLine().ToLower();
            }

            Console.WriteLine("=====================================");
            Console.Write($"\tTotal:\t\t${string.Format("{0:0.00}", terminal.GetTotal())}\n");
            Console.Write($"\tTotal Saved: \t${string.Format("{0:0.00}", terminal.GetTotalSavings())}\n");
            Console.WriteLine("=====================================");
        }
    }
}