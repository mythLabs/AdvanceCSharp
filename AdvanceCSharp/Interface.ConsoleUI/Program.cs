using Interfaces.Library;
using System;
using System.Collections.Generic;

namespace Interfaces.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IProductModel> cart = AddSampleData();
            CustomerModel customer = GetCustomer();

            foreach (IProductModel prod in cart)
            {
                prod.ShipItem(customer);

                if (prod is IDigitalProductModel digital) //C# 7.0
                {
                    Console.WriteLine($"For { digital.Title } you have total downloads left { digital.TotalDownloadsLeft }");
                }
            }

            Console.ReadLine();
        }

        private static List<IProductModel> AddSampleData()
        {
            List<IProductModel> products = new List<IProductModel>
                {
                new PhysicalProductModel {Title = "Oats"},
                new PhysicalProductModel {Title = "Honey"},
                new PhysicalProductModel {Title = "Milk"},
                new DigitalProductModel {Title = "Cyberpunk 2077"}
                };

            return products;
        }

        private static CustomerModel GetCustomer()
        {
            return new CustomerModel
            {
                FirstName = "Amit",
                LastName = "Bisht",
                StreetAddress = "st.Nainital",
                City = "city.Nainital",
                EmailAddress = "amitbisht744@gmail.com",
                PhoneNumber = "01234456789"
            };
        }
    }
}
