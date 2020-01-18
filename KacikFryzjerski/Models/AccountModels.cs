using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KacikFryzjerski.Models
{
    public class AccountModels
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public string Account_email { get; set; }

        public virtual ICollection<OrderModels> Account_Orders { get; set; }
        public AccountData AccountData { get; set; }
    }
}