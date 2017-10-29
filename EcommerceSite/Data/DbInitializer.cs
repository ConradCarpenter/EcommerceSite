using EcommerceSite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSite.Data
{
    public class DbInitializer
    {
        public static void Initialize(ItemContext context)
        {
            context.Database.EnsureCreated();

            var list = new Item[]
            {
                    new Item { Name = "Car", ItemNumber = 1, Price = 10000.00, Desc = "This is some car that I am selling I hope you like it!!!!!", ImageURL = "https://cdn.inventorycc.com/6774648_1.jpg" },
                    new Item { Name = "Computer", ItemNumber = 2, Price = 899.99, Desc = "This is some Computer that I am selling I hope you like it!!!!!", ImageURL = "https://shop.r10s.jp/youplan/cabinet/201510/100008820210_1.jpg" },
                    new Item { Name = "Phone", ItemNumber = 3, Price = 323.50, Desc = "This is some Phone that I am selling I hope you like it!!!!!", ImageURL = "http://allthingsd.com/files/2013/11/P1040626-640x480.jpg" }
            };

            foreach(var item in list){
                context.Items.Add(item);
            }

            context.SaveChanges();
        }
    }
    }

