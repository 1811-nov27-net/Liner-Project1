using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    public partial class Users
    {
        public Users()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DefaultLocationId { get; set; }

        public virtual Store DefaultLocation { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
