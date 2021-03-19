using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAuthenticationAuthorization.Models;

namespace TaskAuthenticationAuthorization
{
    public class SampleData
    {
        public static void Initialize(ShoppingContext context)
        {
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }
            context.Products.AddRange
            (
                new Product { Name = "Butter", Price = 30.0 },
                new Product { Name = "Banana", Price = 20.5 },
                new Product { Name = "Cola", Price = 15.30 },
                new Product { Name = "Apple", Price = 22 },
                new Product { Name = "Tomato", Price = 25 },
                new Product { Name = "Lemon", Price = 20.5 },
                new Product { Name = "Cake", Price = 40 },
                new Product { Name = "Bread", Price = 15 },
                new Product { Name = "Cheese", Price = 160 },
                new Product { Name = "Milk", Price = 18 }
            );
            context.SaveChanges();
            context.Customers.AddRange
            (
                new Customer
                {
                    FirstName = "Ostap",
                    LastName = "Bender",
                    Address = "Rio de Zhmerinka",
                    Email = "abc@mail.ru",
                    Discount = Discount.O,

                },
                new Customer
                {
                    FirstName = "Alex",
                    LastName = "Fender",
                    Address = "Zhmerinka",
                    Email = "abc12@mail.ru",
                    Discount = Discount.O,

                },
                new Customer
                {
                    FirstName = "Shura",
                    LastName = "Balaganov",
                    Address = "Odessa",
                    Email = "f@mail.ru",
                    Discount = Discount.R,

                }
            );
            context.SaveChanges();
            context.SuperMarkets.AddRange
            (
                new SuperMarket { Name = "Billa", Address = "Odessa" },
                new SuperMarket { Name = "Big Pocket", Address = "Lviv" },
                new SuperMarket { Name = "Wellmart", Address = "Lviv" },
                new SuperMarket { Name = "Silpo", Address = "Chernivtsi" },
                new SuperMarket { Name = "ATB", Address = "Kyiv" },
                new SuperMarket { Name = "Metro", Address = "Chernivtsi" }
            );
            context.SaveChanges();
            context.Orders.AddRange
            (
                new Order { CustomerId = 1, SuperMarketId = 1, OrderDate = DateTime.Now.AddDays(2) },
                new Order { CustomerId = 1, SuperMarketId = 2, OrderDate = DateTime.Now.AddDays(5) },
                new Order { CustomerId = 2, SuperMarketId = 3, OrderDate = DateTime.Now.AddDays(3) },
                new Order { CustomerId = 3, SuperMarketId = 5, OrderDate = DateTime.Now.AddDays(4) }
            );
            context.SaveChanges();
            context.OrderDetails.AddRange
            (
                new OrderDetail { OrderId = 1, ProductId = 1, Quantity = 2 },
                new OrderDetail { OrderId = 1, ProductId = 2, Quantity = 6 },
                new OrderDetail { OrderId = 1, ProductId = 3, Quantity = 2 },
                new OrderDetail { OrderId = 2, ProductId = 1, Quantity = 3 },
                new OrderDetail { OrderId = 2, ProductId = 3, Quantity = 3 },
                new OrderDetail { OrderId = 2, ProductId = 5, Quantity = 7 },
                new OrderDetail { OrderId = 2, ProductId = 6, Quantity = 2 },
                new OrderDetail { OrderId = 3, ProductId = 7, Quantity = 1 },
                new OrderDetail { OrderId = 3, ProductId = 8, Quantity = 2 },
                new OrderDetail { OrderId = 4, ProductId = 4, Quantity = 6 }
            );
            context.SaveChanges();
        }
    }
}
