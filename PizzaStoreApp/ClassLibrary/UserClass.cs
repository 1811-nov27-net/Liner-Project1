using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace ClassLibrary
{
    public class UserClass
    {
        public int userID { get; set; }                         //customer ID number
        public string firstName { get; set; }                   //user's first name
        public string lastName { get; set; }                    //user's last name
        public int defaultLocation { get; set; }                //default location to order from
        public IList<OrderClass> OrderHistory = new List<OrderClass>();    //user's order history 

        //constructor
        public UserClass()
        {

        }

        //make an order and add it to the user's history
        public void AddToHistory(OrderClass order)
        {
            OrderHistory.Add(order);
        }

        /*public string SuggestedOrder()
        {

            OrderClass orderSuggestion = new OrderClass();
            orderSuggestion = this.OrderHistory[this.OrderHistory.Count - 1];
            return orderSuggestion.ToString();
        }*/

        //check to see if an order has been made in the past 2 hours at the same location by the same user

        public bool CanOrder()
        {
            if (this.OrderHistory.Count == 0 || timeCheck(this.OrderHistory.OrderByDescending(o => o.orderTime).First().orderTime))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public bool timeCheck(DateTime time)
        {
            if (DateTime.Now.Subtract(time).TotalHours > 2)
            {
                return true;
            }

            else { return false; }
        }
    }
}
