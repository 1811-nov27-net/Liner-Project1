using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class PizzaClass
    {
        public string size;                 //size of the pizza
        public decimal price { get; set; }   //total price of this pizza

        //constructor; determines pizza size and price
        public PizzaClass(string size)
        {
            if (size == "small")
            {
                this.size = "small";
                this.price = 5.00m;
            }

            else if (size == "medium")
            {
                this.size = "medium";
                this.price = 10.00m;
            }

            else if (size == "large")
            {
                this.size = "large";
                this.price = 15.00m;
            }

            else                                //default to small size
            {
                this.size = "small";
                this.price = 5.00m;
            }
        }

        
     

    }
}
