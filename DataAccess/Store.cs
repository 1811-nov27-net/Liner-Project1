using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    public partial class Store
    {
        public Store()
        {
            OrderDetails = new HashSet<OrderDetails>();
            Users = new HashSet<Users>();
        }

        [Key]
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int Stock { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
