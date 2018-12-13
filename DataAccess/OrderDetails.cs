using System;
using System.Collections.Generic;

namespace DataAccess
{
    public partial class OrderDetails
    {
        public int OrderId { get; set; }
        public int Pizzas { get; set; }
        public int LocationId { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public DateTime DatePlaced { get; set; }

        public virtual Store Location { get; set; }
        public virtual Users User { get; set; }
    }
}
