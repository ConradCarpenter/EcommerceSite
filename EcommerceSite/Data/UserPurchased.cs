using EcommerceSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSite.Data
{
    public class UserPurchased
    {
        public int ItemNumber { get; set; }
        public Item Item { get; set; }


        public AppUser User { get; set; }
        public string UserID { get; set; }
    }
}
