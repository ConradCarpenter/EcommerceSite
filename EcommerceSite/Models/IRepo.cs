using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSite.Models
{
    interface IRepo
    {
        List<Item> Items { get; }
    }
}
