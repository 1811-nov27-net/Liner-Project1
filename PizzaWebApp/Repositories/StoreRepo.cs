/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PizzaWebApp.Models;

namespace PizzaWebApp.Repositories
{
    public class StoreRepo : IStoreRepo
    {
        public PizzaStoreAppContext Db { get; }

        public StoreRepo(PizzaStoreAppContext db)
        {
            Db = db ?? throw new ArgumentNullException(nameof(db));
        }

        //make an order to add to the database
        public void MakeOrder()
        {
            
            int userID;
            int locationID;
            int smallPizzas;
            int largePizzas;


            string input;
            Console.WriteLine("Enter your User ID: ");
            input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                userID = Db.Users.First(u => u.UserID == result).UserID;
            }
            else
            {
                userID = 0;
            }


            locationID = Db.Users.First(u => u.UserID == userID).DefaultLocation;   //set the store location as the user's default


            do
            {
                Console.WriteLine("How many small pizzas do you want? ");
                input = Console.ReadLine();
                if (int.TryParse(input, out int result3))
                {
                    smallPizzas = result3;
                }
                else { smallPizzas = 0; }

                Console.WriteLine("How many large pizzas do you want? ");
                input = Console.ReadLine();
                if (int.TryParse(input, out int result4))
                {
                    largePizzas = result4;
                }
                else { largePizzas = 0; }
            } while (largePizzas + smallPizzas >= 12 || (largePizzas*50) + (smallPizzas*25) >= 500);

            UserClass user = UserList().First(u => u.userID == userID);
            StoreClass store = StoreList().First(s => s.locationID == locationID);
            

            if (user.CanOrder())
            {
                var order = new OrderClass(userID, locationID, smallPizzas, largePizzas);
                user.AddToHistory(order);
                OrderDetails trackedOrder = new OrderDetails();         //create a new order to be tracked in database
                trackedOrder.UserID = Db.Users.First(u => u.UserID == order.user).UserID;   //match customer ID with customer in database
                trackedOrder.Pizzas = order.smallPizzas + order.largePizzas;                                         //set number of pizzas in the order
                trackedOrder.LocationID = Db.Store.First(s => s.LocationID == order.location).LocationID;   //match location ID with ID in database
                trackedOrder.Price = order.price;                                           //set the price of the order
                trackedOrder.DatePlaced = order.orderTime;                                  //time of order being placed

                Console.WriteLine(OrderOverview(order));
                Db.OrderDetails.Add(trackedOrder);                                          //add the new tracked order
            }
            else
            {
                Console.WriteLine("You have ordered too recently. You must wait 2 hours between orders from this location");
            }

            }

        //converts user data from database tables into a C# object
        public UserClass UserConversion(Users user)
        {
            return new UserClass(user.UserID, user.DefaultLocation);
        }

        //make a list of object UserClass using library class as a blueprint
        public List<UserClass> UserList()
        {
            List<UserClass> list = new List<UserClass>();
            foreach(var user in Db.Users.ToList())
            {
                list.Add(UserConversion(user));
            }
            return list;
        }

        //converts store location data from database tables into a C# object
        public StoreClass StoreConversion(Store store)
        {
            return new StoreClass(store.LocationID);
        }

        //makes a list of object StoreClass using library class as a blueprint
        public List<StoreClass> StoreList()
        {
            List<StoreClass> list = new List<StoreClass>();
            foreach(var store in Db.Store.ToList())
            {
                list.Add(StoreConversion(store));
            }
            return list;
        }

        //give details of the order
        public string OrderOverview(OrderClass order)
        {
            string firstName = Db.Users.First(u => u.UserID == order.user).FirstName;
            string lastName = Db.Users.First(u => u.UserID == order.user).LastName;


            return "User Name: " + firstName + " " + lastName + ".\n" + "Pizzas: " + (order.smallPizzas + order.largePizzas) + ".\n" +
                "Store ID: " + order.location + ".\n" + "Price: " + order.price + ".\n" + "Time Placed: " + order.orderTime + ".\n";
        }

        /*public IList<UserClass> SearchUserByName(string name)
        {
            var list = new List<UserClass>();
        }*/

        //save changes to the database

        /*
        public void SaveChanges()
        {
            Db.SaveChanges();
        }


    }
}*/
