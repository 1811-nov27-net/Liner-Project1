using System;
using System.Collections.Generic;
using System.Text;
using PizzaWebApp.Models;
using ClassLibrary;

namespace PizzaWebApp.Repositories
{
    public interface IStoreRepo
    {
        IEnumerable<UserClass> GetUsers();
        

        void MakeOrder(OrderClass order);               //Making an order
        //UserClass SearchUserByName(string name);        //Search for a user by their name
        //display order history of a store location     //Display order history for store location
        //display order history for a user              //Display order history for a user
        void SaveChanges();                             //save changes to the repo
        //string OrderOverview(OrderClass order);                         //gives details of the order

    }
}
