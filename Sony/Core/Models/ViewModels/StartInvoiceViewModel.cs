using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sony.Core.Models.ViewModels
{
    public class StartInvoiceViewModel
    {
        public int? Players { get; set; }
        public bool? HasPayed { get; set; }
        public int? ItemId { get; set; }
    }
}
