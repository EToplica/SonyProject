using Sony.Core.Models;
using Sony.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sony.Core.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        public int? ItemId { get; set; }
        public Item Item { get; set; }

        public int? Players { get; set; }

        public DateTime? StartAt { get;  set; } = DateTime.Now;
        public DateTime? EndAt { get; private set; }

        public string InsertedById { get; set; }
        public ApplicationUser InsertedBy { get; set; }

        public bool HasPayed { get; set; }

        public bool IsFinished { get; set; }

        public ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new HashSet<InvoiceDetail>();

        public void Stop()
        {
            EndAt = DateTime.Now;
        }

        public int GetTotalMinutes()
        {
            return (EndAt - StartAt).Value.Minutes;
        }

        [NotMapped]
        public double GetRunninMinutes
        {
            get
            {
                var dt = DateTime.Now.Subtract(StartAt.GetValueOrDefault());
                
                return (TimeSpan.FromTicks(dt.Ticks).TotalMinutes);
            }
            
        }

        public double CalculateTotal()
        {
            double productPrice = 0;
            InvoiceDetails.ToList().ForEach(x => productPrice += x.Total);

            if (ItemId != null)
            {
                if (Item.ItemType.Id == 1)
                    return Players > 2 ? CalculateTotalWithoutProudcts()+ productPrice : CalculateTotalWithoutProudcts() + productPrice;
                return productPrice;
            }
            return productPrice;
        }

        public double CalculateTotalWithoutProudcts()
        {
            return Players > 2 ? GetTotalMinutes() * 0.025 : GetTotalMinutes() * 0.0166;
        }

        [NotMapped]
        public double CalculateTotalWhileRunning
        {
            get
            {
                return Players > 2 ? GetRunninMinutes * 0.025 : GetRunninMinutes * 0.0166666667;

            }
        }
     

    }
}
