using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    public partial class OrderDetails
    {
        [Key]
        public int OrderId { get; set; }
        public int UserID { get; set; }
        public int LocationID { get; set; }
        public string locationName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int smallPizzas { get; set; }
        public int largePizzas { get; set; }
        public decimal Price { get; set; }
        public DateTime DatePlaced { get; set; }

        public virtual Store Location { get; set; }
        public virtual Users User { get; set; }
    }
}
