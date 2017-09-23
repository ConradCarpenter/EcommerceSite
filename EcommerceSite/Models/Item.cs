using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSite.Models
{
    public class Item
    {
        public string ItemName { get; set; }
        public int ItemNumber { get; set; }
        public double Price { get; set; }
        public string Desc { get; set; }
        public string ImagURL { get; set; }
    }
}
