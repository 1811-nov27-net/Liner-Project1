using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class OrderClass
    {

        public int location { get; set; }                //location ID of pizza shop
        public int user { get; set; }                    //user ID of person ordering
        public string firstName { get; set; }            //first name of user
        public string lastName { get; set; }             //last name of user
        public int smallPizzas = 0;                      //number of small pizzas in the order; initialized to 0
        public int largePizzas = 0;                      //number of large pizzas in the order, initialized to 0
        public DateTime orderTime { get; set; }          //time of order               

        public OrderClass(int user, int location, int small, int large)
        {
            this.user = user;
            this.location = location;
            this.smallPizzas = small;
            this.largePizzas = large;
            this.price = small * 25.00m + large * 50.00m;
            this.orderTime = DateTime.Now;
        }

        public decimal price { get; set; }                            //total price of the order
        List<PizzaClass> PizzaList = new List<PizzaClass>();          //list of pizzas in the order





        //finalize order
        public void CompleteOrder(UserClass user, StoreClass store)
        {                        

            this.user = user.userID;
            this.location = store.locationID;
            this.orderTime = DateTime.Now;
        }


    }
}
