using EcommerceSite.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSite.Data
{
    public class AppUser : IdentityUser
    {
        public virtual ICollection<Item> UserItems { get; set; }

        public virtual ICollection<UserPurchased> PurchaseItems { get; set; }
    }
}
