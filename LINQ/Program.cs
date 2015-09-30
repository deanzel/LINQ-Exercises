using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;

namespace LINQ
{
    internal class Program
    {
        /* Practice your LINQ!
         * You can use the methods in Data Loader to load products, customers, and some sample numbers
         * 
         * NumbersA, NumbersB, and NumbersC contain some ints
         * 
         * The product data is flat, with just product information
         * 
         * The customer data is hierarchical as customers have zero to many orders
         */

        private static void Main()
        {
            //PrintOutOfStock();
            //InStockMoreThan3();
            //CustomersInWashington();
            //NameOfProducts();
            //UnitPriceIncrease25();
            //NameOfProductsUpper();
            //EvenUnitsStock();
            //ProductsName8();
            PairsFromBC();

            Console.ReadLine();
        }

        private static void PrintOutOfStock()
        {
            var products = DataLoader.LoadProducts();

            //var results = products.Where(p => p.UnitsInStock == 0);
            var results = from p in products
                where p.UnitsInStock == 0
                select p;

            foreach (var product in results)
            {
                Console.WriteLine(product.ProductName);
            }
        }

        //2. Find all products that are in stock and cost more than 3.00 per unit.
        private static void InStockMoreThan3()
        {
            var products = DataLoader.LoadProducts();

            //var results = products.Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3);
            var results = from p in products
                where p.UnitsInStock > 0 && p.UnitPrice > 3
                select p;

            foreach (var product in results)
            {
                Console.WriteLine("{0} has {1} in stock with unit price {2}", product.ProductName,
                    product.UnitsInStock, product.UnitPrice);
            }
        }

        //3. Find all customers in Washington, print their name then their orders. (Region == "WA")
        private static void CustomersInWashington()
        {
            var customers = DataLoader.LoadCustomers();

            var results = from c in customers
                where c.Region == "WA"
                select c;

            //var resultsM = customers.Where(c => c.Region == "WA");

            foreach (var customer in results)
            {
                Console.WriteLine(customer.CompanyName);

                for (int i = 0; i < customer.Orders.Length; i++)
                {
                    Console.WriteLine("\t{0}", customer.Orders[i].OrderID);
                }
            }
        }

        //4. Create a new sequence with just the names of the products.
        private static void NameOfProducts()
        {
            var products = DataLoader.LoadProducts();

            var results = from p in products
                select p;

            //var resultsM = products.Select(p => p);

            foreach (var product in results)
            {
                Console.WriteLine(product.ProductName);
            }

        }

        //5. Create a new sequence of products and unit prices where the unit prices are increased by 25%.
        private static void UnitPriceIncrease25()
        {
            var products = DataLoader.LoadProducts();

            var results = products.Select(p => p);

            foreach (var product in results)
            {
                Console.WriteLine("{0}'s price is now {1}", product.ProductName, product.UnitPrice*5/4);
            }
        }

        //6. Create a new sequence of just product names in all upper case.
        private static void NameOfProductsUpper()
        {
            var products = DataLoader.LoadProducts();

            var results = products.Select(p => p);

            foreach (var product in results)
            {
                Console.WriteLine(product.ProductName.ToUpper());
            }

        }

        //7. Create a new sequence with products with even numbers of units in stock.
        private static void EvenUnitsStock()
        {
            var products = DataLoader.LoadProducts();
            var results = products.Where(p => (p.UnitsInStock%2 == 0 && p.UnitsInStock > 0));

            foreach (var product in results)
            {
                Console.WriteLine("{0} has {1} units in stock.", product.ProductName, product.UnitsInStock);
            }
        }

        //8. Create a new sequence of products with ProductName, Category, and rename UnitPrice to Price.
        private static void ProductsName8()
        {
            var products = DataLoader.LoadProducts();
            var results = from product in products
                select new
                {
                    product.ProductName,
                    product.Category,
                    Price = product.UnitPrice
                };

            //var results = products.Select(product => new
            //{
            //    product.ProductName,
            //    product.Category,
            //    Price = product.UnitPrice
            //});

            foreach (var product in results)
            {
                Console.WriteLine("{0} is of {1} and has a price of {2}", product.ProductName, product.Category,
                    product.Price);
            }
        }

        //9. Make a query that returns all pairs of numbers from both arrays such that the number from numbersB is less than the number from numbersC.
        public class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private static void PairsFromBC()
        {
            int[] NumbersB = DataLoader.NumbersB;
            int[] NumbersC = DataLoader.NumbersC;
            List<Point> BCPairs = new List<Point>();

            for (int b = 0; b < NumbersB.Length; b++)
            {
                for (int c = 0; c < NumbersC.Length; c++)
                {
                    BCPairs.Add(new Point() {X = NumbersB[b], Y = NumbersC[c]});
                }
            }

            var results = from pairs in BCPairs
                where pairs.X < pairs.Y
                select new
                {
                    B = pairs.X,
                    C = pairs.Y
                };

            foreach (var pair in results)
            {
                Console.WriteLine("({0}, {1})", pair.B, pair.C);
            }

        }

        //10. Select CustomerID, OrderID, and Total where the order total is less than 500.00.
    }
}



    

