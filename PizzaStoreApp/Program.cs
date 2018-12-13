using ClassLibrary;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;

namespace PizzaStoreApp
{
    public class Program
    {
        static DbContextOptions<PizzaStoreAppContext> options = null;


        static void Main(string[] args)
        {
            var connectionString = SecretConfiguration.ConnectionString;
            var optionsBuilder = new DbContextOptionsBuilder<PizzaStoreAppContext>();
            optionsBuilder.UseSqlServer(connectionString);
            options = optionsBuilder.Options;
            IStoreRepo repo;

            using (var db = new PizzaStoreAppContext(options))
            {
                repo = new StoreRepo(db);

                string input = null;
                while (input != "exit")
                {
                    Console.WriteLine("Welcome to the Pizza App. Enter your order here. Small Pizzas are $25, Large Pizzas are $50");
                    //input = Console.ReadLine();

                    repo.MakeOrder();
                    repo.SaveChanges();
                    
                }
            }
        }
    }
}
