using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class StoreClass
    {
        public int locationID { get; set; }                     //store location ID
        public string locationName { get; set; }                //pizza store name
        public int stock;                                       //inventory stock

        //constructor
        /*public StoreClass(int locationID)
        {
            this.locationID = locationID;
        }*/

        //history of orders placed to the location
        List<OrderClass> history = new List<OrderClass>();


        //add order to history

        /*public bool EnoughStock(OrderClass order)       //check if there's enough inventory to complete the order
        {
            if (order.pizzas > this.stock)
            {
                Console.WriteLine("Not enough inventory to complete order");
                return false;
            }

            else { return true; }
        }*/
        
        //place order
        /*public void PlaceOrder(OrderClass order)
        {           
                reduceStock(order.pizzas);
                history.Add(order);
                order.orderTime = order.orderTime;
            
        }*/

        //subtract from inventory

        public void reduceStock(int x)                       //reduce stock by an amount dependent on order size
        {
            stock -= x;
        }


    }
}
