using EcommerceSite.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceSite.ViewModels
{
    public class ItemViewModel
    {
        public int Number { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Desc { get; set; }

        [DisplayName("Image")]
        public string ImageURL { get; set; }

        public AppUser User { get; set; }
    }
}
