﻿using EcommerceSite.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSite.Models
{
    public class Item
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ItemNumber { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Desc { get; set; }
        public string ImageURL { get; set; }
		public string ListingURL { get; set; }
        public DateTime? CreateTime { get; set; }
        public string ForeignListingId { get; set; }
        public string UserID { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<UserPurchased> Buyers { get; set; }
	}
}
