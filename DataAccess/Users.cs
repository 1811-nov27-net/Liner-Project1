using System;
using System.Collections.Generic;

namespace DataAccess
{
    public partial class Users
    {
        public Users()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DefaultLocation { get; set; }

        public virtual Store DefaultLocationNavigation { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
