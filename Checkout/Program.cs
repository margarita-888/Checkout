using MyStore.Data;
using MyStore.Models;
using System;
using System.Linq;

namespace MyStore
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Checkout terminal = new Checkout();
            var products = ProductRepository.Products;

            Console.WriteLine("Welcome to our checkout!\nAvailable today:");
            Console.WriteLine("====================================");
            products.ForEach(p => Console.WriteLine(p.ToString()));
            Console.WriteLine("====================================");

            Console.WriteLine("To scan your next product enter product code 1...9 as per list above.\nTo PAY enter P.");

            var answer = Console.ReadLine().ToUpper();
            while (answer != "P")
            {
                if (!(products.Select(p => p.ProductCode.ToString()).Contains(answer)))
                    Console.WriteLine("Wrong product code. Try again");
                else
                    terminal.Scan(answer);

                answer = Console.ReadLine().ToUpper();
            }

            Console.WriteLine("====================================");
            Console.Write($"\tTotal: ${string.Format("{0:0.00}", terminal.GetTotal())}\n");
        }
    }
}