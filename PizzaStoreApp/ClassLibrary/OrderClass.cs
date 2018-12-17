using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class OrderClass
    {

        public int Location { get; set; } = 0;               //location ID of pizza shop
        public int user { get; set; }                    //user ID of person ordering
        public string firstName { get; set; } = " ";           //first name of user
        public string lastName { get; set; } = " ";            //last name of user
        public int smallPizzas { get; set; }                      //number of small pizzas in the order; initialized to 0
        public int largePizzas { get; set; }                     //number of large pizzas in the order, initialized to 0
        public DateTime orderTime { get; set; } = DateTime.Now;        //time of order               


        public decimal price { get; set; }                            //total price of the order
        List<PizzaClass> PizzaList = new List<PizzaClass>();          //list of pizzas in the order





        //finalize order
        public void CompleteOrder(UserClass user, StoreClass store)
        {                        

            this.user = user.userID;
            this.Location = store.locationID;
            this.orderTime = DateTime.Now;
        }


    }
}
