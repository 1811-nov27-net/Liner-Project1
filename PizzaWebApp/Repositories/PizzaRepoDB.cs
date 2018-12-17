using DataAccess;
using Microsoft.EntityFrameworkCore;
using PizzaWebApp.Models;
using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data = DataAccess;

namespace PizzaWebApp.Repositories
{
    public class PizzaRepoDB : IStoreRepo
    {
        private readonly Data.PizzaStoreAppContext _db;

        public PizzaRepoDB(Data.PizzaStoreAppContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));

            db.Database.EnsureCreated();

        }

        public void MakeOrder(OrderClass order)
        {

            var trackedOrder = new OrderDetails();      //create a new database order object
            UserClass user = new UserClass();           //create a new model user object
            StoreClass store = new StoreClass();        //create a new model store object

            //synching the user model object with the database object
            user.userID = _db.Users.First(u => u.UserId == order.user).UserId;                      //finding user ID
            user.firstName = _db.Users.First(u => u.UserId == order.user).FirstName;                //finding first name
            user.lastName = _db.Users.First(u => u.UserId == order.user).LastName;                  //finding last name
            user.defaultLocation = _db.Users.First(u => u.UserId == order.user).DefaultLocationId;  //finding default location

            //synching the store model object with the database object
            store.locationName = _db.Store.First(s => s.LocationId == user.defaultLocation).LocationName;
            store.stock = _db.Store.First(s => s.LocationId == user.defaultLocation).Stock;

            //this block is for defining the database object for the order
            trackedOrder.LocationID = user.defaultLocation;
            trackedOrder.smallPizzas = order.smallPizzas;
            trackedOrder.largePizzas = order.largePizzas;
            trackedOrder.UserID = user.userID;
            trackedOrder.Price = (order.smallPizzas * 25.00m) + (order.largePizzas * 50.00m);
            trackedOrder.DatePlaced = DateTime.Now;

            var trackedStore = _db.Store.Find(user.defaultLocation);

            //this block of code is only for if the order is able to be completed
            //  -sufficient inventory, time limit expired, 12 or less pizzas, $500 or less

            if (PizzaLimitOkay(order.smallPizzas + order.largePizzas) && PriceOkay(order.price) && TimeLimitOkay(trackedOrder.DatePlaced, trackedOrder.UserID, trackedOrder.LocationID))
            {
                store.reduceStock(order.smallPizzas + order.largePizzas);
                trackedStore.Stock = store.stock;
                _db.Store.Update(trackedStore);
                _db.Add(trackedOrder);
                _db.SaveChanges();
            }

        }

        //check for the 2-hour time limit
        public bool TimeLimitOkay(DateTime x, int userID, int storeID)
        {
            var lastOrder = _db.OrderDetails.Last(o => o.UserID == userID && o.LocationID == storeID);
            DateTime y = lastOrder.DatePlaced;

            if (x.Subtract(y).TotalHours < 2)
            {
                return false;
            }

            else { return true; }
        }

        //check if order has too many pizzas
        public bool PizzaLimitOkay(int x)
        {
            if (x > 12)
            {
                return false;
            }

            else { return true; }
        }

        //check if order is over $500
        public bool PriceOkay(decimal x)
        {
            if (x > 500.00m)
            {
                return false;
            }

            else { return true; }
        }

        //return list of orders made by a specific customer
        public IEnumerable<OrderDetails> UserOrders(int userID)
        {
            return _db.OrderDetails.Where(o => o.UserID == userID);
        }

        //return list of orders made to a specific store
        public IEnumerable<OrderDetails> StoreOrders(int storeID)
        {
            return _db.OrderDetails.Where(o => o.LocationID == storeID);
        }

        //return the last order made by a specific customer
        public OrderDetails SuggestedOrder(int userID)
        {
            return _db.OrderDetails.Last(o => o.UserID == userID);
        }

        /*public OrderDetails DisplayOrderDetails()
        {

        }*/

        //converts user data from database tables into a C# object
        /*public UserClass UserConversion(Users user)
        {
            return new UserClass()
            {
                userID = _db.Users
            }
        }*/

        //make a list of object UserClass using library class as a blueprint
        /*public List<UserClass> UserList()
        {
            List<UserClass> list = new List<UserClass>();
            foreach (var user in _db.Users.ToList())
            {
                list.Add(UserConversion(user));
            }
            return list;
        }*/


        public IEnumerable<UserClass> GetUsers()
        {
            return _db.Users.Select(UserMap);
        }

        /*public static OrderDetails(OrderClass order)
        {
            return new OrderDetails
            {
                Pizzas = order.smallPizzas + order.largePizzas,
                LocationID = 
           
            }
        }*/

        public static UserClass UserMap(Data.Users data)
        {
            return new UserClass
            {
                userID = data.UserId,
                firstName = data.FirstName,
                lastName = data.LastName,
                defaultLocation = data.DefaultLocationId
            };
        }
        public static Data.Users UserMap(UserClass ui)
        {
            return new Data.Users
            {
                UserId = ui.userID,
                FirstName = ui.firstName,
                LastName = ui.lastName,
                DefaultLocationId = ui.defaultLocation
            };
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

    }
}
