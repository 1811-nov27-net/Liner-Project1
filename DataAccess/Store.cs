using System;
using System.Collections.Generic;

namespace DataAccess
{
    public partial class Store
    {
        public Store()
        {
            OrderDetails = new HashSet<OrderDetails>();
            Users = new HashSet<Users>();
        }

        public int LocationId { get; set; }
        public int Stock { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
