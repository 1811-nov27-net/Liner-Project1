using DataAccess;
using Microsoft.EntityFrameworkCore;
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

        //Place an order which will be written to the database
        public bool MakeOrder(OrderClass order)
        {

            var trackedOrder = new OrderDetails();      //Create a new database order object
            UserClass user = new UserClass();           //Create a new model user object
            StoreClass store = new StoreClass();        //Create a new model store object

            //Synching the user model object with the database object
            user.userID = _db.Users.First(u => u.UserId == order.user).UserId;                      //Finding user ID
            user.firstName = _db.Users.First(u => u.UserId == order.user).FirstName;                //Finding first name
            user.lastName = _db.Users.First(u => u.UserId == order.user).LastName;                  //Finding last name
            user.defaultLocation = _db.Users.First(u => u.UserId == order.user).DefaultLocationId;  //Finding default location

            //Synching the store model object with the database object
            store.locationName = order.locationName;
            store.stock = _db.Store.First(s => s.LocationId == user.defaultLocation).Stock;

            //This block is for defining the database object for the order
            trackedOrder.LocationID = _db.Store.First(s => s.LocationName == order.locationName).LocationId;
            trackedOrder.locationName = _db.Store.First(s => s.LocationName == order.locationName).LocationName;
            trackedOrder.firstName = user.firstName;
            trackedOrder.lastName = user.lastName;
            trackedOrder.smallPizzas = order.smallPizzas;
            trackedOrder.largePizzas = order.largePizzas;
            trackedOrder.UserID = user.userID;
            trackedOrder.Price = (order.smallPizzas * 25.00m) + (order.largePizzas * 50.00m);
            trackedOrder.DatePlaced = DateTime.Now;

            var trackedStore = _db.Store.Find(user.defaultLocation);

            //Giving names to necessary parameters to clean up the order check
            int pizzas = order.largePizzas + order.smallPizzas;
            DateTime timedate = trackedOrder.DatePlaced;
            int locationID = trackedOrder.LocationID;
            int userID = trackedOrder.UserID;
            int stock = store.stock;

            //This block of code is only for if the order is able to be completed
            //  -Sufficient inventory, time limit expired, 12 or less pizzas, $500 or less
            if (PizzaLimitOkay(pizzas) && PriceOkay(order.price) && TimeLimitOkay(timedate, userID, locationID) && EnoughStock(pizzas, stock))
            {
                store.reduceStock(order.smallPizzas + order.largePizzas);
                trackedStore.Stock = store.stock;
                _db.Store.Update(trackedStore);
                _db.Add(trackedOrder);
                _db.SaveChanges();
                return true;
            }

            else { return false; }

        }

        //Check if there is enough inventory to fill the order
        private bool EnoughStock(int pizzas, int stock)
        {
            if (pizzas <= stock)
            {
                return true;
            }

            else { return false; }
        }

        //Check for the 2-hour time limit
        private bool TimeLimitOkay(DateTime x, int userID, int storeID)
        {
            if (_db.OrderDetails.Any(o => o.UserID == userID && o.LocationID == storeID))
            {
                var lastOrder = _db.OrderDetails.Last(o => o.UserID == userID && o.LocationID == storeID);
                DateTime y = lastOrder.DatePlaced;


                if ((x.Subtract(y).TotalHours > 2))
                {
                    return true;
                }

                else { return false; }
            } 

            else { return true; }
        }

        //Check if order has too many pizzas
        private bool PizzaLimitOkay(int x)
        {
            if (x > 12)
            {
                return false;
            }

            else { return true; }
        }

        //Check if order is over $500
        private bool PriceOkay(decimal x)
        {
            if (x > 500.00m)
            {
                return false;
            }

            else { return true; }
        }

        //Return list of orders made by a specific customer
        public IEnumerable<OrderDetails> UserOrders(int userID)
        {
            return _db.OrderDetails.Where(o => o.UserID == userID);
        }

        //Return list of orders made to a specific store
        public IEnumerable<OrderDetails> StoreOrders(int storeID)
        {
            return _db.OrderDetails.Where(o => o.LocationID == storeID);
        }

        //Return the last order made by a specific customer
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

        //Displays list of users
        public IEnumerable<UserClass> GetUsers()
        {
            return _db.Users.Select(UserMap);
        }

        public IEnumerable<Store> GetStores()
        {
            return _db.Store.OrderBy(s => s.LocationId);
        }

        public void RestockStore(int storeID)
        {
            var trackedStoreA = _db.Store.First(s => s.LocationId == storeID);

            trackedStoreA.Stock = 20;

            _db.Store.Update(trackedStoreA);

            _db.SaveChanges();

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

        /*public static Data.Store StoreMap(StoreClass)
        {
            return new Data.Store
            {

            }
        }*/

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

    }
}
