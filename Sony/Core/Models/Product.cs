using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sony.Core.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Emri")]
        public string Name { get; set; }

        public double CurrentPrice { get; set; }

        public int StockQuantity { get; set; }
    }
}
