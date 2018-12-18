using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary;
using ClassLibrary;
using DataAccess;

namespace PizzaWebApp.Repositories
{
    public interface IStoreRepo
    {
        IEnumerable<UserClass> GetUsers();                  //Display list of users
        bool MakeOrder(OrderClass order);                   //Making an order
        IEnumerable<OrderDetails> UserOrders(int userID);   //Display a user's order history
        IEnumerable<OrderDetails> StoreOrders(int storeID); //Display a store's order history
        IEnumerable<Store> GetStores();                     //Display list of stores
        void RestockStore(int storeID);                               //Reset store inventories
        OrderDetails SuggestedOrder(int userID);            //Gives a suggested order based on history
        //UserClass SearchUserByName(string name);          //Search for a user by their name
        void SaveChanges();                                 //Save changes to the repo
        //string OrderOverview(OrderClass order);                         //gives details of the order

    }
}
