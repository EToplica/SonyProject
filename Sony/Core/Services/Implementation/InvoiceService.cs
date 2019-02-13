using Microsoft.EntityFrameworkCore;
using Sony.Core.Models;
using Sony.Core.Services.Contract;
using Sony.Core.Services.Helpers;
using Sony.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sony.Core.Services.Implementation
{
    public class InvoiceService : ServiceBase<Invoice>, IInvoiceService
    {
        public InvoiceService(ApplicationDbContext context) : base(context)
        {
        }

        public ICollection<Invoice> GetAllNotFinished()
        {
            return __THIS__.Include(x => x.InvoiceDetails).ThenInclude(z => z.Product).Where(x => x.IsFinished == false).ToArray();
        }

        public Invoice GetLastInvoice(int itemId)
        {
            return __THIS__.FirstOrDefault(x => x.ItemId == itemId && x.IsFinished == false);
        }
        public Invoice GetLastWithDetails(int itemId)
        {
            return __THIS__.Include(x=>x.InvoiceDetails).FirstOrDefault(x => x.ItemId == itemId && x.IsFinished == false);
        }
    }
}
