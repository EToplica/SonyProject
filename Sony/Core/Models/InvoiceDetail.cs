using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sony.Core.Models
{
    public class InvoiceDetail
    {
        public int Id { get; set; }
     
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public InvoiceDetail()
        {

        }

        public InvoiceDetail(int productId, int quantity)
        {
            Quantity = quantity;
            ProductId = productId;
        }

        [NotMapped]
        public double Total
        {
            get
            {
                return Quantity * Product.CurrentPrice;
            }
        }
    }
}
