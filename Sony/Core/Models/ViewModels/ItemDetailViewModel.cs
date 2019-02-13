using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sony.Core.Models.ViewModels
{
    public class ItemDetailViewModel
    {
        public int ItemId { get; set; }
        public List<ProductDetailViewModel> ProductDetails { get; set; }
    }
}
