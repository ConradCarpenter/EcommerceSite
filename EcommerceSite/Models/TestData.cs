using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSite.Models
{
    public class TestData : IRepo
    {
        public List<Item> Items
        {
            get
            {
                List<Item> list = new List<Item>();
                list.Add(new Item { ItemName = "Car", ItemNumber = 1, Price = 10000.00, Desc = "This is some car that I am selling I hope you like it!!!!!", ImagURL = "https://cdn.inventorycc.com/6774648_1.jpg" });
                list.Add(new Item { ItemName = "Computer", ItemNumber = 2, Price = 899.99, Desc = "This is some Computer that I am selling I hope you like it!!!!!", ImagURL = "https://shop.r10s.jp/youplan/cabinet/201510/100008820210_1.jpg" });
                list.Add(new Item { ItemName = "Phone", ItemNumber = 3,  Price = 323.50, Desc = "This is some Phone that I am selling I hope you like it!!!!!", ImagURL = "http://allthingsd.com/files/2013/11/P1040626-640x480.jpg" });

                return list;
            }
        }
    }
}
