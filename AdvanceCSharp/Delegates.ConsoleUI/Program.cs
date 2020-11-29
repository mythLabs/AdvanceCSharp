using Delegates.Library;
using System;
using System.Collections.Generic;

namespace Delegates.ConsoleUI
{
    class Program
    {

        static ShoppingCartModel cart = new ShoppingCartModel();
        static void Main(string[] args)
        {
            PopulateCartWithDemoData();

            Console.WriteLine($"The total for the cart is {cart.GenerateTotal(SubTotalAlter, CalculatedDiscount, AlertUser)}");

            Console.WriteLine();

            // Passing new functions
            decimal total = cart.GenerateTotal(
                    (subTotal => Console.WriteLine($"Subtotal for cart 2: {subTotal}")),
                    ((products, subTotal) => {
                            if (products.Count > 2)
                            {
                                return subTotal * 0.5M;
                            } else
                            {
                                return subTotal;
                            }
                        }
                    ),
                    (message => Console.WriteLine($"Cart 2: {message}"))
                );

            Console.WriteLine($"Total for cart 2: {total}");

            Console.ReadLine();

        }

        private static void PopulateCartWithDemoData()
        {
            cart.Items.Add(new ProductModel {ItemName = "Cyberpunk 2077", Price = 50.34M });
            cart.Items.Add(new ProductModel { ItemName = "Cereal", Price = 2.98M });
            cart.Items.Add(new ProductModel { ItemName = "Play Station 5", Price = 543.67M });
        }

        private static void SubTotalAlter(decimal subTotal)
        {
            Console.WriteLine($"The Sub Total for the cart is {subTotal}");
        }

        private static decimal CalculatedDiscount(List<ProductModel> Items, decimal subtotal)
        {
            if (subtotal > 100)
            {
                return subtotal * 0.80M;
            }
            else if (subtotal > 50)
            {
                return subtotal * 0.85M;
            }
            else if (subtotal > 10)
            {
                return subtotal * 0.90M;
            }
            else
            {
                return subtotal;
            }
        }

        private static void AlertUser(string message)
        {
            Console.WriteLine(message);
        }
    }
}
